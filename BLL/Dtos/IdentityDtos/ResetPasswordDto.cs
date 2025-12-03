using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.IdentityDtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string code { get; set; }

        [Required(ErrorMessage = "كلمة المرور الجديدة مطلوبة")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور يجب أن تكون بين 6 و 100 حرف")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور وتأكيدها غير متطابقين")]
        public string ConfirmPassword { get; set; }
    }
}
