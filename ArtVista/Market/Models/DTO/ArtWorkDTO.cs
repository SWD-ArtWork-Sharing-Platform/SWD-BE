namespace Market.Models.DTO
{
    public class ArtWorkDTO
    {
        public string ArtworkId { get; set; } = null!;

        public string? ArtworkName { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public string? Status { get; set; }

        public ApplicationUser Creator { get; set; } = null!;

        public string CategoryID { get; set; }


        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}
