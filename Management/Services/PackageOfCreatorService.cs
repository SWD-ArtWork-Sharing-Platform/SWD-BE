using AutoMapper;
using Azure;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;

namespace Management.Services
{
    public class PackageOfCreatorService : IPackageOfCreatorService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private IPackageOfCreatorRepository _packageOfCreatorRepository;
        public PackageOfCreatorService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, IPackageOfCreatorRepository packageOfCreatorRepository)
        {
            this._response = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _packageOfCreatorRepository = packageOfCreatorRepository;
        }
        public ResponseDTO CreatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO)
        {
            try
            {
                DPackageOfCreator packageOfCreator = _mapper.Map<DPackageOfCreator>(packageOfCreatorDTO);

                _packageOfCreatorRepository.Add(packageOfCreator);
                _packageOfCreatorRepository.Save();
                _response.Result = _mapper.Map<PackageOfCreatorDTO>(packageOfCreator);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO DeletePackageOfCreator(string id, bool confirm)
        {
            try
            {
                if (confirm == true)
                {
                    DPackageOfCreator packageOfCreator = _packageOfCreatorRepository.Get(u => u.PackageId == id);
                    if (packageOfCreator == null)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Package not found!";
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetAllPackgeOfCreator()
        {
            try
            {
                IEnumerable<DPackageOfCreator> packageOfCreators = _packageOfCreatorRepository.GetAll();
                _response.Result = _mapper.Map<DPackageOfCreator>(packageOfCreators);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetPackageOfCreatorByID(string id)
        {
            try
            {
                DPackageOfCreator packageOfCreator = _packageOfCreatorRepository.Get(u => u.PackageId == id);
                _response.Result = _mapper.Map<PackageOfCreatorDTO>(packageOfCreator);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO UpdatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO)
        {
            try
            {
                DPackageOfCreator packageOfCreator = _mapper.Map<DPackageOfCreator>(packageOfCreatorDTO);
                DPackageOfCreator oldPackageOfCreator = _packageOfCreatorRepository.Get(u => u.PackageId == packageOfCreatorDTO.Id);
                if (oldPackageOfCreator == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Package not found!";
                }
                else
                {
                    oldPackageOfCreator = packageOfCreator;
                    _packageOfCreatorRepository.Update(oldPackageOfCreator);
                    _packageOfCreatorRepository.Save();
                    _response.Message = "Update successfully!";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
