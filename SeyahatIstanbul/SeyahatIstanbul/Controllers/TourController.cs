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
    public class TourController : Controller
    {
        //
        // GET: /Tour/

        SeyahatIstanbulEntities dm = new SeyahatIstanbulEntities();

        public ActionResult TourList()
        {
            loadTourList();
            return View();
        }
        private List<TourList> loadTourList()
        {
            List<TourList> TourList = new List<TourList>();
            List<TourList> sTourList = new List<TourList>();
            dm = new SeyahatIstanbulEntities();
            var v_TourList = (from t in dm.Tour
                               where t.blStatus == true
                               select new
                               {
                                   TourId = t.sqTourId,
                                   fulName = t.chImageName,
                                   Place =t.chPlaces,
                                   Startplace =t.chStartPlace,
                                   EndPlace=t.chEndPlace,
                                   PeopleCount=t.chPersonCount,
                                   Desc =t.chDesc,
                                   ImageRoute = t.chImageName,
                                   blSlider = t.blDisplaySlider,
                                   property = t.chProperties
                               }).Distinct().ToList();

            foreach (var item in v_TourList)
            {
                List<Price> pList = (from p in dm.Price
                                     where p.rfCategoryId == 3
                                        && p.rfProductId == item.TourId
                                        && p.blStatus == true
                                     select p).OrderBy(d => d.dgDay).ToList();


                TourList Tours = new TourList();

                Tours.dgTourId = item.TourId;
                Tours.chFulName = item.fulName;
                Tours.chPlaces = item.Place;
                Tours.chStartPlace = item.Startplace;
                Tours.chEndPlace = item.EndPlace;
                Tours.dgPeopleCount = Convert.ToInt32(item.PeopleCount);
                Tours.chDesc = item.Desc;
                Tours.chImageRoute_1 = item.ImageRoute + "_1.jpg";
                Tours.chImageRoute_2 = item.ImageRoute + "_2.jpg";
                Tours.chImageRoute_3 = item.ImageRoute + "_3.jpg";
                Tours.chPrice_1_7 = pList[0].dgValue.ToString();
                Tours.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                Tours.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                Tours.chPrice_25 = pList[3].dgValue.ToString() + " TL";


                TourList.Add(Tours);
            }

            ViewBag.TourList = TourList;

            return TourList;
        }

    }
}
