using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IReportService
    {
        ResponseDTO MonthlyInspection(DateTime SelectedMoth);
        ResponseDTO ReportByUser(string id);
        ResponseDTO CreateReport(ReportDTO obj);
        ResponseDTO UpdateReport(ReportDTO obj);
        ResponseDTO RemoveREport(string id);


    }
}
