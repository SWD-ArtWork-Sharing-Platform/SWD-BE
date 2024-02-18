using System.ComponentModel.DataAnnotations;

namespace ArtVistaAuthAPI.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
