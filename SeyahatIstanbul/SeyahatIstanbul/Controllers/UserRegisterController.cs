using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeyahatIstanbul.Models;

namespace SeyahatIstanbul.Controllers
{
    public class UserRegisterController : Controller
    {
        //
        // GET: /UserRegister/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cus)
        {
            return View();
        }

    }
}
