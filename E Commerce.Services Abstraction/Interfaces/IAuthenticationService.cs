using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction.Interfaces
{
    public interface IAuthenticationService
    {
        // Login
        // Email, Password => Token, Display Name, Email
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);
        // Register
        // Display Name, PhoneNumber, Email, Password => Token, Display Name, Email
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    }
}
