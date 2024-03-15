namespace Management.Models.DTO
{
    public class UserReportDTO
    {
        public string ExportBy { get; set; } = "";
        public DateTime ExportDate { get; set; } = DateTime.Now;

        public int TotalUser { get; set; } = 0;
        public int userActive { get; set; }= 0;
        public int userInactive { get; set; } = 0;
        public IEnumerable<ApplicationUser> ListUser { get; set; } = Enumerable.Empty<ApplicationUser>();

    }
}
