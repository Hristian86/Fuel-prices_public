namespace AkciqApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;

    public class GasViewModel
    {
        public IEnumerable<Address> Addresses { get; set; }

        public int Id { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string IpAddress { get; set; }

        public string GasStationURL { get; set; }

        public string GasStationName { get; set; }

        public string ParamStart { get; set; }

        public string ParamEnd { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string TableData { get; set; }
    }
}
