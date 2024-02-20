using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class InteractionRepository : Repository<DInteraction>, IInteractionRepository
    {
        public InteractionRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
