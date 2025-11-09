using Domain.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterDto newuser);
        Task<AuthDto> LoginAsync(LoginDto login);
        Task<string> AddRoleAsync(AddRoleDto role);

    }
}
