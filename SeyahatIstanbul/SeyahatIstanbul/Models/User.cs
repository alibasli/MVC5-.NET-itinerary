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
    
    public partial class User
    {
        public User()
        {
            this.RentVehicle = new HashSet<RentVehicle>();
        }
    
        public int sqUserId { get; set; }
        public string userName { get; set; }
        public string chName { get; set; }
        public string chSurname { get; set; }
        public System.DateTime dtBirthDate { get; set; }
        public int rfUserTypeId { get; set; }
        public string chEmail { get; set; }
        public string chPassword { get; set; }
        public bool blPasswordExchange { get; set; }
        public string chDesc { get; set; }
        public bool blStatus { get; set; }
        public string cdCreator { get; set; }
        public Nullable<System.DateTime> dtCreated { get; set; }
    
        public virtual ICollection<RentVehicle> RentVehicle { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
