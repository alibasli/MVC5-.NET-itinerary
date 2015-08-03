using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeyahatIstanbul.Models;
using SeyahatIstanbul.App_Start;

namespace SeyahatIstanbul.Controllers
{
    [Settings]
    public class BookingController : Controller
    {
        SeyahatIstanbulEntities dm;
        [HttpGet]
        public ActionResult Booking(HomePageModel data)
        {
            #region queryString kontrolü

            string catguid = Request.QueryString["cat"];
            string prod_ = Request.QueryString["prod"];

            ErrorLog error = new ErrorLog();
            dm = new SeyahatIstanbulEntities();

            if (Session["kId"] == null)
            {
                error.dgErrorCode = 1;
                error.chError = "Üye girişi yapılmadan rezarvasyon yapılmaya çalışılıyor";
                error.chPageName = "Booking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else if (catguid == "" || prod_ == "")
            {
                error.dgErrorCode = 1;
                error.chError = "Doğru linkle giriş yapılmıyor";
                error.chPageName = "Booking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            #endregion

            #region category ve productId



            Category cat = (from c in dm.Category
                            where 1 == 1
                               && c.blStatus == true
                               && c.chGuid == catguid
                            select c).SingleOrDefault();

            if (cat == null)
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction(cat.chUrl, new { prod = prod_ });

            #endregion

        }

        [HttpGet]
        public ActionResult Vehicle()
        {
            string prod_ = Request.QueryString["prod"];
            ErrorLog error = new ErrorLog();
            dm = new SeyahatIstanbulEntities();

            if (Session["kId"] == null)
            {
                error.dgErrorCode = 1;
                error.chError = "Üye girişi yapılmadan rezarvasyon yapılmaya çalışılıyor";
                error.chPageName = "Vehicle/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else if (prod_ == "")
            {
                error.dgErrorCode = 1;
                error.chError = "Doğru linkle giriş yapılmıyor";
                error.chPageName = "Booking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            int prod;
            try
            {
                prod = Convert.ToInt32(prod_);
            }
            catch (Exception)
            {

                throw;
            }

            SettingController set = new SettingController();
            List<VehicleList> vehicleList = set.loadVehicleList().Where(d => d.dgVehicleId == prod).ToList();

            ViewBag.Clock = set.ddClock();
            ViewBag.TabVehicleList = set.ddVehicleList();
            ViewBag.VehicleDeliveryPlace = set.ddVehicleDeliveryPlace();


            ViewBag.VehicleList = vehicleList;

            return View();
        }

        [HttpPost]
        public ActionResult VehicleBooking(HomePageModel data)
        {
            dm = new SeyahatIstanbulEntities();
            ErrorLog error = new ErrorLog();

            //kullanıcı kontrol
            if (Session["kId"] == null)
            {
                error.dgErrorCode = 1;
                error.chError = "Üye girişi yaplısınız";
                error.chPageName = "VehicleBooking/BookingController";
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            int cusId = Convert.ToInt32(Session["kId"]);

            #region ekrandan alınan bilgiler

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            Int16 catId = 1;
            int prodId = 0;
            int place_1 = 0;
            int place_2 = 0;
            string c_1 = "";
            string c_2 = "";

            try
            {
                c_1 = data.Vehcloock_1;
                c_2 = data.Vehcloock_2;
                startDate = Convert.ToDateTime(data.dtVehStartDate + " " + c_1);
                endDate = Convert.ToDateTime(data.dtVehEndDate + " " + c_2);
                prodId = Convert.ToInt32(data.rfVehicleId);
                place_1 = Convert.ToInt32(data.rfVehDeliveryPlace_1);
                place_2 = Convert.ToInt32(data.rfVehDeliveryPlace_2);

            }
            catch (Exception)
            {

                error.dgErrorCode = 2;
                error.chError = "Alanlar boş geçilemez";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region bilgi kontrolleri
            if (startDate < DateTime.Now)
            {
                error.dgErrorCode = 3;
                error.chError = "Seçilen tarih şuan tarihten küçük olamaz";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            else if (startDate > endDate)
            {
                error.dgErrorCode = 4;
                error.chError = "Bitiş tarihi başlağıç tarihinden küçük olamaz";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion

            List<Process> processList = (from p in dm.Process
                                         where p.rfCustomerId == cusId
                                            && p.blStatus == true
                                            && (p.rfStatusId == 1 || p.rfStatusId == 2)
                                         select p).ToList();
            Customer customer = (from c in dm.Customer
                                 where c.sqCustomerId == cusId
                                 select c).SingleOrDefault();

            #region 1-> Müşteri krumsal değilse rezarvasyonda çakışan tarih var mı?
            List<Process> cakisanRezarvasyon = processList.Where(d => d.rfCategoryId == 1 && !(d.dtStartDate > endDate || d.dtEndDate < startDate)).ToList();

            if (cakisanRezarvasyon.Count() != 0 && customer.rfCustomerTypeId != 3)
            {
                error.dgErrorCode = 5;
                error.chError = "Çakışan rezarvasyonunuz var.! <br /><br />" + String.Format("{0:dd.MM.yyyy}", cakisanRezarvasyon[0].dtStartDate) + " - " + String.Format("{0:dd.MM.yyyy}", cakisanRezarvasyon[0].dtEndDate) + " tarihleri arasında yaptığınız rezarvasyonun ödemesini yaptıktan sonra başka rezarvasyon yapabilirsiniz ";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region 2-> 3'ten fazla rezarvayon var mı
            if (processList.Count() >= 3 && customer.rfCustomerTypeId != 3)
            {
                error.dgErrorCode = 6;
                error.chError = "Ödeme yapmadan 3 ten fazla rezarvasyon yapamazsınız";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now; error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion


            int kacGun = Convert.ToDateTime(endDate).Day - Convert.ToDateTime(startDate).Day;

            Price price = (from p in dm.Price
                           where p.rfCategoryId == catId
                              && p.rfProductId == prodId
                              && p.blStatus == true
                              && p.dgDay <= kacGun
                           select p).OrderByDescending(d => d.dgDay).OrderByDescending(d => d.dtCreated).FirstOrDefault();//hatalı olarak iki fiyat olsda en son eklenen alınacak

            int totalPrice = Convert.ToInt32(price.dgValue) * kacGun;


            int processStatus;
            if (customer.rfCustomerTypeId == 1)
                processStatus = 1;
            else
                processStatus = 2;

            Process process = new Process()
            {
                rfCategoryId = catId,
                rfProductId = prodId,
                rfCustomerId = cusId,
                dtStartDate = Convert.ToDateTime(startDate.ToShortDateString() + " " + c_1),
                dtEndDate = Convert.ToDateTime(endDate.ToShortDateString() + " " + c_2),
                rfDeliveryPlace_1 = place_1,
                rfDeliveryPlace_2 = place_2,
                rfStatusId = processStatus,
                dgPrice = totalPrice,
                blStatus = true,
                dtCreated = DateTime.Now,
                cdCreator = "İnternet"
            };

            dm.Process.Add(process);
            dm.SaveChanges();

            error.dgErrorCode = 0;
            error.chError = "Kaydınız başarıyla oluşmuştur";
            error.chPageName = "VehicleBooking/BookingController";
            error.dtCreated = DateTime.Now;
            error.chIpAddress = Request.UserHostAddress;

            return Json(error, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult ApartBooking(HomePageModel data)
        {
            dm = new SeyahatIstanbulEntities();
            ErrorLog error = new ErrorLog();

            //kullanıcı kontrol
            if (Session["kId"] == null)
            {
                error.dgErrorCode = 1;
                error.chError = "Üye girişi yaplısınız";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            int cusId = Convert.ToInt32(Session["kId"]);

            #region ekrandan alınan bilgiler

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            Int16 catId = 2;
            int prodId = 0;
            string c_1 = "";
            string c_2 = "";

            try
            {
                c_1 = data.Vehcloock_1;
                c_2 = data.Vehcloock_2;
                startDate = Convert.ToDateTime(data.dtApartStartDate + " " + c_1);
                endDate = Convert.ToDateTime(data.dtApartEndDate + " " + c_2);
                prodId = Convert.ToInt32(data.rfApartId);
            }
            catch (Exception)
            {
                error.dgErrorCode = 2;
                error.chError = "Alanlar boş geçilemez";
                error.chPageName = "ApartBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region bilgi kontrolleri
            if (startDate < DateTime.Now)
            {
                error.dgErrorCode = 3;
                error.chError = "Seçilen tarih şuan tarihten küçük olamaz";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            else if (startDate > endDate)
            {
                error.dgErrorCode = 4;
                error.chError = "Bitiş tarihi başlağıç tarihinden küçük olamaz";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion

            List<Process> processList = (from p in dm.Process
                                         where p.rfCustomerId == cusId
                                            && p.blStatus == true
                                            && (p.rfStatusId == 1 || p.rfStatusId == 2)
                                         select p).ToList();
            Customer customer = (from c in dm.Customer
                                 where c.sqCustomerId == cusId
                                 select c).SingleOrDefault();

            #region 1-> Müşteri krumsal değilse rezarvasyonda çakışan tarih var mı?
            List<Process> cakisanRezarvasyon = processList.Where(d => d.rfCategoryId == 2 && !(d.dtStartDate > endDate || d.dtEndDate < startDate)).ToList();

            if (cakisanRezarvasyon.Count() != 0 && customer.rfCustomerTypeId != 3)
            {
                error.dgErrorCode = 5;
                error.chError = "Çakışan rezarvasyonunuz var.! <br /><br />" + String.Format("{0:dd.MM.yyyy}", cakisanRezarvasyon[0].dtStartDate) + " - " + String.Format("{0:dd.MM.yyyy}", cakisanRezarvasyon[0].dtEndDate) + " tarihleri arasında yaptığınız rezarvasyonun ödemesini yaptıktan sonra başka rezarvasyon yapabilirsiniz ";
                error.chPageName = "ApartBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region 2-> 3'ten fazla rezarvayon var mı
            if (processList.Count() >= 3 && customer.rfCustomerTypeId != 3)
            {
                error.dgErrorCode = 6;
                error.chError = "Ödeme yapmadan 3 ten fazla rezarvasyon yapamazsınız";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion


            int kacGun = Convert.ToDateTime(endDate).Day - Convert.ToDateTime(startDate).Day;

            Price price = (from p in dm.Price
                           where p.rfCategoryId == catId
                              && p.rfProductId == prodId
                              && p.blStatus == true
                              && p.dgDay <= kacGun
                           select p).OrderByDescending(d => d.dgDay).OrderByDescending(d => d.dtCreated).FirstOrDefault();//hatalı olarak iki fiyat olsda en son eklenen alınacak

            int totalPrice = Convert.ToInt32(price.dgValue) * kacGun;


            int processStatus;
            if (customer.rfCustomerTypeId == 1)
                processStatus = 1;
            else
                processStatus = 2;

            Process process = new Process()
            {
                rfCategoryId = catId,
                rfProductId = prodId,
                rfCustomerId = cusId,
                dtStartDate = Convert.ToDateTime(startDate.ToShortDateString() + " " + c_1),
                dtEndDate = Convert.ToDateTime(endDate.ToShortDateString() + " " + c_2),
                rfStatusId = processStatus,
                dgPrice = totalPrice,
                blStatus = true,
                dtCreated = DateTime.Now,
                cdCreator = "İnternet"
            };

            dm.Process.Add(process);
            dm.SaveChanges();

            error.dgErrorCode = 0;
            error.chError = "Kaydınız başarıyla oluşmuştur";
            error.chPageName = "VehicleBooking/BookingController";
            error.dtCreated = DateTime.Now;
            error.chIpAddress = Request.UserHostAddress;

            return Json(error, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult TourBooking(HomePageModel data)
        {
            dm = new SeyahatIstanbulEntities();
            ErrorLog error = new ErrorLog();

            //kullanıcı kontrol
            if (Session["kId"] == null)
            {
                error.dgErrorCode = 1;
                error.chError = "Üye girişi yaplısınız";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            int cusId = Convert.ToInt32(Session["kId"]);

            #region ekrandan alınan bilgiler

            DateTime startDate = new DateTime();
            Int16 catId = 3;
            int prodId = 0;
            string c_1 = "";

            try
            {
                c_1 = data.tourCloock_1;
                startDate = Convert.ToDateTime(data.dtTourDate + " " + c_1);
                prodId = Convert.ToInt32(data.rfTourId);
            }
            catch (Exception)
            {
                error.dgErrorCode = 2;
                error.chError = "Alanlar boş geçilemez";
                error.chPageName = "TourBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region bilgi kontrolleri
            if (startDate < DateTime.Now)
            {
                error.dgErrorCode = 3;
                error.chError = "Seçilen tarih şuan tarihten küçük olamaz";
                error.chPageName = "TourBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion

            List<Process> processList = (from p in dm.Process
                                         where p.rfCustomerId == cusId
                                            && p.blStatus == true
                                            && (p.rfStatusId == 1 || p.rfStatusId == 2)
                                         select p).ToList();
            Customer customer = (from c in dm.Customer
                                 where c.sqCustomerId == cusId
                                 select c).SingleOrDefault();

            #region 1-> Müşteri krumsal değilse rezarvasyonda çakışan tarih var mı?
            List<Process> cakisanRezarvasyon = processList.Where(d => d.rfCategoryId == 3 && d.dtStartDate == startDate).ToList();

            if (cakisanRezarvasyon.Count() != 0 && customer.rfCustomerTypeId != 3)
            {
                error.dgErrorCode = 5;
                error.chError = "<p style=\"text-align:Justify;\"> Çakışan rezarvasyonunuz var.! <br /><br />" + String.Format("{0:dd.MM.yyyy}", cakisanRezarvasyon[0].dtStartDate) + " tarihinde yaptığınız rezarvasyonun ödemesini yaptıktan sonra başka rezarvasyon yapabilirsiniz </p> ";
                error.chPageName = "ApartBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region 2-> 3'ten fazla rezarvayon var mı
            if (processList.Count() >= 3 && customer.rfCustomerTypeId != 3)
            {
                error.dgErrorCode = 6;
                error.chError = "Ödeme yapmadan 3 ten fazla rezarvasyon yapamazsınız";
                error.chPageName = "VehicleBooking/BookingController";
                error.dtCreated = DateTime.Now;
                error.chIpAddress = Request.UserHostAddress;
                dm.ErrorLog.Add(error);
                dm.SaveChanges();
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            #endregion

            Price price = (from p in dm.Price
                           where p.rfCategoryId == catId
                              && p.rfProductId == prodId
                              && p.blStatus == true
                           select p).OrderByDescending(d => d.dtCreated).FirstOrDefault();//hatalı olarak iki fiyat olsda en son eklenen alınacak

            int totalPrice = Convert.ToInt32(price.dgValue);

            int processStatus;
            if (customer.rfCustomerTypeId == 1)
                processStatus = 1;
            else
                processStatus = 2;

            Process process = new Process()
            {
                rfCategoryId = catId,
                rfProductId = prodId,
                rfCustomerId = cusId,
                dtStartDate = Convert.ToDateTime(startDate.ToShortDateString() + " " + c_1),
                rfStatusId = processStatus,
                dgPrice = totalPrice,
                blStatus = true,
                dtCreated = DateTime.Now,
                cdCreator = "İnternet"
            };

            dm.Process.Add(process);
            dm.SaveChanges();

            error.dgErrorCode = 0;
            error.chError = "Kaydınız başarıyla oluşmuştur";
            error.chPageName = "VehicleBooking/BookingController";
            error.dtCreated = DateTime.Now;
            error.chIpAddress = Request.UserHostAddress;

            return Json(error, JsonRequestBehavior.AllowGet);

        }

        public string TarihGetir()
        {
            return "Tarih : " + DateTime.Now;
        }

        public ActionResult MyBooking()
        {
            //if (Session["kId"] == null)
            //{
            //    return RedirectToAction("Index", "Home");;
            //}
            //if (Session["kId"] == null)
            //    return RedirectToAction("index", "Home");
            SettingController set = new SettingController();
            //set.bookingSettings();
            ViewBag.BookingList = loadVehicleList();

            return View();
        }

        public List<BookingList> loadVehicleList()
        {
            dm = new SeyahatIstanbulEntities();

            int kId = 1;//Convert.ToInt32(Session["kId"].ToString());
            int prodId;

            List<BookingList> bookingList = new List<BookingList>();
            BookingList booking;

            SettingController set = new SettingController();

            var procesesList = (from p in dm.Process
                                where 1 == 1
                                   && p.blStatus == true //yönetici tarafından silinmiş olabilir
                                   && (p.rfStatusId == 1 || p.rfStatusId == 2 || p.rfStatusId == 3)
                                   && p.dtStartDate >= DateTime.Now
                                   && p.rfCustomerId == kId
                                select p).ToList().OrderByDescending(d => d.dtStartDate);


            foreach (var item in procesesList)
            {
                #region Araç
                prodId = Convert.ToInt32(item.rfProductId);

                if (item.rfCategoryId == 1)
                {
                    List<VehicleList> vehicleList = set.loadVehicleList(prodId);
                    VehicleList vh = vehicleList[0];

                    string place1 = (from p in dm.Address
                                     where 1 == 1
                                        && p.rfAddressTypeId == 3 //araç teslim adresi
                                        && p.sqAddressId == item.rfDeliveryPlace_1
                                     select p.chValue).ToString();

                    string place2 = (from p in dm.Address
                                     where 1 == 1
                                        && p.rfAddressTypeId == 3 //araç teslim adresi
                                        && p.sqAddressId == item.rfDeliveryPlace_2
                                     select p.chValue).ToString();

                    string chDurum_ = (from ps in dm.ProcessStatus
                                       where 1 == 1
                                          && ps.sqProcessStatusId == item.rfStatusId
                                       select ps.chValue).ToString();

                    booking = new BookingList
                    {
                        rfCategoryId = 1,
                        rfProductId = vh.dgVehicleId,
                        sqProcessId = item.sqProcessId,
                        chImageName = "~/Content/img/RentVehicle/" + vh.chImageRoute_2,
                        chProductName = vh.chBrand + " " + vh.chModel + " " + vh.chModelYear,
                        dgTotalPrice = Convert.ToInt32(item.dgPrice),
                        chDeliveryPlace_1 = place1,
                        chDeliveryPlace_2 = place2,
                        dgDurum = Convert.ToInt32(item.rfStatusId),
                        chDurum = chDurum_,
                        dtStartDate = Convert.ToDateTime(item.dtStartDate).ToString().Substring(0, 14),
                        dtEndDate = Convert.ToDateTime(item.dtEndDate).ToString().Substring(0, 14),
                        dtCreated = Convert.ToDateTime(item.dtCreated),
                        imageList = vh.imageList

                    };

                    bookingList.Add(booking);




                }
                #endregion
                #region Apart
                if (item.rfCategoryId == 2)
                {
                    List<ApartList> apartList = set.loadApartList(prodId);

                    string chDurum_ = (from ps in dm.ProcessStatus
                                       where 1 == 1
                                          && ps.sqProcessStatusId == item.rfStatusId
                                       select ps.chValue).ToString();



                    foreach (var item2 in apartList)
                    {
                        booking = new BookingList
                        {
                            rfCategoryId = 2,
                            rfProductId = item2.dgApartId,
                            sqProcessId = item.sqProcessId,
                            chProductName = item2.chFulName,
                            dgTotalPrice = Convert.ToInt32(item.dgPrice),
                            chDeliveryPlace_1 = item2.chAdress,
                            dgDurum = Convert.ToInt32(item.rfStatusId),
                            chDurum = chDurum_,
                            dtStartDate = Convert.ToDateTime(item.dtStartDate).ToString().Substring(0, 14),
                            dtEndDate = Convert.ToDateTime(item.dtEndDate).ToString().Substring(0, 14),
                            dtCreated = Convert.ToDateTime(item.dtCreated)
                        };

                        bookingList.Add(booking);

                    }


                }
                #endregion
                #region Tour
                if (item.rfCategoryId == 3)
                {
                    List<TourList> tourList = set.loadTourList(prodId);

                    string chDurum_ = (from ps in dm.ProcessStatus
                                       where 1 == 1
                                          && ps.sqProcessStatusId == item.rfStatusId
                                       select ps.chValue).ToString();



                    foreach (var item2 in tourList)
                    {
                        booking = new BookingList
                        {
                            rfCategoryId = 2,
                            rfProductId = item2.dgTourId,
                            sqProcessId = item.sqProcessId,
                            chProductName = item2.chFulName,
                            dgTotalPrice = Convert.ToInt32(item.dgPrice),
                            chDeliveryPlace_1 = item2.chPlaces,
                            dgDurum = Convert.ToInt32(item.rfStatusId),
                            chDurum = chDurum_,
                            dtStartDate = Convert.ToDateTime(item.dtStartDate).ToString().Substring(0, 14),
                            dtEndDate = Convert.ToDateTime(item.dtEndDate).ToString().Substring(0, 14),
                            dtCreated = Convert.ToDateTime(item.dtCreated)
                        };

                        bookingList.Add(booking);

                    }


                }
                #endregion


            }


            return bookingList;

            //araçlar için yaptığı rezervayon


        }
    }
}
