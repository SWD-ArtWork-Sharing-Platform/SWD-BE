using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class ReportRepository : Repository<FReport>, IReportRepository
    {
        public ReportRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
