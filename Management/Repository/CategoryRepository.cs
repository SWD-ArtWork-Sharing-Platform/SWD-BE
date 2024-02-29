using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class CategoryRepository : Repository<DCategory>, ICategoryRepository
    {
        public CategoryRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
