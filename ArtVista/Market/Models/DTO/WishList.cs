namespace Market.Models.DTO
{
    public class WishList
    {
        public WishListDTO Header { get; set; } = new WishListDTO();
        public List<WishListDetailDTO> Details {  get; set; } = new List<WishListDetailDTO> { new WishListDetailDTO() };
    }
}
