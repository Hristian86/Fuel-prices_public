namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Constants;
    using AkciqApp.Data;
    using AkciqApp.Models;
    using AkciqApp.Models.Models;
    using AkciqApp.Services;
    using AkciqApp.Services.GasStation;
    using AkciqApp.ViewModels;
    using AkciqApp.ViewModels.GasStationViewHolder;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Web_Scraper;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionContextAccessor accessor;
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IGasStationService gasService;
        private readonly IHttpClientFactory clientFactory;
        private readonly IUserService userService;
        private int ItemsPerPage = 5;

        //private readonly string adminRole = "Admin";
        //private readonly string userRole = "User";

        public HomeController(ILogger<HomeController> logger, IActionContextAccessor accessor, ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IGasStationService gasService,
            IHttpClientFactory clientFactory,
            IUserService userService)
        {
            _logger = logger;
            this.accessor = accessor;
            this.db = db;
            this.userManager = userManager;
            _roleManager = roleManager;
            this.gasService = gasService;
            this.clientFactory = clientFactory;
            this.userService = userService;
        }

        public async Task<IActionResult> Index(string city = null, string country = null, int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var model = new GasStationViewModel();
            int[] pagesCount = new int[1];

            if (city?.Length > 0 || country?.Length > 0)
            {
                model.City = city;
                model.Country = country;

                model.GasStatuons = await this.gasService.GetAllGasStations<GasStationViewCollectionModel>(pagesCount, city, country, page, this.ItemsPerPage);
            }
            else
            {
                var ip = string.Empty;

                string ipAdr = this.Request.Headers["x-forwarded-for"];

                if (ipAdr != null)
                {
                    var ips = ipAdr.Split(",");
                    ip = ips[ips.Length - 1];
                }
                else
                {
                    ip = this.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                    // Hard coded for testing.
                    ip = "212.25.63.243";
                }

                var request = new HttpRequestMessage(
                       HttpMethod.Get, $"http://api.ipstack.com/" + ip + "");
                request.Headers.Add("Accept", "application/json");

                var client = this.clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();

                //var getLocation = JsonConvert.DeserializeObject<object>(content);

                string extraxtCity = this.GetCity(content);

                string x = "City: " + extraxtCity;

                await this.userService.SaveIpAddress(ip, x);

                model.City = extraxtCity;
                model.Country = country;

                model.GasStatuons = await this.gasService.GetAllGasStations<GasStationViewCollectionModel>(pagesCount, extraxtCity, country, page, this.ItemsPerPage);
            }

            model.Pages = pagesCount[0];
            model.CurrentPage = page;
            model.ItemsPerPage = this.ItemsPerPage;

            this.GetAllCities(model);

            return this.View(model);
        }

        // TODO make a service.
        private void GetAllCities(GasStationViewModel model)
        {
            model.Addresses = this.db.Addresses
                .Select(a => a.City)
                .ToList();
        }

        // TODO export in another location.
        private string Extract(string content, string param)
        {
            string[] arr = content.Split("\" + param     +\"").ToArray();
            var nameString = arr[1];
            string result = string.Empty;
            for (int i = 2; i < nameString.Length; i++)
            {
                if (nameString[i] == '\"')
                {
                    break;
                }

                result += nameString[i];
            }

            return result;
        }

        // TODO export in another location.
        private string GetCity(string content)
        {
            string[] arr = content.Split("\"city\"").ToArray();
            var nameString = arr[1];
            string result = string.Empty;
            for (int i = 2; i < nameString.Length; i++)
            {
                if (nameString[i] == '\"')
                {
                    break;
                }

                result += nameString[i];
            }

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code = 404)
        {
            this.ViewData["ErrorMessage"] = $"Error occurred. Error: {code}";
            return this.View("~/Views/Shared/HandleError.cshtml");
        }
    }
}