using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoeClorito.Commands
{
    public class CommonCommands : ModuleBase
    {
        [Command("hi")]
        [RequireOwner]
        public async Task HiCommand()
        {
            if (!Context.User.IsBot)
            {
                await ReplyAsync($"Hi Master, how can i serve you today?");
                return;
            }
        }

        [Command("ping"), Alias("Ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }

        [Command("talk"), Alias("ask")]
        public async Task Askasync([Remainder] string args = null)
        {
            var sb = new StringBuilder();

            var embed = new EmbedBuilder();

            var replies = new List<string>();

            replies.Add("Ye");
            replies.Add("No");
            replies.Add("Maybe");
            replies.Add("Fuck You");
            replies.Add("Who knows?");
            replies.Add("Ask (((them)))");

            embed.WithColor(new Color(256, 251, 234));
            embed.Title = "yeah, i know, why the fuck does this exist?!";

            sb.AppendLine($",");
            sb.AppendLine();

            if (args == null)
            {
                sb.AppendLine("?????");
            }
            else
            {
                var answer = replies[new Random().Next(replies.Count - 1)];

                sb.AppendLine($"Question: [****]...");
                sb.AppendLine();
                sb.AppendLine($"Answer: [****]");

                switch (answer)
                {
                    case "Ye":
                        {
                            embed.WithColor(new Color(0, 255, 0));
                            break;
                        }
                    case "No":
                        {
                            embed.WithColor(new Color(255, 0, 0));
                            break;
                        }
                    case "Maybe":
                        {
                            embed.WithColor(new Color(255, 255, 0));
                            break;
                        }
                    case "Fuck You":
                        {
                            embed.WithColor(new Color(255, 251, 233));
                            break;
                        }
                    case "Who knows?":
                        {
                            embed.WithColor(new Color(255, 0, 255));
                            break;
                        }
                    case "Ask (((them)))":
                        {
                            embed.WithColor(new Color(255, 235, 231));
                            break;
                        }
                }
            }

            embed.Description = sb.ToString();

            await ReplyAsync(null, false, embed.Build());
        }

        [Command("pfp"), Alias("avatar", "profile")]
        public async Task ProfilePic([Remainder] IUser Input = null)
        {
            if (Input == null)
            {
                Input = Context.User;
            }

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor(Input.Username);
            embed.WithTimestamp(Context.Message.Timestamp);
            embed.WithImageUrl(Input.GetAvatarUrl());
            embed.Color = Color.DarkPurple;
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("help"), Alias("elp", "helps", "commands", "command")]
        public async Task Help([Remainder] string Input = null)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Those are the Bot commands: ");
            embed.WithDescription(
            "\n**1.)** Help: `;help [Page/Command]`." +
            "\n**2.)** Ping: `;ping`." +
            "\n**3.)** View your profile: `;pfp` or `;pfp [@user]`." +
            "\n**4.)** 8Ball shit: `;talk`." +
            "\n\n ---------MUSIC RELATED---------" +
            "\n**1.)** `;join`." +
            "\n**2.)** `;leave`" +
            "\n**3.)** `;paly`or `;p`" +
            "\n**4.)** `;pause`or `;pp`" +
            "\n**5.)** `;resume`" +
            "\n**6.)** `;stop`" +
            "\n**7.)** `;skip`" +
            "\n**8.)** `;seek`" +
            "\n**9.)** `;volume`" +
            "\n**10.)** `;nowplaying` or `;np`" +
            "\n**11.)** Lyrics From Genius: `;genius` or `;lyrics`" +
            "\n**12.)** Lyrivs From OVH: `;ovh` or `;lyrics2`" +
            "\n**13.)** `;queue` or `;q`");
            embed.WithColor(40, 200, 150);
            embed.Color = Color.Gold;
            var msg = await Context.Channel.SendMessageAsync("", false, embed.Build());

            await Task.Delay(20000);
            await msg.DeleteAsync();
            await Context.Message.DeleteAsync();
        }


        [Command("rpghelp"), Alias("rpg"), Summary("RPG Commands.")]
        public async Task RPGCommandsHelp([Remainder] string Input = null)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("The RPG Commands Are: ");
            embed.WithDescription(
                "\n**1.)** Begin: `;begin [Class] [Name] [Age]`." +
                "\n**2.)** List available classes: `;class` or `;classes`." +
                "\n**3.)** Spawn an enemy: `;spawn`." +
                "\n**4.)** Give gold: `;gold give [@user] [amount]`." +
                "\n**5.)** View how much gold you have: `;gold`." +
                "\n**6.)** Check leaderboard: `;leaderboard` or `;lb`." +
                "\n**7.)** Quest: `;quest` or `;q`." +
                "\n**8.)** Fight a monster: `;fight` or `;f`. Use ;f [x] to simulate x turns.." +
                "\n**9.)** Change your class (500 gold): `;switchclass` or `;changeclass`." +
                "\n**10.)** Show the stats of a class: `;info [ClassName]`." +
                "\n**11.)** Show your current equipment: `;equipment [Helmet, Gauntlets, Chestplate, Leggings, Boots]`." +
                "\n**12.)** Equip a dropped item: `;equip`." +
                "\n**13.)** Sell an item you have equipped, or sell a dropped item: `;sell [Helmet, Gauntlets, Chestplate, Leggings, Boots, Drop]`." +
                "\n**14.)** See the chests you own: `;Lootbox`." +
                "\n**15.)** Open a Chest: `;lootchest [Common, Uncommon, Rare, VeryRare, Legendary, Mythic, OmegaPoggers]`." +
                "\n**16.)** See your current skill points: `;skill`." +
                "\n**17.)** Upgrade a certain skill with a skill point: `;skill [Stamina, Stability, Dexterity, Strength, Luck]`." +
                "\n**18.)** Show your RPG profile: `;rpgpfp` or `;rpgpfp [@user]`");
            embed.WithColor(40, 200, 150);
            embed.Color = Color.Gold;
            var msg = await Context.Channel.SendMessageAsync("", false, embed.Build());

            await Task.Delay(20000);
            await msg.DeleteAsync();
            await Context.Message.DeleteAsync();
        }
    }
}
