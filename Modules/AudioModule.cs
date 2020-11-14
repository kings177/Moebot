using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria;
using Victoria.Enums;

namespace MoeClorito.Modules
{
    public class AudioModule : ModuleBase<SocketCommandContext>
    {
        private readonly LavaNode _lavaNode;
        private readonly AudioService _audioService;
        private static readonly IEnumerable<int> Range = Enumerable.Range(1900, 2000);
        private LavaPlayer player { get; set; }

        public AudioModule(LavaNode lavaNode, AudioService audioService)
        {
            _lavaNode = lavaNode;
            _audioService = audioService;
        }


        /////////////////////////////////////////////////////////////////


        [Command("Join", RunMode = RunMode.Async)]
        public async Task JoinAsync()
        {
            if (_lavaNode.HasPlayer(Context.Guild))
            {
                await ReplyAsync("Im already being used.");
                return;
            }

            var voiceState = Context.User as IVoiceState;
            if (voiceState?.VoiceChannel == null)
            {
                await ReplyAsync("Where?");
                return;
            }

            try
            {
                await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync($"I'm in #{voiceState.VoiceChannel.Name}.");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("leave")]
        public async Task LeaveAsync()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("Fuck you.");
                return;
            }

            var voiceChannel = (Context.User as IVoiceState).VoiceChannel ?? player.VoiceChannel;
            if (voiceChannel == null)
            {
                await ReplyAsync("From Where?");
                return;
            }

            try
            {
                await _lavaNode.LeaveAsync(voiceChannel);
                await ReplyAsync($"I'm outta {voiceChannel.Name} bitch.");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }

        /////////////////////////////////////////////////////////////////

        [Command("Play"), Alias ("p")]
        public async Task PlayAsync([Remainder] string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                await ReplyAsync("Play what?");
                return;
            }

            if (!_lavaNode.HasPlayer(Context.Guild))
            {
                await ReplyAsync("I'm not connected to a voice channel.");
                return;
            }

            var queries = searchQuery.Split(' ');
            foreach (var query in queries)
            {
                var searchResponse = await _lavaNode.SearchYouTubeAsync(query);
                if (searchResponse.LoadStatus == LoadStatus.LoadFailed ||
                    searchResponse.LoadStatus == LoadStatus.NoMatches)
                {
                    await ReplyAsync($"Couldn't to find anything for `{query}`.");
                    return;
                }

                var player = _lavaNode.GetPlayer(Context.Guild);

                if (player.PlayerState == PlayerState.Playing || player.PlayerState == PlayerState.Paused)
                {
                    if (!string.IsNullOrWhiteSpace(searchResponse.Playlist.Name))
                    {
                        foreach (var track in searchResponse.Tracks)
                        {
                            player.Queue.Enqueue(track);
                        }

                        await ReplyAsync($"{searchResponse.Tracks.Count} tracks in queue.");
                    }
                    else
                    {
                        var track = searchResponse.Tracks[0];
                        player.Queue.Enqueue(track);
                        await ReplyAsync($"New enqueued: {track.Title}");
                    }
                }
                else
                {
                    var track = searchResponse.Tracks[0];

                    if (!string.IsNullOrWhiteSpace(searchResponse.Playlist.Name))
                    {
                        for (var i = 0; i < searchResponse.Tracks.Count; i++)
                        {
                            if (i == 0)
                            {
                                await player.PlayAsync(track);
                                await ReplyAsync($"Playing: {track.Title}");
                            }
                            else
                            {
                                player.Queue.Enqueue(searchResponse.Tracks[i]);
                            }
                        }

                        await ReplyAsync($"{searchResponse.Tracks.Count} tracks in queue.");
                    }
                    else
                    {
                        await player.PlayAsync(track);
                        await ReplyAsync($"Playing: {track.Title}");
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("pause"), Alias("pp")]
        public async Task PauseAsync()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("dude... im not even in a voice channel...");
                return;
            }

            if (player.PlayerState != PlayerState.Playing)
            {
                await ReplyAsync("I'm not playing anything, are you dumb???");
                return;
            }

            try
            {
                await player.PauseAsync();
                await ReplyAsync($"Paused: {player.Track.Title}");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("resume")]
        public async Task ResumeAsync()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("I'm not connected to a VC.");
                return;
            }

            if (player.PlayerState != PlayerState.Paused)
            {
                await ReplyAsync("I'm not playing anything.");
                return;
            }

            try
            {
                await player.ResumeAsync();
                await ReplyAsync($"Resumed: {player.Track.Title}");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("stop")]
        public async Task StopAsync()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("I'm not in a VC.");
                return;
            }

            if (player.PlayerState == PlayerState.Stopped)
            {
                await ReplyAsync("Hey, you dumb fuck, im already stopped.");
                return;
            }

