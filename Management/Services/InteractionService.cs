using AutoMapper;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository;
using Management.Repository.IRepository;
using Management.Services.IService;

namespace Management.Services
{
    public class InteractionService : IInteractionService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private IInteractionRepository _interactionRepository;
        public InteractionService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, IInteractionRepository interactionRepository)
        {
            this._response = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _interactionRepository = interactionRepository; 
        }

        public ResponseDTO GetAllInteraction()
        {
            try
            {
                IEnumerable<DInteraction> interactionList = _interactionRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<InteractionDTO>>(interactionList);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        } 

        public ResponseDTO GetInteractionByID(string interactionID)
        {
            try
            {
                DInteraction interaction = _interactionRepository.Get(u => u.InteractionId == interactionID);
                _response.Result = _mapper.Map<InteractionDTO>(interaction);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetInteractionByUserID(string id)
        {
            try
            {
                IEnumerable<DInteraction> interaction = _interactionRepository.GetList(u => u.Id == id);
                _response.Result = _mapper.Map<IEnumerable<InteractionDTO>>(interaction);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetInteractionByPostId(string postId)
        {
            try
            {
                IEnumerable<DInteraction> interaction = _interactionRepository.GetList(u => u.PostId == postId);
                _response.Result = _mapper.Map<IEnumerable<InteractionDTO>>(interaction);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO CreateInteraction(InteractionDTO interactionDTO)
        {
            try
            {
                DInteraction interaction = _mapper.Map<DInteraction>(interactionDTO);
                interaction.InteractionId = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); ;
                _interactionRepository.Add(interaction);
                _interactionRepository.Save();
                _response.Result = _mapper.Map<InteractionDTO>(interaction);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO UpdateInteraction(InteractionDTO interactionDTO)
        {
            try
            {
                DInteraction interaction = _mapper.Map<DInteraction>(interactionDTO);
                DInteraction oldInteraction = _db.DInteractions.First(u => u.InteractionId == interactionDTO.InteractionId);
                if (oldInteraction == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Interaction not found!";
                }
                else
                {
                    oldInteraction = interaction;
                    oldInteraction.Post = null;
                    _interactionRepository.Update(oldInteraction);
                    _interactionRepository.Save();
                    _response.Message = "Update successfully!";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO DeleteInteraction(string id)
        {
            try
            {
                DInteraction interaction = _db.DInteractions.First(u => u.InteractionId == id);
                if (interaction == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No interaction found!";
                }
                else
                {
                    _interactionRepository.Remove(interaction);
                    _interactionRepository.Save();
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
