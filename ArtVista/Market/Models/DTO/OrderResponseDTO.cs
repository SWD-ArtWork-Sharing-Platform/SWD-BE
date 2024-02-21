namespace Market.Models.DTO
{
    public class OrderResponseDTO
    {
        public OrderDTO Header { get; set; } = new OrderDTO();
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();

    }

}
