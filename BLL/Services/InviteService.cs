using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Services
{
    public class InviteService : IInviteService
    {
        // list لتخزين الدعوات 
        private readonly List<InviteFriendDto> _invites = new();

        public Task<bool> InviteFriendAsync(InviteFriendDto invite)
        {
            if (invite == null || string.IsNullOrWhiteSpace(invite.FriendEmail))
                return Task.FromResult(false);

            // بيعمل الدعوه بالموبايل او الايميل
            Console.WriteLine($"Passenger {invite.PassengerId} invited friend {invite.FriendEmail}");

            _invites.Add(invite);

            return Task.FromResult(true);
        }

        public Task<bool> ApplyBonusForInviteAsync(string friendEmail)
        {
            // بيتاكد انه تم دعوته فعلا
            var existingInvite = _invites.Find(i =>
                i.FriendEmail.Equals(friendEmail, StringComparison.OrdinalIgnoreCase));

            if (existingInvite == null)
                return Task.FromResult(false);

            // بيزود بونص أو خصم للراكب اللي عمل الدعوة 
            Console.WriteLine($"Bonus applied for passenger {existingInvite.PassengerId} after friend {friendEmail} registered.");

            return Task.FromResult(true);
        }
    }
}