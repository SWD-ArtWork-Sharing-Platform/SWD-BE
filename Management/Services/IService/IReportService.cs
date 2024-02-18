using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IReportService
    {
        Task<ResponseDTO> MonthlyInspection(DateTime SelectedMoth);
        Task<ResponseDTO> ReportByUser(string id);
    }
}
