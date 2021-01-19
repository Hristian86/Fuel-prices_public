namespace AkciqApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;

    public class GasStationSeed : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GasStations.Any())
            {
                return;
            }

            dbContext.GasStations.Add(new GasStation()
            {
                GasStationURL = "https://bg.fuelo.net/gasstation/id/1400?lang=en",
                GasStationName = "Antim",
                ParamStart = "<tbody>",
                ParamEnd = "</tbody>",
                City = "Ruse",
            });

            dbContext.GasStations.Add(new GasStation()
            {
                GasStationURL = "https://bg.fuelo.net/brand/id/1?lang=en",
                GasStationName = "EKO",
                ParamStart = "<tbody>",
                ParamEnd = "</tbody>",
                City = "Ruse",
            });


            dbContext.GasStations.Add(new GasStation()
            {
                GasStationURL = "https://bg.fuelo.net/brand/id/3?lang=en",
                GasStationName = "OMV",
                ParamStart = "<tbody>",
                ParamEnd = "</tbody>",
                City = "Ruse",
            });

            dbContext.GasStations.Add(new GasStation()
            {
                GasStationURL = "https://bg.fuelo.net/brand/id/2?lang=en",
                GasStationName = "Lukoil",
                ParamStart = "<tbody>",
                ParamEnd = "</tbody>",
                City = "Ruse",
            });

            dbContext.GasStations.Add(new GasStation()
            {
                GasStationURL = "https://bg.fuelo.net/gasstation/id/1401?lang=en",
                GasStationName = "Petrol",
                ParamStart = "<tbody>",
                ParamEnd = "</tbody>",
                City = "Ruse",
            });

            dbContext.GasStations.Add(new GasStation()
            {
                GasStationURL = "https://bg.fuelo.net/gasstation/id/641?lang=en",
                GasStationName = "Gasprom",
                ParamStart = "<tbody>",
                ParamEnd = "</tbody>",
                City = "Ruse",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
