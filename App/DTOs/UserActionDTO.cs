using System.ComponentModel.DataAnnotations;

namespace App.DTOs
{
    public class UserActionDTO
    {
        [Required]
        public Guid JobId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}