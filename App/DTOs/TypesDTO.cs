using System.ComponentModel.DataAnnotations;

namespace App.DTOs
{
    public class TypesDTO
    {
        [Required]
        public string name {get; set;}
    }
}