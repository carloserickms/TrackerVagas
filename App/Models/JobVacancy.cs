using System.Text.Json.Serialization;

namespace App.Models
{
    public class JobVacancy : ModelBase
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string EnterpriseName { get; set; }
        public Guid VacancyStatusId {get; set;}
        public Guid ModalityId {get; set;}
        public Guid UserId {get; set;}

        [JsonIgnore]
        public Modality Modality {get; set;}
        [JsonIgnore]
        public VacancyStatus VacancyStatus {get; set;}
        [JsonIgnore]
        public User User {get; set;}

        public JobVacancy() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now) { }

        public JobVacancy(string title, string link, string enterpriseName, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Title = title;
            Link = link;
            EnterpriseName = enterpriseName;
        }
    }
}