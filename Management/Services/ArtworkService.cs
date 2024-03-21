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
                //DPackageOfCreator? currentPackage = _db.DPackageOfCreators.Where(u => u.Id == model.Id).OrderByDescending(item => item.ExpiredDate).FirstOrDefault();
                //if (currentPackage == null || currentPackage.Remain < 1)
                //{
                //    return new ResponseDTO()
                //    {
                //        IsSuccess = false,
                //        Message = "Creator must buy package to create artwork!"
                //    };
                //}
                FArtwork artwork = _mapper.Map<FArtwork>(model);
                artwork.ArtworkId = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
               /* if (model.Image != null)
                {
                    string fileName = artwork.ArtworkName + Path.GetExtension(model.Image.FileName);
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                    artwork.ImageUrl =fileName;
                }*/

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
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            artworkDTO.Image.CopyTo(fileStream);
                        }
                        artwork.ImageUrl = fileName;
                    }

                    oldArtwork = artwork;
                    oldArtwork.FPosts = null;
                    oldArtwork.FReports = null;
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
