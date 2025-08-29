using System.Text.Json.Serialization;

namespace App.Models
{
    public class JobVacancy : ModelBase
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string EnterpriseName { get; set; }
        public string? Location { get; set; }
        public float? Salary { get; set; }
        public float? Workload { get; set; }
        public Guid? TypeOfContractId { get; set; }
        public Guid? InterestLevelId { get; set; }
        public Guid VacancyStatusId { get; set; }
        public Guid ModalityId {get; set;}
        public Guid UserId {get; set;}

        public Modality Modality {get; set;}
        public VacancyStatus VacancyStatus {get; set;}
        public InterestLevel InterestLevel { get; set; }
        public TypeOfContract TypeOfContract { get; set; }

        [JsonIgnore]
        public User User {get; set;}

        public JobVacancy() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now) { }

        public JobVacancy(string title, string link, string enterpriseName, string location, float salary, float workload,
        Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Title = title;
            Link = link;
            EnterpriseName = enterpriseName;
            Location = location;
            Salary = salary;
            Workload = workload;
        }
    }
}