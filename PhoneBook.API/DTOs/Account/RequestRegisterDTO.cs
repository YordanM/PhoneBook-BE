using System.ComponentModel.DataAnnotations;

namespace PhoneBook.API.DTOs.Account
{
    public class RequestRegisterDTO
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
