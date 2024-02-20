using AutoMapper;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;

namespace Management.Services
{
    public class PackageService : IPackageService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private IPackageRepository _packageRepository;
        public PackageService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, IPackageRepository packageRepository)
        {
            this._response = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _packageRepository = packageRepository; 
        }
        public ResponseDTO CreatePackage(PackageDTO packageDTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO DeletePackage(string id, bool confirm)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetAllPackage()
        {
            try
            {
                IEnumerable<FPackage> packageList = _packageRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<FPackage>>(packageList);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;   
        }

        public ResponseDTO GetPackageByCondtion(string name, decimal price, decimal discount)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetPackageByID(string id)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO UpdatePackage(PackageDTO packageDTO)
        {
            throw new NotImplementedException();
        }
    }
}
