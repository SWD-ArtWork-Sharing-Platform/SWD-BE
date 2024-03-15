using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IArtworkServices
    {
        Task<IEnumerable<ArtWorkDTO>> GetAllArtwork( string? searchkey, decimal? minPrice,
            decimal? MaxPrice, decimal? discount, string? status, string? cateID);
        Task<ResponseDTO> CreateArtWork(string creatorID, ArtWorkDTO artworkDTO);
        Task<bool> UpdateArtWork(ArtWorkDTO artworkDTO);
        Task<bool> DeleteArtWork(string id);
        Task<ArtWorkDTO> GetArtWork(string artWorkID);




    }
}
