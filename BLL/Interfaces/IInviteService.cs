using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface IInviteService
    {
        /// Allows a passenger to invite a friend by email or phone.
        Task<bool> InviteFriendAsync(InviteFriendDto invite);

        /// Applies a bonus or discount to the inviter's account
        /// once the invited friend registers successfully.
        Task<bool> ApplyBonusForInviteAsync(string friendEmail);
    }
}
