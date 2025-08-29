namespace App.DTOs
{
    public class JobResponseDTO
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string enterpriseName { get; set; }
        public string? location { get; set; }
        public float? salary { get; set; }
        public float? workload { get; set; }
        public string? typeOfContract { get; set; }
        public Guid? typeOfContractId { get; set; }
        public string? interestLevel { get; set; }
        public Guid? interestLevelId { get; set; }
        public string status { get; set; }
        public Guid statusId { get; set; }
        public string modality { get; set; }
        public Guid modalityId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}