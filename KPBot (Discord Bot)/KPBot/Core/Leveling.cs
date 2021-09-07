using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBot.Core
{
    internal static class Leveling
    {
        internal static void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            var userAccount = UserAccounts.UserAccounts.GetAccount(user);
            userAccount.XP += 1;
            UserAccounts.UserAccounts.SaveAccounts();

            if (userAccount.XP == 100)
            {
                userAccount.active_points = userAccount.active_points + 10;
                userAccount.XP = 0;
                userAccount.total_points = userAccount.claim_points + userAccount.active_points + userAccount.admin_points;
                UserAccounts.UserAccounts.SaveAccounts();
            }
        }
    }
}
