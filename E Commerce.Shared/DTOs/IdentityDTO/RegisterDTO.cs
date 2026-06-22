using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.IdentityDTO
{
    public record RegisterDTO
    (
        string DisplayName,
        string Email,
        string Token,
        [Phone]
        string PhoneNumber,
        string Password
    );
}
