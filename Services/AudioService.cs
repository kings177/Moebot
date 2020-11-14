using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Victoria;
using Victoria.EventArgs;


namespace MoeClorito.Modules
{
    public sealed class AudioService
    {
        private readonly LavaNode _lavaNode;
        private readonly ILogger _logger;
        public readonly HashSet<ulong> VoteQueue;
        private readonly ConcurrentDictionary<ulong, CancellationTokenSource> _disconnectedTokens;

        public AudioService(LavaNode lavaNode)
        {
            _lavaNode = lavaNode;
            _disconnectedTokens = new ConcurrentDictionary<ulong, CancellationTokenSource>();



            _lavaNode.OnPlayerUpdated += OnPlayerUpdated;
            _lavaNode.OnStatsReceived += OnStatsReceived;
            _lavaNode.OnTrackEnded += OnTrackEnded;
            _lavaNode.OnTrackStarted += OnTrackStarted;
            _lavaNode.OnTrackException += OnTrackException;
            _lavaNode.OnTrackStuck += OnTrackStuck;
            _lavaNode.OnWebSocketClosed += OnWebSocketClosed;

            VoteQueue = new HashSet<ulong>();
        }


        private Task OnPlayerUpdated(PlayerUpdateEventArgs arg)
        {
            _logger.LogInformation($"Track update received for {arg.Track.Title}: {arg.Position}");
            return Task.CompletedTask;
        }

        private Task OnStatsReceived(StatsEventArgs arg)
        {
            _logger.LogInformation($"Lavalink has been up for {arg.Uptime}.");
            return Task.CompletedTask;
        }

        private async Task OnTrackStarted(TrackStartEventArgs arg)
        {
            if (!_disconnectedTokens.TryGetValue(arg.Player.VoiceChannel.Id, out var value))
            {
                return;
            }

            if (value.IsCancellationRequested)
            {
                return;
            }

            value.Cancel(true);
            await arg.Player.TextChannel.SendMessageAsync("Auto disconnect has been cancelled.");
        }

        private async Task OnTrackEnded(TrackEndedEventArgs args)
        {
            if (!args.Reason.ShouldPlayNext())
            {
                return;
            }

            var player = args.Player;
            if (!player.Queue.TryDequeue(out var queueable))
            {
                await player.TextChannel.SendMessageAsync("Queue finished.");
                _ = InitiateDisconnectAsync(args.Player, TimeSpan.FromSeconds(10));
                return;
            }

            if (!(queueable is LavaTrack track))
            {
                await player.TextChannel.SendMessageAsync("That's not a song.");
                return;
            }

            await args.Player.PlayAsync(track);
            await args.Player.TextChannel.SendMessageAsync($"{args.Reason}: {args.Track.Title}\nPlaying: {track.Title}");
        }

        private async Task InitiateDisconnectAsync(LavaPlayer player, TimeSpan timeSpan)
        {
            if (!_disconnectedTokens.TryGetValue(player.VoiceChannel.Id, out var value))
            {
                value = new CancellationTokenSource();
                _disconnectedTokens.TryAdd(player.VoiceChannel.Id, value);
            }
            else if (value.IsCancellationRequested)
            {
                _disconnectedTokens.TryUpdate(player.VoiceChannel.Id, new CancellationTokenSource(), value);
                value = _disconnectedTokens[player.VoiceChannel.Id];
            }

            await player.TextChannel.SendMessageAsync($"Disconnecting in {timeSpan}");
            var isCancelled = SpinWait.SpinUntil(() => value.IsCancellationRequested, timeSpan);
            if (isCancelled)
            {
                return;
            }

            await _lavaNode.LeaveAsync(player.VoiceChannel);
            await player.TextChannel.SendMessageAsync("Next time be more gentle, you cunt.");
        }

        private async Task OnTrackException(TrackExceptionEventArgs arg)
        {
            _logger.LogError($"Track {arg.Track.Title} threw an exception. Check Lavalink logs.");
            arg.Player.Queue.Enqueue(arg.Track);
            await arg.Player.TextChannel?.SendMessageAsync($"The song {arg.Track.Title} was re-added to the queue after an error.");
        }

        private async Task OnTrackStuck(TrackStuckEventArgs arg)
        {
            _logger.LogError($"Track {arg.Track.Title} got stuck for {arg.Threshold}ms. Verify Lavalink Logs.");
            arg.Player.Queue.Enqueue(arg.Track);
            await arg.Player.TextChannel?.SendMessageAsync($"The song {arg.Track.Title} was re-added to the queue after freezing for a bit.");
        }

        private Task OnWebSocketClosed(WebSocketClosedEventArgs arg)
        {
            _logger.LogCritical($"Discord WebSocket connection closed with following reason: {arg.Reason}");
            return Task.CompletedTask;
        }
    }
}
