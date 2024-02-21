using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class PostRepository : Repository<FPost>, IPostRepository
    {
        public PostRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
