using Discord;
using KPBot.Core.UserAccounts;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace KPBot
{
    internal static class RepeatingTimer
    {
        private static Timer loopingTimer;
        internal static Task StartTimer()
        {

            OnStartUp();

            loopingTimer = new Timer()
            {
                Interval = 1800000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        private static async void OnStartUp()
        {
            Console.WriteLine("Timer started");
        }

        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            var channelcount = 0;
            var usercount = 0;
            var channels = Global.Client.GetGuild(761027409667424277).VoiceChannels;
            foreach(var chan in channels)
            {
                channelcount++;
                var users = Global.Client.GetGuild(761027409667424277).GetVoiceChannel(chan.Id).Users;
                if (chan.Id != 694977444305961041)
                {
                    foreach (var user in users)
                    {
                        usercount++;
                        var acc = UserAccounts.GetAccount(Global.Client.GetUser(user.Id));
                        acc.active_points = acc.active_points + 5;
                        acc.total_points = acc.claim_points + acc.active_points + acc.admin_points;
                    }
                }
            }
            UserAccounts.SaveAccounts();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Given " + (usercount * 5) + " points to " + usercount + " users in " + channelcount + " channels.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}