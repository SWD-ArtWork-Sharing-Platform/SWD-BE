using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IPackageOfCreatorService
    {
        Task<ResponseDTO> GetAllPackgeOfCreator();
        Task<ResponseDTO> GetPackageOfCreatorByID(string id);
        Task<ResponseDTO> CreatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO);
        Task<ResponseDTO> UpdatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO);
        Task<ResponseDTO> DeletePackageOfCreator(string id, bool comfirm);
    }
}
