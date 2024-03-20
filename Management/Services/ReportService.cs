using AutoMapper;
using Management.Data;
using Management.Models;
using Management.Models.DTO;
using Management.Repository.IRepository;
using Management.Services.IService;
using Management.Util;
using Microsoft.AspNetCore.Identity;

namespace Management.Services
{
    public class ReportService : IReportService
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private  IReportRepository _reportRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReportService(ArtworkSharingPlatformContext db, IMapper mapper, IReportRepository reportRepository, UserManager<ApplicationUser> userManager)
        {
            this._response = new ResponseDTO(); 
            _mapper = mapper;
            _db = db;
            _reportRepository = reportRepository;   
            _userManager = userManager; 
        }
        public ResponseDTO MonthlyInspection(DateTime SelectedMoth)
        {
            try
            {
                DateTime currentMonthStart = new DateTime(SelectedMoth.Year, SelectedMoth.Month, 1);
                DateTime currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);

                IEnumerable<FReport> reportList = _reportRepository.GetAll().Where(u => u.CreatedOn >= currentMonthStart && u.CreatedOn <= currentMonthEnd);
                IEnumerable<ReportDTO> reportDTOList = _mapper.Map<IEnumerable<ReportDTO>>(reportList);

                _response.Result = reportDTOList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO ReportByUser(string id)
        {
            try
            {
                IEnumerable<FReport> objList = _reportRepository.GetAll().Where(u => u.Id == id).OrderBy(u => u.Id);
                IEnumerable<ReportDTO> reportDTOs = new List<ReportDTO>();

                if (objList != null)
                {
                    reportDTOs = _mapper.Map<IEnumerable<ReportDTO>>(objList);       
                }
                _response.Result = reportDTOs;  
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDTO CreateReport(ReportDTO obj)
        {
            FReport updateObj = _mapper.Map<FReport>(obj);
            if (updateObj == null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "Object not exist!"
                };
            }
            updateObj.ReportId = DateTime.Now.ToString();
            _reportRepository.Add(updateObj);
            _reportRepository.Save();
            return _response;
        }

        public ResponseDTO UpdateReport(ReportDTO obj)
        {
            FReport data = _reportRepository.Get(u => u.ReportId == obj.ReportId);
            FReport updateObj =_mapper.Map<FReport>(obj);
            if (data == null && obj== null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "Object not exist!"
                };
            }
            data = updateObj;
            data.Artwork = null;
            _reportRepository.Update(data);
            _reportRepository.Save();
            return _response;
        }

        public ResponseDTO RemoveREport(string id)
        {
            FReport data = _reportRepository.Get(u => u.ReportId == id);
            if (data == null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    Message ="Object not exist!"
                };
            }
            _reportRepository.Remove(data);
            _reportRepository.Save();
            return _response;
        }
    }
}
