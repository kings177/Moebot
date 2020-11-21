using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoeClorito.Modules
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

        [Command("talk")]
        [Alias("ask")]
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
    }
}
