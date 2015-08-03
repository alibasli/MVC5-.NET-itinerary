using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeyahatIstanbul.Controllers
{
    [Settings]
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult AboutUs()
        {
            return View();
        }

    }
}
