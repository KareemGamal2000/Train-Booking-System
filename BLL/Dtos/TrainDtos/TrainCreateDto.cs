using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TrainDtos
{
    public class TrainCreateDto
    {
        [Required(ErrorMessage = "رقم القطار مطلوب.")]
        [Range(1, long.MaxValue, ErrorMessage = "رقم القطار يجب أن يكون أكبر من الصفر.")]
        public long Train_ID { get; set; } 

        [Required(ErrorMessage = "اسم/رقم رحلة القطار مطلوب.")]
        [MaxLength(50)]
        public string TrainName { get; set; } 

       
    }
}
