using PhoneBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Business.Services.ContactServices
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync(string? currentUserId = null);

        Task<Contact> GetContactByIdAsync(string contactId, string? currentUserId = null);

        Task<Contact> AddContactAsync(Contact contact);

        Task<Contact> UpdateContactAsync(string contactId, Contact newContact, string? currentUserId = null);

        Task DeleteContactAsync(string contactIdstring, string? currentUserId = null);

    }
}
