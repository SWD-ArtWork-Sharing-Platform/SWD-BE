using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IArtworkService
    {
        Task<ResponseDTO> GetAllArtwork();
        Task<ResponseDTO> GetArtworkById(string id);
        Task<ResponseDTO> UpdateArtwork(ArtworkDTO artworkDTO);
        Task<ResponseDTO> DeleteArtWorkByID(string id, bool confirm);
        Task<ResponseDTO> CreateNewArtwork(ArtworkDTO artworkDTO);
        Task<ResponseDTO> GetArtWorkByCondition(string? name, string id, string status, decimal discount);
    }
}
