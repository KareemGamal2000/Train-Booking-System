using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Class
    {
        [Key]
        public int Class_ID { get; set; }
        public string ClassNameEN { get; set; }
        public string ClassNameAR { get; set; }
        public virtual ICollection<Coach> Coaches { get; set; } = new HashSet<Coach>();
    }
}

