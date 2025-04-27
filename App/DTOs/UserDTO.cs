using System.ComponentModel.DataAnnotations;

namespace App.DTOs
{
    public class UserDTO
    {
        [Required]
        [MaxLength(20)]
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
    }

}