namespace Market.Models.DTO
{
    public class PackageOFCreatorDTO
    {
        public string PackageId { get; set; } = "";

        public string Id { get; set; } = "";

        public DateTime? ExpiredDate { get; set; }

        public DateTime? GraceDate { get; set; }

        public decimal? Price { get; set; }

        public string? Status { get; set; }
        public int Remain { get; set; }
    }
}
