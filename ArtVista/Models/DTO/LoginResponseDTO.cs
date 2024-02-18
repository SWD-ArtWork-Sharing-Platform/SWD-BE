using System.ComponentModel.DataAnnotations;

namespace ArtVistaAuthAPI.Models.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO? User { get; set; }

        [Required]
        public string Token { get; set; } = "";
    }
}
