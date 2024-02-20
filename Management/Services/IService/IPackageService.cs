using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPackageService
    {
        ResponseDTO GetAllPackage();
        ResponseDTO GetPackageByID(string id);
        ResponseDTO CreatePackage(PackageDTO packageDTO);
        ResponseDTO UpdatePackage(PackageDTO packageDTO);
        ResponseDTO DeletePackage(string id, bool confirm);
        ResponseDTO GetPackageByCondtion(string name, decimal price, decimal discount);
    }
}
