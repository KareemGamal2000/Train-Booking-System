using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.IdentityDtos
{
    public class ResetPasswordDto
    {

        public string Email { get; set; }

        [Required, StringLength(10)]
        public string OTP { get; set; }

        [Required, StringLength(256)]
        public string NewPassword { get; set; }
    }
}
