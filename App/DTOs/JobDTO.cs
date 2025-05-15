using System.ComponentModel.DataAnnotations;

namespace App.DTOs
{
    public class JobDTO
    {
        [Required]
        public string title {get; set;}
        [Required]
        public string link {get; set;}
        [Required]
        public string enterpriseName {get; set;}
        [Required]
        public Guid status {get; set;}
        [Required]
        public Guid modality {get; set;}
        public Guid userId {get; set;}
    }
}