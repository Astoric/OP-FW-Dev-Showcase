using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using KPBot.Core;
using KPBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using KPBot;

namespace KPBot
{
    internal class Program
    {
        private DiscordSocketClient _client;
        private CommandService commands;
        private IServiceProvider services;
        internal static int adminactions = 0;
        internal static string token;

        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("  _  ___       _      _____  _                                             _ ");
            Console.WriteLine(" | |/ (_)     | |    |  __ \\| |                                           | |");
            Console.WriteLine(" | ' / _ _ __ | | __ | |__) | | __ _ _   _  __ _ _ __ ___  _   _ _ __   __| |");
            Console.WriteLine(" |  < | | '_ \\| |/ / |  ___/| |/ _` | | | |/ _` | '__/ _ \\| | | | '_ \\ / _` |");
            Console.WriteLine(" | . \\| | | | |   <  | |    | | (_| | |_| | (_| | | | (_) | |_| | | | | (_| |");
            Console.WriteLine(" |_|\\_\\_|_| |_|_|\\_\\ |_|    |_|\\__,_|\\__, |\\__, |_|  \\___/ \\__,_|_| |_|\\__,_|");
            Console.WriteLine("                                      __/ | __/ |                            ");
            Console.WriteLine("                                     |___/ |___/                             ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please input any string to start test");
            System.Timers.Timer timer = new System.Timers.Timer(5000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(T_Elapsed);
            timer.AutoReset = false;
            timer.Start();
            var i = Console.ReadLine();
            if (!string.IsNullOrEmpty(i))
            {
                Global.test = true;
                token = "NDU3MjU3OTIyODUwODQ4Nzcw.WyQMoA.6sjihYuVk8SpGOxU2DBZ-YIJuw4"; // Token no longer active
                Console.WriteLine("Launching test");
                Startup();
                timer.Stop();
            }
        }


