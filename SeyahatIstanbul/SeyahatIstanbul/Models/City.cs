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
    
    public partial class City
    {
        public City()
        {
            this.County = new HashSet<County>();
        }
    
        public byte sqCityId { get; set; }
        public string chCityName { get; set; }
        public Nullable<bool> blStatus { get; set; }
    
        public virtual ICollection<County> County { get; set; }
    }
}
