using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountDTO>> GetAllBankAccount(string? userId, string? accountType);
        Task<bool> AddBankAccount(string userId, BankAccountDTO bankAccountDTO, string code);
        Task<bool> RemoveBankAccount(string userId);
        Task<bool> UpdateBankAccount(BankAccountDTO bankAccountDTO);
        Task<BankAccountDTO> GetBankAccount(string userId);
        Task<bool> WithdrawMoney(string userId,decimal ammount);
        Task<string> GenerateVerifyCode(string email);
    }
}
