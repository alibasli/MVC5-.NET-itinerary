using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class ApartList
    {
        public int dgApartId { get; set; }
        public string chFulName { get; set; }
        public string chCounty { get; set; }
        public string chAdress { get; set; }
        [Display(ResourceType = typeof(Res), Name = "apartDesc")]
        public string chDesc { get; set; }
        public int dgRoomSize { get; set; }
        public string chProperties { get; set; }
        public string chImageRoute_1 { get; set; }
        public string chImageRoute_2 { get; set; }
        public string chImageRoute_3 { get; set; }
        public string chPrice_1_7 { get; set; }
        public string chPrice_8_15 { get; set; }
        public string chPrice_16_24 { get; set; }
        public string chPrice_25 { get; set; }

        //image
        public List<Images> imageList { get; set; }
    }
}