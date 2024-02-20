using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class PackageOfCreatorRepository : Repository<DPackageOfCreator>, IPackageOfCreatorRepository
    {
        public PackageOfCreatorRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
