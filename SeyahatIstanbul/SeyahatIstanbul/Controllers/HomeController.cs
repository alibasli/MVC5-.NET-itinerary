using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeyahatIstanbul.Models;
using SeyahatIstanbul.App_Start;
using System.Web.UI.WebControls;

namespace SeyahatIstanbul.Controllers
{
    [Settings]
    public class HomeController : Controller
    {
        SeyahatIstanbulEntities dm;
        public ActionResult Index()
        {
            loadTour();
            loadClock();
            loadTabVehicleList();
            loadVehicleDeliveryPlace();
            loadApart();
            slider();
            return View();
        }

        public string TarihGetir()
        {
            return "Tarih : " + DateTime.Now;
        }
        public ActionResult RentBooking(HomePageModel data)
        {
            //ViewBag.data = data.cloock_1 + "-" + data.rfProductId;
            return View();
        }
        public void slider()
        {
            List<SliderList> sliderList = new List<SliderList>();
            dm = new SeyahatIstanbulEntities();
            var vehicleList_ = (from rv in dm.RentVehicle
                                from m in dm.Model
                                from b in dm.Brand
                                where rv.blStatus == true
                                  && rv.rfModelId == m.sqModelId
                                  && m.rfBrandId == b.sqBrandId
                                  && rv.blDisplaySlider == true
                                select new { imageRotute = rv.chImageName }).Distinct().ToList();

            int t = 0;
            for (int i = 0; i < 7; i++)
            {
                SliderList slider = new SliderList();
                switch (i)
                {
                    case 2:
                        slider.imageRoute_1 = "Content/img/RentApart/apart_4_1.jpg";
                        slider.imageRoute_2 = "Content/img/RentApart/apart_4_3.jpg";
                        break;
                    case 5:
                        slider.imageRoute_1 = "Content/img/Renttur/tur_1_1.jpg";
                        slider.imageRoute_2 = "Content/img/Renttur/tur_1_2.jpg";
                        break;
                    default:
                        slider.imageRoute_1 = "Content/img/RentVehicle/" + vehicleList_[t].imageRotute + "_1.jpg";
                        slider.imageRoute_2 = "Content/img/RentVehicle/" + vehicleList_[t].imageRotute + "_3.jpg";
                        t++;
                        break;
                }
                sliderList.Add(slider);
            }

            ViewBag.sliderList = sliderList;


        }
        public void loadTabVehicleList()
        {
            dm = new SeyahatIstanbulEntities();
            SelectList selectList = new SelectList((from m in dm.Model
                                                    from b in dm.Brand
                                                    from rv in dm.RentVehicle
                                                    where m.blStatus == true
                                                       && b.blStatus == true
                                                       && rv.blStatus == true
                                                       && b.sqBrandId == m.rfBrandId
                                                    select new { Value = m.sqModelId, Text = b.chValue + " " + m.chValue + " ( " + rv.chModelYear + " model ) " }).Distinct(), "Value", "Text");

            ViewBag.TabVehicleList = selectList;

        }
        public void loadVehicleDeliveryPlace()
        {
            dm = new SeyahatIstanbulEntities();
            SelectList selectList = new SelectList(from d in dm.Address
                                                   where d.blStatus == true && d.rfAddressTypeId == 3
                                                   select new { Value = d.sqAddressId, Text = d.chValue }, "Value", "Text");

            ViewBag.VehicleDeliveryPlace = selectList;

        }
        public void loadApart()
        {
            dm = new SeyahatIstanbulEntities();
            SelectList selectList = new SelectList(from d in dm.RentApart
                                                   where d.blStatus == true
                                                   select new { Value = d.sqRentApartId, Text = d.chAddress }, "Value", "Text");

            ViewBag.ApartPlace = selectList;

            SelectList selectList2 = new SelectList(from d in dm.RentApart
                                                    where d.blStatus == true
                                                    select new { Value = d.sqRentApartId, Text = d.dgRoomSize }, "Value", "Text");
            ViewBag.RoomCount = selectList2;


            SelectList selectList3 = new SelectList(from r in dm.RentApart
                                                    from c in dm.County
                                                    where r.blStatus == true
                                                       && r.rfCountyId == c.sqCountyId
                                                    select new { Value = r.sqRentApartId, Text = c.chCountyName + " - " + r.chAddress.Substring(0,20) +"... ( "+ r.chProperties +" )"}, "Value", "Text");

            ViewBag.TabApartleList = selectList3;
        }
        public void loadTour()
        {
            dm = new SeyahatIstanbulEntities();
            SelectList selectList = new SelectList(from d in dm.Tour
                                                   where d.blStatus == true
                                                   select new { Value = d.sqTourId, Text = d.chPlaces }, "Value", "Text");

            ViewBag.TabTourList = selectList;
        }
        public void loadClock()
        {
            List<SelectListItem> list = new List<SelectListItem>(){

            new SelectListItem { Text = "01:00", Value = "01:00"},
            new SelectListItem { Text = "02:00", Value = "02:00"},
            new SelectListItem { Text = "03:00", Value = "03:00"},
            new SelectListItem { Text = "04:00", Value = "04:00"},
            new SelectListItem { Text = "05:00", Value = "05:00"},
            new SelectListItem { Text = "06:00", Value = "06:00"},
            new SelectListItem { Text = "07:00", Value = "07:00"},
            new SelectListItem { Text = "08:00", Value = "08:00"},
            new SelectListItem { Text = "09:00", Value = "09:00",Selected = true },
            new SelectListItem { Text = "10:00", Value = "10:00" },
            new SelectListItem { Text = "11:00", Value = "11:00" },
            new SelectListItem { Text = "12:00", Value = "12:00" },
            new SelectListItem { Text = "13:00", Value = "13:00" },
            new SelectListItem { Text = "14:00", Value = "14:00" },
            new SelectListItem { Text = "15:00", Value = "15:00" },
            new SelectListItem { Text = "16:00", Value = "16:00" },
            new SelectListItem { Text = "17:00", Value = "17:00" },
            new SelectListItem { Text = "18:00", Value = "18:00" },
            new SelectListItem { Text = "19:00", Value = "19:00" },
            new SelectListItem { Text = "20:00", Value = "20:00" },
            new SelectListItem { Text = "21:00", Value = "21:00" },
            new SelectListItem { Text = "22:00", Value = "22:00" },
            new SelectListItem { Text = "23:00", Value = "23:00" },
            new SelectListItem { Text = "24:00", Value = "24:00" }
            };


            ViewBag.Clock = list;

        }
        public void laodLng()
        {
            List<ListItem> lst = new List<ListItem>();
            if (Session["lng"] == "tr")
            {
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
            if (Session["lng"] == "en")
            {
                ListItem item = new ListItem()
                {
                    Text = "Türkçe",
                    Value = "tr"
                };
                lst.Add(item);
                item = new ListItem()
                {
                    Text = "English",
                    Value = "en",
                    Selected = true
                };
                lst.Add(item);
            }
            ViewBag.ddLng = lst;
        }
        public void lngSet(string lng)
        {
            Session["lng"] = lng;
            Settings set = new Settings();
            set.lngSettings();
        }


    }
}
