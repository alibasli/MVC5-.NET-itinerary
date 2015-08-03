using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class TourList
    {
        public int dgTourId { get; set; }
        public string chFulName { get; set; }
        public string chPlaces { get; set; }
        public int dgPeopleCount { get; set; }
        public string chStartPlace { get; set; }
        public string chEndPlace { get; set; }
        public string chDesc { get; set; }
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