using Market.Data;
using Market.Models;
using Market.Repository.IRepository;

namespace Market.Repository
{
    public class BankAccountRepository : Repository<DBankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
