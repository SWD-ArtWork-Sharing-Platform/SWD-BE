using Market.Models.DTO;

namespace Market.Services.IServices
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> GetOrder(string orderID, string? status, DateTime? CreatedOn);
       // check every dowwnloaded
        Task<bool> DownloadArtWorkCheck(string userID,string OrderID, string artID);
        Task<bool> CreateOrder(OrderResponseDTO obj);
        Task<IEnumerable<OrderDTO>> GetHistoryOrder(string userID);





    }
}
