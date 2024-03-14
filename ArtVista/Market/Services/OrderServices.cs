using AutoMapper;
using Market.Data;
using Market.Models;
using Market.Models.DTO;
using Market.Repository.IRepository;
using Market.Services.IServices;
using Market.Utils;

namespace Market.Services
{
    public class OrderServices : IOrderService
    {
        private IMapper _mapper;
        private readonly ArtworkSharingPlatformContext _db;
        private IOrderRepository _orderRepository;
        private IOrderDetailsRepository _orderDetailsRepository;    
        public OrderServices(ArtworkSharingPlatformContext db, IMapper mapper, IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            _db = db;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;   
        }

        public async Task<OrderResponseDTO> GetOrder(string OrderID,
            string? status, DateTime? CreatedOn)
        {
            FOrder? headerData = _db.FOrders.FirstOrDefault( u => u.OrderId == OrderID);
            if (headerData != null)
            {
                List<DOrderDetail>? detailsList = _db.DOrderDetails.Where(u=> u.OrderId == headerData.OrderId).ToList();
                OrderResponseDTO orderResponseDTO = new OrderResponseDTO()
                {
                    Header = _mapper.Map<OrderDTO>(headerData),
                    OrderDetails = _mapper.Map<List<OrderDetailDTO>>(detailsList)
                };
                return orderResponseDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DownloadArtWorkCheck(string userID, string OrderID, string artID)
        {
            FOrder? headerData = _db.FOrders.FirstOrDefault(u => u.Id == userID && u.OrderId == OrderID);
            if (headerData != null)
            {
                headerData.NumberOfDowload += 1;
                _db.FOrders.Update(headerData);
                await _db.SaveChangesAsync();   

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateOrder(OrderResponseDTO obj)
        {
            FOrder? headerData = _mapper.Map<FOrder>(obj.Header);
            List<DOrderDetail>? details = _mapper.Map<List<DOrderDetail>>(obj.OrderDetails);
            if (headerData != null && details != null)
            {
                _orderRepository.Add(headerData);       
                foreach(DOrderDetail detail in details)
                {
                    _orderDetailsRepository.Add(detail);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetHistoryOrder(string userID)
        {
            IEnumerable<FOrder>? orderdata = _db.FOrders.Where(u => u.Id == userID).OrderBy(u => u.CreatedOn);
            if (orderdata != null)
            {
                return _mapper.Map<IEnumerable<OrderDTO>>(orderdata);
            }else
            {
                return null;
            }
        }


    }
}
