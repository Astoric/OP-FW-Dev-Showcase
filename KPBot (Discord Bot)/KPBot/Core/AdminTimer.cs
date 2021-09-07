using System;
using System.Threading.Tasks;
using System.Timers;

namespace KPBot
{
    internal static class AdminTimer
    {
        private static Timer loopingTimer;

        internal static Task StartTimer()
        {

            OnStartUp();

            loopingTimer = new Timer()
            {
                Interval = 180000,
                AutoReset = false,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        private static async void OnStartUp()
        {
            Console.WriteLine("Admin Timer started");
            Global.admintimer = true;
            
        }

        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            Program.adminactions = 0;
            Console.WriteLine("Reset Admin Action Limit");
            Global.admintimer = false;
        }
    }
}