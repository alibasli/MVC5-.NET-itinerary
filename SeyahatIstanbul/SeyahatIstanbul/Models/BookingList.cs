using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class BookingList
    {
        public int rfCategoryId { get; set; }
        public int rfProductId { get; set; }
        public int sqProcessId { get; set; }
        public string chImageName { get; set; }
        public string chProductName { get; set; }
        public string chDeliveryPlace_1 { get; set; }
        public string chDeliveryPlace_2 { get; set; }
        public String dtStartDate { get; set; }
        public String dtEndDate { get; set; }
        public DateTime dtCreated { get; set; }
        public int dgTotalPrice { get; set; }
        public int dgDurum { get; set; }
        public string chDurum { get; set; }

        public List<Images> imageList { get; set; } 

    }
}