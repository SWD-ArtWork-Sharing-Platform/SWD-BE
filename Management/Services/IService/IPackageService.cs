using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPackageService
    {
        Task<ResponseDTO> GetAllPackage();
        Task<ResponseDTO> GetPackageByID(string id);
        Task<ResponseDTO> CreatePackage(PackageDTO packageDTO);
        Task<ResponseDTO> UpdatePackage(PackageDTO packageDTO);
        Task<ResponseDTO> DeletePackage(string id, bool confirm);
        Task<ResponseDTO> GetPackageByCondtion(string name, decimal price, decimal discount);
    }
}
