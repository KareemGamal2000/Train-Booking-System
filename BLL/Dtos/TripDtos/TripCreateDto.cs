using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TripDtos
{
    public class TripCreateDto
    {
        [Required(ErrorMessage = "معرف القطار مطلوب.")]
        public long TrainID { get; set; }

        [Required(ErrorMessage = "معرف محطة المغادرة الرئيسية مطلوب.")]
        public long DepartureStationID { get; set; }

        [Required(ErrorMessage = "معرف محطة الوصول الرئيسية مطلوب.")]
        public long ArrivalStationID { get; set; }

        [Required(ErrorMessage = "قائمة محطات التوقف مطلوبة.")]
        [MinLength(2)]
        public ICollection<TripStopCreateDto> Stops { get; set; } = new List<TripStopCreateDto>();

        [Required(ErrorMessage = "قائمة أسعار المقاطع مطلوبة.")]
        public ICollection<TripSegmentPriceCreateDto> SegmentPrices { get; set; } = new List<TripSegmentPriceCreateDto>();
    }
}
