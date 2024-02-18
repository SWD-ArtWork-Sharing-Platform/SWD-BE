

using Auth.Models;
using Auth.Repository.IRepository;

namespace Auth.Repository.IRepository
{
    public interface IAccountRepository : IRepository<ApplicationUser>
    {
    }
}
