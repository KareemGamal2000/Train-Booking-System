using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TripDtos
{
    public class TripSegmentPriceCreateDto
    {
        [Required]
        public long ClassID { get; set; }

        [Required]
        public int StartStopSequence { get; set; }

        [Required]
        public int EndStopSequence { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
