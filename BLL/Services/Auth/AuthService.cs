using Data.Models;
using Domain.Dtos.IdentityDtos;
using Domain.Helpers;
using Domain.Profiles;
using Domain.Services.Auth.Email;
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

namespace Domain.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailService _emailService;
        private readonly JWT _jwt;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IEmailService emailService,
            IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _jwt = jwt.Value;
        }

        //REGISTER
        public async Task<AuthDto> RegisterAsync(RegisterDto newuser)
        {
            if (newuser == null)
                return new AuthDto { Message = "Registration data cannot be null" };

            if (await _userManager.FindByEmailAsync(newuser.Email) != null)
                return new AuthDto { Message = "Email is already registered" };

            var user = newuser.ToUser();

            var result = await _userManager.CreateAsync(user, newuser.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                return new AuthDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthDto
            {
                Email = user.Email,
                Role = "User",
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Message = "User Registered Successfully"
            };
        }

        //LOGIN
        public async Task<AuthDto> LoginAsync(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
                return new AuthDto { Message = "Email Or Password Incorrect" };

            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var jwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault(),
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Message = "Login Successfully"
            };
        }

        //ADD ROLE
        public async Task<string> AddRoleAsync(AddRoleDto role)
        {
            var user = await _userManager.FindByIdAsync(role.UserId.ToString());

            if (user == null)
                return "Invalid user ID";

            if (!await _roleManager.RoleExistsAsync(role.RoleName))
                return "Role does not exist";

            if (await _userManager.IsInRoleAsync(user, role.RoleName))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, role.RoleName);

            return result.Succeeded ? "Role added successfully" : "Failed to add role";
        }

        //FORGOT PASSWORD
        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return "code will be sent to you";

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var shortToken = Math.Abs(resetToken.GetHashCode()).ToString().Substring(0, 6);

            user.SecurityStamp = shortToken;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            // إرسال البريد الإلكتروني
            await _emailService.SendPasswordResetEmailAsync(user.Email, shortToken);

            return "A password reset code has been sent to your email address.";
        }

        //VERIFY CODE
        public async Task<string> VerifyCodeAsync(VerifyCodeDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return "البريد الإلكتروني غير موجود";

            // التحقق من الرمز
            if (user.SecurityStamp != model.Code)
                return "الكود غير صحيح";

            if ((DateTime.UtcNow - user.UpdatedAt.GetValueOrDefault()).TotalMinutes > 15)
                return "الكود منتهي الصلاحية";

            return "تم التحقق من الكود بنجاح";
        }

        //RESET PASSWORD
        public async Task<string> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return "Email Not Found";

            // التحقق من الرمز
            if (user.SecurityStamp != model.code)
                return "The code is Invalid or Expired.";

            if ((DateTime.UtcNow - user.UpdatedAt.GetValueOrDefault()).TotalMinutes > 15)
                return "The code is Expired";

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
                return "فشل في إعادة تعيين كلمة المرور";

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
                return string.Join(", ", addPasswordResult.Errors.Select(e => e.Description));

            // مسح الرمز بعد الاستخدام
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            // إرسال إشعار بتغيير كلمة المرور
            await _emailService.SendPasswordChangedNotificationAsync(user.Email);

            return "تم إعادة تعيين كلمة المرور بنجاح";
        }

        //CHANGE PASSWORD 
        public async Task<string> ChangePasswordAsync(string userId, ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return "User is Not Found";

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            await _emailService.SendPasswordChangedNotificationAsync(user.Email);

            return "The password has been successfully changed.";
        }

        //CHANGE EMAIL 
        public async Task<string> ChangeEmailAsync(string userId, ChangeEmailDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return "User is Not Found";

            var existingUser = await _userManager.FindByEmailAsync(model.NewEmail);
            if (existingUser != null)
                return "Email is already in use";

            var confirmationToken = Math.Abs(Guid.NewGuid().GetHashCode()).ToString().Substring(0, 6);

            user.SecurityStamp = $"{model.NewEmail}|{confirmationToken}|{DateTime.UtcNow:o}";
            await _userManager.UpdateAsync(user);

            // إرسال رمز التأكيد للبريد الجديد
            await _emailService.SendEmailChangeConfirmationAsync(model.NewEmail, confirmationToken);

            return "A confirmation code has been sent to your new email address.";
        }

        //CONFIRM EMAIL CHANGE
        public async Task<string> ConfirmEmailChangeAsync(ConfirmEmailChangeDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.NewEmail);
            if (user != null)
                return "البريد الإلكتروني مستخدم بالفعل";

            // البحث عن المستخدم بالرمز
            var allUsers = _userManager.Users.ToList();
            var targetUser = allUsers.FirstOrDefault(u => u.SecurityStamp != null && u.SecurityStamp.Contains(model.NewEmail));

            if (targetUser == null)
                return "الرمز غير صحيح";

            var parts = targetUser.SecurityStamp.Split('|');
            if (parts.Length != 3)
                return "الرمز غير صحيح";

            var storedEmail = parts[0];
            var storedToken = parts[1];
            var storedTime = DateTime.Parse(parts[2]);

            // التحقق من الرمز والوقت
            if (storedEmail != model.NewEmail || storedToken != model.Token)
                return "الرمز غير صحيح";

            if ((DateTime.UtcNow - storedTime).TotalMinutes > 15)
                return "الرمز منتهي الصلاحية";

            // تحديث البريد الإلكتروني
            var oldEmail = targetUser.Email;
            targetUser.Email = model.NewEmail;
            targetUser.NormalizedEmail = model.NewEmail.ToUpper();
            targetUser.UserName = model.NewEmail;
            targetUser.NormalizedUserName = model.NewEmail.ToUpper();
            targetUser.SecurityStamp = Guid.NewGuid().ToString();
            targetUser.UpdatedAt = DateTime.UtcNow;
            targetUser.EmailConfirmed = true;

            var result = await _userManager.UpdateAsync(targetUser);

            if (!result.Succeeded)
                return "فشل في تحديث البريد الإلكتروني";

            return "تم تغيير البريد الإلكتروني بنجاح";
        }

        //CREATE JWT TOKEN
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
            );
        }
    }
}
