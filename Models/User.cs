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

    public partial class User
    {
        public int userId { get; set; }

        [Display(Name = " First name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        public System.DateTime joinDate { get; set; }
        public int roleId { get; set; }
        public int loginId { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number cannot be left blank")]
        //[Phone(ErrorMessage = "Please enter a valid phone number ")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = " Please enter a valid phone number")]
        public string phone { get; set; }

        public string FullName()
        {
            return this.firstName + " " + this.lastName;
        }

        public virtual Password Password { get; set; }
        public virtual Position Position { get; set; }
    }
}
