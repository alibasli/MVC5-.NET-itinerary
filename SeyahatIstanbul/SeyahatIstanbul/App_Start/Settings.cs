using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SeyahatIstanbul.App_Start
{
    public class Settings : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            lngSettings(filterContext);
            base.OnActionExecuting(filterContext);

        }
        public void lngSettings(ActionExecutingContext filterContext)
        {
            List<ListItem> lst = new List<ListItem>();
            if (HttpContext.Current.Session["lng"] == null)
            {
                HttpContext.Current.Session["lng"] = "tr";
            }
            if (HttpContext.Current.Session["lng"].ToString() == "en")
            {
                Res.Culture = new CultureInfo("En");
                ListItem item1 = new ListItem()
                {
                    Text = "Türkçe",
                    Value = "tr"
                };
                lst.Add(item1);
                ListItem item2 = new ListItem()
                {
                    Text = "English",
                    Value = "en",
                    Selected = true
                };
                lst.Add(item2);
            }
                
            else
            {
                Res.Culture = new CultureInfo("");
                ListItem item = new ListItem()
                {
                    Text = "Türkçe",
                    Value = "tr",
                    Selected = true
                };
                lst.Add(item);
                item = new ListItem()
                {
                    Text = "English",
                    Value = "en"
                };
                lst.Add(item);
            }
            filterContext.Controller.ViewBag.ddLng = lst;

        }
        public void lngSettings()
        {
            
            if (HttpContext.Current.Session["lng"] == null)
            {
                HttpContext.Current.Session["lng"] = "tr";
            }
            if (HttpContext.Current.Session["lng"].ToString() == "en")
                Res.Culture = new CultureInfo("En");
            else
                Res.Culture = new CultureInfo("");
               
           

        }
    }
}