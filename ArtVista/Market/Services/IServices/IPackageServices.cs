﻿using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IPackageServices
    {
        Task<IEnumerable<PackageDTO>> GetAllAvailablePackage(string? name, int? max, decimal? price, decimal? discount);
        Task<IEnumerable<PackageOFCreatorDTO>> GetAllPurchasePackagebyUserID(string userID);
        Task<bool> BuyPackage(string userID, PackageOFCreatorDTO obj);
        Task<bool> AdminUpdatePackage(PackageDTO obj);
        Task<bool> AdminDeletePackage(string packageID);



    }
}