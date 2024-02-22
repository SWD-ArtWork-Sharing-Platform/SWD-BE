﻿using Management.Models.DTO;
using Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("api/package")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private ResponseDTO _response;
        private PackageService _packageService;

        public PackageController(PackageService packageService)
        {
            this._response = new ResponseDTO();  
            _packageService = packageService;   
        }

        [HttpPost("CreatePackage")]
        public ResponseDTO CreatePackage(PackageDTO packageDTO)
        {
            try
            {
                _response.Result = _packageService.CreatePackage(packageDTO);       
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeletePackage")]
        public ResponseDTO DeletePackage(string id, bool confirm)
        {
            try
            {
                _response.Result = _packageService.DeletePackage(id, confirm);      
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetAllPackage")]
        public ResponseDTO GetAllPackage()
        {
            try
            {
                _response.Result = _packageService.GetAllPackage(); 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetPackageByCondtion")]
        public ResponseDTO GetPackageByCondtion(string name, decimal price, decimal discount)
        {
            try
            {
                _response.Result = _packageService.GetPackageByCondtion(name,price,discount);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetPackageByID")]
        public ResponseDTO GetPackageByID(string id)
        {
            try
            {
                _response.Result = _packageService.GetPackageByID(id);  
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdatePackage")]
        public ResponseDTO UpdatePackage(PackageDTO packageDTO)
        {
            try
            {
                _response.Result = _packageService.UpdatePackage(packageDTO);   
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