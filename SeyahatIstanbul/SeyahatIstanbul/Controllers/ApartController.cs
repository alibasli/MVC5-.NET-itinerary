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
    public class ApartController : Controller
    {
        //
        // GET: /Apart/
        SeyahatIstanbulEntities dm = new SeyahatIstanbulEntities();
        public ActionResult ApartList()
        {
            loadApartList();
            //loadImageList();
            return View();
        }
        //private List<Images> loadImageList()
        //{
        //    dm = new SeyahatIstanbulEntities();

        //    //ViewBag.HomeList = (from ra in dm.RentApart
        //    //                     where ra.blStatus == true
        //    //                     select ra).ToList();


        //    List<Images> imageList_ = (from img in dm.Images
        //                               from ra in dm.RentApart
        //                               where
        //                                img.rfCategory == 2 // apart
        //                                && img.rfDestination == ra.sqRentApartId
        //                               select img).Distinct().ToList();

        //    ViewBag.ImageList = imageList_;
        //    return imageList_;
        //}
        private List<ApartList> loadApartList()
        {
            List<ApartList> apartList = new List<ApartList>();
            List<ApartList> sApartList = new List<ApartList>();
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

            ViewBag.ApartList = apartList;

            return apartList;
        }

    }
}
