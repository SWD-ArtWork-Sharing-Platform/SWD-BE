using Management.Models.DTO;

namespace Management.Services.IService
{
    public interface IInteractionService
    {
        ResponseDTO GetAllInteraction();
        ResponseDTO GetInteractionByID(string  interactionID);
        ResponseDTO GetInteractionByUserID(string id);
        ResponseDTO GetInteractionByPostId(string postId);
        ResponseDTO CreateInteraction(InteractionDTO interactionDTO);
        ResponseDTO UpdateInteraction(InteractionDTO interactionDTO); 
        ResponseDTO DeleteInteraction(string id);
    }
}
