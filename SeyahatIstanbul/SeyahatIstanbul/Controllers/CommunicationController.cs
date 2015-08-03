using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeyahatIstanbul.Controllers
{
    [Settings]
    public class CommunicationController : Controller
    {
        //
        // GET: /Communication/

        public ActionResult Communication()
        {
            return View();
        }

    }
}
