using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MoeClorito.Services
{
    public class Logging
    {
        private readonly ILogger _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public Logging(ILogger<Logging> logger, DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
            _logger = logger;

            _client.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        public Task OnLogAsync(LogMessage msg)
        {
            string logText = $"{msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";

            switch (msg.Severity.ToString())
            {
                case "Critical":
                    {
                        _logger.LogCritical(logText);
                        break;
                    }
                case "Warning":
                    {
                        _logger.LogWarning(logText);
                        break;
                    }
                case "Info":
                    {
                        _logger.LogInformation(logText);
                        break;
                    }
                case "Verbose":
                    {
                        _logger.LogInformation(logText);
                        break;
                    }
                case "Debug":
                    {
                        _logger.LogDebug(logText);
                        break;
                    }
                case "Error":
                    {
                        _logger.LogError(logText);
                        break;
                    }
            }
            return Task.CompletedTask;
        }
    }
}

