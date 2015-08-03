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
    
    public partial class Process
    {
        public int sqProcessId { get; set; }
        public Nullable<short> rfCategoryId { get; set; }
        public Nullable<int> rfProductId { get; set; }
        public Nullable<int> rfCustomerId { get; set; }
        public Nullable<int> dgPrice { get; set; }
        public Nullable<System.DateTime> dtStartDate { get; set; }
        public Nullable<System.DateTime> dtEndDate { get; set; }
        public Nullable<int> rfDeliveryPlace_1 { get; set; }
        public Nullable<int> rfDeliveryPlace_2 { get; set; }
        public Nullable<int> rfStatusId { get; set; }
        public string chDesc { get; set; }
        public Nullable<bool> blStatus { get; set; }
        public string cdCreator { get; set; }
        public Nullable<System.DateTime> dtCreated { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProcessStatus ProcessStatus { get; set; }
    }
}
