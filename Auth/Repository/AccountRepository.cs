using Microsoft.Identity.Client;
using Auth.Repository.IRepository;
using Auth.Data;
using Auth.Models;
using Auth.Repository;

namespace Auth.Repository
{
    public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
    {
        public AccountRepository(AppDbContext db) : base(db)
        {
        }
    }
}
