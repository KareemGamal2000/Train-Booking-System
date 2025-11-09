using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.IdentityDtos
{
    public class AddRoleDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }
    }
}
