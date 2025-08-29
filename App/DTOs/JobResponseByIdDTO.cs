namespace App.DTOs
{
    public class JobResponseByIdDTO
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string enterpriseName { get; set; }
        public string? location { get; set; }
        public float? salary { get; set; }
        public float? workload { get; set; }
        public Guid? typeOfContract { get; set; }
        public Guid? interestLevel { get; set; }
        public Guid status { get; set; }
        public Guid modality { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}