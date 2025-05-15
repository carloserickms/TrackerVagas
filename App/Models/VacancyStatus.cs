
using System.Text.Json.Serialization;

namespace App.Models
{
    public class VacancyStatus : ModelBase
    {
        public string Name {get; set;}
        [JsonIgnore]
        public ICollection<JobVacancy> JobVacancy {get; set;}

        
        public VacancyStatus() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now){}
        public VacancyStatus(string name, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
        }
    }
}