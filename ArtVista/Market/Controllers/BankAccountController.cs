using Azure;
using Market.Models;
using Market.Models.DTO;
using Market.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/bankAccount")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private ResponseDTO _response;
        private IBankAccountService _bankAccountService;
        public BankAccountController(IBankAccountService bankAccountService)
        {
            this._response = new ResponseDTO();
            _bankAccountService = bankAccountService;
        }

        [HttpGet("GetBankAccount")]
        public ResponseDTO GetBankAccount(string userId)
        {
            try
            {
                _response.Result = _bankAccountService.GetBankAccount(userId);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response; 
        }

        [HttpPost("AddBankAccount")]
        public ResponseDTO AddBankAccount(string userId, BankAccountDTO bankAccountDTO, string code)
        {
            try
            {
                _response.Result = _bankAccountService.AddBankAccount(userId, bankAccountDTO, code);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("GenerateCode")]
        public async Task<ResponseDTO> GenerateCode(string userEmail)
        {
            try
            {
                _response.Result = await _bankAccountService.GenerateVerifyCode(userEmail);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetAllBankAccount")]
        public ResponseDTO GetAllBankAccount(string userId, string accountType)
        {
            try
            {
                _response.Result = _bankAccountService.GetAllBankAccount(userId, accountType);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("RemoveBankAccount")]
        public ResponseDTO RemoveBankAccount(string userId)
        {
            try
            {
                _response.Result = _bankAccountService.RemoveBankAccount(userId);       
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdateBankAccount")]
        public ResponseDTO UpdateBankAccount(BankAccountDTO bankAccountDTO)
        {
            try
            {
                _response.Result = _bankAccountService.UpdateBankAccount(bankAccountDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("WithdrawMoney")]
        public ResponseDTO WithdrawMoney(string userId, decimal ammount)
        {
            try
            {
                _response.Result = _bankAccountService.WithdrawMoney(userId, ammount);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
