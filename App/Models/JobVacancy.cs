namespace App.Models
{
    public class JobVacancy : ModelBase
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string EnterpriseName { get; set; }
        public string Modality {get; set;}
        public Guid VacancyStatusId {get; set;}
        public Guid UserId {get; set;}

        public VacancyStatus VacancyStatus {get; set;}
        public User User {get; set;}

        public JobVacancy() : base(Guid.NewGuid(), DateTime.Now, DateTime.Now) { }

        public JobVacancy(string title, string link, string enterpriseName, string modality, Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Title = title;
            Link = link;
            EnterpriseName = enterpriseName;
            Modality = modality;
        }
    }
}