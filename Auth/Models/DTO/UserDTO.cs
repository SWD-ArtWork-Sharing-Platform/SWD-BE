using System.ComponentModel.DataAnnotations;

namespace Auth.Models.DTO
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
        public string Status { get; set; } = "";
        public string Address { get; set; } = "";
        public IEnumerable<string> Role { get; set; } = new List<string>();
    }
}
