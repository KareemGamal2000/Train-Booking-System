using Data.Models;
using Data.Models.Trips;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Trips
{
    public class TripSegmentPrice
    {
        [Key]
        public int SegmentPriceID { get; set; } 

        [ForeignKey("TripID")]
        public int TripID { get; set; }
        public virtual Trip Trip { get; set; }

        [ForeignKey("StartStopID")]
        public int StartStopID { get; set; }
        public virtual TripStop StartStop { get; set; }

        [ForeignKey("EndStopID")]
        public int EndStopID { get; set; }
        public virtual TripStop EndStop { get; set; }

        [ForeignKey("ClassID")]
        public long ClassID { get; set; }
        public virtual Class Class { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Price { get; set; }
    }
}
