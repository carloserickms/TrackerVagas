using System.Text.Json.Serialization;

namespace App.Models
{
    public class InterestLevel : ModelBase
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<JobVacancy> JobVacancy { get; set; }

        public InterestLevel() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now) { }

        public InterestLevel(string name, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
        }
    }
}