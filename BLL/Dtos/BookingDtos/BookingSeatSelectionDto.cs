using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDto
{
    public class BookingSeatSelectionDto
    {
        [Required(ErrorMessage = "معرف العربة/الدرجة مطلوب")]
        [Range(1, long.MaxValue, ErrorMessage = "معرف العربة/الدرجة غير صحيح")]
        public int CoachId { get; set; }
        
        [Required(ErrorMessage = "يجب اختيار مقعد واحد على الأقل")]
        [MinLength(1, ErrorMessage = "يجب اختيار مقعد واحد على الأقل")]
        public List<int> SelectedSeatIDs { get; set; }
        
        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0.01, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal PricePerSeat { get; set; }
    }
}