            try
            {
                await player.StopAsync();
                await ReplyAsync("Stopped...");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("skip")]
        public async Task SkipAsync()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("Im not even in a voice channel...");
                return;
            }

            if (player.PlayerState != PlayerState.Playing)
            {
                await ReplyAsync("There's nothing playing man...");
                return;
            }

            var voiceChannelUsers = (player.VoiceChannel as SocketVoiceChannel).Users.Where(x => !x.IsBot).ToArray();
            if (_audioService.VoteQueue.Contains(Context.User.Id))
            {
                await ReplyAsync("You already voted :EleGiggle:");
                return;
            }

            _audioService.VoteQueue.Add(Context.User.Id);
            var percentage = _audioService.VoteQueue.Count / voiceChannelUsers.Length * 100;
            if (percentage < 75)
            {
                await ReplyAsync("GET FUCKED!, its a trash democracy, you need the majority to vote too (majority => 75%)");
                return;
            }

            try
            {
                var oldTrack = player.Track;
                var currenTrack = await player.SkipAsync();
                await ReplyAsync($"Skipped: {oldTrack.Title}\nPlaying: {currenTrack.Title}");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("seek")]
        public async Task SeekAsync(TimeSpan timeSpan)
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("I'm not in a voice channel.");
                return;
            }

            if (player.PlayerState != PlayerState.Playing)
            {
                await ReplyAsync("there's literally nothing playing rn dude.");
                return;
            }

            try
            {
                await player.SeekAsync(timeSpan);
                await ReplyAsync($"'{player.Track.Title}' seeked to {timeSpan}.");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("volume")]
        public async Task VolumeAsync(ushort volume)
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("Volume of what???????");
                return;
            }

            try
            {
                await player.UpdateVolumeAsync(volume);
                await ReplyAsync($"Volume set to {volume}.");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }


        /////////////////////////////////////////////////////////////////


        [Command("NowPlaying"), Alias("np")]
        public async Task NowPlayingAsync()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync($"Im not in a voice channel.");
                return;
            }

            if (player.PlayerState != PlayerState.Playing)
            {
                await ReplyAsync("Nothing.");
                return;
            }

            var track = player.Track;
            var artwork = await track.FetchArtworkAsync();

            var embed = new EmbedBuilder
            {
                Title = $"{track.Author} - {track.Title}",
                ThumbnailUrl = artwork,
                Url = track.Url
            }
            .AddField("Id", track.Id)
            .AddField("Duration", track.Duration)
            .AddField("Position", track.Position);

            await ReplyAsync(embed: embed.Build());
        }


        /////////////////////////////////////////////////////////////////


        [Command("Genius", RunMode = RunMode.Async), Alias("lyrics")]
        public async Task ShowGeniusLyrics()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("I'm not in a voice channel.");
                return;
            }

            if (player.PlayerState != PlayerState.Playing)
            {
                await ReplyAsync("of what?");
                return;
            }

            var lyrics = await player.Track.FetchLyricsFromGeniusAsync();
            if (string.IsNullOrWhiteSpace(lyrics))
            {
                await ReplyAsync($"Couldn't find any lyrics.");
                return;
            }

            var splitLyrics = lyrics.Split('\n');
            var stringBuilder = new StringBuilder();
            foreach (var line in splitLyrics)
            {
                if (Range.Contains(stringBuilder.Length))
                {
                    await ReplyAsync($"---{stringBuilder}---");
                    stringBuilder.Clear();
                }
                else
                {
                    stringBuilder.AppendLine(line);
                }
            }

            await ReplyAsync($"---{stringBuilder}---");
        }


        /////////////////////////////////////////////////////////////////


        [Command("OVH", RunMode = RunMode.Async), Alias("lyrics2")]
        public async Task OVHLyrics()
        {
            if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
            {
                await ReplyAsync("Im not in a voice channel");
                return;
            }

            if (player.PlayerState != PlayerState.Playing)
            {
                await ReplyAsync("of what?");
                return;
            }

            var lyrics = await player.Track.FetchLyricsFromOVHAsync();
            if (string.IsNullOrWhiteSpace(lyrics))
            {
                await ReplyAsync($"Couldn't find any lyrics.");
                return;
            }

            var splitLyrics = lyrics.Split('\n');
            var stringBuilder = new StringBuilder();
            foreach (var line in splitLyrics)
            {
                if (Range.Contains(stringBuilder.Length))
                {
                    await ReplyAsync($"---{stringBuilder}---");
                    stringBuilder.Clear();
                }
                else
                {
                    stringBuilder.AppendLine(line);
                }
            }

            await ReplyAsync($"---{stringBuilder}---");
        }


        /////////////////////////////////////////////////////////////////
        


        [Command("queue"), Alias ("q")]
        public Task Queue()
        {
            var player = _lavaNode.GetPlayer(Context.Guild);

            var track = player.Queue.Cast<LavaTrack>().Select(x => x.Title);
            return ReplyAsync(track.Count() is 0 ? "No tracks in queue." : string.Join("\n", track));
        }
    }
}
