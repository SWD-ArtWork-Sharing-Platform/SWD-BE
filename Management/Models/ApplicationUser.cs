using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Management.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = "User";
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string Status { get; set; } = "";
        public string? ConfirmCode { get; set; }


    }
}
