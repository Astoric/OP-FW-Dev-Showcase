using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using KPBot.Core;
using KPBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KPBot.Modules
{
    public class Commands : InteractiveBase
    {

        [Command("blacklist")]
        public async Task blacklist(ulong id)
        {
            if (!(Context.User as SocketGuildUser).Roles.Contains(Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821)) && Context.User.Id != 198948853611757568) { await Context.Message.DeleteAsync(); return; }
            var acc = UserAccounts.GetAccount(Global.Client.GetUser(id));
            if(acc != null)
            {
                if (acc.blacklisted)
                {
                    acc.blacklisted = false;
                    await ReplyAsync(Global.Client.GetUser(id).Mention + " has been unblacklisted.");
                }
                else
                {
                    acc.blacklisted = true;
                    await ReplyAsync(Global.Client.GetUser(id).Mention + " has been blacklisted.");
                }
                UserAccounts.SaveAccounts();
            }
        }
        //[Command("editpoints")]
        //public async Task editPoints(ulong id, string type, int amount)
        //{
        //    if (!(Context.User as SocketGuildUser).Roles.Contains(Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821)) && Context.User.Id != 198948853611757568) { await Context.Message.DeleteAsync(); return; }
        //    try
        //    {
        //        var user = Global.Client.GetGuild(Context.Guild.Id).GetUser(id);
        //        try
        //        {
        //            var acc = UserAccounts.GetAccount(user);
        //            if(type.ToLower() == "add")
        //            {
        //                acc.admin_points = acc.admin_points + amount;
        //                acc.total_points = acc.admin_points + acc.active_points + acc.claim_points;
        //                UserAccounts.SaveAccounts();
        //                await ReplyAsync("Points updated for " + user.Mention);
        //            }
        //            else if(type.ToLower() == "remove")
        //            {
        //                acc.admin_points = acc.admin_points - amount;
        //                acc.total_points = acc.admin_points + acc.active_points + acc.claim_points;
        //                UserAccounts.SaveAccounts();
        //                await ReplyAsync("Points updated for " + user.Mention);
        //            }
        //            else if(type.ToLower() == "set")
        //            {
        //                acc.claim_points = 0;
        //                acc.active_points = 0;
        //                acc.admin_points = amount;
        //                acc.total_points = acc.admin_points + acc.active_points + acc.claim_points;
        //                UserAccounts.SaveAccounts();
        //                await ReplyAsync("Points updated for " + user.Mention);
        //            }
        //            else
        //            {
        //                await ReplyAsync("You can only Add, Remove and Set points.");
        //            }
        //        }
        //        catch(Exception e)
        //        {

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        await ReplyAsync("User not found");
        //    }
        //}

        [Command("resetnuke")]
        public async Task resetNuke()
        {
            if(Context.User.Id != 198948853611757568 & Context.User.Id != 359505097270624256) { await Context.Message.DeleteAsync(); return; }
            Global.admintimer = false;
            Program.adminactions = 0;
            var perms = new GuildPermissions(true, true, true, false, true, true, true, false, true, true, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, true);
            var role = Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821);
            await role.ModifyAsync(x => x.Permissions = perms);
        }

        [Command("updateaccounts")]
        [RequireOwner]
        public async Task updateAccounts()
        {
            var accs = UserAccounts.GetAllAccounts();
            foreach(var item in accs)
            {
                item.total_points = item.claim_points + item.active_points;
            }
            UserAccounts.SaveAccounts();
            await ReplyAsync("Accounts Updated.");
        }

        //[Command("spawn")]
        //public async Task spawnCandy(int spawncode = 0)
        //{
        //    if (Context.Guild.Id != 761027409667424277)
        //    {
        //        if (!(Context.User as SocketGuildUser).Roles.Contains(Global.Client.GetGuild(688129483387306059).GetRole(694564080635084821)) && Context.User.Id != 198948853611757568) { await Context.Message.DeleteAsync(); return; }
        //    }
        //    if (Global.claimable)
        //    {
        //        await ReplyAsync("You cannot spawn another item while the previous is claimable.");
        //        return;
        //    }
        //    int candy = spawncode;
        //    if (candy == 0)
        //    {
        //        Random rnd = new Random();
        //        candy = rnd.Next(1, 22);
        //    }
        //    await Context.Message.DeleteAsync();
        //    Global.claimable = true;
        //    Global.claims_left = 3;
        //    Global.claimer1 = null;
        //    Global.claimer2 = null;
        //    Global.claimer3 = null;
        //    var embed = new EmbedBuilder();
        //    embed.WithColor(new Color(239, 173, 255));
        //    if(candy == 1)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Witches Hat";
        //        embed.WithTitle("A " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/i/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpaj02-13aec48b-6658-459e-87db-4af0fb64c5ec.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 2)
        //    {
        //        Global.points = 5 + 3;
        //        Global.candyname = "Witches Cauldron";
        //        embed.WithTitle("A " + Global.candyname + " has magically appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/i/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpaiiw-19837a7d-1c5c-4a66-a5b9-edbb7df88640.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 3)
        //    {
        //        Global.points = 5 + 5;
        //        Global.candyname = "Jack-O-Lantern";
        //        embed.WithTitle("A " + Global.candyname + " has rolled into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/i/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpahv9-d045e232-1e0d-42cd-8063-b214eb717c93.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 4)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Evil Kitty";
        //        embed.WithTitle("A " + Global.candyname + " has waddled into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/i/47c1cda0-07b2-47be-b382-c36180fd8080/dbm4tz3-27b65775-c9e1-4a37-bf04-ec5a3dffbb40.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 5)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Bat";
        //        embed.WithTitle("A " + Global.candyname + " has flew into chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/i/cf2836cb-5893-4a6c-b156-5a89d94fc721/db4kqky-457afc81-2167-4e1a-970b-ee3ede818e07.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 6)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Zombie Bunny";
        //        embed.WithTitle("A " + Global.candyname + " has hopped into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/8c90b6d1-61b4-424e-8623-0aae5f024ef9/ddub4ke-ad2c86d4-3b49-4f4b-a364-84e48ccda441.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvOGM5MGI2ZDEtNjFiNC00MjRlLTg2MjMtMGFhZTVmMDI0ZWY5XC9kZHViNGtlLWFkMmM4NmQ0LTNiNDktNGY0Yi1hMzY0LTg0ZTQ4Y2NkYTQ0MS5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.AOGd3rf5MsBEJjJlj0tHAAiL57KbDuKoQ4dtZ9Gep1I";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 7)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Crystal Ball";
        //        embed.WithTitle("A " + Global.candyname + " has rolled into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpkc1q-7c102ea8-adc3-4ffa-bf29-e911c89c74ab.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kY3BrYzFxLTdjMTAyZWE4LWFkYzMtNGZmYS1iZjI5LWU5MTFjODljNzRhYi5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.Ag1HS3n7srEcO-VFp9iWYX_4STyu--t8_titaUUlQ4U";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 8)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Spooky Candles";
        //        embed.WithTitle("Some " + Global.candyname + " have appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpainj-00658075-fa30-448c-903d-1392e6043b90.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kY3BhaW5qLTAwNjU4MDc1LWZhMzAtNDQ4Yy05MDNkLTEzOTJlNjA0M2I5MC5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.irg2LZlZgArpSyTvzb_NAyWFZ54_vMzQFH9tz9BB8-U";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 9)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Witch's Kitty";
        //        embed.WithTitle("A " + Global.candyname + " has waddled into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpaiuk-40c95fc3-46d4-4823-a69b-b0c490a8b4aa.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kY3BhaXVrLTQwYzk1ZmMzLTQ2ZDQtNDgyMy1hNjliLWIwYzQ5MGE4YjRhYS5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.tgaB8DtvdmVWjSFH7KbvlZQKjMx-9asphak92DMWu50";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 10)
        //    {
        //        Global.points = 5 + 3;
        //        Global.candyname = "Ouija Planchette (Thanks pup for making me spell that one!)";
        //        embed.WithTitle("A " + Global.candyname + " has floated into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/0d25b279-6e5a-4e71-b317-58689e3c13a4/dc5sdmo-b07be772-ce55-40d8-a3d1-d2376f9d79ca.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvMGQyNWIyNzktNmU1YS00ZTcxLWIzMTctNTg2ODllM2MxM2E0XC9kYzVzZG1vLWIwN2JlNzcyLWNlNTUtNDBkOC1hM2QxLWQyMzc2ZjlkNzljYS5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.cPfCRaQs_KWeZHylaM_HRV4Q7hzvIecT4327lz7WZTg";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 11)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Stack of Witch's Spell Books";
        //        embed.WithTitle("A " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcphco0-f9305500-6b8a-4457-b0cc-b98f76c02fe2.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kY3BoY28wLWY5MzA1NTAwLTZiOGEtNDQ1Ny1iMGNjLWI5OGY3NmMwMmZlMi5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.jYBR6KvgWqudXwCSllmdXDj6tsCV2umSJkXZJQPFX_k";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 12)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Candy Corn";
        //        embed.WithTitle("Some " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/dcpahqd-6f2760a6-af43-4b79-997c-4fcf7899cfd2.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kY3BhaHFkLTZmMjc2MGE2LWFmNDMtNGI3OS05OTdjLTRmY2Y3ODk5Y2ZkMi5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.vAtcjZ7sNWuMyyTykXxlXHoD09o0wM7uyut3u5wRows";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 13)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Spooky Ghost";
        //        embed.WithTitle("Larry the " + Global.candyname + " has floated into the chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://i.imgur.com/gt2Jo39.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 14)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Spooky Ghost";
        //        embed.WithTitle("Mary the " + Global.candyname + " has floated into chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://i.imgur.com/t1dtlRs.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 15)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Spooky Ghost";
        //        embed.WithTitle("Deobrah the " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://i.imgur.com/hh4aGyq.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 16)
        //    {
        //        Global.points = 5 + 1;
        //        Global.candyname = "Spooky Ghost";
        //        embed.WithTitle("Layton the " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://i.imgur.com/vCWnwWu.gif";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 17)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Devil Set";
        //        embed.WithTitle("A " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/c1f51a87-d4ad-4797-bf3f-287feb70d165/db41msz-670cc3d6-ffad-4216-bf42-8fc2fbefe049.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvYzFmNTFhODctZDRhZC00Nzk3LWJmM2YtMjg3ZmViNzBkMTY1XC9kYjQxbXN6LTY3MGNjM2Q2LWZmYWQtNDIxNi1iZjQyLThmYzJmYmVmZTA0OS5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.cV_mRolXLzmpLV7GemN5Kgd8j3tuCkyh_G6AMUTW-KA";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 18)
        //    {
        //        Global.points = 5 + 2;
        //        Global.candyname = "Sad Ghost";
        //        embed.WithTitle("Aww, a " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/c1f51a87-d4ad-4797-bf3f-287feb70d165/dbff6vo-6cac828f-1e00-4dbe-b04b-b35ecb7181d0.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvYzFmNTFhODctZDRhZC00Nzk3LWJmM2YtMjg3ZmViNzBkMTY1XC9kYmZmNnZvLTZjYWM4MjhmLTFlMDAtNGRiZS1iMDRiLWIzNWVjYjcxODFkMC5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.UWnySLMrMaoFzT8gyoPw7SSh-Ooci1KD6mWRmJV0yPQ";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 19)
        //    {
        //        Global.points = 5 + 5;
        //        Global.candyname = "Magik Sneke";
        //        embed.WithTitle("A " + Global.candyname + " has slithered it's way into chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/bef0daba-c602-4369-8f26-b2c45acbeac9/db1lpp3-6fdafbcf-9b4d-4d68-a00e-53d1f2f4b884.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvYmVmMGRhYmEtYzYwMi00MzY5LThmMjYtYjJjNDVhY2JlYWM5XC9kYjFscHAzLTZmZGFmYmNmLTliNGQtNGQ2OC1hMDBlLTUzZDFmMmY0Yjg4NC5wbmcifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.tiJr7wCh51XMgIkG2OpTeLMp9obAlj06kDg_GcpxMCM";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 20)
        //    {
        //        Global.points = 5 + 3;
        //        Global.candyname = "haunted love letter";
        //        embed.WithTitle("A " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/db4dc6c-1155f828-d378-4772-b532-fa9dd8af8ced.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kYjRkYzZjLTExNTVmODI4LWQzNzgtNDc3Mi1iNTMyLWZhOWRkOGFmOGNlZC5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.DG21pCEeceKkGgBVTn0SM-EG_O3pdFsFwW3vMtomRmY";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //    if (candy == 21)
        //    {
        //        Global.points = 5 + 3;
        //        Global.candyname = "Carton of Blood";
        //        embed.WithTitle("A " + Global.candyname + " has appeared in chat!");
        //        embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). Not Claimed\n2). Not Claimed\n3). Not Claimed");
        //        Global.candyimage = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/cf2836cb-5893-4a6c-b156-5a89d94fc721/db4tdsr-56c3be10-ce9e-41bd-bc05-964c2e80483d.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvY2YyODM2Y2ItNTg5My00YTZjLWIxNTYtNWE4OWQ5NGZjNzIxXC9kYjR0ZHNyLTU2YzNiZTEwLWNlOWUtNDFiZC1iYzA1LTk2NGMyZTgwNDgzZC5naWYifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ._-PKSe01fiF54S5bGZ81MooxZSXVpOhpmtXis9T4yo0";
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        Global.candy = await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //}

        //[Command("claim")]
        //public async Task claimCandy()
        //{
        //    if (Global.claimable & Global.claims_left > 0 & Global.claimer1 != Context.User.Username & Global.claimer2 != Context.User.Username & Global.claimer3 != Context.User.Username)
        //    {
        //        var embed = new EmbedBuilder();
        //        embed.WithColor(new Color(239, 173, 255));
        //        embed.WithThumbnailUrl(Global.candyimage);
        //        if (Global.claims_left == 3)
        //        {
        //            Global.claimer1 = Context.User.Username;
        //            var acc = UserAccounts.GetAccount(Context.User);
        //            acc.claim_points = acc.claim_points + Global.points;
        //            acc.total_points = acc.admin_points + acc.active_points + acc.claim_points;
        //            UserAccounts.SaveAccounts();
        //            Global.points = Global.points - 2;
        //            embed.WithTitle("1/3 Points Claimed!");
        //            embed.WithDescription("use !claim to claim the " + Global.candyname + " for " + Global.points + " points!\n\n1). " + Global.claimer1 + " (" + (Global.points + 2) + " Points!)\n2). Not Claimed\n3). Not Claimed");
        //        }
        //        if (Global.claims_left == 2)
        //        {
        //            Global.claimer2 = Context.User.Username;
        //            var acc = UserAccounts.GetAccount(Context.User);
        //            acc.claim_points = acc.claim_points + Global.points;
        //            acc.total_points = acc.admin_points + acc.active_points + acc.claim_points;
        //            UserAccounts.SaveAccounts();
        //            Global.points = Global.points - 3;
        //            embed.WithTitle("2/3 Points Claimed!");
        //            embed.WithDescription("use !claim to claim the " + Global.candyname + " for "  + Global.points + " points!\n\n1). " + Global.claimer1 + " (" + (Global.points + 5) + " Points!)\n2). " + Global.claimer2 + " (" + (Global.points + 3) + " Points!)\n3). Not Claimed");
        //        }
        //        if (Global.claims_left == 1)
        //        {
        //            Global.claimer3 = Context.User.Username;
        //            var acc = UserAccounts.GetAccount(Context.User);
        //            acc.claim_points = acc.claim_points + Global.points;
        //            acc.total_points = acc.admin_points + acc.active_points + acc.claim_points;
        //            UserAccounts.SaveAccounts();
        //            embed.WithTitle("3/3 Points Claimed!");
        //            embed.WithDescription("The " + Global.candyname + " has been claimed! Congrats to the winners!\n\n1). " + Global.claimer1 + " (" + (Global.points + 5) + " Points!)\n2). " + Global.claimer2 + " (" + (Global.points + 3) + " Points!)\n3). " + Global.claimer3 + " (" + Global.points + " Points!)");
        //            Global.claimable = false;
        //        }
        //        Global.claims_left--;
        //        await Global.candy.ModifyAsync(x =>
        //        {
        //            x.Embed = embed.Build();
        //        });
        //    }
        //    else
        //    {
        //        await Context.Message.DeleteAsync();
        //    }
        //}

        [Command("audit")]
        public async Task getaudit()
        {
            var audit = Global.Client.GetGuild(Context.Guild.Id).GetAuditLogsAsync(10, null, null, null, ActionType.ChannelDeleted);
            Console.WriteLine(audit);
        }

        //[Command("track")]
        //public async Task xpTracker()
        //{
        //    var acc = UserAccounts.GetAccount(Context.User);
        //    var embed = new EmbedBuilder();
        //    embed.WithTitle(Context.User.Username + "'s Spooktober Stats!");
        //    embed.WithDescription("You have " + acc.XP + " Spooktober Fest XP! This means you only need " + (100 - acc.XP).ToString() + " XP until you get 10 bonus points!");
        //    embed.AddField("Claimed Points:", acc.claim_points, true);
        //    embed.AddField("Activity Points:", acc.active_points, true);
        //    embed.AddField("Total Points:", acc.total_points, true);
        //    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
        //    embed.WithColor(new Color(239, 173, 255));
        //    if (Context.Guild.Id == 688129483387306059 && Context.Channel.Id != 761801611995512892) { await Context.Message.DeleteAsync(); await ReplyAndDeleteAsync("This command can only be used in the " + Global.Client.GetGuild(688129483387306059).GetTextChannel(761801611995512892).Mention + " channel.", false, null, timeout: TimeSpan.FromSeconds(5)); return; }
        //    await Context.Channel.SendMessageAsync("", false, embed.Build());
        //}

        [Command("spooktober")]
        [RequireOwner]
        public async Task Leader()
        {
            try
            {
                int count = 1;
                var eb = new EmbedBuilder();
                var listedusers1 = UserAccounts.GetAllAccounts().OrderByDescending(x => x.total_points).ToList().Take(10);
                var listedusers2 = UserAccounts.GetAllAccounts().OrderByDescending(x => x.total_points).ToList().Take(20);
                var listedusers3 = UserAccounts.GetAllAccounts().OrderByDescending(x => x.total_points).ToList().Take(30);
                var listedusers4 = UserAccounts.GetAllAccounts().OrderByDescending(x => x.total_points).ToList().Take(40);
                var listedusers5 = UserAccounts.GetAllAccounts().OrderByDescending(x => x.total_points).ToList().Take(50);
                StringBuilder builder1 = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                StringBuilder builder3 = new StringBuilder();
                StringBuilder builder4 = new StringBuilder();
                StringBuilder builder5 = new StringBuilder();
                eb.WithTitle(":jack_o_lantern: Kink Playground Spooktober Fest Leaderboard :jack_o_lantern:");
                eb.WithColor(new Color(239, 173, 255));
                foreach (var user in listedusers1)
                {
                    try
                    {
                        var user_id = user.ID.ToString();
                        var user_name = Context.Client.GetUser(user.ID).Username;
                        builder1.Append("**" + count.ToString() + ".** __**" + user_name + "" + "**__ • :jack_o_lantern: " + user.total_points).AppendLine();
                        count++;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(user.ID + " not found. Skipping.");
                    }
                }
                foreach (var user in listedusers2)
                {
                    if (!listedusers1.Contains(user))
                    {
                        try
                        {
                            var user_id = user.ID.ToString();
                            var user_name = Context.Client.GetUser(user.ID).Username;
                            builder2.Append("**" + count.ToString() + ".** __**" + user_name + "" + "**__ • :jack_o_lantern: " + user.total_points).AppendLine();
                            count++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(user.ID + " not found. Skipping.");
                        }
                    }
                }
                foreach (var user in listedusers3)
                {
                    if (!listedusers1.Contains(user) && !listedusers2.Contains(user))
                    {
                        try
                        {
                            var user_id = user.ID.ToString();
                            var user_name = Context.Client.GetUser(user.ID).Username;
                            builder3.Append("**" + count.ToString() + ".** __**" + user_name + "" + "**__ • :jack_o_lantern: " + user.total_points).AppendLine();
                            count++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(user.ID + " not found. Skipping.");
                        }
                    }

                }
                foreach (var user in listedusers4)
                {
                    if (!listedusers1.Contains(user) && !listedusers2.Contains(user) && !listedusers3.Contains(user))
                    {
                        try
                        {
                            var user_id = user.ID.ToString();
                            var user_name = Context.Client.GetUser(user.ID).Username;
                            builder4.Append("**" + count.ToString() + ".** __**" + user_name + "" + "**__ • :jack_o_lantern: " + user.total_points).AppendLine();
                            count++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(user.ID + " not found. Skipping.");
                        }
                    }

                }
                foreach (var user in listedusers5)
                {
                    if (!listedusers1.Contains(user) && !listedusers2.Contains(user) && !listedusers3.Contains(user) && !listedusers4.Contains(user))
                    {
                        try
                        {
                            var user_id = user.ID.ToString();
                            var user_name = Context.Client.GetUser(user.ID).Username;
                            builder5.Append("**" + count.ToString() + ".** __**" + user_name + "" + "**__ • :jack_o_lantern: " + user.total_points).AppendLine();
                            count++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(user.ID + " not found. Skipping.");
                        }
                    }

                }
                eb.WithDescription(builder1.ToString());
                var pages = new[] { "" + builder1, "" + builder2, "" + builder3, "" + builder4, "" + builder5 };
                if(Context.Guild.Id == 688129483387306059 && Context.Channel.Id != 761801611995512892 && Context.Channel.Id != 757762375771357244) { await Context.Message.DeleteAsync();  await ReplyAndDeleteAsync("This command can only be used in the " + Global.Client.GetGuild(688129483387306059).GetTextChannel(761801611995512892).Mention + " channel.", false, null, timeout: TimeSpan.FromSeconds(5)); return; }
                await LbReplyAsync(pages);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Context.Channel.SendMessageAsync("There was an error with the leaderboard command. Please contact " + Context.Guild.GetUser(198948853611757568).Mention + "!");
            }
        }

        //[Command("itemlist")]
        //[RequireUserPermission(GuildPermission.BanMembers)]
        //public async Task ItemSpawns()
        //{
        //    var pages = new[] { "**1** - Witches Hat | 7 Points\n**2** - Witches Cauldron | 8 Points\n**3** - Jack-O-Lantern | 10 Points\n**4** - Evil Kitty | 6 Points\n**5** - Bat | 6 Points\n**6** - Zombie Bunny | 7 Points\n**7** - Crystal Ball | 7 Points\n**8** - Spooky Candles | 6 Points\n**9** - Witch's Kitty | 7 Points\n**10** - Ouija Planchette | 8 Points", "**11** - Stack of Witch's Spell Books | 6 Points\n**12** - Candy Corn | 7 Points\n**13** - Spooky Ghost | 6 Points\n**14** - Spooky Ghost | 6 Points\n**15** - Spooky Ghost | 6 Points\n**16** - Spooky Ghost | 6 Points\n**17** - Devil Set | 7 Points\n**18** - Sad Ghost | 7 Points\n**19** - Magik Sneke | 10 Points\n**20** - haunted love letter | 8 Points", "**21** - Carton of Blood | 8 Points" };
        //    await ItemReplyAsync(pages);
        //}

        [Command("addrole")]
        [RequireOwner]
        public async Task addrole(SocketRole newrole)
        {
            var users = Context.Guild.Users;
            int count = 0;
            foreach(var user in users)
            {
                if((user.Roles.Contains(Context.Guild.GetRole(696074197872672830)) || user.Roles.Contains(Context.Guild.GetRole(759624101982896191))) && !user.Roles.Contains(Context.Guild.GetRole(762196097723793428)))
                {
                    await user.AddRoleAsync(newrole);
                    count++;
                }
            }
            await ReplyAsync("Added role to " + count + " users.");
        }

        [Command("banner")]
        public async Task banner()
        {
            await ReplyAsync(Context.Guild.BannerUrl);
        }

        [Command("checkboosts")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task checkBoosts()
        {
            StringBuilder builder1 = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            List<ulong> broles = new List<ulong> { };
            List<ulong> rusers = new List<ulong> { };
            List<ulong> nonpusers = new List<ulong> { };
            foreach(var role in Context.Guild.Roles)
            {
                if(role.Position > Context.Guild.GetRole(699010254331183164).Position && role.Position < Context.Guild.GetRole(696531972276748338).Position)
                {
                    broles.Add(role.Id);
                    var members = role.Members;
                    int count = 0;
                    foreach (var item in members)
                    {
                        count++;
                    }
                    if(count == 0)
                    {
                        builder1.Append(role.Name).AppendLine();
                    }
                }
            }
            foreach(var usr in Context.Guild.Users)
            {
                foreach(var urole in usr.Roles)
                {
                    if (broles.Contains(urole.Id))
                    {
                        if(usr.PremiumSince == null)
                        {
                            nonpusers.Add(usr.Id);
                        }
                    }
                }
            }
            foreach(var user in nonpusers)
            {
                var usr = Context.Guild.GetUser(user);
                var roles = Context.Guild.GetUser(user).Roles;
                foreach (var role in roles)
                {
                    if (broles.Contains(role.Id))
                    {
                        builder2.Append(usr.Username + " - " + role.Name).AppendLine();
                    }
                }
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Booster Stats:");
            embed.WithColor(new Color(239, 173, 255));
            if(builder1.Length > 3)
            {
                embed.AddField("Unused Roles:", builder1.ToString(), true);
            }
            else
            {
                embed.AddField("Unused Roles:", "None", true);
            }
            if (builder2.Length > 3)
            {
                embed.AddField("Non-Boosted Users:", builder2.ToString(), true);
            }
            else
            {
                embed.AddField("Non-Boosted Users:", "None", true);
            }
            await ReplyAsync("", false, embed.Build());
        }

        [Command("addboost")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task CreateBooster(SocketGuildUser user, string hex, [Remainder]string name)
        {
            if (!hex.Contains("#")) { hex = "#" + hex; }
            Random rnd = new Random();
            var pos = rnd.Next(Context.Guild.GetRole(699010254331183164).Position, Context.Guild.GetRole(696531972276748338).Position);
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(hex);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);
            var role = await Context.Guild.CreateRoleAsync(name, null, new Color(r, g, b), false, null);
            await role.ModifyAsync(x => x.Position = pos);
            await user.AddRoleAsync(role);
            await ReplyAsync(role.Mention + " created.");
        }

        [Command("verified")]
        public async Task getVerified()
        {
            int count = 0;
            foreach(var user in Context.Guild.Users)
            {
                if(user.Roles.Contains(Context.Guild.GetRole(696074197872672830)) || user.Roles.Contains(Context.Guild.GetRole(759624101982896191)))
                {
                    count++;
                }
            }
            var chan = Context.Guild.GetVoiceChannel(762204761902546944);
            await chan.ModifyAsync(x => x.Name = "❥ Verified: " + count);
            await ReplyAsync("Verified count is: " + count);
        }

        [Command("testcommand")]
        public async Task testCommand(ulong id)
        {
            await ReplyAsync(Global.Client.GetUser(id).Username);
        }
    }
}
