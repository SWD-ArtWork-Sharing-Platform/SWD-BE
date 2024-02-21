using AutoMapper;
using Market.Data;
using Market.Models;
using Market.Models.DTO;
using Market.Services.IServices;
using Market.Utils;
using Microsoft.AspNetCore.Identity;

namespace Market.Services
{
    public class ArtworkServices : IArtworkServices
    {
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        public ArtworkServices(ArtworkSharingPlatformContext db, IHttpClientFactory httpClientFactory,
             IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateArtWork(ArtWorkDTO artworkDTO)
        {
            FArtwork data = _mapper.Map<FArtwork>(artworkDTO);
           
            if (data != null)
            {
                data = Refresh.FArtwork(data);
                _db.FArtworks.Add(data);
                await _db.SaveChangesAsync();
                return true;
            } else
            { return false; }


        }

        public async Task<bool> DeleteArtWork(string id)
        {
            FArtwork data = _db.FArtworks.FirstOrDefault(x => x.ArtworkId == id);
            if (data != null)
            {                
                _db.FArtworks.Remove(data);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            { return false; }
        }

        public async Task<IEnumerable<ArtWorkDTO>> GetAllArtwork(string? searchkey, decimal? minPrice, decimal? MaxPrice,
            decimal? discount, string? status, string? cateID)
        {
            IEnumerable<ArtWorkDTO> Datalist = _mapper.Map<IEnumerable<ArtWorkDTO>>(_db.FArtworks.ToList());
            if (!string.IsNullOrEmpty(searchkey))
            {
                Datalist = Datalist.Where( u => u.ArtworkName.Contains(searchkey));
            }
            if(minPrice > 0)
            {
                Datalist = Datalist.Where(u => u.Price >= minPrice);
            }
            if (MaxPrice > 0)
            {
                Datalist = Datalist.Where(u => u.Price <= MaxPrice);
            }
            if(discount > 0)
            {
                Datalist = Datalist.Where(u => u.Discount >= discount);
            }
            if (!string.IsNullOrEmpty(status))
            {
                Datalist = Datalist.Where(u => u.Status == status);
            }
            if (!string.IsNullOrEmpty(cateID))
            {
                Datalist = Datalist.Where(u => u.Category.CategoryId == cateID);
            }




            return Datalist;
        }

        public async Task<bool> UpdateArtWork(ArtWorkDTO artworkDTO)
        {
            FArtwork updateObj = _mapper.Map<FArtwork>(artworkDTO);
            FArtwork data = _db.FArtworks.FirstOrDefault(u => u.ArtworkId == artworkDTO.ArtworkId);

            if (data != null && updateObj!= null)
            {
                data = updateObj;
                _db.FArtworks.Update(data);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            { return false; }
        }
    }
}
