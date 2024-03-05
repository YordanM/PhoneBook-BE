using PhoneBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repositories.Contacts
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync(Expression<Func<Contact, bool>> filter = null);

        Task<Contact> GetContactByIdAsync(string contactId);

        Task<Contact> AddContactAsync(Contact contact);

        Task<Contact> UpdateContactAsync(string contactId, Contact newContact);

        Task DeleteContactAsync(Contact contact);
    }
}
