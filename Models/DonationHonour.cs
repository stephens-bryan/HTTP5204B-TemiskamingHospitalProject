//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace $safeprojectname$.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonationHonour
    {
        public int donationHonourId { get; set; }
        public Nullable<int> donorsId { get; set; }
        public Nullable<int> donationsId { get; set; }
        public string honFirstName { get; set; }
        public string honLastName { get; set; }
        public string honType { get; set; }
    
        public virtual Donation Donation { get; set; }
        public virtual Donor Donor { get; set; }
    }
}
