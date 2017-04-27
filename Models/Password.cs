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
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Password
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Password()
        {
            this.Users = new HashSet<User>();
        }

        public int loginId { get; set; }

        [Required(ErrorMessage = "Password require")]
        [Display(Name = "Password")]
        public string password1 { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Not a valid email format")]
        [Required(ErrorMessage = "Please provide a valid an email")]
        //[Remote("IsEmailValid", "Account", ErrorMessage = "No user by that email address, please enter a valid email address")]
        public string email { get; set; }
        public string passwordReset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
