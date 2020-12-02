using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoeClorito.Modules;
using MoeClorito.Commands;
using System;
using System.Threading.Tasks;
using Victoria;

namespace MoeClorito.Configs
{
    public class MoeBot
    {
        private IConfigurationRoot _config;


        public async Task MainAsync()
        {
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");
            _config = _builder.Build();

            var services = new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    DefaultRunMode = RunMode.Async,
                    LogLevel = LogSeverity.Verbose,
                    CaseSensitiveCommands = false,
                    ThrowOnError = false
                }))
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<CommonCommands>()
                .AddSingleton<AudioService>()
                .AddSingleton<StartService>()
                .AddLavaNode(x => { x.SelfDeaf = false; });


            var serviceProvider = services.BuildServiceProvider();


            await serviceProvider.GetRequiredService<StartService>().MainAsync();

            serviceProvider.GetRequiredService<CommandService>().Log += LogAsync;
            serviceProvider.GetRequiredService<CommandHandler>();

            await Task.Delay(-1);
        }

        public Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

    }
}
