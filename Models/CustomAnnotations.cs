using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // inherit ValidationAttribute library
using System.Text.RegularExpressions;

namespace $safeprojectname$.Models
{
    /*
     * Note(bryanstephens): Phone validation needs to check more than just empty; also needs to check that input value is in a valid phone number format. That format can be global (anoy phone number in the world), since the user could have a phone number from another country.
     */
    public class PhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //return base.IsValid(value);
            // regex from : https://regex101.com/r/yE2iH5/1/codegen?language=csharp
            string phonePattern = @"(?:(\+?\d{1,3}) )?(?:([\(]?\d+[\)]?)[ -])?(\d{1,5}[\- ]?\d{1,5})";

            var phone = value.ToString();

            if (Regex.Match(phone, phonePattern) != null)
            {
                return phone == null;
            }
            else
            {
                return phone != null;
            }
        }
    }
}