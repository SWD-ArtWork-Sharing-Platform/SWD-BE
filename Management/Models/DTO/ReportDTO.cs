namespace Management.Models.DTO
{
    public class ReportDTO
    {
        public string ReportId { get; set; } = null!;

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public string? ArtworkId { get; set; }

        public string? Status { get; set; }

        public string? Detail { get; set; }

        public string? Id { get; set; }

        public string? ActionNote { get; set; }
        public ReportDTO? Report { get; set; }
    }
}
