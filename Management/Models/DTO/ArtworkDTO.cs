using System.Drawing;

namespace Management.Models.DTO
{
    public class ArtworkDTO
    {
        public string? ArtworkId { get; set; } 

        public string? ArtworkName { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public string? Status { get; set; }

        public string? Id { get; set; } 

        public string? CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public IEnumerable<PostDTO>? PostDTOs { get; set; }
        public IEnumerable<ReportDTO>? ReportDTOs { get; set; }
    }
}
