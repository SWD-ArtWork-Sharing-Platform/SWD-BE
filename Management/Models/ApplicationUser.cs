using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Management.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = "User";
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 11 characters.")]
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
