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
            try
            {
                FPackage package = _mapper.Map<FPackage>(packageDTO);
                _packageRepository.Add(package);
                _packageRepository.Save();
                _response.Result = _mapper.Map<PackageDTO>(package);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;   
        }

        public ResponseDTO DeletePackage(string id, bool confirm)
        {
            try
            {
                if (confirm == true)
                {
                    FPackage package = _packageRepository.Get(u => u.PackageId == id);
                    if (package == null)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Package not found!";
                    }
                    else
                    {
                        _packageRepository.Remove(package);
                        _packageRepository.Save();
                    }
                }
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
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
            try
            {
                IEnumerable<FPackage> packageList = _packageRepository.GetAll();
                IEnumerable<PackageDTO> packageDTOs = new List<PackageDTO>();

                if (!string.IsNullOrEmpty(name))
                {
                    packageList = packageList.Where(u => u.PackageName == name);
                }
                if (price > 0)
                {
                    packageList = packageList.Where(u => u.Price == price); 
                }
                if (discount > 0)
                {
                    packageList = packageList.Where(u => u.Discount == discount);
                }
                if (packageList != null)
                {
                    packageDTOs = _mapper.Map<IEnumerable<PackageDTO>>(packageList);
                }
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetPackageByID(string id)
        {
            try
            {
                FPackage package = _packageRepository.Get(u => u.PackageId == id);
                if (package == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Package not found!";
                } else
                {
                    _response.Result = _mapper.Map<PackageDTO>(package);
                }
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO UpdatePackage(PackageDTO packageDTO)
        {
            try
            {
                FPackage package = _mapper.Map<FPackage>(packageDTO);
                FPackage oldPackage = _packageRepository.Get(u => u.PackageId == packageDTO.PackageId);
                if (oldPackage == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Package not found!";
                }
                else
                {
                    _packageRepository.Update(oldPackage);
                    _packageRepository.Save();
                    _response.Message = "Package updated successfully!";
                }
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
