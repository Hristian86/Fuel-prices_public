namespace AkciqApp.Services.GasStation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;
    using Web_Scraper;

    public class GasStationService : IGasStationService
    {
        private readonly IRepository<GasStation> gasRepository;
        private readonly WebScraper webScr = new WebScraper();

        public GasStationService(IRepository<GasStation> gasRepository)
        {
            this.gasRepository = gasRepository;
        }

        public async Task UpdateGasStationsManually()
        {
            var stations = this.gasRepository.All().ToList();

            foreach (var station in stations)
            {
                var res = await this.webScr.WebScraperInit(station.GasStationURL, station.ParamStart, station.ParamEnd, station.GasStationName);
                station.TableData = res;
                this.gasRepository.Update(station);
            }

            await this.gasRepository.SaveChangesAsync();
        }

        public int GetCountOFStations()
        {
            return this.gasRepository.All().Count();
        }

        public async Task<IEnumerable<T>> GetAllGasStations<T>(int[] pagesCount, string city = null, string country = null, int pageToSkip = 1, int take = 5)
        {
            await this.CheckForUpdates();

            IQueryable<GasStation> allGasStations = this.gasRepository.All();

            if (country != null && city != null)
            {
                allGasStations = allGasStations.Where(x => x.Country == country && x.City == city);
            }
            else if (city != null)
            {
                allGasStations = allGasStations.Where(x => x.City == city);
            }

            allGasStations = allGasStations.OrderBy(x => x.CreatedOn);

            int countOfStations = allGasStations.Count();

            if (pageToSkip > 0)
            {
                int pages = countOfStations / take;
                if (countOfStations % take != 0)
                {
                    pages += 1;
                }

                pagesCount[0] = pages;
                allGasStations = allGasStations.Skip((pageToSkip - 1) * take).Take(take);
            }

            List<T> model = allGasStations.To<T>().ToList();

            //if (model.Count == 0)
            //{
            //    throw new ArgumentException("Not found any stations.");
            //}

            return model;
        }

        /// <summary>
        /// Checking if it is needet to update the current data in sql.
        /// </summary>
        /// <returns>None.</returns>
        protected virtual async Task CheckForUpdates()
        {
            var modEntity = this.gasRepository.All()
                .Where(x => x.id == 1).FirstOrDefault();

            // Check the sql date for compare.
            if (modEntity.ModifiedOn < DateTime.Now)
            {
                // Set it with .AddHours(4).
                await this.UpdatingPrices();
            }
        }

        /// <summary>
        /// Update logic for prices and data in the sql tables.
        /// </summary>
        /// <returns>None.</returns>
        protected virtual async Task UpdatingPrices()
        {
            var stations = this.gasRepository.All().ToList();

            // Make static variable for hours.
            var sqlTimeToUpdate = DateTime.Now.AddHours(4);

            foreach (var station in stations)
            {
                var res = await this.webScr.WebScraperInit(station.GasStationURL, station.ParamStart, station.ParamEnd, station.GasStationName);

                station.TableData = res;

                if (station.id == 1)
                {
                    station.ModifiedOn = sqlTimeToUpdate;
                }

                this.gasRepository.Update(station);
            }

            await this.gasRepository.SaveChangesAsync();
        }
    }
}