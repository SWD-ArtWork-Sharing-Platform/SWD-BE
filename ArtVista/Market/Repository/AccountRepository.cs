using Microsoft.Identity.Client;
using Market.Repository.IRepository;
using Market.Data;
using Market.Models;
using Market.Repository;

namespace Market.Repository
{
    public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
    {
        public AccountRepository(ArtworkSharingPlatformContext db) : base(db)
        {

        }
    }
}
