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
    
    public partial class UserType
    {
        public UserType()
        {
            this.User = new HashSet<User>();
        }
    
        public int sqUserTypeId { get; set; }
        public string chValue { get; set; }
        public string chDesc { get; set; }
        public Nullable<bool> blStatus { get; set; }
        public string cdCreator { get; set; }
        public Nullable<System.DateTime> dtCreated { get; set; }
    
        public virtual ICollection<User> User { get; set; }
    }
}
