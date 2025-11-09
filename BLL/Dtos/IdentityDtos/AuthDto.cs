using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.IdentityDtos
{
    public class AuthDto
    {
        public string Message { get; set; }

        public bool IsAuthenticated { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; } = String.Empty;

        public string Token { get; set; }
    }
}
