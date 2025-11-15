using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TrainDtos
{
    public class TrainReadDto
    {
        public long Train_ID { get; set; }
        public string TrainName { get; set; }
        public ICollection<TrainWithClassesDto> AvailableClasses { get; set; } = new HashSet<TrainWithClassesDto>();

    }

}
