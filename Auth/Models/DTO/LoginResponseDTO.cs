using System.ComponentModel.DataAnnotations;

namespace Auth.Models.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO? User { get; set; }

        [Required]
        public string Token { get; set; } = "";
    }
}
