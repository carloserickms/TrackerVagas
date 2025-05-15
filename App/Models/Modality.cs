
using System.Text.Json.Serialization;

namespace App.Models
{
    public class Modality : ModelBase
    {

        public string Name {get; set;}
        [JsonIgnore]
        public ICollection<JobVacancy> JobVacancy {get; set;}


        public Modality() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now) { }

        public Modality(string name, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
        }
    }
}