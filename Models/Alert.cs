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
    
    public partial class Alert
    {
        public int akertId { get; set; }
        public string alertText { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
        public string status { get; set; }
        public int employeeId { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
