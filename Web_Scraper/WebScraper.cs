using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web_Scraper.Interfaces;

namespace Web_Scraper
{
    public class WebScraper : StripDataLogic, IWebScraper
    {
        private const string GAS_STATION_ANTIM = "Antim";

        public async Task<string> WebScraperInit(string url, string param1, string param2, string gasStation)
        {
            var data = await this.WebScrap(url);

            var result = base.StripData(data, param1, param2);
            
            //result = this.GasStationTypes(gasStation, result);

            return result;
        }

        protected virtual async Task<string> WebScrap(string url)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = await client.GetStringAsync(url);
            return response;
        }

        // Override.
        //protected override string SplitByImg(string data)
        //{
        //    return "";
        //}

        // Modify the gas stations in the switch.
        private string GasStationTypes(string gasStation, string result)
        {
            switch (gasStation)
            {
                case GAS_STATION_ANTIM:
                    return this.SplitByNewLine(result);
                default:
                    break;
            }

            return result;
        }

        private string SplitByNewLine(string result)
        {
            // return array.
            var arr = result.Split("\t\t").ToArray();
            var res = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                res += arr[i];
            }

            return res;
        }

        private string SplitByType(string fixRes)
        {
            string[] res = fixRes.Split("+");
            string rest = string.Empty;
            rest += res[0];
            for (int i = 1; i < res.Length; i++)
            {
                if (char.IsDigit(res[i][0]))
                {
                    res[i] = res[i].Substring(4, res[i].Length - 5);
                }

                rest += res[i] + "\n";
            }

            return rest;
        }
    }
}
