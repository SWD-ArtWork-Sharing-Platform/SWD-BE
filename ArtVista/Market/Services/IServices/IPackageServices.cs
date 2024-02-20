using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IPackageServices
    {
        Task<IEnumerable<PackageDTO>> GetAllAvailablePackage(string? name, int? max, decimal? price, decimal? discount);
        Task<IEnumerable<PackageOFCreatorDTO>> GetAllPurchasePackagebyUserID(string userID);
        Task<IEnumerable<PackageDTO>> BuyPackage(string userID, PackageOFCreatorDTO obj);
        Task<IEnumerable<PackageDTO>> ReturnPackage(string userID, PackageOFCreatorDTO obj);
        Task<IEnumerable<PackageDTO>> AdminUpdatePackage(PackageDTO obj);
        Task<IEnumerable<PackageDTO>> AdminDeletePackage(string packageID);



    }
}
