using Data.Models.Trips;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Class
    {
        [Key]
        public long Class_ID { get; set; }
        public string ClassNameEN { get; set; }
        public string ClassNameAR { get; set; }
        public virtual ICollection<Coach> Coaches { get; set; } = new HashSet<Coach>();

        public virtual ICollection<TripSegmentPrice> SegmentPrices { get; set; } = new HashSet<TripSegmentPrice>();
    }
}

