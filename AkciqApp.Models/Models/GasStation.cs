namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AkciqApp.Common.Models;
    using AkciqApp.Models.PreBuildModels;

    public class GasStation : PreModel
    {
        public string GasStationURL { get; set; }

        public string GasStationName { get; set; }

        public string ParamStart { get; set; }

        public string ParamEnd { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string TableData { get; set; }
    }
}
