using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.IdentityDtos
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "كلمة المرور الحالية مطلوبة")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "كلمة المرور الجديدة مطلوبة")]
        [StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور الجديدة وتأكيدها غير متطابقين")]
        public string ConfirmPassword { get; set; }
    }
}
