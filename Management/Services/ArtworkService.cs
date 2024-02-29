using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Services.IService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Management.Util;
using Management.Repository.IRepository;

namespace Management.Services
{
    public class ArtworkService : IArtworkService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IConfiguration _configuration;
        public ArtworkService(ArtworkSharingPlatformContext db, IMapper mapper, IConfiguration configuration, IArtworkRepository artworkRepository)
        {
            _db = db;
            this._response = new ResponseDTO();
            _mapper = mapper;
            _configuration = configuration;
            _artworkRepository = artworkRepository;
        }
        public ResponseDTO CreateNewArtwork(ArtworkDTO model)
        {
            try
            {
                FArtwork artwork = _mapper.Map<FArtwork>(model);
                if (model.Image != null)
                {
                    string fileName = artwork.Id + Path.GetExtension(model.Image.FileName);
                    string localPathDirector = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), localPathDirector);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                    artwork.ImageUrl = "/ProductImages/" + fileName;
                    artwork.ImageLocalPath = localPathDirector;
                }

                _artworkRepository.Add(artwork);
                _artworkRepository.Save();
                _response.Result = _mapper.Map<ArtworkDTO>(artwork);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public  ResponseDTO DeleteArtWorkByID(string id, bool confirm)
        {
            try
            {
                if (confirm == true)
                {
                    FArtwork artwork =  _db.FArtworks.First(u => u.Id == id);

                    if (!string.IsNullOrEmpty(artwork.ImageLocalPath))
                    {
                        var oldFilePathLocalDirectory = Path.Combine(Directory.GetCurrentDirectory(), artwork.ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathLocalDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    _artworkRepository.Remove(artwork);
                    _artworkRepository.Save();
                    _response.Result = _mapper.Map<ArtworkDTO>(artwork);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public  ResponseDTO GetAllArtwork()
        {
            try
            {
                IEnumerable<FArtwork> artworkList = _artworkRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<ArtworkDTO>>(artworkList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetArtWorkByCondition(string? name, string id, string status, decimal discount)
        {
            try
            {
                IEnumerable<FArtwork> artworkList = _artworkRepository.GetAll();
                IEnumerable<ArtworkDTO> artworkDTOList = new List<ArtworkDTO>();

                if (!string.IsNullOrEmpty(name))
                {
                    artworkList = artworkList.Where(u => u.ArtworkName == name);
                }
                if (!string.IsNullOrEmpty(id))
                {
                    artworkList = artworkList.Where(u => u.Id == name);
                }
                if (discount > 0)
                {
                    artworkList = artworkList.Where(u => u.Discount == discount);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    SD.ArtworkStatus artworkStatus = SD.CheckArtworkStatus(status);
                    if (artworkStatus != SD.ArtworkStatus.All)
                    {
                        artworkList = artworkList.Where(u => (u.Status ?? "").Equals(artworkStatus.ToString()));
                    }
                }
                if (artworkList != null)
                {
                    artworkDTOList = _mapper.Map<IEnumerable<ArtworkDTO>>(artworkList);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO GetArtworkById(string id)
        {
            try
            {
                FArtwork artwork =  _artworkRepository.Get(u => u.ArtworkId == id);   
                _response.Result = _mapper.Map<FArtwork>(artwork);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        public ResponseDTO UpdateArtwork(ArtworkDTO artworkDTO)
        {
            try
            {
                FArtwork artwork = _mapper.Map<FArtwork>(artworkDTO);
                FArtwork oldArtwork = _db.FArtworks.First(u => u.ArtworkId == artworkDTO.ArtworkId);

                if (oldArtwork == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No artwork found!";
                }
                else
                {
                    if (artworkDTO.Image != null)
                    {
                        var oldfilePathDirectory = Path.Combine(Directory.GetCurrentDirectory());
                        FileInfo file = new FileInfo(oldfilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                        string fileName = artwork.ArtworkId + Path.GetExtension(Directory.GetCurrentDirectory());
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            artworkDTO.Image.CopyTo(fileStream);
                        }
                        artwork.ImageUrl = fileName;
                    }

                    oldArtwork = artwork;

                    _artworkRepository.Update(oldArtwork);
                    _artworkRepository.Save();
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
    }
}
