using Data.Models;
using Domain.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class UserProfile
    {
        public static User ToUser(this RegisterDto dto)
        {
            if (dto == null)
                return null;

            return new User
            {
                UserName = dto.FirstName + dto.LastName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                IsActive = true,

            };
        }
 
    }
}
