using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Train
    {
        [Key]
        public int Train_ID { get; set; }

        public string TrainNumber { get; set; } //950
        public string TrainDescriptionEN { get; set; }  // AC 3

        public string TrainDescriptionAR { get; set; } // ثالثة مكيفة

        public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<Coach> Coaches { get; set; } = new HashSet<Coach>();
    }
}
