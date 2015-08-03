using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class CustomerLogin
    {
        [Display(ResourceType = typeof(Res), Name = "chEmail")]
        public string chEmail { get; set; }
        [Display(ResourceType = typeof(Res), Name = "chForgotPassword")]
        public string chForgotPassword { get; set; }
        [Display(ResourceType = typeof(Res), Name = "chPassword")]
        public string chPassword { get; set; }
    }
}