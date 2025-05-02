using System.ComponentModel.DataAnnotations;

namespace App.DTOs
{
    public class UserDTO
    {
        [Required]
        [MaxLength(40)]
        public string userName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(8)]
        public string password { get; set; }
        [Required]
        [MinLength(8)]
        public string rePassword { get; set; }

        public string provider { get; set; }

        public string providerId {get; set;}
    }
}