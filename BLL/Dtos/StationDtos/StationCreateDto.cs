using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.StationDtos
{
    public class StationCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string StationNameAR { get; set; }

        [Required]
        [MaxLength(100)]
        public string StationNameEN { get; set; }

        [MaxLength(10)]
        public string? StationCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string ShortName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
