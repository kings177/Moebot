using Discord.Commands;
using System.Threading.Tasks;

namespace MoeClorito.Modules
{
    public class SimpleCommands : ModuleBase
    {
        [RequireOwner]
        [Command("hi")]
        public async Task HiCommand()
        {
            if (!Context.User.IsBot)
            {
                await ReplyAsync($"Hi lovely, what can i do for you today?");
                return;
            }
        }
    }
}
