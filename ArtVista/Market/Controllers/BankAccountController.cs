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
        private ResponseDTO responseDTO;
        private IBankAccountService _bankAccountService;
        public BankAccountController(IBankAccountService  bankAccountService)
        {
            this.responseDTO = new ResponseDTO();   
            _bankAccountService = bankAccountService;       
        }


    }
}
