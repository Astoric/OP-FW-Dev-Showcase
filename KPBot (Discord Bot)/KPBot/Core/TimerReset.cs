using System;
using System.Threading.Tasks;
using System.Timers;

namespace KPBot
{
    internal static class TimerReset
    {
        private static Timer loopingTimer;

        internal static Task StartTimer()
        {
            OnStartUp();

            loopingTimer = new Timer()
            {
                Interval = 86400000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        private static async void OnStartUp()
        {
            Console.WriteLine("Reset Timer started");
            
        }

        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Reset timer ticks from " + Global.TimerTicker + " to 0");
            Global.TimerTicker = 0;
            RepeatingTimer.StartTimer();
        }
    }
}