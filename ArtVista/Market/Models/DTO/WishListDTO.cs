namespace Market.Models.DTO
{
    public class WishListDTO
    {
        public string WishlistId { get; set; } = null!;

        public string Id { get; set; } = null!;

        public decimal? Total { get; set; }

        public string? Note { get; set; }
    }
}
