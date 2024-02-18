using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IInteractionService
    {
        Task<ResponseDTO> GetAllInteraction();
        Task<ResponseDTO> GetInteractionByID(string  interactionID);
        Task<ResponseDTO> GetInteractionByUserID(string id);
        Task<ResponseDTO> GetInteractionByPostId(string id);
        Task<ResponseDTO> CreateInteraction(InteractionDTO interactionDTO);
        Task<ResponseDTO> UpdateInteraction(InteractionDTO interactionDTO); 
        Task<ResponseDTO> DeleteInteraction(string id);
    }
}
