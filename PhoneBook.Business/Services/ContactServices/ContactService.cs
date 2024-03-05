using PhoneBook.Data.Repositories.Contacts;
using PhoneBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Business.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            if (contact.UserId == null)
            {
                throw new Exception("User must not be null");
            }

            await _contactRepository.AddContactAsync(contact);

            return contact;
        }

        public async Task DeleteContactAsync(string contactId, string? currentUserId = null)
        {
            var contact = await _contactRepository.GetContactByIdAsync(contactId);

            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            if (contact.UserId != currentUserId)
            {
                throw new Exception("Can delete only owned contacts");
            }

            await _contactRepository.DeleteContactAsync(contact);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync(string? currentUserId = null)
        {
            Expression<Func<Contact, bool>> filter = null;

            if (currentUserId != null)
            {
                filter = c => c.UserId == currentUserId;
            }

            return await _contactRepository.GetAllContactsAsync(filter);
        }

        public async Task<Contact> GetContactByIdAsync(string contactId, string? currentUserId = null)
        {
            var contact =  await _contactRepository.GetContactByIdAsync(contactId);

            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            if (contact.UserId != currentUserId) 
            {
                throw new Exception("Can only get owned contacts");
            }

            return contact;
        }

        public async Task<Contact> UpdateContactAsync(string contactId, Contact newContact, string? currentUserId = null)
        {
            var contact  = await GetContactByIdAsync(contactId, currentUserId);

            if (contact.UserId != currentUserId)
            {
                throw new Exception("Can only update owned contacts");
            }

            return await _contactRepository.UpdateContactAsync(contactId, newContact);
        }
    }
}
