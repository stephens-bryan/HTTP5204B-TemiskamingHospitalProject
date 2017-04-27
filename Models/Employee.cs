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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Alerts = new HashSet<Alert>();
            this.EmployeeSchedules = new HashSet<EmployeeSchedule>();
            this.JobPostings = new HashSet<JobPosting>();
            this.Navigations = new HashSet<Navigation>();
            this.OnCalls = new HashSet<OnCall>();
            this.Pages = new HashSet<Page>();
            this.Requests = new HashSet<Request>();
            this.SubNavigations = new HashSet<SubNavigation>();
            this.Tickets = new HashSet<Ticket>();
        }
    
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string position { get; set; }
        public int departmentId { get; set; }
        public Nullable<int> contactId { get; set; }
        public Nullable<int> addressId { get; set; }
        public Nullable<int> emerageContactID { get; set; }
        public System.DateTime birthDate { get; set; }
        public string sinNum { get; set; }
        public string schedule { get; set; }
        public string admin { get; set; }
        public Nullable<int> employeeStatusId { get; set; }
        public Nullable<int> insuranceId { get; set; }
    
        public virtual Addresses1 Addresses1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual EmerageContact EmerageContact { get; set; }
        public virtual EmployeeStatu EmployeeStatu { get; set; }
        public virtual Insurance Insurance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobPosting> JobPostings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Navigation> Navigations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnCall> OnCalls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page> Pages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubNavigation> SubNavigations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
