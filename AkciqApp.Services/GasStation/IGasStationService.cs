namespace AkciqApp.Services.GasStation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGasStationService
    {
        /// <summary>
        /// Update manually gas station prices.
        /// </summary>
        /// <returns>None.</returns>
        Task UpdateGasStationsManually();

        /// <summary>
        /// Get stations by city or country paramater.
        /// </summary>
        /// <typeparam name="T">-.</typeparam>
        /// <param name="city">City.</param>
        /// <param name="country">Country.</param>
        /// <returns>Collection of gas stations.</returns>
        Task<IEnumerable<T>> GetAllGasStations<T>(int[] pagesCount, string city = null, string country = null, int skip = 1, int take = 5);

        /// <summary>
        /// Get the count of stations in sql.
        /// </summary>
        /// <returns>Number of stations.</returns>
        int GetCountOFStations();
    }
}