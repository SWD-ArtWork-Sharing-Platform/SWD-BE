namespace Market.Models.DTO
{
    public class PackageDTO
    {
        public string PackageId { get; set; } = null!;

        public string? PackageName { get; set; }

        public int? MaximumArtworks { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }
        public string? PackageTime { get; set; }

    }
}
