using System.ComponentModel.DataAnnotations;

namespace ArtVistaAuthAPI.Models.DTO
{
    public class ChangePasswordDTO
    {
        public string? Email { get; set; }
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        public string? NewPassword { get; set;}
    }
}
