namespace Market.Models.DTO
{
    public class OrderDTO
    {
        public string OrderId { get; set; } = null!;

        public string Id { get; set; } = null!;

        public DateTime? CreatedOn { get; set; }

        public string? PaymentId { get; set; }

        public string? OrderStatus { get; set; }

        public decimal? Total { get; set; }

        public int? NumberOfDowload { get; set; }

        public DateTime? SoldDate { get; set; }
    }
}
