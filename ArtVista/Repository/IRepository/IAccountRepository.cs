

using ArtVistaAuthAPI.Models;
using ArtVistaAuthAPI.Repository.IRepository;

namespace ArtVistaAuthAPI.Repository.IRepository
{
    public interface IAccountRepository : IRepository<ApplicationUser>
    {
    }
}
