using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MoeClorito.Services
{
    public class CommandHandler
    {
        private readonly IConfigurationRoot _config;
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;


        public CommandHandler(IServiceProvider services)
        {
            _services = services;
            _config = services.GetRequiredService<IConfigurationRoot>();
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _client.MessageReceived += MessageReceivedAsync;
        }



        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _commands.CommandExecuted += CommandExecutedAsync;
        }


        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message))
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            int argPos = 0;

            var context = new SocketCommandContext(_client, message);

            char prefix = Char.Parse(_config["Prefix"]);

            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
            {
                return;
            }

            var result = await _commands.ExecuteAsync(context, argPos, _services);

        }


        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
                System.Console.WriteLine($"Command failed to execute due to -> {result.ErrorReason}");
                return;
            }

            if (result.IsSuccess)
            {
                System.Console.WriteLine($"Command executed for -> {command.Value.Name}");
                return;
            }

            await context.Channel.SendMessageAsync($"Something happened with me, im retarded.\n@KingS177 fix me :'(");
        }
    }
}
