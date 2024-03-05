using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.API.DTOs.Contacts;
using PhoneBook.Business.Services.ContactServices;
using PhoneBook.Domain.Models;
using System.Security.Claims;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactsAsync()
        { 
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var contacts = await _contactService.GetAllContactsAsync(currentUserId);

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactByIdAsync([FromRoute] string id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var contact = await _contactService.GetContactByIdAsync(id, currentUserId);

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContactAsync([FromBody] RequestContactDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var contact = await _contactService.AddContactAsync(request.ToContactModel(currentUserId));

            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactAsync(
            [FromRoute] string id,
            [FromBody] RequestContactDTO request)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var updatedContact = request.ToContactModel();

            var result = await _contactService.UpdateContactAsync(id, updatedContact, currentUserId);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] string id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _contactService.DeleteContactAsync(id, currentUserId);

            return Ok();
        }
    }
}
