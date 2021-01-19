using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AkciqApp.ViewModels.GasStationViewHolder
{
    public class GasStationViewModel
    {
        public string City { get; set; }

        public string Country { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<GasStationViewCollectionModel> GasStatuons { get; set; }

        public IEnumerable<string> Addresses { get; set; }
    }
}