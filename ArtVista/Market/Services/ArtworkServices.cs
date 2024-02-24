﻿using AutoMapper;
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
        private readonly IImageProcessingService _imageServices;

        public ArtworkServices(ArtworkSharingPlatformContext db,IMapper mapper, IImageProcessingService imageServices)
        {
            _db = db;
            _mapper = mapper;
            _imageServices = imageServices;
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
            FArtwork? data = _db.FArtworks.FirstOrDefault(x => x.ArtworkId == id);
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
                Datalist = Datalist.Where(u => u.CategoryID == cateID);
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

        public async Task<ArtWorkDTO> GetArtWork(string artWorkID)
        {/*
          IImageProcessingService imageProcessingService = new ImageProcessingService();

        await imageProcessingService.CropImageAsync("F:\\CodeDebugged\\Img\\image.jpg", "F:\\CodeDebugged\\Img\\croppedimage.jpg", 0, 0, 400, 400);
        await imageProcessingService.InsertTextIntoImageAsync("F:\\CodeDebugged\\Img\\image.jpg", "F:\\CodeDebugged\\Img\\imagewithtext.jpg", "Code Debugged", new PointF(405f, 430f), "Verdana", 25);
    
          */

            FArtwork? data = _db.FArtworks.FirstOrDefault(u => u.ArtworkId == artWorkID);
            if (data != null)
            {
                ArtWorkDTO? obj = _mapper.Map<ArtWorkDTO>(data);


                return obj;
            }
            else
            {
                return null;
            }
        }


        

    }
}
