namespace Management.Models.DTO
{
    public class ArtworkDTO
    {
        public string ArtworkId { get; set; } = null!;

        public string? ArtworkName { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public string? Status { get; set; }

        public string Id { get; set; } = null!;

        public string? CategoryId { get; set; }

        public IEnumerable<PostDTO>? PostDTOs { get; set; }
        public IEnumerable<ReportDTO>? ReportDTOs { get; set; }
    }
}
