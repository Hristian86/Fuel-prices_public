namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AntiForgeryController : ControllerBase
    {
        private readonly IAntiforgery antiForgery;

        public AntiForgeryController(IAntiforgery antiForgery)
        {
            this.antiForgery = antiForgery;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult GetAntiFrogery()
        {
            var tokens = this.antiForgery.GetAndStoreTokens(this.HttpContext);
            this.Response.Cookies.Append("XSRF-REQUEST-TOKEN", tokens.RequestToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = false,
            });

            return this.Ok(new
            {
                CookieToken = tokens.CookieToken,
                HeaderName = tokens.HeaderName,
            });
        }
    }
}