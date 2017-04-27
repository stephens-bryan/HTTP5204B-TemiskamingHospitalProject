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
    
    public partial class JobPosting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobPosting()
        {
            this.JobApplicants = new HashSet<JobApplicant>();
        }
    
        public int jobId { get; set; }
        public string jobTitle { get; set; }
        public string jobDesc { get; set; }
        public int employeeId { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
        public string status { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobApplicant> JobApplicants { get; set; }
    }
}
