using Management.Models.DTO;
using Management.Services.IService;

namespace Management.Services
{
    public class ReportService : IReportService
    {
        public Task<ResponseDTO> MonthlyInspection(DateTime SelectedMoth)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> ReportByUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
