namespace App.DTOs
{
    public class JobResponseDTO
    {
        public string title { get; set; }
        public string link { get; set; }
        public string enterpriseName { get; set; }
        public string status { get; set; }
        public string modality { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}