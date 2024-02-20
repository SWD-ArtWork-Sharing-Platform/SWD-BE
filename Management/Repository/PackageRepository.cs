using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class PackageRepository : Repository<FPackage>, IPackageRepository
    {
        public PackageRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
