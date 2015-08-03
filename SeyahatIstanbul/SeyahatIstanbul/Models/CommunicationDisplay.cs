using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class CommunicationDisplay
    {
        [Display(ResourceType = typeof(Res), Name = "comPlace_1")]
        public string comPlace_1 { get; set; }
        [Display(ResourceType = typeof(Res), Name = "comPlace_2")]
        public string comPlace_2 { get; set; }
    }
}