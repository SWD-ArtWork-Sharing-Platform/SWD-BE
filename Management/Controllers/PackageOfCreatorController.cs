﻿using Management.Models.DTO;
using Management.Services;
using Management.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/packageOfCreator")]
    [ApiController]
    public class PackageOfCreatorController : ControllerBase
    {
        private ResponseDTO _response;
        private IPackageOfCreatorService _packageOfCreatorService;
        public PackageOfCreatorController(IPackageOfCreatorService packageOfCreatorService)
        {
            _response = new ResponseDTO();  
            _packageOfCreatorService = packageOfCreatorService;     
        }
        //[Authorize(Policy = "CUSTOMER_USER")]
        [HttpPost("CreatePackageOfCreator")]
        public ResponseDTO CreatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO)
        {
            try
            {
                _response.Result = _packageOfCreatorService.CreatePackageOfCreator(packageOfCreatorDTO);        
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        //[Authorize]
        [HttpDelete("DeletePackageOfCreator")]
        public ResponseDTO DeletePackageOfCreator(string id, bool confirm)
        {
            try
            {
                _response.Result = _packageOfCreatorService.DeletePackageOfCreator(id, confirm);        
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpGet("GetAllPackgeOfCreator")]
        public ResponseDTO GetAllPackgeOfCreator()
        {
            try
            {
                _response.Result = _packageOfCreatorService.GetAllPackgeOfCreator();    
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpGet("GetPackageOfCreatorByID")]
        public ResponseDTO GetPackageOfCreatorByID(string id)
        {
            try
            {
                _response.Result = _packageOfCreatorService.GetPackageOfCreatorByID(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Policy = "ARTWORKMANAGEMENT")]
        [HttpPut("UpdatePackageOfCreator")]
        public ResponseDTO UpdatePackageOfCreator(PackageOfCreatorDTO packageOfCreatorDTO)
        {
            try
            {
                _response.Result = _packageOfCreatorService.UpdatePackageOfCreator(packageOfCreatorDTO);    
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
