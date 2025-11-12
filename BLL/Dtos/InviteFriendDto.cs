using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class InviteFriendDto
    {
        public Guid PassengerId { get; set; } 
        public string FriendEmail { get; set; }
    }
}
