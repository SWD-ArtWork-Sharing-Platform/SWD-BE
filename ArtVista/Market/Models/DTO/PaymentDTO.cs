namespace Market.Models.DTO
{
    public class PaymentDTO
    {
        public string? PaymentId { get; set; }

        public string? PaymentName { get; set; }

        public decimal? Total { get; set; }

        public string? PaymentStatus { get; set; }

        public string? Note { get; set; }

        public OrderDTO Order { get; set; }
    }
}
