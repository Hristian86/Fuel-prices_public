namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AkciqApp.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public string City { get; set; }

        public string Country { get; set; }
    }
}
