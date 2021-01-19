using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web_Scraper.Interfaces
{
    public interface IWebScraper
    {
        Task<string> WebScraperInit(string url, string param1, string param2, string gasStation);
    }
}
