using System;
using AkciqApp.Mapping;
using AkciqApp.Models.Models;

namespace AkciqApp.ViewModels.GasStationViewHolder
{
    public class GasStationViewCollectionModel : IMapFrom<GasStation>
    {
        public string GasStationName { get; set; }

        public string TableData { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}