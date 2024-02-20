using AutoMapper;
using Market.Data;
using Market.Models;
using Market.Models.DTO;
using Market.Services.IServices;
using Market.Utils;

namespace Market.Services
{
    public class PackageServices : IPackageServices
    {
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        public PackageServices(ArtworkSharingPlatformContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PackageDTO>> GetAllAvailablePackage
            (string? name, int? max, decimal? minprice, decimal? discount)
        {
            IEnumerable<FPackage>? packageList = _db.FPackages.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                packageList = packageList.Where(u => u.PackageName.Contains(name));
            }
            if (max>0)
            {
                packageList = packageList.Where( u=> u.Price <= max);
            }
            if (minprice > 0)
            {
                packageList = packageList.Where(u => u.Price >= minprice);
            }
            if(discount > 0)
            {
                packageList = packageList.Where(u => u.Discount >= discount);
            }

            if(packageList!= null)
            {
                IEnumerable<PackageDTO>? ObjList = _mapper.Map<IEnumerable<PackageDTO>>(packageList);   
                return ObjList;

            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<PackageOFCreatorDTO>> GetAllPurchasePackagebyUserID(string userID)
        {
            IEnumerable<DPackageOfCreator>? Datalist = _db.DPackageOfCreators.Where( u =>u.Id ==userID)
                    .OrderBy( u => u.GraceDate);  
            if(Datalist != null)
            {
                IEnumerable<PackageOFCreatorDTO> returnObj= _mapper.Map<IEnumerable<PackageOFCreatorDTO>>(Datalist);
                return returnObj;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<PackageDTO>> BuyPackage(string userID, PackageOFCreatorDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PackageDTO>> ReturnPackage(string userID, PackageOFCreatorDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PackageDTO>> AdminUpdatePackage(PackageDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PackageDTO>> AdminDeletePackage(string packageID)
        {
            throw new NotImplementedException();
        }
 


    }
}
