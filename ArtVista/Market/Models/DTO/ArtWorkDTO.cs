namespace Market.Models.DTO
{
    public class ArtWorkDTO
    {
        public string ArtworkId { get; set; } = null!;

        public string? ArtworkName { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public string? Status { get; set; }

        public string Id { get; set; } = null!;

        public string CategoryID { get; set; }
        public IFormFile? Image { get; set; }
    }
}
