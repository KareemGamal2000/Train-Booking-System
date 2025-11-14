using Data.Entities.Trips;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Train
    {
        [Key]
        public long Train_ID { get; set; } 
        public string TrainName { get; set; } //950

        public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<TrainCoach> TrainCoaches { get; set; } = new HashSet<TrainCoach>();
    }
}
