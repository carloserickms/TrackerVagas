
namespace App.Models
{
    public class VacancyStatus : ModelBase
    {
        public string Name {get; set;}
        public JobVacancy JobVacancy {get; set;}

        
        public VacancyStatus(Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt){}
        public VacancyStatus(string name, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
        }
    }
}