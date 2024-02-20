using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IWishListServices
    {
        Task<WishListDTO>GetWishList(string userID);
        Task<bool> RemoveArtWorkFromWishList(string userID, string artworkID);
        Task<bool> AddArtWorkToWishList(string userID, ArtWorkDTO artwork, int quantity);
        Task<bool> UpdateWishList(string userID, WishListDTO obj);



    }
}
