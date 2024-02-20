using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class ArtworkRepository : Repository<FArtwork>, IArtworkRepository
    {
        public ArtworkRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
