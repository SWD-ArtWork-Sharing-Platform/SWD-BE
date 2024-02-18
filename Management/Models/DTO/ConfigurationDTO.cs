namespace Management.Models.DTO
{
    public class ConfigurationDTO
    {
        public string ConfigurationId { get; set; } = null!;

        public decimal? CommisionFee { get; set; }

        public DateTime? AppliedDate { get; set; }

        public string? Status { get; set; }

        public string? Id { get; set; }
    }
}
