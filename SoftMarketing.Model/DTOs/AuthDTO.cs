using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model.DTOs
{
    public class AuthResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class AuthRequestDTO
    {
        [Required(ErrorMessage = "country is required")]
        public int countryId { get; set; }

        [Required(ErrorMessage = "phone number is required")]
        public string phone { get; set; }

        [Required(ErrorMessage = "otp number is required")]
        public string otp { get; set; }
    }
}
