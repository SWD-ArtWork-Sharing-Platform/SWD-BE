using Market.Data;
using Market.Models;
using Market.Repository.IRepository;

namespace Market.Repository
{
    public class OrderRepository : Repository<FOrder>, IOrderRepository
    {
        public OrderRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
