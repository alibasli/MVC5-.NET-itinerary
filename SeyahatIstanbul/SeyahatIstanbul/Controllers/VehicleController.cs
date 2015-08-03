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
    public class VehicleController : Controller
    {
        SeyahatIstanbulEntities dm; 

        public ActionResult VehicleList()
        {
            loadVehicleList();

            return View();
        }

        private List<VehicleList> loadVehicleList()
        {
            List<VehicleList> vehicleList = new List<VehicleList>();
            List<VehicleList> sVehicleList = new List<VehicleList>();
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
                                     select p).OrderBy(d=>d.dgDay).ToList();

                Category cat = (from c in dm.Category
                                where c.sqCategoryId == 1
                                select c).SingleOrDefault();


                VehicleList vehicle = new VehicleList();

                vehicle.chCatGuid = cat.chGuid;
                vehicle.dgVehicleId = item.vehicleId;
                vehicle.chBrand = item.brand;
                vehicle.chModel = item.model;
                vehicle.chFuelType = item.fuelType;
                vehicle.chGearType = item.gearType;
                vehicle.chImageRoute_1 = item.imageRotute + "_1.jpg";
                vehicle.chImageRoute_2 = item.imageRotute + "_2.jpg";
                vehicle.chImageRoute_3 = item.imageRotute + "_3.jpg";
                vehicle.chFulName = item.brand + " " + item.model + " - " + item.modelYear;
                vehicle.chCapacity = item.capacity + " Kişilik";
                vehicle.chPrice_1_7 = pList[0].dgValue.ToString();
                vehicle.chPrice_8_15 = pList[1].dgValue.ToString() + " TL";
                vehicle.chPrice_16_24 = pList[2].dgValue.ToString() + " TL";
                vehicle.chPrice_25 = pList[3].dgValue.ToString() + " TL";


                vehicleList.Add(vehicle);
            }

            ViewBag.vehicleList = vehicleList;

            return vehicleList;

        }
    }
}
