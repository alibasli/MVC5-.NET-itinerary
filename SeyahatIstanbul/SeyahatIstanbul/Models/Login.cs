using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeyahatIstanbul.Models
{
    public class Login
    {
        [Display(ResourceType = typeof(Res), Name = "login")]
        public string login { get; set; }
        [Display(ResourceType = typeof(Res), Name = "register")]
        public string register { get; set; }
    }
}