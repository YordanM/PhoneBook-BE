using System.ComponentModel.DataAnnotations;

namespace PhoneBook.API.DTOs.Account
{
    public class RequestLoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
