using SeyahatIstanbul.App_Start;
using SeyahatIstanbul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeyahatIstanbul.Controllers
{
    [Settings]
    public class SettingController : Controller
    {
        SeyahatIstanbulEntities dm;

        [HttpPost]
        public void lngSetting(string lng)
        {
            Session["lng"] = lng;
            Settings set = new Settings();
            set.lngSettings();
            return;
        }

        [HttpPost]
        public ActionResult sessionControl()
        {
            int durum = 1;
            if (Session["kId"] == null)
                durum = 0;
            return Json(durum, JsonRequestBehavior.AllowGet);
        }

        public List<VehicleList> loadVehicleList()
        {
            List<VehicleList> vehicleList = new List<VehicleList>();
            dm = new SeyahatIstanbulEntities();
            var vehicleList_ = (from rv in dm.RentVehicle
                                from m in dm.Model
                                from b in dm.Brand
                                from gt in dm.GearType
                                from ft in dm.FuelType
                                where rv.blStatus == true
                                   && rv.rfModelId == m.sqModelId
                                   && m.rfBrandId == b.sqBrandId
                                   && gt.sqGearTypeId == rv.rfGearTypeId
                                   && ft.sqFuelTypeId == rv.rfFuelTypeId
                                select new
                                {
                                    vehicleId = rv.sqVehicleId,
                                    model = m.chValue,
                                    brand = b.chValue,
                                    gearType = gt.chValue,
                                    fuelType = ft.chValue,
                                    capacity = rv.qtCapacity,
                                    blSlider = rv.blDisplaySlider,
                                    imageRotute = rv.chImageName,
                                    modelYear = rv.chModelYear
                                }).Distinct().ToList();


            foreach (var item in vehicleList_)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 1
                                        && p.rfProductId == item.vehicleId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();

                Category cat = (from c in dm.Category
                                where c.sqCategoryId == 1
                                select c).SingleOrDefault();

                List<Images> imageList_ = (from i in dm.Images
                                           where 1 == 1
                                              && i.rfCategory == 1
                                              && i.rfDestination == item.vehicleId
                                           select i).ToList();


                VehicleList vehicle = new VehicleList();

                vehicle.chCatGuid = cat.chGuid;
                vehicle.dgVehicleId = item.vehicleId;
                vehicle.chBrand = item.brand;
                vehicle.chModel = item.model;
                vehicle.chFuelType = item.fuelType;
                vehicle.chGearType = item.gearType;
                vehicle.chImageRoute_1 = "~/Content/img/RentVehicle/" + item.imageRotute + "_1.jpg";
                vehicle.chImageRoute_2 = "~/Content/img/RentVehicle/" + item.imageRotute + "_2.jpg";
                vehicle.chImageRoute_3 = "~/Content/img/RentVehicle/" + item.imageRotute + "_3.jpg";
                vehicle.chFulName = item.brand + " " + item.model + " - " + item.modelYear;
                vehicle.chCapacity = item.capacity + " Kişilik";
                vehicle.chPrice_1_7 = pList[0].dgValue.ToString();
                vehicle.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                vehicle.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                vehicle.chPrice_25 = pList[3].dgValue.ToString() + " TL";
                vehicle.imageList = imageList_;

                vehicleList.Add(vehicle);
            }

            return vehicleList;

        }
        public List<VehicleList> loadVehicleList(int sqVehicleId)
        {
            List<VehicleList> vehicleList = new List<VehicleList>();
            dm = new SeyahatIstanbulEntities();
            var vehicleList_ = (from rv in dm.RentVehicle
                                from m in dm.Model
                                from b in dm.Brand
                                from gt in dm.GearType
                                from ft in dm.FuelType
                                where rv.blStatus == true
                                   && rv.rfModelId == m.sqModelId
                                   && m.rfBrandId == b.sqBrandId
                                   && gt.sqGearTypeId == rv.rfGearTypeId
                                   && ft.sqFuelTypeId == rv.rfFuelTypeId
                                   && rv.sqVehicleId == sqVehicleId
                                select new
                                {
                                    vehicleId = rv.sqVehicleId,
                                    model = m.chValue,
                                    brand = b.chValue,
                                    gearType = gt.chValue,
                                    fuelType = ft.chValue,
                                    capacity = rv.qtCapacity,
                                    blSlider = rv.blDisplaySlider,
                                    imageRotute = rv.chImageName,
                                    modelYear = rv.chModelYear
                                }).Distinct().ToList();


            foreach (var item in vehicleList_)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 1
                                        && p.rfProductId == item.vehicleId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();

                Category cat = (from c in dm.Category
                                where c.sqCategoryId == 1
                                select c).SingleOrDefault();

                List<Images> imageList_ = (from i in dm.Images
                                           where 1 == 1
                                              && i.rfCategory == 1
                                              && i.rfDestination == item.vehicleId
                                           select i).ToList();


                VehicleList vehicle = new VehicleList();

                vehicle.chCatGuid = cat.chGuid;
                vehicle.dgVehicleId = item.vehicleId;
                vehicle.chBrand = item.brand;
                vehicle.chModel = item.model;
                vehicle.chFuelType = item.fuelType;
                vehicle.chGearType = item.gearType;
                vehicle.chImageRoute_1 = "~/Content/img/RentVehicle/" + item.imageRotute + "_1.jpg";
                vehicle.chImageRoute_2 = "~/Content/img/RentVehicle/" + item.imageRotute + "_2.jpg";
                vehicle.chImageRoute_3 = "~/Content/img/RentVehicle/" + item.imageRotute + "_3.jpg";
                vehicle.chFulName = item.brand + " " + item.model + " - " + item.modelYear;
                vehicle.chCapacity = item.capacity + " Kişilik";
                vehicle.chPrice_1_7 = pList[0].dgValue.ToString();
                vehicle.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                vehicle.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                vehicle.chPrice_25 = pList[3].dgValue.ToString() + " TL";
                vehicle.imageList = imageList_;

                vehicleList.Add(vehicle);
            }

            return vehicleList;

        }

        public List<ApartList> loadApartList()
        {
            List<ApartList> apartList = new List<ApartList>();
            dm = new SeyahatIstanbulEntities();
            var ApartList_ = (from ra in dm.RentApart
                              from c in dm.County
                              where ra.blStatus == true
                                 && ra.rfCountyId == c.sqCountyId
                              select new
                              {
                                  ApartId = ra.sqRentApartId,
                                  Roomsize = ra.dgRoomSize,
                                  Properties = ra.chProperties,
                                  Countyy = c.chCountyName,
                                  Desc = ra.chDesc,
                                  Adres = ra.chAddress,
                                  blSlider = ra.blDisplaySlider,
                                  imageRotute = ra.chImageName,
                              }).Distinct().ToList();

            foreach (var item in ApartList_)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 1
                                        && p.rfProductId == item.ApartId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();

                ApartList Apart = new ApartList();

                Apart.dgApartId = item.ApartId;
                Apart.chAdress = item.Adres;
                Apart.dgRoomSize = Convert.ToInt32(item.Roomsize);
                Apart.chProperties = item.Properties;
                Apart.chDesc = item.Desc;
                Apart.chCounty = item.Countyy;
                Apart.chImageRoute_1 = item.imageRotute + "_1.jpg";
                Apart.chImageRoute_2 = item.imageRotute + "_2.jpg";
                Apart.chImageRoute_3 = item.imageRotute + "_3.jpg";
                Apart.chFulName = item.imageRotute;
                Apart.chPrice_1_7 = pList[0].dgValue.ToString();
                Apart.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                Apart.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                Apart.chPrice_25 = pList[3].dgValue.ToString() + " TL";

                Apart.imageList = (from img in dm.Images
                                   where img.rfCategory == 2 // apart
                                      && img.rfDestination == item.ApartId
                                   select img).Distinct().ToList();

                apartList.Add(Apart);
            }

            return apartList;
        }
        public List<ApartList> loadApartList(int sqApartId)
        {
            List<ApartList> apartList = new List<ApartList>();
            dm = new SeyahatIstanbulEntities();
            var ApartList_ = (from ra in dm.RentApart
                              from c in dm.County
                              where ra.blStatus == true
                                 && ra.rfCountyId == c.sqCountyId
                                 && ra.sqRentApartId == sqApartId
                              select new
                              {
                                  ApartId = ra.sqRentApartId,
                                  Roomsize = ra.dgRoomSize,
                                  Properties = ra.chProperties,
                                  County = c.chCountyName,
                                  Desc = ra.chDesc,
                                  Adres = ra.chAddress,
                                  blSlider = ra.blDisplaySlider,
                                  imageRotute = ra.chImageName,
                              }).Distinct().ToList();

            foreach (var item in ApartList_)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 1
                                        && p.rfProductId == item.ApartId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();

                ApartList Apart = new ApartList();

                Apart.dgApartId = item.ApartId;
                Apart.chAdress = item.Adres;
                Apart.dgRoomSize = Convert.ToInt32(item.Roomsize);
                Apart.chProperties = item.Properties;
                Apart.chDesc = item.Desc;
                Apart.chCounty = item.County;
                Apart.chImageRoute_1 = item.imageRotute + "_1.jpg";
                Apart.chImageRoute_2 = item.imageRotute + "_2.jpg";
                Apart.chImageRoute_3 = item.imageRotute + "_3.jpg";
                Apart.chFulName = item.Desc;
                Apart.chPrice_1_7 = pList[0].dgValue.ToString();
                Apart.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                Apart.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                Apart.chPrice_25 = pList[3].dgValue.ToString() + " TL";

                Apart.imageList = (from img in dm.Images
                                   where img.rfCategory == 2 // apart
                                      && img.rfDestination == item.ApartId
                                   select img).Distinct().ToList();

                apartList.Add(Apart);
            }

            return apartList;
        }

        public List<TourList> loadTourList()
        {
            List<TourList> tourList = new List<TourList>();
            dm = new SeyahatIstanbulEntities();
            
            var TourList_ = (from t in dm.Tour
                             where t.blStatus == true
                              select new
                              {
                                  dgTourId = t.sqTourId,
                                  chFulName = t.chDesc,
                                  chPlaces = t.chPlaces,
                                  dgPeopleCount = t.chPersonCount,
                                  chStartPlace = t.chStartPlace,
                                  chEndPlace = t.chEndPlace,
                                  chDesc = t.chDesc,
                                  chImageRoute = t.chImageName
                              }).Distinct().ToList();

            foreach (var item in TourList_)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 1
                                        && p.rfProductId == item.dgTourId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();

                TourList tour = new TourList();

                tour.dgTourId = item.dgTourId;
                tour.chPlaces = item.chPlaces;
                tour.dgPeopleCount = Convert.ToInt32(item.dgPeopleCount);
                tour.chStartPlace = item.chStartPlace;
                tour.chEndPlace = item.chEndPlace;
                tour.chDesc = item.chDesc;
                tour.chImageRoute_1 = item.chImageRoute + "_1.jpg";
                tour.chImageRoute_2 = item.chImageRoute + "_2.jpg";
                tour.chImageRoute_3 = item.chImageRoute + "_3.jpg";
                tour.chPrice_1_7 = pList[0].dgValue.ToString();
                tour.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                tour.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                tour.chPrice_25 = pList[3].dgValue.ToString() + " TL";

                tour.imageList = (from img in dm.Images
                                   where img.rfCategory == 2 // apart
                                      && img.rfDestination == item.dgTourId
                                   select img).Distinct().ToList();

                tourList.Add(tour);
            }

            return tourList;
        }
        public List<TourList> loadTourList(int sqTourId)
        {
            List<TourList> tourList = new List<TourList>();
            dm = new SeyahatIstanbulEntities();

            var TourList_ = (from t in dm.Tour
                             where t.blStatus == true
                                && t.sqTourId == sqTourId
                             select new
                             {
                                 dgTourId = t.sqTourId,
                                 chFulName = t.chDesc,
                                 chPlaces = t.chPlaces,
                                 dgPeopleCount = t.chPersonCount,
                                 chStartPlace = t.chStartPlace,
                                 chEndPlace = t.chEndPlace,
                                 chDesc = t.chDesc,
                                 chImageRoute = t.chImageName
                             }).Distinct().ToList();

            foreach (var item in TourList_)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 1
                                        && p.rfProductId == item.dgTourId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();

                TourList tour = new TourList();

                tour.dgTourId = item.dgTourId;
                tour.chPlaces = item.chPlaces;
                tour.dgPeopleCount = Convert.ToInt32(item.dgPeopleCount);
                tour.chStartPlace = item.chStartPlace;
                tour.chEndPlace = item.chEndPlace;
                tour.chDesc = item.chDesc;
                tour.chImageRoute_1 = item.chImageRoute + "_1.jpg";
                tour.chImageRoute_2 = item.chImageRoute + "_2.jpg";
                tour.chImageRoute_3 = item.chImageRoute + "_3.jpg";
                tour.chPrice_1_7 = pList[0].dgValue.ToString();
                tour.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                tour.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                tour.chPrice_25 = pList[3].dgValue.ToString() + " TL";

                tour.imageList = (from img in dm.Images
                                  where img.rfCategory == 2 // apart
                                     && img.rfDestination == item.dgTourId
                                  select img).Distinct().ToList();

                tourList.Add(tour);
            }

            return tourList;
        }


        public SelectList ddVehicleList()
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

            return selectList;

        }
        public SelectList ddVehicleDeliveryPlace()
        {
            dm = new SeyahatIstanbulEntities();
            SelectList selectList = new SelectList(from d in dm.Address
                                                   where d.blStatus == true && d.rfAddressTypeId == 3
                                                   select new { Value = d.sqAddressId, Text = d.chValue }, "Value", "Text");

            return selectList;

        }
        public SelectList ddApart()
        {
            dm = new SeyahatIstanbulEntities();

            SelectList selectList = new SelectList(from r in dm.RentApart
                                                   from c in dm.County
                                                   where r.blStatus == true
                                                      && r.rfCountyId == c.sqCountyId
                                                   select new { Value = r.sqRentApartId, Text = c.chCountyName + " - " + r.chAddress.Substring(0, 20) + "... ( " + r.chProperties + " )" }, "Value", "Text");

            return selectList;
        }
        public SelectList ddTour()
        {
            dm = new SeyahatIstanbulEntities();
            SelectList selectList = new SelectList(from d in dm.Tour
                                                   where d.blStatus == true
                                                   select new { Value = d.sqTourId, Text = d.chPlaces }, "Value", "Text");

            return selectList;
        }
        public List<SelectListItem> ddClock()
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
            new SelectListItem { Text = "09:00", Value = "09:00"},
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


            return list;

        }

        public void bookingSettings()
        {
            dm = new SeyahatIstanbulEntities();

            CommonSettings sonControl = (from cs in dm.CommonSettings
                                 where 1 == 1
                                    && cs.blStatus == true
                                    && cs.chKey == "bookingControl"
                                 select cs).SingleOrDefault();

            DateTime dtSonControl = Convert.ToDateTime(sonControl.chValue);

            if (dtSonControl >=DateTime.Now.Date)
                return;

            DateTime bugun = DateTime.Now.Date;
            List<Process> pGecmisList = (from p in dm.Process
                                   where 1 == 1
                                      && p.blStatus == true
                                      && (p.rfStatusId == 1 || p.rfStatusId == 2 || p.rfStatusId == 3)
                                      &&  p.dtStartDate < bugun
                                   select p).ToList();

            foreach (var item in pGecmisList)
            {
                item.rfStatusId = 8;
                dm.SaveChanges();

                sonControl.chValue = bugun.ToString();
                dm.SaveChanges();
            }


        }


        //public List<SliderList> sliderApart()
        //{
        //    List<SliderList> sliderList = new List<SliderList>();
        //    dm = new SeyahatIstanbulEntities();
        //    var vehicleList_ = (from rv in dm.RentVehicle
        //                        from i in dm.Images
        //                        where rv.blStatus == true
        //                          && i.rfCategory == 1
        //                          && i.rfDestination == rv.sqVehicleId
        //                        select new { imageRotute = i. }).Distinct().ToList();


        //    for (int i = 0; i < 7; i++)
        //    {
        //        SliderList slider = new SliderList();
        //        slider.imageRoute_1 = "Content/img/RentVehicle/" + vehicleList_[t].imageRotute + "_1.jpg";
        //        slider.imageRoute_2 = "Content/img/RentVehicle/" + vehicleList_[t].imageRotute + "_3.jpg";

        //        sliderList.Add(slider);
        //    }

        //    return sliderList;


        //}
    }
}
