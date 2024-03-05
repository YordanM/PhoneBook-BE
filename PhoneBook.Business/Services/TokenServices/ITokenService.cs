using PhoneBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Business.Services.TokenServices
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
