namespace Market.Models.DTO
{
    public class OrderDetailDTO
    {
        public string OrderDetailId { get; set; } = null!;

        public string? OrderId { get; set; }

        public ArtWorkDTO ArtworkId { get; set; }

        public decimal? Price { get; set; }

        public bool? Dowloaded { get; set; } 
    }
}
