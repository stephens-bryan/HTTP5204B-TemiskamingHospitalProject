﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace $safeprojectname$.Models
{
    public class PasswordReset
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm new password")]
        [Display(Name = "Confirm New Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords must match" )]
        public string ComparePassword { get; set; }
    }
}