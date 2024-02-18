using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Management.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = "User";
    }
}
