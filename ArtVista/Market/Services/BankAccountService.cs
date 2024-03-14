using AutoMapper;
using Market.Data;
using Market.Helper;
using Market.Models;
using Market.Models.DTO;
using Market.Repository.IRepository;
using Market.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Market.Services
{
    public class BankAccountService : IBankAccountService
    {
        private IMapper _mapper;
        private ArtworkSharingPlatformContext _db;
        private IBankAccountRepository _bankAccountRepository;
        private UserManager<ApplicationUser> _userManager;  
        public BankAccountService(IMapper mapper, ArtworkSharingPlatformContext db, IBankAccountRepository bankAccountRepository, UserManager<ApplicationUser> userManager)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;   
            _db = db;   
        }
        public async Task<bool> AddBankAccount(string userId, BankAccountDTO model, string code)
        {
            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                string confirmCode = await GenerateVerifyCode(user.Email);
                if (code == confirmCode)
                {
                    DBankAccount bankAccount = _mapper.Map<DBankAccount>(model);
                    if (bankAccount != null)
                    {
                        bankAccount.Confirmed = true;
                        _bankAccountRepository.Add(bankAccount);
                        _bankAccountRepository.Save();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        public async Task<string> GenerateVerifyCode(string email)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                string code = Guid.NewGuid().ToString("N").Substring(0, 6);
                string sendMail = SendMail.SendEmail(user.Email, "Confirm your bank account",
                                       "Your code to confirm bank account: " +
                                       code , "");
                return code;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<IEnumerable<BankAccountDTO>> GetAllBankAccount(string? userId, string? accountType)
        {
            IEnumerable<DBankAccount> dataList = _bankAccountRepository.GetAll();
            IEnumerable<BankAccountDTO> bankAccountDTOs = new List<BankAccountDTO>();
            if (!string.IsNullOrEmpty(userId))
            {
                dataList = dataList.Where(u => u.UserId == userId); 
            }
            if (!string.IsNullOrEmpty(accountType))
            {
                dataList = dataList.Where(u => u.AccountType == accountType);
            }
            if (dataList != null)
            {
                bankAccountDTOs = _mapper.Map<IEnumerable<BankAccountDTO>>(dataList);
            }
            return bankAccountDTOs;
        }

        public async Task<BankAccountDTO> GetBankAccount(string userId)
        {
            DBankAccount bankAccount = _bankAccountRepository.Get(u => u.UserId == userId);
            if (bankAccount != null)
            {
                BankAccountDTO bankAccountDTO = _mapper.Map<BankAccountDTO>(bankAccount);
                return bankAccountDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveBankAccount(string userId)
        {
            DBankAccount bankAccount = _bankAccountRepository.Get(u => u.UserId == userId);
            if (bankAccount != null)
            {
                _bankAccountRepository.Remove(bankAccount);
                _bankAccountRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateBankAccount(BankAccountDTO bankAccountDTO)
        {
            DBankAccount bankAccount = _bankAccountRepository.Get(u => u.UserId == bankAccountDTO.UserId);
            DBankAccount updateObj = _mapper.Map<DBankAccount>(bankAccountDTO);
            if (bankAccount != null && updateObj != null)
            {
                bankAccount = updateObj;    
                _bankAccountRepository.Update(bankAccount); 
                _bankAccountRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> WithdrawMoney(string userId,decimal ammount)
        {
            var user = _bankAccountRepository.Get(u => u.UserId == userId);
            if (user != null)
            {
               user.Balance -= ammount;
                _bankAccountRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
