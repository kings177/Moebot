using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MoeClorito.Services
{
    public class StartService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _services;

        public StartService(IServiceProvider services)
        {
            _services = services;
            _config = _services.GetRequiredService<IConfigurationRoot>();
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _commands = _services.GetRequiredService<CommandService>();

            _client.Ready += ReadyAsync;
        }


        public async Task MainAsync()
        {
            string clientToken = _config["Token"];
            if (string.IsNullOrWhiteSpace(clientToken))
            {
                throw new Exception("Token missing from config.json! Enter you token there (root directory).");
            }

            
            await _client.LoginAsync(TokenType.Bot, clientToken);
            await _client.StartAsync();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        public async Task ReadyAsync()
        {
            Console.WriteLine($"Connected as Moe Clorito ");
            await Task.CompletedTask;
        }
    }
}
