using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class VehicleList
    {
        public string chCatGuid { get; set; }
        public int dgVehicleId { get; set; }
        public string chModel { get; set; }
        public string chModelYear { get; set; }
        public string chBrand { get; set; }
        public string chFulName { get; set; }
        public string chFuelType { get; set; }
        public string chGearType { get; set; }
        public string chCapacity { get; set; }
        public string chImageRoute_1 { get; set; }
        public string chImageRoute_2 { get; set; }
        public string chImageRoute_3 { get; set; }
        public string chPrice_1_7 { get; set; }
        public string chPrice_8_15 { get; set; }
        public string chPrice_16_24 { get; set; }
        public string chPrice_25 { get; set; }


        public List<Images> imageList { get; set; }



    }
}