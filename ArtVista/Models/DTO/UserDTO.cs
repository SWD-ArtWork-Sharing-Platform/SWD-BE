using System.ComponentModel.DataAnnotations;

namespace ArtVistaAuthAPI.Models.DTO
{
    public class UserDTO
    {
        [Required]
        public string Id { get; set; } = "0";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }
}
