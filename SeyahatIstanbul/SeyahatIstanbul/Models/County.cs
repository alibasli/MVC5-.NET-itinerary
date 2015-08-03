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
    
    public partial class County
    {
        public County()
        {
            this.AccountAddress = new HashSet<AccountAddress>();
            this.CustomerAddress = new HashSet<CustomerAddress>();
            this.RentApart = new HashSet<RentApart>();
            this.Address = new HashSet<Address>();
        }
    
        public short sqCountyId { get; set; }
        public Nullable<byte> rfCityId { get; set; }
        public string chCountyName { get; set; }
        public Nullable<bool> blStatus { get; set; }
    
        public virtual ICollection<AccountAddress> AccountAddress { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<RentApart> RentApart { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
