namespace Market.Models.DTO
{
    public class BankAccountDTO
    {
        public int AccountId { get; set; }

        public string UserId { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public string AccountType { get; set; } = null!;

        public decimal Balance { get; set; }
    }
}
