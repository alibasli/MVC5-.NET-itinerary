using SeyahatIstanbul.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeyahatIstanbul.Models
{
    public class HomePageModel
    {

        //label
        [Display(ResourceType = typeof(Res), Name = "lblSelectTour")]
        public string lblSelectTour { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblTourDate")]
        public string lblTourDate { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblSelectApart")]
        public string lblSelectApart { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblSave")]
        public string lblSave { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblSelectVehicle")]
        public string lblSelectVehicle { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblSlctVehPlace1")]
        public string lblSlctVehPlace1 { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblSlctVehPlace2")]
        public string lblSlctVehPlace2 { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblVehDate1")]
        public string lblVehDate1 { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblVehDate2")]
        public string lblVehDate2 { get; set; }
        [Display(ResourceType = typeof(Res), Name = "lblSelectMoney")]
        public string lblSelectMoney { get; set; }


        //menu
        [Display(ResourceType = typeof(Res), Name = "menuHome")]
        public string menuHome { get; set; }
        [Display(ResourceType = typeof(Res), Name = "menuVehicleList")]
        public string menuVehicleList { get; set; }
        [Display(ResourceType = typeof(Res), Name = "menuApartList")]
        public string menuApartList { get; set; }
        [Display(ResourceType = typeof(Res), Name = "menuTourlist")]
        public string menuTourlist { get; set; }
        [Display(ResourceType = typeof(Res), Name = "menuAbout")]
        public string menuAbout { get; set; }
        [Display(ResourceType = typeof(Res), Name = "menuCommunication")]
        public string menuCommunication { get; set; }
        public string ddLng { get; set; }



        //fastBooking

        //araç
        public string rfVehCategoryId { get; set; }
        public string rfVehicleId { get; set; }
        public string rfVehDeliveryPlace_1 { get; set; }
        public string dtVehStartDate { get; set; }
        public string Vehcloock_1 { get; set; }
        public string rfVehDeliveryPlace_2 { get; set; }
        public string dtVehEndDate { get; set; }
        public string Vehcloock_2 { get; set; }


        //apart
        public string rfApartCategoryId { get; set; }
        public string rfApartId { get; set; }
        public string dtApartStartDate { get; set; }
        public string apartCloock_1 { get; set; }
        public string dtApartEndDate { get; set; }
        public string apartCloock_2 { get; set; }
        public string dgRoomCount { get; set; }


        //tur
        public string rfTourCategoryId { get; set; }
        public string rfTourId { get; set; }
        public string dtTourDate { get; set; }
        public string tourCloock_1 { get; set; }
    }
}