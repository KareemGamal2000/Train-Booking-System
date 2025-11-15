using Domain.Dtos.IdentityDtos;
using Domain.Third_Party.Token;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Domain.Services.Auth
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<User> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<AuthDto> RegisterAsync(RegisterDto newuser)
        {
            if (newuser == null)
            {
                return new AuthDto() { Message = "Registration data cannot be null" };
            }
            if (await _userManager.FindByEmailAsync(newuser.Email) is not null)
            {
                return new AuthDto() { Message = "Email is already registered" };
            }
            var user = new User()
            {
                FirstName = newuser.FirstName,
                LastName = newuser.LastName,
                UserName = newuser.FirstName + newuser.LastName,
                DateOfBirth = newuser.DateOfBirth,
                Gender = newuser.Gender,
                PhoneNumber = newuser.PhoneNumber,
                Email = newuser.Email
            };
            var result = await _userManager.CreateAsync(user, newuser.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthDto { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "Student");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthDto
            {
                Email = user.Email,
                Role = "Student",
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Message = "User Registered Successfully"
            };
        }
        public async Task<AuthDto> LoginAsync(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return new AuthDto { Message = "Email Or Password Incorrect" };
            }
            var jwtSecuirtyToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            return new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault(),
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecuirtyToken),
                Message = "Login Successfully"
            };
        }
        public async Task<string> AddRoleAsync(AddRoleDto role)
        {
            var user = await _userManager.FindByIdAsync(role.UserId.ToString());
            if (user is null || !await _roleManager.RoleExistsAsync(role.RoleName))
            {
                return "Invalid user ID or Role";
            }
            if (await _userManager.IsInRoleAsync(user, role.RoleName))
            {
                return "User already assigned to this role";
            }
            var result = await _userManager.AddToRoleAsync(user, role.RoleName);
            return result.Succeeded ? "Role added successfully" : "Failed to add role";

        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
                new Claim (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email , user.Email),
                new Claim("uid",user.Id.ToString())
            };
            var userclaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userclaims);

            var roles = await _userManager.GetRolesAsync(user);
            var rolesclaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            claims.AddRange(rolesclaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(

                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
              );
            return jwtSecurityToken;
        }
    }
}
