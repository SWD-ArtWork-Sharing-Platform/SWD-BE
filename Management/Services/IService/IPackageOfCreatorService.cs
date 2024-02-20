using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPackageOfCreatorService
    {
        ResponseDTO GetAllPackgeOfCreator();
        ResponseDTO GetPackageOfCreatorByID(string id);
        ResponseDTO CreatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO);
        ResponseDTO UpdatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO);
        ResponseDTO DeletePackageOfCreator(string id, bool confirm);
    }
}
