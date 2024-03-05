using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.DTOs.Account;
using PhoneBook.Business.Services.TokenServices;
using PhoneBook.Domain.Models;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
            this._signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RequestLoginDTO requestLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == requestLoginDTO.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, requestLoginDTO.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password incorrect");
            }

            return Ok(new ResponseLoginDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RequestRegisterDTO registerDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = new AppUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
            };
                
            var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (createdUser.Succeeded) 
            {
                return Ok (
                    new ResponseRegisterDTO
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });

                /*var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(
                        new ResponseRegisterDTO
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }*/
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }
        }
    }
}
