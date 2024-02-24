using System.ComponentModel.DataAnnotations;

namespace Auth.Models.DTO
{
    public class RegisterationRequestDTO
    {
        [Required]
        public string Email { get; set; } = "";

        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Status { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string? Role { get; set; }
    }
}
