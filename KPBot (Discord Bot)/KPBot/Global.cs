using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Collections.Generic;
namespace KPBot
{
    public static class Global
    {
        internal static int claims_left { get; set; }
        internal static bool test = false;
        internal static bool claimable = false;
        internal static DiscordSocketClient Client { get; set; }
        internal static int TimerTicker { get; set; }
        internal static bool admintimer = false;
        internal static bool nuked = false;
        internal static int points { get; set; }
        internal static RestUserMessage candy { get; set; }
        internal static string candyimage { get; set; }
        internal static string candyname { get; set; }
        internal static List<ulong> lvlchan = new List<ulong> {
            716667961074778132, //gen
            694563239752761344, //horny
            710635927520673834, //cgl
            727193783271161968, //petplay
            729685013896888330, //subby
            754200861357637742 //Test server
        };
        internal static string claimer1 { get; set; }
        internal static string claimer2 { get; set; }
        internal static string claimer3 { get; set; }
        internal static List<ulong> logchan = new List<ulong>
        {
            697074678707126353,
            697074709828861962,
            697074725452513310,
            697074740535492638,
            697074762387816528,
            697074777902284921,
            697074790523076658,
            697074806247653436,
            697074825671213128,
            697074840749998190,
            697074887868678224,
            697074919405518948,
            697074908806643802,
            701115332512710726,
            721866247280525384
        };
        internal static List<ulong> vcs = new List<ulong> { };
    }
}