using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public abstract class ModelBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        [Required]
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        

        public ModelBase(Guid id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}