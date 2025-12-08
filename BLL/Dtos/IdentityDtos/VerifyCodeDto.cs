using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.IdentityDtos
{
    public class VerifyCodeDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الكود مطلوب")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "الكود يجب أن يكون 6 أرقام")]
        public string Code { get; set; }
    }
}