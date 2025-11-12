using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class DrinkDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } 
        public decimal Price { get; set; } 
        public string ImageUrl { get; set; } 
        public bool IsAvailable { get; set; } //  متاح على الرحلة
    }
}
