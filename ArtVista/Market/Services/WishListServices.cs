using AutoMapper;
using Market.Data;
using Market.Models;
using Market.Models.DTO;
using Market.Services.IServices;
using Market.Utils;

namespace Market.Services
{
    public class WishListServices : IWishListServices
    {
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        public WishListServices(ArtworkSharingPlatformContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<WishList> GetWishList(string userID)
        {
            FWishlist? HeaderData = _db.FWishlists.FirstOrDefault( u=> u.Id == userID);
            if (HeaderData != null)
            {
                IEnumerable<DWishlistDetail> detailsdata = _db.DWishlistDetails.Where(u => u.WishlistId == HeaderData.WishlistId);
                
                return new WishList()
                {
                    Header = _mapper.Map<WishListDTO>(HeaderData),
                    Details = _mapper.Map<List<WishListDetailDTO>>(detailsdata)
                };
            }else
            {
                return null;
            }

        }

        public async Task<bool> RemoveArtWorkFromWishList(string userID, string artworkID)
        {
            FWishlist? HeaderData = _db.FWishlists.FirstOrDefault(u => u.Id == userID);
            if (HeaderData != null)
            {
                DWishlistDetail? details = _db.DWishlistDetails.FirstOrDefault( u => u.ArtworkId == artworkID && u.WishlistId == HeaderData.WishlistId);
                if (details != null)
                {
                    _db.DWishlistDetails.Remove(details);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddArtWorkToWishList(string userID, string artwork, int quantity)
        {
            try {

                FWishlist? HeaderData = _db.FWishlists.FirstOrDefault(u => u.Id == userID);
                if (HeaderData != null)
                {
                    DWishlistDetail add = new DWishlistDetail()
                    {
                        WishlistDetailId = DateTime.Now.ToString(),
                        ArtworkId = artwork,
                        WishlistId = HeaderData.WishlistId
                    };
                    HeaderData.Total += 1;
                    _db.DWishlistDetails.Add(add);
                    _db.FWishlists.Update(HeaderData);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    FWishlist header = new FWishlist()
                    {
                        WishlistId = DateTime.Now.ToString(),
                        Id = userID,
                        Total = 1,
                        Note = ""
                    };
                    _db.FWishlists.Add(header);
                    await _db.SaveChangesAsync();

                    DWishlistDetail add = new DWishlistDetail()
                    {
                        WishlistDetailId = DateTime.Now.ToString(),
                        ArtworkId = artwork,
                        WishlistId = header.WishlistId
                    };
                    _db.DWishlistDetails.Add(add);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }catch (Exception e)
            {

                return false;
            }
        }
        

        public async Task<bool> UpdateWishList(string userID, WishList obj)
        {
            FWishlist? HeaderData = _db.FWishlists.FirstOrDefault(u => u.Id == userID);
            if (HeaderData.WishlistId == obj.Header.WishlistId)
            {
                IEnumerable<DWishlistDetail> removeList = _db.DWishlistDetails.Where( u => u.WishlistId == HeaderData.WishlistId);
                foreach (DWishlistDetail detail in removeList)
                {
                    _db.DWishlistDetails.Remove(detail);
                }
                foreach(WishListDetailDTO add in obj.Details)
                {
                    DWishlistDetail datanew = _mapper.Map<DWishlistDetail>(add);
                    if (datanew != null)
                    {
                        datanew.WishlistDetailId = null;
                        _db.DWishlistDetails.Add(datanew);
                    }
                }

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
