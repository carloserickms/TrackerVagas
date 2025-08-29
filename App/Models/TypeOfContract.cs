using System.Text.Json.Serialization;

namespace App.Models
{
    public class TypeOfContract : ModelBase
    {
        public string Name { get; set; }
        
        [JsonIgnore]
        public ICollection<JobVacancy> JobVacancy { get; set; }

        public TypeOfContract() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now) { }

        public TypeOfContract(string name, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
        }
    }
}