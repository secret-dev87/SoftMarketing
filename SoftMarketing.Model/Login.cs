using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Login
    {
        [Required(ErrorMessage = "country is required")]
        public string CountryCode { get; set; }


        [Required(ErrorMessage = "phone number is required")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "otp number is required")]
        public string OTPCode { get; set; }


        [Required(ErrorMessage = "language is required")]
        public string LanguageId { get; set; }

    }
}
