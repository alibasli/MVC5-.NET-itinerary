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
    
    public partial class Tour
    {
        public short sqTourId { get; set; }
        public Nullable<byte> rfTourTypeId { get; set; }
        public string chPlaces { get; set; }
        public string chProperties { get; set; }
        public string chDesc { get; set; }
        public Nullable<bool> blStatus { get; set; }
        public string cdCreator { get; set; }
        public Nullable<System.DateTime> dtCreated { get; set; }
        public Nullable<bool> blDisplaySlider { get; set; }
        public string chImageName { get; set; }
        public string chStartPlace { get; set; }
        public string chEndPlace { get; set; }
        public Nullable<int> chPersonCount { get; set; }
    
        public virtual TourType TourType { get; set; }
    }
}