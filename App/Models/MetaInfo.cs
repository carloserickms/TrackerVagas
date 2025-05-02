
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class MetaInfo
    {
        [Key]
        [Required]
        public string ProviderId {get; set;}
        [Required]
        public string Provider {get; set;}
        [Required]
        public Guid UserId {get; set;}
        [Required]
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        [Required]
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public User User {get; set;}

        public MetaInfo(string providerId, string provider)
        {
            ProviderId = providerId;
            Provider = provider;
        }
    }
}