        static void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Global.test)
            {
                Console.ForegroundColor = ConsoleColor.White;
                token = "NzU3Nzc4MjAwMTM3ODI2MzQ0.X2lV8Q.Eqag-VIJZ6tMu0i5bPymIlacLyQ"; // Token no longer active
                Console.WriteLine("Launching live");
                Startup();
            }
        }

        private static void Startup()
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            _client.Log += Log;
            Global.TimerTicker = 0;
            _client.Ready += fillVoice;
            _client.ChannelDestroyed += OnChannelDeletion;
            _client.RoleDeleted += onRoleDeletion;
            _client.MessageDeleted += onMsgDeletion;
            _client.UserVoiceStateUpdated += onVoiceStateUpdate;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            Global.Client = _client;

            services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<InteractiveService>()
                .BuildServiceProvider();

            commands = new CommandService();
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            _client.MessageReceived += HandleCommandAsync;
            await Task.Delay(-1);
        }

        private async Task onVoiceStateUpdate(SocketUser user, SocketVoiceState state1, SocketVoiceState state2)
        {
            try
            {
                //var vcs = VoiceChats.GetVoiceChats();
                //if (user.Id == 198948853611757568) { await user.SendMessageAsync(state1.ToString() + " " + state2.ToString()); }

                if (state1.ToString() == "Unknown")
                {
                    goto makechan;
                }
                else
                {
                    if (Global.vcs.Contains(state1.VoiceChannel.Id))
                    {
                        if (state1.VoiceChannel.Users.Count < 1)
                        {
                            await removeVoice(state1.VoiceChannel.Id);
                            await state1.VoiceChannel.DeleteAsync();
                        }
                    }
                }
                makechan:
                if (state2.VoiceChannel.Id == 694566672446521374)
                {
                    var admin = new OverwritePermissions(
                        manageChannel: PermValue.Allow
                                );
                    var muted = new OverwritePermissions(
                        speak: PermValue.Deny
                        );
                    var newcomer = new OverwritePermissions(
                        viewChannel: PermValue.Deny,
                        connect: PermValue.Deny
                        );
                    var unver = new OverwritePermissions(
                        viewChannel: PermValue.Deny,
                        connect: PermValue.Deny,
                        speak: PermValue.Deny
                        );
                    var ver = new OverwritePermissions(
                        viewChannel: PermValue.Allow,
                        connect: PermValue.Allow,
                        speak: PermValue.Allow
                        );
                    var everyone = new OverwritePermissions(
                        muteMembers: PermValue.Deny,
                        deafenMembers: PermValue.Deny
                        );
                    var creator = new OverwritePermissions(
                        manageChannel: PermValue.Allow,
                        manageRoles: PermValue.Allow
                        );
                    var channame = "";
                    if((user as SocketGuildUser).Nickname != null)
                    {
                        channame = (user as SocketGuildUser).Nickname + "'s Channel";
                    }
                    else
                    {
                        channame = (user as SocketGuildUser).Username + "'s Channel";
                    }
                    var newchan = await Global.Client.GetGuild(688129483387306059).CreateVoiceChannelAsync(channame, null, null);
                    await newchan.ModifyAsync(x => x.CategoryId = 694566634429612115);
                    await (user as SocketGuildUser).ModifyAsync(x => x.Channel = newchan);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821), admin);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).GetRole(696531972276748338), muted);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).GetRole(696145056553238610), newcomer);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).GetRole(696570779889958912), unver);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).GetRole(696074197872672830), ver);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).GetRole(759624101982896191), ver);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetGuild(688129483387306059).EveryoneRole, everyone);
                    await newchan.AddPermissionOverwriteAsync(Global.Client.GetUser(user.Id), creator);
                    await WriteVoice(newchan.Id);
                }
            }
            catch(Exception e)
            {
                
            }
        }

        private async Task onMsgDeletion(Cacheable<IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            //var role = _client.GetGuild(688129483387306059).GetRole(694564080635084821);
            //var msg = arg1.GetOrDownloadAsync();
            //IEnumerable<RestAuditLogEntry> logs = null;
            //try
            //{
            //    logs = await _client.GetGuild(688129483387306059).GetAuditLogsAsync(1).FlattenAsync();
            //}
            //catch (Exception e)
            //{
            //}
            //var entry = logs?.FirstOrDefault(x => (x.Data as MessageDeleteAuditLogData)?.ChannelId == arg2.Id);
            //var embed = new EmbedBuilder();
            //embed.WithColor(new Color(255, 0, 0));
            //embed.WithTitle("Log Deleted.");
            //embed.WithDescription(entry.User + " deleted a log. Admin perms have been removed.");
            ////await _client.GetGuild(688129483387306059).GetUser(entry.User.Id).RemoveRoleAsync(role);
            //await _client.GetGuild(688129483387306059).GetTextChannel(758586686564204575).SendMessageAsync("", false, embed.Build());
        }

        private async Task onChannelUpdate(SocketChannel arg1, SocketChannel arg2)
        {
            adminactions++;
            if (adminactions > 3)
            {
                Global.nuked = true;
                var embed = new EmbedBuilder();
                embed.WithColor(new Color(255, 0, 0));
                embed.WithTitle("Possible Nuke Attempt");
                embed.WithDescription(Global.Client.GetGuild(688129483387306059).GetUser(359505097270624256).Mention);
                await Global.Client.GetGuild(688129483387306059).GetTextChannel(758586686564204575).SendMessageAsync("", false, embed.Build());
                var nukedperms = new GuildPermissions(true, false, false, false, false, false, true, false, true, true, false, false, true, true, true, false, true, true, true, false, false, false, true, true, true, true, false, false, false, false);
                var role = Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821);
                await role.ModifyAsync(x => x.Permissions = nukedperms);
                adminactions = 0;
            }
            if (!Global.admintimer)
            {
                await AdminTimer.StartTimer();
            }
        }

        private async Task onRoleDeletion(SocketRole arg)
        {
            adminactions++;
            if (adminactions > 3)
            {
                Global.nuked = true;
                var embed = new EmbedBuilder();
                embed.WithColor(new Color(255, 0, 0));
                embed.WithTitle("Possible Nuke Attempt");
                embed.WithDescription(Global.Client.GetGuild(688129483387306059).GetUser(359505097270624256).Mention);
                await Global.Client.GetGuild(688129483387306059).GetTextChannel(688129483915395312).SendMessageAsync("", false, embed.Build());
                var nukedperms = new GuildPermissions(true, false, false, false, false, false, true, false, true, true, false, false, true, true, true, false, true, true, true, false, false, false, true, true, true, true, false, false, false, false);
                var role = Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821);
                await role.ModifyAsync(x => x.Permissions = nukedperms);
                adminactions = 0;
            }
            if (!Global.admintimer)
            {
                await AdminTimer.StartTimer();
            }
        }

        private async Task OnChannelDeletion(SocketChannel chn)
        {
            adminactions++;
            if (adminactions > 3)
            {
                Global.nuked = true;
                var embed = new EmbedBuilder();
                embed.WithColor(new Color(255, 0, 0));
                embed.WithTitle("Possible Nuke Attempt");
                embed.WithDescription(Global.Client.GetGuild(688129483387306059).GetUser(359505097270624256).Mention);
                await Global.Client.GetGuild(688129483387306059).GetTextChannel(688129483915395312).SendMessageAsync("", false, embed.Build());
                var nukedperms = new GuildPermissions(true, false, false, false, false, false, true, false, true, true, false, false, true, true, true, false, true, true, true, false, false, false, true, true, true, true, false, false, false, false);
                var role = Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821);
                await role.ModifyAsync(x => x.Permissions = nukedperms);
                adminactions = 0;
            }
            if (!Global.admintimer)
            {
                await AdminTimer.StartTimer();
            }
        }



        private async Task HandleCommandAsync(SocketMessage s)
        {
            try
            {
                var msg = s as SocketUserMessage;
                if (msg == null) return;
                var context = new SocketCommandContext(_client, msg);
                if (context.User.IsBot) return;
                int argPos = 0;
                var acc = UserAccounts.GetAccount(context.User);
                if ((msg.HasStringPrefix("!", ref argPos) && (context.Guild == null || context.Guild.Id != 377879473158356992)) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
                {

                    if (acc.blacklisted) { await context.Channel.SendMessageAsync(context.User.Mention + " You are banned from taking part in the Spooktober Festival."); return; }
                    if (context.Channel.Id != 694968065644822570)
                    {
                        var result = await commands.ExecuteAsync(context, argPos, services);
                        if (result.Error != null)
                        {
                            Console.WriteLine(result.ErrorReason);
                        }
                    }
                }
                else if(msg.Channel.Id == 696851137336180786 || msg.Channel.Id == 759628061276241960)
                {
                    int count = 0;
                    foreach (var user in Global.Client.GetGuild(688129483387306059).Users)
                    {
                        if (user.Roles.Contains(Global.Client.GetGuild(688129483387306059).GetRole(696074197872672830)) || user.Roles.Contains(Global.Client.GetGuild(688129483387306059).GetRole(759624101982896191)))
                        {
                            count++;
                        }
                    }
                    var chan = Global.Client.GetGuild(688129483387306059).GetVoiceChannel(762204761902546944);
                    await chan.ModifyAsync(x => x.Name = "❥ Verified: " + count);
                }
                else
                {
                    if (!acc.blacklisted)
                    {
                        if (Global.lvlchan.Contains(context.Channel.Id))
                        {
                            Leveling.UserSentMessage((context.User as SocketGuildUser), (context.Channel as SocketTextChannel));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }

        public async Task WriteVoice(ulong id)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\decla\Desktop\Files\DiscordBots\Kink Bot\Signal Bot\bin\Debug\Config\vcs.txt");
            text = text + id + ",";
            System.IO.File.WriteAllText(@"C:\Users\decla\Desktop\Files\DiscordBots\Kink Bot\Signal Bot\bin\Debug\Config\vcs.txt", text);
            Global.vcs.Add(id);
        }

        private async Task fillVoice()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\decla\Desktop\Files\DiscordBots\Kink Bot\Signal Bot\bin\Debug\Config\vcs.txt");
            if (text != null)
            {
                string[] ids = text.Split(',');
                foreach (var id in ids)
                {
                    Global.vcs.Add(UInt64.Parse(id));
                }
            }
        }

        private async Task removeVoice(ulong id)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\decla\Desktop\Files\DiscordBots\Kink Bot\Signal Bot\bin\Debug\Config\vcs.txt");
            string sid = id.ToString() + ",";
            text = text.Replace(sid, "");
            System.IO.File.WriteAllText(@"C:\Users\decla\Desktop\Files\DiscordBots\Kink Bot\Signal Bot\bin\Debug\Config\vcs.txt", String.Empty);
            System.IO.File.WriteAllText(@"C:\Users\decla\Desktop\Files\DiscordBots\Kink Bot\Signal Bot\bin\Debug\Config\vcs.txt", text);
        }
    }
}