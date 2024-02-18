using Microsoft.Identity.Client;
using ArtVistaAuthAPI.Repository.IRepository;
using ArtVistaAuthAPI.Data;
using ArtVistaAuthAPI.Models;
using ArtVistaAuthAPI.Repository;

namespace ArtVistaAuthAPI.Repository
{
    public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
    {
        public AccountRepository(AppDbContext db) : base(db)
        {
        }
    }
}
