using System.ComponentModel.DataAnnotations;

namespace App.DTOs
{
    public class JobDTO
    {
        [Required]
        public string Title {get; set;}
        [Required]
        public string Link {get; set;}
        [Required]
        public string EnterpriseName {get; set;}
        [Required]
        public Guid Status {get; set;}
        [Required]
        public string Modality {get; set;}
        [Required]
        public Guid UserId {get; set;}
    }
}