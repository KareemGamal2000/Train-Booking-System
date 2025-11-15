using Data.Models;
using Data.Models.Trips;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Index(nameof(TrainName), IsUnique = false)]
    public class Train
    {
        [Key]
        public long TrainID { get; set; } 
        public string? TrainName { get; set; } //950

        public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<TrainCoach> TrainCoaches { get; set; } = new HashSet<TrainCoach>();
    }
}
