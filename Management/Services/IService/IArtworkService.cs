using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IArtworkService
    {
        ResponseDTO GetAllArtwork();
        ResponseDTO GetArtworkById(string id);
        ResponseDTO UpdateArtwork(ArtworkDTO artworkDTO);
        ResponseDTO DeleteArtWorkByID(string id, bool confirm);
        ResponseDTO CreateNewArtwork(ArtworkDTO artworkDTO);
        ResponseDTO GetArtWorkByCondition(string? name, string id, string status, decimal discount);
    }
}
