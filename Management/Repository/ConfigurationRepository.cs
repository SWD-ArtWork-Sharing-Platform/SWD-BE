using Management.Data;
using Management.Models;
using Management.Repository.IRepository;

namespace Management.Repository
{
    public class ConfigurationRepository : Repository<FConfiguration>, IConfigurationRepository
    {
        public ConfigurationRepository(ArtworkSharingPlatformContext db) : base(db)
        {
        }
    }
}
