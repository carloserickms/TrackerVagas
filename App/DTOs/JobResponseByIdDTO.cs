namespace App.DTOs
{
    public class JobResponseByIdDTO
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string enterpriseName { get; set; }
        public Guid status { get; set; }
        public Guid modality { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}