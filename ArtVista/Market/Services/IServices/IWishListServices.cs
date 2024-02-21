using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IWishListServices
    {
        Task<WishList>GetWishList(string userID);
        Task<bool> RemoveArtWorkFromWishList(string userID, string artworkID);
        Task<bool> AddArtWorkToWishList(string userID, string artwork, int quantity);
        Task<bool> UpdateWishList(string userID, WishList obj);



    }
}
