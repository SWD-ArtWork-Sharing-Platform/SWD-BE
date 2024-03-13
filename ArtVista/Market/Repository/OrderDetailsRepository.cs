using Market.Data;
using Market.Models;
using Market.Repository.IRepository;

namespace Market.Repository
{
    public class OrderDetailsRepository : Repository<DOrderDetail>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
