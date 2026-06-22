using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction.Interfaces;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (User == null)
                return Error.InvalidCredentials("User.InvalidCredentials");

            var CheckPassword = await _userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (!CheckPassword)
                return Error.InvalidCredentials("User.InvalidCredentials");
            return new UserDTO(User.Email!, User.DisplayName, "ThisIsAToken");

        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber
            };
            var IdentityResult = await _userManager.CreateAsync(User, registerDTO.Password);
            
            if(IdentityResult.Succeeded)
                return new UserDTO(User.Email!, User.DisplayName, "ThisIsAToken");
            return IdentityResult.Errors.Select(e => Error.Validation(e.Code,e.Description)).ToList();
        }
    }
}
