using System.ComponentModel.DataAnnotations;

namespace SoftMarketing.WebAPI.Security
{
    public class AuthenticateRequest
    {
        public int UserId { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string password{ get; set; }

        [Required]
        public string AppSourceId{ get; set; }
    }
}
