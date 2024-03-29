﻿using System.ComponentModel.DataAnnotations;

namespace ArtVistaAuthAPI.Models.DTO
{
    public class RegisterationRequestDTO
    {
        [Required]
        public string Email { get; set; } = "";

        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string? Role { get; set; }
    }
}
