//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeyahatIstanbul.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentVehicle
    {
        public short sqVehicleId { get; set; }
        public Nullable<byte> rfVehicleTypeId { get; set; }
        public string chPlate { get; set; }
        public string chVinNo { get; set; }
        public string chColor { get; set; }
        public Nullable<byte> rfFuelTypeId { get; set; }
        public Nullable<short> rfModelId { get; set; }
        public Nullable<byte> rfGearTypeId { get; set; }
        public Nullable<bool> blDisplayHomePage { get; set; }
        public Nullable<byte> qtOrderDisplay { get; set; }
        public Nullable<int> rfVehicleOwner { get; set; }
        public Nullable<bool> blDisplaySlider { get; set; }
        public string chImageName { get; set; }
        public string chValue { get; set; }
        public string chDesc { get; set; }
        public Nullable<bool> blStatus { get; set; }
        public string cdCreator { get; set; }
        public Nullable<System.DateTime> dtCreated { get; set; }
        public string chModelYear { get; set; }
        public Nullable<byte> qtCapacity { get; set; }
    
        public virtual FuelType FuelType { get; set; }
        public virtual GearType GearType { get; set; }
        public virtual Model Model { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual User User { get; set; }
    }
}