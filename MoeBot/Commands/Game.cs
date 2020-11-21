using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoeClorito.RPG;
using MoeClorito.RPG.Resources;
using MoeClorito.RPG.Resources.Database;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace MoeClorito.RPG.Commands
{
    public class Game : ModuleBase<SocketCommandContext>
    {
        public class ServerIntegratedEnemy
        {
            public Enemy[] currentSpawn { get; set; } = new Enemy[200];
            public ulong ServerID { get; set; } = 0;

            public ServerIntegratedEnemy() { }

            ~ServerIntegratedEnemy() { }
        }

        public class Joiner
        {
            public ulong UserID = 0;
            public ulong Damage = 0;
            public Joiner()
            {
                UserID = 0;
                Damage = 0;
            }
            public Joiner(ulong id, ulong damage)
            {
                UserID = id;
                Damage = damage;
            }
            ~Joiner() { }
        }

        public class BossJoiningSystem
        {
            public Joiner[] JoinBoss1 = new Joiner[100];
            public Joiner[] JoinBoss2 = new Joiner[100];
            public Joiner[] JoinBoss3 = new Joiner[100];
            public Joiner[] JoinBoss4 = new Joiner[100];
            public Joiner[] Joiners = new Joiner[100];
            public ulong ServerID { get; set; } = 0;
            public BossJoiningSystem()
            {
                for (int i = 0; i < 100; ++i)
                {
                    JoinBoss1[i] = new Joiner();
                    JoinBoss2[i] = new Joiner();
                    JoinBoss3[i] = new Joiner();
                    JoinBoss4[i] = new Joiner();
                    Joiners[i] = new Joiner();
                }
            }
            ~BossJoiningSystem() { }
        }

        public class ServerEditMessages
        {
            public Discord.Rest.RestUserMessage[] FightMessages = new Discord.Rest.RestUserMessage[200];
            public ulong ServerID { get; set; } = 0;
            public ServerEditMessages() { }
            ~ServerEditMessages() { }
        }

        static ServerIntegratedEnemy[] CurrentSpawn = new ServerIntegratedEnemy[2500];
        static BossJoiningSystem[] CurrentJoiners = new BossJoiningSystem[2500];
        static ServerEditMessages[] ServerMessages = new ServerEditMessages[2500];
        public static List<DesignedEnemy> designedEnemiesI = new List<DesignedEnemy>();

        public static Dictionary<string, Spell> spellDatabase = new Dictionary<string, Spell>();

        static Random rng = new Random();

        static public async Task ServerConnector()
        {
            Console.WriteLine("Server is connecting");
            int i = 0;
            CurrentSpawn[i] = new ServerIntegratedEnemy();
            Console.WriteLine("Connecting..." + "There are " + CurrentSpawn.Count() + "object pool availables");

            await Game.UpdateUserData();
            await UpdateUserDate();
            Console.WriteLine("Connection complete.");
            Console.WriteLine(" (: --------------------------------------- :)");
        }

        static public List<Helmet> droppedHelmets = new List<Helmet>();
        static public List<Chestplate> droppedChestplate = new List<Chestplate>();
        static public List<Gauntlets> droppedGauntlets = new List<Gauntlets>();
        static public List<Belt> droppedBelts = new List<Belt>();
        static public List<Leggings> droppedLegggings = new List<Leggings>();
        static public List<Boots> droppedBoots = new List<Boots>();
        static public List<Artifact> droppedAtifacts = new List<Artifact>();


        [Command("explore"), Alias("ex", "search", "s", "S")]
        public async Task Explore(string remaining = null)
        {
            int serverId = 0;
            bool registered = false;

            if (Context.Channel.Name == "training")
            {
                if (!Data.Data.Explored(Context.User.Id, "Training_Zone"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }

            else if (Context.Channel.Name == "haunted-towers")
            {
                if (!Data.Data.Explored(Context.User.Id, "Haunted_Towers"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }

            else if (Context.Channel.Name == "forsaken-graves")
            {
                if (!Data.Data.Explored(Context.User.Id, "Forsaken_Graves"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }

            else if (Context.Channel.Name == "lost-caves")
            {
                if (!Data.Data.Explored(Context.User.Id, "Lost_Caves"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }

            else if (Context.Channel.Name == "goblins-lair")
            {
                if (!Data.Data.Explored(Context.User.Id, "Goblins_Lair"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }

            else if (Context.Channel.Name == "spooky-town")
            {
                if (!Data.Data.Explored(Context.User.Id, "Spooky_Town"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }

            else if (Context.Channel.Name == "broken-village")
            {
                if (!Data.Data.Explored(Context.User.Id, "Broken_Village"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this zone.");
                    return;
                }
            }


            if (!registered) return;

            if (Context.Channel.Name == "adventurer-title")
            {
                Console.WriteLine(Data.Data.GetData_Level(Context.User.Id));

                if (Data.Data.GetData_Level(Context.User.Id) == 0 || Data.Data.GetData_Level(Context.User.Id) == 1)
                {
                    await SpawnEnemy(Resources.EnemyTemplates.TrainingDoll, 1);
                    return;
                }
                else
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithTitle("Strength Measurement Finished.");
                    Embed.WithDescription("You have successfully destroyed one of the Training Dolls that the League put so much effort in to creating \nThey feel anger for you." +
                        "\nAs of your strength, they've decided that you need to go fight some monsters and di... \nsurvive!!!.");
                    Embed.Color = Color.DarkPurple;
                    Embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779316950832644117/img_9592.png");
                    Embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await ReplyAsync("", false, Embed.Build());
                    return;
                }
            }
            else
            {
                int levelTargetMax = 0;
                int levelTargetMin = 0;

                bool searchingForEnemy = false;
                bool searchingForLoot = false;
                bool searchingForArea = false;
                bool searchingForLore = false;
                bool searchingForBoss = false;
                bool searchingForNothing = false;


                Random dice = new Random();
                int roll = dice.Next(0, 100);

                if (Context.Channel.Name == "training")
                {
                    if (CurrentSpawn[serverId].currentSpawn[2].CurrentHealth > 0)
                    {
                        EmbedBuilder emCama = new EmbedBuilder();
                        emCama.WithTitle("An enemy appears.");
                        emCama.WithDescription("It's blocking the path, you gotta get rid of it.");
                        emCama.Color = Color.Purple;
                        emCama.WithThumbnailUrl(Context.User.GetAvatarUrl());


                        var msg = await Context.Channel.SendMessageAsync("", false, emCama.Build());
                        await Task.Delay(6000);
                        await msg.DeleteAsync();
                        await Context.Message.DeleteAsync();
                        return;
                    }

                    if (roll < 50) searchingForEnemy = true;
                    else if (roll < 55) searchingForLoot = true;
                    else if (roll < 77) searchingForNothing = true;
                    else if (roll < 80) searchingForLore = true;
                    else if (roll < 83) searchingForBoss = true;
                    else if (roll < 90) searchingForArea = true;
                    else if (roll < 100) searchingForEnemy = true;

                    if (searchingForEnemy)
                    {
                        if (roll < 10) await SpawnDesignedEnemy("Log Spider", 2);
                        else if (roll < 20) await SpawnDesignedEnemy("Zombie", 2);
                        else if (roll < 30) await SpawnDesignedEnemy("Pumpkin", 3);
                        else if (roll < 40) await SpawnDesignedEnemy("Slime", 5);
                        else await SpawnDesignedEnemy("Random Thieve", 2);
                    }
                    else if (searchingForLoot)
                    {
                        if (roll < 52)
                        {
                            EmbedBuilder Emcamado = new EmbedBuilder();
                            Emcamado.WithTitle("You stumble upon a tiny chest.");
                            Emcamado.WithDescription("You kicked something, you look to the ground and see a little chest \nIt's all rusty and messed up, feels like its been there for a while. \nYou open it.");
                            Emcamado.Color = Color.Gold;
                            Emcamado.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, Emcamado.Build());

                            await LootItem(Context, "small chest", 3, 1);
                        }
                        else if (roll < 55)
                        {
                            EmbedBuilder embebedado = new EmbedBuilder();
                            embebedado.WithTitle("You see a small pouch of gold on the ground.");
                            embebedado.WithDescription("On the ground you see a pouch with gold in it \nYou open and found 40 pieces of gold");
                            embebedado.Color = Color.Gold;
                            embebedado.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779327114536026122/116929501-gold-coins-in-a-velvet-pouch-on-white-background-.png");
                            embebedado.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embebedado.Build());

                            await Data.Data.SaveData(Context.User.Id, 40, 0, "", 0, 0, 0, 0, 0);
                        }
                        else if (roll < 60)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("You Found a Chest!");
                            embed.WithDescription("On the ground you see a large chest with carves of gold \nThe chest is in good condition. you pick it up.");
                            embed.Color = Color.LightOrange;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());

                            await LootItem(Context, "large chest", 3, 2);
                        }
                    }
                    else if (searchingForNothing)
                    {
                        if (roll < 57)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("You didn't find nothing.");
                            embed.WithDescription("You kept looking for something, but couldn't find anything of interest. \nYou head in to other directions trying to find something.");
                            embed.Color = Color.Red;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 59)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("Plaque for PUSSIES.");
                            embed.WithDescription("Why are you letting your emotions get control over you? FIGHT YOU PUSSY!, DON'T GIVE UP!, chase after your dreams, realize your desires!! \nDon't be a fucking pussy that is always complaining about sad things but dont do SHIT TO CHANGE IT, FIGHT MOTHER FUCKER!!!");
                            embed.Color = Color.DarkBlue;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 61)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("!!!!!!");
                            embed.WithDescription("You hear a sound, someone is laughing, but why? \nYou feel uneasy and decide to let the guards take care of it.");
                            embed.Color = Color.Red;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 64)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("Pile of Dolls"); ;
                            embed.WithDescription("You pass through a pile of broken Training Dolls, they all look old and cheap. \nExcept for one, it looks like the new version of the training dolls that the League just released created. \nYou feel like there's something familiar about that doll. ");
                            embed.Color = Color.DarkPurple;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.User.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 66)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("Strange Robot");
                            embed.WithDescription("You hear a sound, its a music, as you approach the sound, you see what it looks to be a robot, jamming to the music, you sneak in and try to attack it \nA field force appears and block your attack, the robot is still dancing, it looks like it didn't even cared about you. \n" +
                                "You decided to keep going.");
                            embed.Color = Color.Red;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 71)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("You found... \nNothing!!!!");
                            embed.WithDescription("You really didn't find anything man, that's actually sad...");
                            embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779338171875852328/hqdefault.png");
                            embed.Color = Color.Red;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 75)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("What are you on about?");
                            embed.WithDescription("You where killing some slimes when suddenly a strange woman appear out of nowhere and starts complaining about how youre mistreating the slimes, and that they deserve respect too.");
                            embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779340270885666836/ISH0Ou5.png");
                            embed.Color = Color.Red;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll < 80)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("You found... \nNothing!!!!");
                            embed.WithDescription("You really didn't find anything man, that's actually sad...");
                            embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779339468904726588/744479.png");
                            embed.Color = Color.Red;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else if (searchingForLore)
                    {
                        if (roll < 83)
                        {
                            EmbedBuilder emcama = new EmbedBuilder();
                            emcama.WithTitle("You Found A Notebook");
                            emcama.WithDescription("*You open and read it*");
                            emcama.Color = Color.DarkerGrey;
                            emcama.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779341824853671976/EminentFoolishEasternnewt-small.gif");
                            emcama.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, emcama.Build());

                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("Adventurer Note");
                            embed.WithDescription("Day 52 of travelling: i have fought with a Armoured Goblin, i wasn't expecting them to be that strong... \n\n\n*You finish reading the notebook...* wtf is a armoured goblin?!?! you talked with yourself.");
                            embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779341824853671976/EminentFoolishEasternnewt-small.gif");
                            embed.Color = Color.DarkBlue;
                        }
                        else if (roll < 85)
                        {
                            EmbedBuilder emcama = new EmbedBuilder();
                            emcama.WithTitle("You Found A Strange book");
                            emcama.WithDescription("*You open and try to read it*");
                            emcama.Color = Color.DarkerGrey;
                            emcama.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779345860324360212/14-Ascendance-of-a-Bookworm.png");
                            emcama.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.Channel.SendMessageAsync("", false, emcama.Build());

                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("ᚪ ᛋᛏᚱᚪᛝᛖ ᛒᚩᚩᚳ");
                            embed.WithDescription("The book is writen in some type of language that you can't understand well, there's only a few letters that you could understand \nThey...Back...What will...In the end.");
                            embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779345860324360212/14-Ascendance-of-a-Bookworm.png");
                            embed.Color = Color.DarkBlue;
                            await Context.User.SendMessageAsync("", false, embed.Build());

                        }
                    }
                    else if (searchingForBoss)
                    {
                        await SpawnEnemy(Resources.EnemyTemplates.WoodenBoss, 2);
                    }
                    else if (searchingForArea)
                    {
                        if (Data.Data.Explored(Context.User.Id, "Haunted_Towers"))
                        {
                            if (!Data.Data.Explored(Context.User.Id, "Forsaken_Graves"))
                            {
                                SocketTextChannel channel = null;

                                foreach (SocketGuildChannel channelsInServer in Context.Guild.Channels)
                                {
                                    if (channelsInServer.Name == "forsaken-graves")
                                        channel = channelsInServer as SocketTextChannel;
                                }

                                await Data.Data.Explore(Context.User.Id, "Forsaken_Graves");

                                if (channel == null)
                                {
                                    EmbedBuilder embed = new EmbedBuilder();
                                    embed.WithTitle("Open Graves");
                                    embed.WithDescription("You passed by a Graveyard, you noticed that all the graves where open, you decided to take a closer look. \n" +
                                        "You Have Unlocked The Area [You must create a channel with the name forsaken-graves.]");
                                    embed.WithColor(Color.Red);
                                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                                }
                                else
                                {
                                    EmbedBuilder embed = new EmbedBuilder();
                                    embed.WithTitle("Open Graves");
                                    embed.WithDescription("You passed by a Graveyard, you noticed that all the graves where open, you decided to take a closer look. \n" +
                                        "You Have Unlocked The Area <#" + channel.Id + ">");
                                    embed.WithColor(Color.Green);
                                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                            }
                            else
                            {
                                EmbedBuilder embed = new EmbedBuilder();
                                embed.WithTitle("You Found Nothing.");
                                embed.WithDescription("Dude... i'm sorry but you actually didn't find shit.");
                                embed.Color = Color.Red;
                                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            SocketTextChannel channel = null;

                            foreach (SocketGuildChannel channelsInServer in Context.Guild.Channels)
                            {
                                if (channelsInServer.Name == "haunted-towers")
                                    channel = channelInServer as SocketTextChannel;
                            }

                            await Data.Data.Explore(Context.User.Id, "Hautend_Towers");

                            if (channel == null)
                            {
                                EmbedBuilder embed = new EmbedBuilder();
                                embed.WithTitle("Exploration Time");
                                embed.WithDescription("You see a large and creepy tower that looks endless at first sight\nYou get weird out about it but enters either way..\nYou have unlocked the are [You Must Create A Channel Named haunted-towers]");
                                embed.Color = Color.Red;
                                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            }
                            else
                            {
                                EmbedBuilder embed = new EmbedBuilder();
                                embed.WithTitle("Exploration Time");
                                embed.WithDescription("You see a large and creepy tower that looks endless at first sight\nYou get weird out about it but enters either way..\nYou have unlocked the area <#" + channel.Id + "> ");
                                embed.Color = Color.Green;
                                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                                await Context.Channel.SendMessageAsync("", false, embed.Build());

                            }
                        }
                    }
                    return;
                }
                else if (Context.Channel.Name == "haunted-towers")
                {
                    if (CurrentSpawn[serverId].currentSpawn[3].CurrentHealth > 0)
                    {
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("There's a enemy blocking your way.");
                        embed.WithDescription("You must kill it in order to continue.");
                        embed.Color = Color.Red;
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());

                        var msg = await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Task.Delay(6000);
                        await msg.DeleteAsync();
                        await Context.Message.DeleteAsync();
                        return;
                    }

                    if (roll < 50) searchingForEnemy = true;
                    else if (roll < 55) searchingForLoot = true;
                    else if (roll < 77) searchingForNothing = true;
                    else if (roll < 80) searchingForLore = true;
                    else if (roll < 83) searchingForBoss = true;
                    else if (roll < 90) searchingForArea = true;
                    else if (roll < 100) searchingForEnemy = true;

                    if (searchingForEnemy)
                    {
                        if (roll < 10) await SpawnDesignedEnemy("Poisonous Spider", 3);
                        else if (roll < 20) await SpawnDesignedEnemy("Skeleton", 2);
                        else if (roll < 30) await SpawnDesignedEnemy("Wolf", 4);
                        else if (roll < 40) await SpawnDesignedEnemy("Spider", 2);
                        else if (roll < 45) await SpawnDesignedEnemy("Ghost", 2);
                        else await SpawnDesignedEnemy("Skeleton", 2);
                    }
                    else if (searchingForLoot)
                    {
                        await Context.Channel.SendMessageAsync("Loot");
                    }
                    else if (searchingForNothing)
                    {
                        await Context.Channel.SendMessageAsync("Nothing");
                    }
                    else if (searchingForLore)
                    {
                        await Context.Channel.SendMessageAsync("Lore");
                    }
                    else if (searchingForBoss)
                    {
                        await Context.Channel.SendMessageAsync("Boss");
                    }
                    else if (searchingForArea)
                    {
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("You Found Nothing.");
                        embed.WithDescription("You wander around the places looking for something" +
                            "You look at the walls and see dust clinged in it" +
                            "The ground doesn't look much different.");
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/779538208093896725/post_7.png?width=742&height=464");
                        embed.Color = Color.Red;
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }

                    return;
                }
                else if (Context.Channel.Name == "forsaken-graves")
                {
                    if (CurrentSpawn[serverId].currentSpawn[4].CurrentHealth > 0)
                    {
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("There's a enemy blocking your way.");
                        embed.WithDescription("You must kill it in order to continue.");
                        embed.Color = Color.Red;
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());

                        var msg = await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Task.Delay(6000);
                        await msg.DeleteAsync();
                        await Context.Message.DeleteAsync();
                        return;
                    }

                    if (roll < 50) searchingForEnemy = true;
                    else if (roll < 55) searchingForLoot = true;
                    else if (roll < 77) searchingForNothing = true;
                    else if (roll < 80) searchingForLore = true;
                    else if (roll < 83) searchingForBoss = true;
                    else if (roll < 90) searchingForArea = true;
                    else if (roll < 100) searchingForEnemy = true;

                    if (searchingForEnemy)
                    {

                    }

                }
            }
        }

        [Command("fight"), Alias("f", "F", "attack"), Summary("Fight the enemy.")]
        public async Task Fight(int times = 1)
        {
            int serverId = 0;
            bool registered = false;

            if (!registered) return;

            if (Context.Channel.Name == "training")
            {
                if (!Data.Data.Explored(Context.User.Id, "Training_Zone"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }
            else if (Context.Channel.Name == "haunted-towers")
            {
                if (!Data.Data.Explored(Context.User.Id, "Haunted_towers"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }
            else if (Context.Channel.Name == "forsaken-graves")
            {
                if (!Data.Data.Explored(Context.User.Id, "Forsaken_Graves"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }
            else if (Context.Channel.Name == "lost-caves")
            {
                if (!Data.Data.Explored(Context.User.Id, "Lost_Caves"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }
            else if (Context.Channel.Name == "goblins-lair")
            {
                if (!Data.Data.Explored(Context.User.Id, "Goblins_Lair"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }
            else if (Context.Channel.Name == "spooky-town")
            {
                if (!Data.Data.Explored(Context.User.Id, "Spooky_Town"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }
            else if (Context.Channel.Name == "broken-village")
            {
                if (!Data.Data.Explored(Context.User.Id, "Broken_Village"))
                {
                    await Context.Channel.SendMessageAsync("You don't know the path to this area.");
                    return;
                }
            }

            for (int gg = 0; gg < times; ++gg)
            {
                int server = 0;
                if (Context.Channel.Name == "adventurer-title") server = 1;
                else if (Context.Channel.Name == "training") server = 2;
                else if (Context.Channel.Name == "forsaken-graves") server = 3;
                else if (Context.Channel.Name == "goblins-lair") server = 4;
                else if (Context.Channel.Name == "haunted-towers") server = 5;
                else if (Context.Channel.Name == "lost-caves") server = 6;
                else if (Context.Channel.Name == "spooky-town") server = 7;
                else if (Context.Channel.Name == "broken-village") server = 8;
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("This is not a RPG area you fucking dweeb.");
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    return;
                }

                if (CurrentSpawn[serverId].currentSpawn[server].IsInCombat) return;

                CurrentSpawn[serverId].currentSpawn[server].IsInCombat = true;

                if (CurrentSpawn[serverId].currentSpawn[server].CurrentHealth > 0)
                {
                    if (CurrentSpawn[serverId].currentSpawn[server].IsDead)
                        return;
                    uint userdmg = Data.Data.GetData_Damage(Context.User.Id);
                    uint currentArmor = 0;
                    uint currentRegen = 0;

                    if (Data.Data.GetHelmet(Context.User.Id) != null)
                    {
                        currentArmor += Data.Data.GetHelmet(Context.User.Id).Armor;
                        currentRegen += Data.Data.GetHelmet(Context.User.Id).HealthGainOnDamage;
                    }

                    if (Data.Data.GetChestplate(Context.User.Id) != null)
                    {
                        currentArmor += Data.Data.GetChestplate(Context.User.Id).Armor;
                        currentRegen += Data.Data.GetChestplate(Context.User.Id).HealthGainOnDamage;
                    }

                    if (Data.Data.GetGauntlet(Context.User.Id) != null)
                    {
                        currentArmor += Data.Data.GetGauntlet(Context.User.Id).Armor;
                        currentRegen += Data.Data.GetGauntlet(Context.User.Id).HealthGainOnDamage;
                    }

                    if (Data.Data.GetBelt(Context.User.Id) != null)
                    {
                        currentArmor += Data.Data.GetBelt(Context.User.Id).Armor;
                        currentRegen += Data.Data.GetBelt(Context.User.Id).HealthGainOnDamage;
                    }

                    if (Data.Data.GetLeggings(Context.User.Id) != null)
                    {
                        currentArmor += Data.Data.GetLeggings(Context.User.Id).Armor;
                        currentRegen += Data.Data.GetLeggings(Context.User.Id).HealthGainOnDamage;
                    }
                    if (Data.Data.GetBoots(Context.User.Id) != null)
                    {
                        currentArmor += Data.Data.GetBoots(Context.User.Id).Armor;
                        currentRegen += Data.Data.GetBoots(Context.User.Id).HealthGainOnDamage;
                    }
                    await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, 0, 0, 0, (currentRegen / 10) + (Data.Data.GetData_Stability(Context.User.Id) * 10));
                    uint dmg = (uint)rng.Next((int)CurrentSpawn[serverId].currentSpawn[server].MinDamage, (int)CurrentSpawn[serverId].currentSpawn[server].MaxDamage);
                    bool blocked = false;
                    bool critical = false;
                    bool regenerate = false;
                    if ((int)dmg - (int)currentArmor / 10 <= 0) dmg = 0;
                    else dmg -= currentArmor / 10;

                    Random rando = new Random();
                    int dodge = rando.Next(0, 100 / ((int)Data.Data.GetData_Dexterity(Context.User.Id) + 1));
                    int crit = rando.Next(0, 100 / ((int)Data.Data.GetData_Strength(Context.User.Id) + 1));
                    int staminaregen = rando.Next(0, 100 / ((int)Data.Data.GetData_Stamina(Context.User.Id) + 1));
                    if (dodge == 0 || dodge == 1) blocked = true;
                    if (crit == 0 || crit == 1) critical = true;
                    if (staminaregen == 0 || staminaregen == 1) regenerate = true;
                    if (regenerate)
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, 0, 0, 0, Data.Data.GetData_Health(Context.User.Id) / 4);
                    if (Data.Data.GetData_CurrentHealth(Context.User.Id) > dmg)
                    {
                        if (!critical && CurrentSpawn[serverId].currentSpawn[server].CurrentHealth > userdmg)
                            CurrentSpawn[serverId].currentSpawn[server].CurrentHealth -= userdmg;
                        else if (critical && CurrentSpawn[serverId].currentSpawn[server].CurrentHealth > userdmg * 2)
                            CurrentSpawn[serverId].currentSpawn[server].CurrentHealth -= userdmg * 2;
                        else CurrentSpawn[serverId].currentSpawn[server].CurrentHealth = 0;
                        if (!blocked)
                            await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, 0, 0, 0, (uint)(-dmg));
                        if (CurrentSpawn[serverId].currentSpawn[server].CurrentHealth > 0)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithAuthor(CurrentSpawn[serverId].currentSpawn[server].Name + " Lvl" + CurrentSpawn[serverId].currentSpawn[server].MaxLevel);
                            embed.WithImageUrl(CurrentSpawn[serverId].currentSpawn[server].WebURL);
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            embed.Color = Color.Red;
                            embed.WithFooter(CurrentSpawn[serverId].currentSpawn[server].Name + "'s Health: " + CurrentSpawn[serverId].currentSpawn[server].CurrentHealth + " / "
                                + CurrentSpawn[serverId].currentSpawn[server].MaxHealth);
                            string emcamado = blocked == true ? "You're the dodge king!\n" : "You Received " + dmg + " Damage.\n";
                            embed.WithDescription(emcamado + "Health Remaining: " + Data.Data.GetData_CurrentHealth(Context.User.Id) +
                                (regenerate == true ? "\nYou have regenerated 25% of your health with Stamina Skill!" : "") +
                                (critical == true ? "\nYou crited for +" + dmg + " damage" : ""));
                            var deadmsg = await Context.Channel.SendMessageAsync("", false, embed.Build());

                            CurrentSpawn[serverId].currentSpawn[server].IsInCombat = false;
                            break;
                        }
                    }


                    else
                    {
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor(Context.User.Username + " Died.");
                        embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779566453820620860/tenor_1.gif");
                        embed.Color = Color.Red;
                        embed.WithFooter("You lost: " + (uint)Math.Round(Data.Data.GetData_GoldAmount(Context.User.Id) * 0.20) + "Gold & " + (uint)Math.Round(Data.Data.GetData_XP(Context.User.Id) * 0.30) + "XP");
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        embed.WithDescription("You died. You lost Gold and XP. Revive in a church to continue.");
                        CurrentSpawn[serverId].currentSpawn[server].IsInCombat = false;
                        var deadmsg = await Context.Channel.SendMessageAsync("", false, embed.Build());

                        await Task.Delay(5000);
                        await deadmsg.DeleteAsync();
                        break;
                    }
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("There are no monster nearby.");
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    break;
                }
            }

            await Context.Message.DeleteAsync();
        }


        [Command("cast"), Alias("c", "castspell")]
        public async Task CastSpell([Remainder] string spell)
        {

        }

    }
}
