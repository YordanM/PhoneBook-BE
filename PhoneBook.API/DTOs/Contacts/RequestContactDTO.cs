using PhoneBook.Domain.Models;

namespace PhoneBook.API.DTOs.Contacts
{
    public class RequestContactDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Contact ToContactModel(string? userId = null)
            => new Contact
            {
                Id = Guid.NewGuid().ToString(),
                Name = this.Name,
                PhoneNumber = this.PhoneNumber,
                UserId = userId
            };
    }
}
