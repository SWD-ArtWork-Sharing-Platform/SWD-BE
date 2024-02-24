using Azure;
using Management.Models.DTO;
using Management.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private ResponseDTO _response;
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            this._response = new ResponseDTO();  
            _reportService = reportService;    
        }

        [Authorize(Policy = "ORGANIZATION")]
        public ResponseDTO MonthlyInspection(DateTime SelectedMoth)
        {
            try
            {
                _response.Result = _reportService.MonthlyInspection(SelectedMoth);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO ReportByUser(string id)
        {
            try
            {
                _response.Result = _reportService.ReportByUser(id); 
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
