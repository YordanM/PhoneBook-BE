using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repositories.Contacts
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _dBcontext;

        public ContactRepository(AppDbContext dbContext)
        {
            this._dBcontext = dbContext;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            await _dBcontext.AddAsync(contact);

            await _dBcontext.SaveChangesAsync();

            return contact;
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            _dBcontext.Remove(contact);

            await _dBcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync(Expression<Func<Contact, bool>> filter = null)
        {
            var query = _dBcontext.Contacts.AsQueryable();
            
            var result = new List<Contact>();

            if (filter != null)
            {
                result = await query.Where(filter).ToListAsync();
            }
            else
            {
                result = await query.ToListAsync();
            }

            return result;
        }

        public async Task<Contact> GetContactByIdAsync(string contactId)
        {
            var contact = await _dBcontext.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);

            return contact;
        }

        public async Task<Contact> UpdateContactAsync(string contactId, Contact newContact)
        {
            var existingContact = await GetContactByIdAsync(contactId);

            existingContact.Name = newContact.Name;
            existingContact.PhoneNumber = newContact.PhoneNumber;

            await _dBcontext.SaveChangesAsync();

            return existingContact;
        }
    }
}
