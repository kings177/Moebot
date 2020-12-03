using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MoeClorito.Configs;
using RPG.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RPG.Resources.EnemyTemplates;

namespace RPG.Config
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
            await UpdateUserData();
            Console.WriteLine("Connection complete.");
            Console.WriteLine(" (: --------------------------------------- :)");
        }

        static public List<Helmet> droppedHelmets = new List<Helmet>();
        static public List<Chestplate> droppedChestplates = new List<Chestplate>();
        static public List<Gauntlets> droppedGauntlets = new List<Gauntlets>();
        static public List<Belt> droppedBelt = new List<Belt>();
        static public List<Leggings> droppedLeggings = new List<Leggings>();
        static public List<Boots> droppedBoots = new List<Boots>();
        static public List<Artifact> droppedArtifacts = new List<Artifact>();


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
                    await SpawnEnemy(RPG.Resources.EnemyTemplates.TrainingDoll, 1);
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
                            Emcamado.WithDescription("You kicked something, you look to the ground and see a little chest \nIt's all rusty and messed up, feels like its been there for a while. \n" +
                                "You open it.");
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
                            embed.WithDescription("Why are you letting your emotions get control over you? FIGHT YOU PUSSY!, DON'T GIVE UP!, chase after your dreams, realize your desires!! \n" +
                                "Don't be a fucking pussy that is always complaining about sad things but dont do SHIT TO CHANGE IT, FIGHT MOTHER FUCKER!!!");
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
                            embed.WithDescription("You pass through a pile of broken Training Dolls, they all look old and cheap. \nExcept for one, it looks like the new version of the training dolls" +
                                " that the League just released created. \nYou feel like there's something familiar about that doll. ");
                            embed.Color = Color.DarkPurple;
                            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            await Context.User.SendMessageAsync("", false, embed.Build());
                        }
                        else if (roll == 66)
                        {
                            EmbedBuilder embed = new EmbedBuilder();
                            embed.WithTitle("Strange Robot");
                            embed.WithDescription("You hear a sound, its a music, as you approach the sound, you see what it looks to be a robot, jamming to the music," +
                                " you sneak in and try to attack it \nA field force appears and block your attack, the robot is still dancing, it looks like it didn't even cared about you. \n" +
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
                            embed.WithDescription("You where killing some slimes when suddenly a strange woman appear out of nowhere and starts complaining about " +
                                "how youre mistreating the slimes, and that also have rights and deserve respect too.");
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
                            embed.WithDescription("Day 52 of travelling: i have fought with a Armoured Goblin, i wasn't expecting them to be that strong... \n\n\n" +
                                "*You finish reading the notebook...* wtf is a armoured goblin?!?! you think.");
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
                            embed.WithDescription("The book is writen in some type of language that you can't understand well, you managed to grasp a few letters \nBeware of...when...only...");
                            embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/779345860324360212/14-Ascendance-of-a-Bookworm.png");
                            embed.Color = Color.DarkBlue;
                            await Context.User.SendMessageAsync("", false, embed.Build());

                        }
                    }
                    else if (searchingForBoss)
                    {
                        await SpawnEnemy(RPG.Resources.EnemyTemplates.WoodenBoss, 2);
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
                                    channel = channelsInServer as SocketTextChannel;
                            }

                            await Data.Data.Explore(Context.User.Id, "Hautend_Towers");

                            if (channel == null)
                            {
                                EmbedBuilder embed = new EmbedBuilder();
                                embed.WithTitle("Exploration Time");
                                embed.WithDescription("You see a large and creepy tower that looks endless at first sight\nYou get weird out about it but enters either way..\n" +
                                    "You have unlocked the are [You Must Create A Channel Named haunted-towers]");
                                embed.Color = Color.Red;
                                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                            }
                            else
                            {
                                EmbedBuilder embed = new EmbedBuilder();
                                embed.WithTitle("Exploration Time");
                                embed.WithDescription("You see a large and creepy tower that looks endless at first sight\nYou get weird out about it but enters either way..\n" +
                                    "You have unlocked the area <#" + channel.Id + "> ");
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
                        else if (roll < 20) await SpawnDesignedEnemy("Skeleton", 3);
                        else if (roll < 30) await SpawnDesignedEnemy("Wolf", 4);
                        else if (roll < 40) await SpawnDesignedEnemy("Spider", 3);
                        else if (roll < 45) await SpawnDesignedEnemy("Ghost", 3);
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
            // Check Spell
            // Check if spell exists
            // Check if player knows the spell
            // Check the target
            // Check if the player has enough mana and stamina
            // Check effect
            // Resolve
            // Update combat
            // ~~

            await Task.Delay(-1);
        }

        [Command("bindspell"), Alias("spellbind", "bind"), Summary("Bind a spell")]
        public async Task bindspell(string slot, [Remainder] string spell)
        {
            if (slot == "1")
            {
                // Check if player knows spell

                // Bind
            }
            else if (slot == "2")
            {
                // Check  if player knows spell

                // Bind
            }
            else if (slot == "3")
            {
                // Check if player knows spell 

                // Bind
            }
            else if (slot == "4")
            {
                // Check if player knows spell

                // Bind
            }
            else
            {
                // something wrong with slot
            }

            await Task.Delay(-1);
        }

        [Command("prune"), Alias("clear"), Summary("clean the current channel."), RequireUserPermission(GuildPermission.Administrator), RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task ClearChat(uint amount)
        {
            var messages = await Context.Channel.GetMessagesAsync((int)amount).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
        }

        public async Task CheckLevelUp(ulong? ID = null)
        {
            ulong UserId = 0;
            if (ID == null)
                UserId = Context.User.Id;

            else UserId = (ulong)ID;
            string Username = Context.User.Username;
            while (Data.Data.GetData_XP(UserId) >= (Data.Data.GetData_Level(UserId) * Data.Data.GetData_Level(UserId)))
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor(Username + " leveled up.");
                int genType = rng.Next(1, 7);
                string url = "";
                if (genType == 1)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782533912945098752/61805415090d1248d9358ef9b0c01090.gif";
                if (genType == 2)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782534283595087902/wxdh0TSzH0H6x_IS3tHqnHXhUfK9UNO5g8rNeK_a4WrCCJzNSVlcdYhRZ6EDNsESPsRFUC7tbiapgGf-Oo6Ceg.gif";
                if (genType == 3)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782534532833607730/ok-goku.gif";
                if (genType == 4)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782534677100888074/1475035763.gif";
                if (genType == 5)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782535317092696094/tenor_2.gif";
                if (genType == 6)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782535367567474708/tenor_3.gif";
                if (genType == 7)
                    url = "https://cdn.discordapp.com/attachments/773626859585142868/782535308163416134/anime-thumbs-up-gif-11.gif";
                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                embed.WithImageUrl(url);
                embed.Color = Color.Red;
                embed.WithFooter("You are Level: " + (Data.Data.GetData_Level(UserId) + 1));
                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await Data.Data.SaveData(UserId, 0, 0, "", 5 + ((uint)Math.Round(Data.Data.GetData_Level(UserId) * 0.75)), 5 +
                    Data.Data.GetData_Level(UserId), 1, (uint)(Data.Data.GetData_Level(UserId) * Data.Data.GetData_Level(UserId) * -1), 0);
                await Data.Data.AddSkillPoints(UserId, 1);

                if (Data.Data.GetData_Level(UserId) == 2)
                {
                    SocketTextChannel channel = null;

                    foreach (SocketGuildChannel channelIsInServer in Context.Guild.Channels)
                    {
                        if (channelIsInServer.Name == "training")
                            channel = channelIsInServer as SocketTextChannel;
                    }

                    if (channel == null)
                    {
                        await Data.Data.Explore(Context.User.Id, "Training_Zone");

                        EmbedBuilder embodiment = new EmbedBuilder();
                        embodiment.Title = "Gratz";
                        embodiment.Description = "The Guild have now finished registering you, feel free to use the Training Zone. \nUse `;explore` in [Create a channel named `training`] to continue.";
                        embodiment.Color = Color.Gold;

                        await Context.Channel.SendMessageAsync("", false, embodiment.Build());
                    }
                    else
                    {
                        await Data.Data.Explore(Context.User.Id, "Training_Zone");

                        EmbedBuilder embodiment = new EmbedBuilder();
                        embodiment.Title = "Gratz";
                        embodiment.Description = "The Guild have now finished registering you, feel free to use the Training Zone. \nUse `;explore` in #" + channel.Id + " to continue.";
                        embodiment.Color = Color.Gold;

                        await Context.Channel.SendMessageAsync("", false, embodiment.Build());
                    }
                }
            }
        }

        public async Task SpawnEnemy(Enemy enemy, int channel)
        {
            int serverId = 0;
            bool registered = false;

            if (!registered) return;
            uint health = (uint)rng.Next((int)enemy.MinHealth, (int)enemy.MaxHealth);
            uint level = (uint)rng.Next((int)enemy.MinLevel, (int)enemy.MaxLevel);

            CurrentSpawn[serverId].currentSpawn[channel] = enemy;
            CurrentSpawn[serverId].currentSpawn[channel].MaxHealth = health;
            CurrentSpawn[serverId].currentSpawn[channel].CurrentHealth = health;
            CurrentSpawn[serverId].currentSpawn[channel].MaxLevel = level;

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithAuthor("A " + enemy.Name + " lv" + level + " appeared. Type -fight to fight.");
            embed.WithImageUrl(enemy.WebURL);
            embed.Color = Discord.Color.Red;
            embed.WithFooter("Health: " + health + " / " + health);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        public async Task SpawnDesignedEnemy(string EnemyName, int channel)
        {
            int serverId = 0;
            bool registered = false;

            if (!registered)
                return;

            Enemy? generatedEnemy = null;

            int z = -1;

            for (int i = 1; i < designedEnemiesI.Count; i++)
            {
                if (designedEnemiesI[i].name == EnemyName)
                {
                    z = i;
                    break;
                }
            }

            if (z == -1)
                return;

            int level = (int)rng.Next((int)designedEnemiesI[z].minLevel, (int)designedEnemiesI[z].maxLevel);
            int ilevel = (int)Math.Round(Math.Pow(level, (designedEnemiesI[z].difficulty / 100) + 1));

            uint health = (uint)rng.Next((int)Math.Round
                (
                    (designedEnemiesI[z].tankiness * 5 * (21 + ilevel * (5 + .75f * ilevel))) * 0.45f),
                    (int)Math.Round((designedEnemiesI[z].tankiness * 5 * (21 + ilevel * (5 + .75f * ilevel))) * 1.1f)
                );


            CurrentSpawn[serverId].currentSpawn[channel] = new Enemy
                (
                designedEnemiesI[z].iconURL,
                health, // max health
                health, // min health
                (uint)MathF.Round((((ilevel - 1) * 5 + 48) / (designedEnemiesI[z].tankiness * 5)) * 1.25f), // max damage
                (uint)MathF.Round((((ilevel - 1) * 5 + 48) / (designedEnemiesI[z].tankiness * 5)) * 0.1f), // min damage
                (uint)designedEnemiesI[z].maxLevel,
                (uint)designedEnemiesI[z].minLevel,
                (uint)level * (uint)designedEnemiesI[z].difficulty, // max gold drop
                1, // min gold drop
                (uint)level * (uint)designedEnemiesI[z].difficulty, // max xp drop
                1, // min xp drop
                designedEnemiesI[z].name // name
                );

            CurrentSpawn[serverId].currentSpawn[channel].MaxHealth = health;
            CurrentSpawn[serverId].currentSpawn[channel].CurrentHealth = health;
            CurrentSpawn[serverId].currentSpawn[channel].MaxLevel = (uint)level;

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle(designedEnemiesI[z].flavor + "\n ****Level " + level + "****");
            embed.WithImageUrl(designedEnemiesI[z].iconURL);
            embed.Color = Color.Red;
            embed.WithFooter("Health: " + health + " / " + health);
            EmbedFieldBuilder builder = new EmbedFieldBuilder();
            builder.Name = "Minimum Damage";
            builder.Value = "" + CurrentSpawn[serverId].currentSpawn[channel].MinDamage;

            embed.AddField("Minimum Damage", "" + CurrentSpawn[serverId].currentSpawn[channel].MinDamage, true);
            embed.AddField("Max Damage", "" + CurrentSpawn[serverId].currentSpawn[channel].MaxDamage, true);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


        [Command("StartServer"), Alias("startserver")]
        public async Task StartServer()
        {
            IGuildUser user = Context.User as IGuildUser;
            if (!user.GuildPermissions.Administrator && user.Id != 353453210389839872)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("ye.");
                embed.WithDescription("This command will create the channels needed to play the RPG of the bot.");
                embed.Color = Color.DarkRed;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            Console.WriteLine("Guild: " + Context.Guild.Name + " initialized the bot. (" + Context.Guild.Id + "-" + Context.Guild.IconUrl + ")");

            bool[] channels = new bool[50];

            for (int q = 0; q < 50; ++q) channels[q] = false;

            foreach (IGuildChannel channel in Context.Guild.Channels)
            {
                if (channel.Name == "adventurer-title") channels[0] = true;
                else if (channel.Name == "training") channels[1] = true;
                else if (channel.Name == "forsaken-graves") channels[2] = true;
                else if (channel.Name == "goblins-lair") channels[3] = true;
                else if (channel.Name == "haunted-towers") channels[4] = true;
                else if (channel.Name == "lost-caves") channels[5] = true;
                else if (channel.Name == "spooky-town") channels[6] = true;
                else if (channel.Name == "broken-village") channels[7] = true;
            }

            var MoeRPG = await Context.Guild.CreateCategoryChannelAsync("MoeRPG");

            if (!channels[0]) await Context.Guild.CreateTextChannelAsync("adventurer-title", c => c.CategoryId = MoeRPG.Id);
            if (!channels[1]) await Context.Guild.CreateTextChannelAsync("training", c => c.CategoryId = MoeRPG.Id);
            if (!channels[2]) await Context.Guild.CreateTextChannelAsync("forsaken-graves", c => c.CategoryId = MoeRPG.Id);
            if (!channels[3]) await Context.Guild.CreateTextChannelAsync("goblins-lair", c => c.CategoryId = MoeRPG.Id);
            if (!channels[4]) await Context.Guild.CreateTextChannelAsync("haunted-towers", c => c.CategoryId = MoeRPG.Id);
            if (!channels[5]) await Context.Guild.CreateTextChannelAsync("lost-caves", c => c.CategoryId = MoeRPG.Id);
            if (!channels[6]) await Context.Guild.CreateTextChannelAsync("spooky-town", c => c.CategoryId = MoeRPG.Id);
            if (!channels[7]) await Context.Guild.CreateTextChannelAsync("broken-village", c => c.CategoryId = MoeRPG.Id);

            foreach (IGuildChannel channel in Context.Guild.Channels)
            {
                if (channel.Name == "adventurer-title")
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("yo");
                    embed.WithDescription("yeye.");
                    embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782566935903731712/tenor_4.gif");
                    embed.Color = Color.Gold;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    break;
                }
            }

            bool[] Ranks = new bool[18];

            for (int a = 0; a < Ranks.Count(); ++a)
                Ranks[a] = false;

            foreach (SocketRole roles in Context.Guild.Roles)
            {
                if (roles.Name == "Rank - Bronze") Ranks[1] = true;
                if (roles.Name == "Rank - Silver") Ranks[2] = true;
                if (roles.Name == "Rank - Gold") Ranks[3] = true;
                if (roles.Name == "Rank - Platinum") Ranks[4] = true;
                if (roles.Name == "Rank - GrandMaster") Ranks[5] = true;
                if (roles.Name == "Archer") Ranks[6] = true;
                if (roles.Name == "Paladin") Ranks[7] = true;
                if (roles.Name == "Warrior") Ranks[8] = true;
                if (roles.Name == "Wizard") Ranks[9] = true;
                if (roles.Name == "Witch") Ranks[10] = true;
                if (roles.Name == "Rogue") Ranks[11] = true;
                if (roles.Name == "Monk") Ranks[12] = true;
                if (roles.Name == "Assassin") Ranks[13] = true;
                if (roles.Name == "Tamer") Ranks[14] = true;
                if (roles.Name == "Druid") Ranks[15] = true;
                if (roles.Name == "Necromancer") Ranks[16] = true;
                if (roles.Name == "Berserker") Ranks[17] = true;
            }

            if (!Ranks[1]) await Context.Guild.CreateRoleAsync("Rank - Bronze", GuildPermissions.None, new Color(156, 71, 0), true, null);
            if (!Ranks[2]) await Context.Guild.CreateRoleAsync("Rank - Silver", GuildPermissions.None, new Color(131, 131, 131), true, null);
            if (!Ranks[3]) await Context.Guild.CreateRoleAsync("Rank - Gold", GuildPermissions.None, new Color(255, 181, 0), true, null);
            if (!Ranks[4]) await Context.Guild.CreateRoleAsync("Rank - Platinum", GuildPermissions.None, new Color(186, 192, 255), true, null);
            if (!Ranks[5]) await Context.Guild.CreateRoleAsync("Rank - GrandMaster", GuildPermissions.None, new Color(255, 0, 0), true, null);
            if (!Ranks[6]) await Context.Guild.CreateRoleAsync("Archer", GuildPermissions.None, new Color(71, 149, 156), true, null);
            if (!Ranks[7]) await Context.Guild.CreateRoleAsync("Paladin", GuildPermissions.None, new Color(10, 146, 153), true, null);
            if (!Ranks[8]) await Context.Guild.CreateRoleAsync("Warrior", GuildPermissions.None, new Color(87, 87, 87), true, null);
            if (!Ranks[9]) await Context.Guild.CreateRoleAsync("Wizard", GuildPermissions.None, new Color(162, 135, 0), true, null);
            if (!Ranks[10]) await Context.Guild.CreateRoleAsync("Witch", GuildPermissions.None, new Color(184, 65, 65), true, null);
            if (!Ranks[11]) await Context.Guild.CreateRoleAsync("Rogue", GuildPermissions.None, new Color(79, 102, 73), true, null);
            if (!Ranks[12]) await Context.Guild.CreateRoleAsync("Monk", GuildPermissions.None, new Color(160, 112, 233), true, null);
            if (!Ranks[13]) await Context.Guild.CreateRoleAsync("Assassin", GuildPermissions.None, new Color(97, 97, 97), true, null);
            if (!Ranks[14]) await Context.Guild.CreateRoleAsync("Tamer", GuildPermissions.None, new Color(152, 168, 74), true, null);
            if (!Ranks[15]) await Context.Guild.CreateRoleAsync("Druid", GuildPermissions.None, new Color(255, 148, 225), true, null);
            if (!Ranks[16]) await Context.Guild.CreateRoleAsync("Necromancer", GuildPermissions.None, new Color(73, 73, 73), true, null);
            if (!Ranks[17]) await Context.Guild.CreateRoleAsync("Berserker", GuildPermissions.None, new Color(145, 4, 4), true, null);



            Console.WriteLine("Server was initialized.");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("YEET");

            await Game.UpdateUserData();

            for (int q = 0; q < CurrentSpawn.Count(); ++q)
            {
                if (CurrentJoiners[q] == null)
                    CurrentJoiners[q] = new BossJoiningSystem();
                if (CurrentSpawn[q].ServerID == Context.Guild.Id)
                    break;

                if (CurrentSpawn[q].ServerID == 0)
                {
                    if (CurrentSpawn[q] == null)
                        CurrentSpawn[q] = new ServerIntegratedEnemy();
                    CurrentSpawn[q].ServerID = Context.Guild.Id;
                    CurrentJoiners[q].ServerID = Context.Guild.Id;
                    Console.WriteLine("Connected to: " + Context.Guild.Name);
                    break;
                }
            }

            Console.WriteLine("Servers Connected successfully");
            Console.WriteLine("---------------------------------------");
        }


        public static async Task UpdateUserData()
        {
            Console.WriteLine("------------------------");
            foreach (SocketGuild guilds in StartService._client.Guilds)
            {

                bool[] contains = new bool[18];
                foreach (SocketRole role in guilds.Roles)
                {
                    if (role.Name == "Rank - Bronze") contains[0] = true;
                    if (role.Name == "Rank - Silver") contains[1] = true;
                    if (role.Name == "Rank - Gold") contains[2] = true;
                    if (role.Name == "Rank - Platinum") contains[3] = true;
                    if (role.Name == "Rank - GrandMaster") contains[4] = true;
                    if (role.Name == "Archer") contains[5] = true;
                    if (role.Name == "Paladin") contains[6] = true;
                    if (role.Name == "Warrior") contains[7] = true;
                    if (role.Name == "Wizard") contains[8] = true;
                    if (role.Name == "Witch") contains[9] = true;
                    if (role.Name == "Rogue") contains[10] = true;
                    if (role.Name == "Monk") contains[11] = true;
                    if (role.Name == "Assassin") contains[12] = true;
                    if (role.Name == "Tamer") contains[13] = true;
                    if (role.Name == "Druid") contains[14] = true;
                    if (role.Name == "Necromancer") contains[15] = true;
                    if (role.Name == "Berserker") contains[16] = true;
                }

                if (!contains[0]) continue;
                else if (!contains[1]) continue;
                else if (!contains[2]) continue;
                else if (!contains[3]) continue;
                else if (!contains[4]) continue;
                else if (!contains[5]) continue;
                else if (!contains[6]) continue;
                else if (!contains[7]) continue;
                else if (!contains[8]) continue;
                else if (!contains[9]) continue;
                else if (!contains[10]) continue;
                else if (!contains[11]) continue;
                else if (!contains[12]) continue;
                else if (!contains[13]) continue;
                else if (!contains[14]) continue;
                else if (!contains[15]) continue;
                else if (!contains[16]) continue;
                else
                {
                    Console.WriteLine("Guild: " + guilds.Name);
                    foreach (SocketGuildUser user in guilds.Users)
                    {
                        Console.WriteLine("User: " + user.Username);
                        var Archer = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Archer");
                        var Paladin = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Paladin");
                        var Warrior = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Warrior");
                        var Wizard = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Wizard");
                        var Witch = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Witch");
                        var Rogue = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rogue");
                        var Monk = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Monk");
                        var Assassin = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Assassin");
                        var Tamer = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Tamer");
                        var Druid = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Druid");
                        var Necromancer = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Necromancer");
                        var Berserker = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Berserker");
                        var Rank_Bronze = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Bronze");
                        var Rank_Silver = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Silver");
                        var Rank_Gold = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Gold");
                        var Rank_Platinum = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Platinum");
                        var Rank_GrandMaster = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - GrandMaster");
                        string Class = Data.Data.GetClass(user.Id);
                        string Rank = Data.Data.GetRank(user.Id);

                        if (Rank == "Bronze") if (!user.Roles.Contains(Rank_Bronze))
                            {
                                await RemoveUsersRank(user);
                                await user.AddRoleAsync(Rank_Bronze);
                            }
                        if (Rank == "Silver") if (!user.Roles.Contains(Rank_Silver))
                            {
                                await RemoveUsersRank(user);
                                await user.AddRoleAsync(Rank_Silver);
                            }
                        if (Rank == "Gold") if (!user.Roles.Contains(Rank_Gold))
                            {
                                await RemoveUsersRank(user);
                                await user.AddRoleAsync(Rank_Gold);
                            }
                        if (Rank == "Platinum") if (!user.Roles.Contains(Rank_Platinum))
                            {
                                await RemoveUsersRank(user);
                                await user.AddRoleAsync(Rank_Platinum);
                            }
                        if (Rank == "GrandMaster") if (!user.Roles.Contains(Rank_GrandMaster))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Rank_GrandMaster);
                            }
                        if (Class == "Archer") if (!user.Roles.Contains(Archer))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Archer);
                            }
                        if (Class == "Paladin") if (!user.Roles.Contains(Paladin))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Paladin);
                            }
                        if (Class == "Warrior") if (!user.Roles.Contains(Warrior))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Warrior);
                            }
                        if (Class == "Wizard") if (!user.Roles.Contains(Wizard))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Wizard);
                            }
                        if (Class == "Witch") if (!user.Roles.Contains(Witch))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Witch);
                            }
                        if (Class == "Rogue") if (!user.Roles.Contains(Rogue))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Rogue);
                            }
                        if (Class == "Monk") if (!user.Roles.Contains(Monk))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Monk);
                            }
                        if (Class == "Assassin") if (!user.Roles.Contains(Assassin))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Assassin);
                            }
                        if (Class == "Tamer") if (!user.Roles.Contains(Tamer))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Tamer);
                            }
                        if (Class == "Druid") if (!user.Roles.Contains(Druid))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Druid);
                            }
                        if (Class == "Necromancer") if (!user.Roles.Contains(Necromancer))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Necromancer);
                            }
                        if (Class == "Berserker") if (!user.Roles.Contains(Berserker))
                            {
                                await RemoveUsersClass(user);
                                await user.AddRoleAsync(Berserker);
                            }
                    }
                }
            }
        }

        public static async Task RemoveUsersClass(SocketGuildUser user)
        {
            Console.WriteLine("Removing " + user.Username + "'s class");
            var Archer = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Archer");
            var Paladin = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Paladin");
            var Warrior = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Warrior");
            var Wizard = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Wizard");
            var Witch = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Witch");
            var Rogue = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rogue");
            var Monk = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Monk");
            var Assassin = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Assassin");
            var Tamer = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Tamer");
            var Druid = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Druid");
            var Necromancer = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Necromancer");
            var Berserker = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Berserker");

            if (user.Roles.Contains(Archer)) await user.RemoveRoleAsync(Archer);
            if (user.Roles.Contains(Paladin)) await user.RemoveRoleAsync(Paladin);
            if (user.Roles.Contains(Warrior)) await user.RemoveRoleAsync(Warrior);
            if (user.Roles.Contains(Wizard)) await user.RemoveRoleAsync(Wizard);
            if (user.Roles.Contains(Witch)) await user.RemoveRoleAsync(Witch);
            if (user.Roles.Contains(Rogue)) await user.RemoveRoleAsync(Rogue);
            if (user.Roles.Contains(Monk)) await user.RemoveRoleAsync(Monk);
            if (user.Roles.Contains(Assassin)) await user.RemoveRoleAsync(Assassin);
            if (user.Roles.Contains(Tamer)) await user.RemoveRoleAsync(Tamer);
            if (user.Roles.Contains(Druid)) await user.RemoveRoleAsync(Druid);
            if (user.Roles.Contains(Necromancer)) await user.RemoveRoleAsync(Necromancer);
            if (user.Roles.Contains(Berserker)) await user.RemoveRoleAsync(Berserker);
            Console.WriteLine("Removed " + user.Username + "'s class");
            return;
        }

        public static async Task RemoveUsersRank(SocketGuildUser user)
        {
            Console.WriteLine("Removing " + user.Username + "'s rank");
            var Rank_Bronze = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Bronze");
            var Rank_Silver = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Silver");
            var Rank_Gold = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Gold");
            var Rank_Platinum = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - Platinum");
            var Rank_GrandMaster = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Rank - GrandMaster");
            if (user.Roles.Contains(Rank_Bronze)) await user.RemoveRoleAsync(Rank_Bronze);
            if (user.Roles.Contains(Rank_Silver)) await user.RemoveRoleAsync(Rank_Silver);
            if (user.Roles.Contains(Rank_Gold)) await user.RemoveRoleAsync(Rank_Gold);
            if (user.Roles.Contains(Rank_Platinum)) await user.RemoveRoleAsync(Rank_Platinum);
            if (user.Roles.Contains(Rank_GrandMaster)) await user.RemoveRoleAsync(Rank_GrandMaster);
            Console.WriteLine("Removed " + user.Username + "'s rank");
            return;
        }

        [Command("list")]
        public async Task GetUserListAsync()
        {
            var guild = Context.Guild as SocketGuild;
            await guild.DownloadUsersAsync();
            var online = guild.Users.Where((x) => x.Status == UserStatus.Online).Count();
            var offline = guild.Users.Where((x) => x.Status == UserStatus.Offline).Count();
            await ReplyAsync($"User count: {guild.Users.Count()}      Online: {online}, Offline: {offline}");
        }

        [Command("cleanup"), Alias("clean"), Summary("Remove the channels and ranks that the bot made.")]
        public async Task CleanServer()
        {
            IGuildUser user = Context.User as IGuildUser;
            if (!user.GuildPermissions.Administrator && user.Id != 353453210389839872)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("???");
                embed.WithDescription("Dude, you don't have powers, you can't use commands that are meant to ADMs.");
                embed.Color = Color.DarkRed;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            Console.WriteLine("Guild: " + Context.Guild.Name +
                " has killed the bot :( (" + Context.Guild.Id + "-" + Context.Guild.IconUrl + ")");




            // deleting the stuffs


            foreach (ICategoryChannel category in Context.Guild.CategoryChannels)
            {
                if (category.Name == "MoeRPG") await category.DeleteAsync();
            }

            foreach (IGuildChannel channel in Context.Guild.Channels)
            {
                if (channel.Name == "adventurer-title") await channel.DeleteAsync();
                if (channel.Name == "training") await channel.DeleteAsync();
                if (channel.Name == "forsaken-graves") await channel.DeleteAsync();
                if (channel.Name == "goblins-lair") await channel.DeleteAsync();
                if (channel.Name == "haunted-towers") await channel.DeleteAsync();
                if (channel.Name == "lost-caves") await channel.DeleteAsync();
                if (channel.Name == "spooky-town") await channel.DeleteAsync();
                if (channel.Name == "broken-village") await channel.DeleteAsync();
            }

            foreach (SocketRole roles in Context.Guild.Roles)
            {
                if (roles.Name == "Rank - Bronze") await roles.DeleteAsync();
                if (roles.Name == "Rank - Silver") await roles.DeleteAsync();
                if (roles.Name == "Rank - Gold") await roles.DeleteAsync();
                if (roles.Name == "Rank - Platinum") await roles.DeleteAsync();
                if (roles.Name == "Rank - GrandMaster") await roles.DeleteAsync();
                if (roles.Name == "Archer") await roles.DeleteAsync();
                if (roles.Name == "Paladin") await roles.DeleteAsync();
                if (roles.Name == "Warrior") await roles.DeleteAsync();
                if (roles.Name == "Wizard") await roles.DeleteAsync();
                if (roles.Name == "Witch") await roles.DeleteAsync();
                if (roles.Name == "Rogue") await roles.DeleteAsync();
                if (roles.Name == "Monk") await roles.DeleteAsync();
                if (roles.Name == "Assassin") await roles.DeleteAsync();
                if (roles.Name == "Tamer") await roles.DeleteAsync();
                if (roles.Name == "Druid") await roles.DeleteAsync();
                if (roles.Name == "Necromancer") await roles.DeleteAsync();
                if (roles.Name == "Berserker") await roles.DeleteAsync();
            }

            Console.WriteLine("Server cleaned up.");
            Console.WriteLine("--------------------------------");
        }


        public async Task LootItem(SocketCommandContext Context, string monsterName, int monsterLevel, int forceLoot = 0)
        {
            var vuser = Context.User as SocketGuildUser;
            Random ran = new Random();
            int get = ran.Next(0, 6);
            if (get == 0)
            {
                EmbedBuilder embed = new EmbedBuilder();
                Random rando = new Random();

                string itemName = Items.mod_names[rando.Next(0, Items.mod_names.Count - 1)] + " Helmet";
                string imageURL = Items.helmet_icons[rando.Next(0, Items.helmet_icons.Length - 1)];

                string Rarity = "Common";

                if (forceLoot == 0)
                {
                    int roll = rando.Next(0, 1000);
                    if (roll == 747)
                        Rarity = "OmegaPoggers";
                    else if (roll < 600)
                        Rarity = "Common";
                    else if (roll < 900)
                        Rarity = "Uncommon";
                    else if (roll < 930)
                        Rarity = "Rare";
                    else if (roll < 950)
                        Rarity = "Very Rare";
                    else if (roll < 992)
                        Rarity = "Legendary";
                    else if (roll < 1000)
                        Rarity = "Mythic";
                }
                else if (forceLoot == 1) Rarity = "Common";
                else if (forceLoot == 2) Rarity = "Uncommon";
                else if (forceLoot == 3) Rarity = "Rare";
                else if (forceLoot == 4) Rarity = "Very Rare";
                else if (forceLoot == 5) Rarity = "Legendary";
                else if (forceLoot == 6) Rarity = "Mythic";
                else if (forceLoot == 7) Rarity = "OmegaPoggers";

                uint Armor = 0;
                uint Health = 0;
                uint HealthRegen = 0;
                uint SellPrice = 0;

                if (Rarity == "Common")
                {
                    int _roll = rando.Next(1, 8);
                    Armor = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(8, 9 + (monsterLevel / 7) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(0, 50) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.LightGrey);
                }
                else if (Rarity == "Uncommon")
                {
                    int _roll = rando.Next(2, 15);
                    Armor = (uint)rando.Next(4, 5 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(3, 4 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(30, 150) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.Green);
                }
                else if (Rarity == "Rare")
                {
                    int _roll = rando.Next(4, 30);
                    Armor = (uint)rando.Next(5, 6 + (monsterLevel / 3) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(13, 14 + (monsterLevel / 3) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(8, 9 + (monsterLevel / 3) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(140, 200) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.Blue);
                }
                else if (Rarity == "Very Rare")
                {
                    int _roll = rando.Next(8, 48);
                    Armor = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(16, 17 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(125, 350) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.Magenta);
                }
                else if (Rarity == "Legendary")
                {
                    int _roll = rando.Next(40, 180);
                    Armor = (uint)rando.Next(8, 9 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(2000, 2500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.Orange);
                }
                else if (Rarity == "Mythic")
                {
                    int _roll = rando.Next(50, 350);
                    Armor = (uint)rando.Next(10, 11 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(8000, 12500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.Red);
                }
                else if (Rarity == "OmegaPoggers")
                {
                    int _roll = rando.Next(200, 1250);
                    Armor = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(25, 26 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(28, 29 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(100000, 112500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Color.Gold);
                }

                embed.WithAuthor(monsterName + " dropped a " + itemName, Context.User.GetAvatarUrl());
                embed.WithImageUrl(imageURL);
                embed.WithDescription("Rarity: " + Rarity + "\n\n" + "Armor: " + Armor / 2 + "\n" + "Health: " + Health / 2 + "\n"
                    + "Health Regeneration: " + HealthRegen / 2 + "\nSell Price: " + SellPrice);
                embed.WithFooter("Type ;Equip to equip, type ;Sell [Armor Piece] to sell your current armor slot to use this item. Do ;Sell Drop to sell this item.");

                for (int i = 0; i < droppedHelmets.Count; i++)
                {
                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }

                droppedHelmets.Add(new Helmet(Context.User.Id, 0, imageURL, itemName, SellPrice, Rarity, Armor / 2, Health / 2, HealthRegen / 2));

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            else if (get == 1)
            {
                EmbedBuilder embed = new EmbedBuilder();
                Random rando = new Random();
                string itemName = Items.mod_names[rando.Next(0, Items.mod_names.Count - 1)] + " Chestplate";
                string imageURL = Items.chestplate_icons[rando.Next(0, Items.chestplate_icons.Length - 1)];
                string Rarity = "Common";
                if (forceLoot == 0)
                {
                    int roll = rando.Next(0, 1000);
                    if (roll == 747)
                        Rarity = "OmegaPoggers";
                    else if (roll < 600)
                        Rarity = "Common";
                    else if (roll < 900)
                        Rarity = "Uncommon";
                    else if (roll < 930)
                        Rarity = "Rare";
                    else if (roll < 950)
                        Rarity = "Very Rare";
                    else if (roll < 992)
                        Rarity = "Legendary";
                    else if (roll < 1000)
                        Rarity = "Mythic";
                }

                else if (forceLoot == 1) Rarity = "Common";
                else if (forceLoot == 2) Rarity = "Uncommon";
                else if (forceLoot == 3) Rarity = "Rare";
                else if (forceLoot == 4) Rarity = "Very Rare";
                else if (forceLoot == 5) Rarity = "Legendary";
                else if (forceLoot == 6) Rarity = "Mythic";
                else if (forceLoot == 7) Rarity = "OmegaPoggers";
                uint Armor = 0;
                uint Health = 0;
                uint HealthRegen = 0;
                uint SellPrice = 0;

                if (Rarity == "Common")
                {
                    int _roll = rando.Next(1, 8);
                    Armor = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(8, 9 + (monsterLevel / 7) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(0, 50) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.LightGrey);
                }
                else if (Rarity == "Uncommon")
                {
                    int _roll = rando.Next(2, 15);
                    Armor = (uint)rando.Next(4, 5 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(3, 4 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(30, 150) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Green);
                }
                else if (Rarity == "Rare")
                {
                    int _roll = rando.Next(4, 30);
                    Armor = (uint)rando.Next(5, 6 + (monsterLevel / 3) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(13, 14 + (monsterLevel / 3) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(8, 9 + (monsterLevel / 3) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(140, 200) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Blue);
                }
                else if (Rarity == "Very Rare")
                {
                    int _roll = rando.Next(8, 48);
                    Armor = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(16, 17 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(125, 350) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Magenta);
                }

                else if (Rarity == "Legendary")
                {
                    int _roll = rando.Next(40, 180);
                    Armor = (uint)rando.Next(8, 9 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(2000, 2500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Orange);
                }
                else if (Rarity == "Mythic")
                {
                    int _roll = rando.Next(50, 350);
                    Armor = (uint)rando.Next(10, 11 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(8000, 12500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Red);
                }
                else if (Rarity == "OmegaPoggers")
                {
                    int _roll = rando.Next(200, 1250);
                    Armor = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(25, 26 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(28, 29 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(100000, 112500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Gold);
                }

                embed.WithAuthor(monsterName + " dropped a " + itemName, Context.User.GetAvatarUrl());
                embed.WithImageUrl(imageURL);
                embed.WithDescription("Rarity: " + Rarity + "\n\n" + "Armor: " + Armor / 2 + "\n" + "Health: " + Health / 2 + "\n"
                    + "Health Regeneration: " + HealthRegen / 2 + "\nSell Price: " + SellPrice);
                embed.WithFooter("Type ;Equip to equip, type ;Sell [Armor Piece] to sell your current armor slot to use this item. Do ;Sell Drop to sell this item.");
                for (int i = 0; i < droppedHelmets.Count; i++)
                {

                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }

                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }

                droppedChestplates.Add(new Chestplate(Context.User.Id, 0, imageURL, itemName, SellPrice, Rarity, Armor / 2, Health / 2, HealthRegen / 2));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else if (get == 2)
            {
                EmbedBuilder embed = new EmbedBuilder();
                Random rando = new Random();
                string itemName = Items.mod_names[rando.Next(0, Items.mod_names.Count - 1)] + " Gauntlets";
                string imageURL = Items.gauntlet_icons[rando.Next(0, Items.gauntlet_icons.Length - 1)];
                string Rarity = "Common";
                if (forceLoot == 0)
                {
                    int roll = rando.Next(0, 1000);
                    if (roll == 747)
                        Rarity = "OmegaPoggers";
                    else if (roll < 600)
                        Rarity = "Common";
                    else if (roll < 900)
                        Rarity = "Uncommon";
                    else if (roll < 930)
                        Rarity = "Rare";
                    else if (roll < 950)
                        Rarity = "Very Rare";
                    else if (roll < 992)
                        Rarity = "Legendary";
                    else if (roll < 1000)
                        Rarity = "Mythic";
                }

                else if (forceLoot == 1) Rarity = "Common";
                else if (forceLoot == 2) Rarity = "Uncommon";
                else if (forceLoot == 3) Rarity = "Rare";
                else if (forceLoot == 4) Rarity = "Very Rare";
                else if (forceLoot == 5) Rarity = "Legendary";
                else if (forceLoot == 6) Rarity = "Mythic";
                else if (forceLoot == 7) Rarity = "OmegaPoggers";
                uint Armor = 0;
                uint Health = 0;
                uint HealthRegen = 0;
                uint SellPrice = 0;
                if (Rarity == "Common")
                {
                    int _roll = rando.Next(1, 8);
                    Armor = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(8, 9 + (monsterLevel / 7) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(0, 50) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.LightGrey);
                }
                else if (Rarity == "Uncommon")
                {
                    int _roll = rando.Next(2, 15);
                    Armor = (uint)rando.Next(4, 5 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(3, 4 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(30, 150) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Green);
                }
                else if (Rarity == "Rare")
                {
                    int _roll = rando.Next(4, 30);
                    Armor = (uint)rando.Next(5, 6 + (monsterLevel / 3) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(13, 14 + (monsterLevel / 3) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(8, 9 + (monsterLevel / 3) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(140, 200) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Blue);
                }
                else if (Rarity == "Very Rare")
                {
                    int _roll = rando.Next(8, 48);
                    Armor = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(16, 17 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(125, 350) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Magenta);

                }
                else if (Rarity == "Legendary")
                {
                    int _roll = rando.Next(40, 180);
                    Armor = (uint)rando.Next(8, 9 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(2000, 2500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Orange);
                }
                else if (Rarity == "Mythic")
                {
                    int _roll = rando.Next(50, 350);
                    Armor = (uint)rando.Next(10, 11 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(8000, 12500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Red);
                }
                else if (Rarity == "OmegaPoggers")
                {
                    int _roll = rando.Next(200, 1250);
                    Armor = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(25, 26 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(28, 29 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(100000, 112500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Gold);
                }

                embed.WithAuthor(monsterName + " dropped a " + itemName, Context.User.GetAvatarUrl());
                embed.WithImageUrl(imageURL);
                embed.WithDescription("Rarity: " + Rarity + "\n\n" + "Armor: " + Armor / 2 + "\n" + "Health: " + Health / 2 + "\n"
                    + "Health Regeneration: " + HealthRegen / 2 + "\nSell Price: " + SellPrice);
                embed.WithFooter("Type ;Equip to equip, type ;Sell [Armor Piece] to sell your current armor slot to use this item. Do ;Sell Drop to sell this item.");
                for (int i = 0; i < droppedHelmets.Count; i++)
                {
                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }
                droppedGauntlets.Add(new Gauntlets(Context.User.Id, 0, imageURL, itemName, SellPrice, Rarity, Armor / 2, Health / 2, HealthRegen / 2));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else if (get == 3)
            {
                EmbedBuilder embed = new EmbedBuilder();
                Random rando = new Random();
                string itemName = Items.mod_names[rando.Next(0, Items.mod_names.Count - 1)] + " Belt";
                string imageURL = Items.belt_icons[rando.Next(0, Items.belt_icons.Length - 1)];
                string Rarity = "Common";
                if (forceLoot == 0)
                {
                    int roll = rando.Next(0, 1000);
                    if (roll == 747)
                        Rarity = "OmegaPoggers";
                    else if (roll < 600)
                        Rarity = "Common";
                    else if (roll < 900)
                        Rarity = "Uncommon";
                    else if (roll < 930)
                        Rarity = "Rare";
                    else if (roll < 950)
                        Rarity = "Very Rare";
                    else if (roll < 992)

                        Rarity = "Legendary";
                    else if (roll < 1000)
                        Rarity = "Mythic";
                }
                else if (forceLoot == 1) Rarity = "Common";
                else if (forceLoot == 2) Rarity = "Uncommon";
                else if (forceLoot == 3) Rarity = "Rare";
                else if (forceLoot == 4) Rarity = "Very Rare";
                else if (forceLoot == 5) Rarity = "Legendary";
                else if (forceLoot == 6) Rarity = "Mythic";
                else if (forceLoot == 7) Rarity = "OmegaPoggers";
                uint Armor = 0;
                uint Health = 0;
                uint HealthRegen = 0;
                uint SellPrice = 0;
                if (Rarity == "Common")
                {
                    int _roll = rando.Next(1, 8);
                    Armor = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(8, 9 + (monsterLevel / 7) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(0, 50) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.LightGrey);
                }
                else if (Rarity == "Uncommon")
                {
                    int _roll = rando.Next(2, 15);
                    Armor = (uint)rando.Next(4, 5 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(3, 4 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(30, 150) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Green);
                }
                else if (Rarity == "Rare")
                {
                    int _roll = rando.Next(4, 30);
                    Armor = (uint)rando.Next(5, 6 + (monsterLevel / 3) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(13, 14 + (monsterLevel / 3) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(8, 9 + (monsterLevel / 3) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(140, 200) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Blue);
                }
                else if (Rarity == "Very Rare")
                {
                    int _roll = rando.Next(8, 48);
                    Armor = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(16, 17 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(125, 350) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Magenta);
                }

                else if (Rarity == "Legendary")
                {
                    int _roll = rando.Next(40, 180);
                    Armor = (uint)rando.Next(8, 9 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(2000, 2500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Orange);
                }
                else if (Rarity == "Mythic")
                {
                    int _roll = rando.Next(50, 350);
                    Armor = (uint)rando.Next(10, 11 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(8000, 12500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Red);
                }
                else if (Rarity == "OmegaPoggers")
                {
                    int _roll = rando.Next(200, 1250);
                    Armor = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(25, 26 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(28, 29 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(100000, 112500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Gold);
                }

                embed.WithAuthor(monsterName + " dropped a " + itemName, Context.User.GetAvatarUrl());
                embed.WithImageUrl(imageURL);
                embed.WithDescription("Rarity: " + Rarity + "\n\n" + "Armor: " + Armor / 2 + "\n" + "Health: " + Health / 2 + "\n"
                    + "Health Regeneration: " + HealthRegen / 2 + "\nSell Price: " + SellPrice);
                embed.WithFooter("Type ;Equip to equip, type ;Sell [Armor Piece] to sell your current armor slot to use this item. Do ;Sell Drop to sell this item.");

                for (int i = 0; i < droppedHelmets.Count; i++)
                {
                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }
                droppedBelt.Add(new Belt(Context.User.Id, 0, imageURL, itemName, SellPrice, Rarity, Armor / 2, Health / 2, HealthRegen / 2));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            else if (get == 4)
            {
                EmbedBuilder embed = new EmbedBuilder();
                Random rando = new Random();
                string itemName = Items.mod_names[rando.Next(0, Items.mod_names.Count - 1)] + " Leggings";
                string imageURL = Items.legging_icons[rando.Next(0, Items.legging_icons.Length - 1)];
                string Rarity = "Common";
                if (forceLoot == 0)
                {
                    int roll = rando.Next(0, 1000);
                    if (roll == 747)
                        Rarity = "OmegaPoggers";
                    else if (roll < 600)
                        Rarity = "Common";
                    else if (roll < 900)
                        Rarity = "Uncommon";
                    else if (roll < 930)
                        Rarity = "Rare";
                    else if (roll < 950)
                        Rarity = "Very Rare";
                    else if (roll < 992)
                        Rarity = "Legendary";
                    else if (roll < 1000)
                        Rarity = "Mythic";
                }

                else if (forceLoot == 1) Rarity = "Common";
                else if (forceLoot == 2) Rarity = "Uncommon";
                else if (forceLoot == 3) Rarity = "Rare";
                else if (forceLoot == 4) Rarity = "Very Rare";
                else if (forceLoot == 5) Rarity = "Legendary";
                else if (forceLoot == 6) Rarity = "Mythic";
                else if (forceLoot == 7) Rarity = "OmegaPoggers";
                uint Armor = 0;
                uint Health = 0;
                uint HealthRegen = 0;
                uint SellPrice = 0;
                if (Rarity == "Common")
                {
                    int _roll = rando.Next(1, 8);
                    Armor = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(8, 9 + (monsterLevel / 7) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(0, 50) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.LightGrey);
                }

                else if (Rarity == "Uncommon")
                {
                    int _roll = rando.Next(2, 15);
                    Armor = (uint)rando.Next(4, 5 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(3, 4 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(30, 150) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Green);
                }
                else if (Rarity == "Rare")
                {
                    int _roll = rando.Next(4, 30);
                    Armor = (uint)rando.Next(5, 6 + (monsterLevel / 3) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(13, 14 + (monsterLevel / 3) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(8, 9 + (monsterLevel / 3) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(140, 200) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Blue);
                }
                else if (Rarity == "Very Rare")
                {
                    int _roll = rando.Next(8, 48);
                    Armor = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(16, 17 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(125, 350) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Magenta);
                }
                else if (Rarity == "Legendary")
                {
                    int _roll = rando.Next(40, 180);
                    Armor = (uint)rando.Next(8, 9 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(2000, 2500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Orange);
                }
                else if (Rarity == "Mythic")
                {
                    int _roll = rando.Next(50, 350);
                    Armor = (uint)rando.Next(10, 11 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(8000, 12500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Red);
                }
                else if (Rarity == "OmegaPoggers")
                {
                    int _roll = rando.Next(200, 1250);
                    Armor = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(25, 26 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(28, 29 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(100000, 112500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Gold);
                }

                embed.WithAuthor(monsterName + " dropped a " + itemName, Context.User.GetAvatarUrl());
                embed.WithImageUrl(imageURL);
                embed.WithDescription("Rarity: " + Rarity + "\n\n" + "Armor: " + Armor / 2 + "\n" + "Health: " + Health / 2 + "\n"
                    + "Health Regeneration: " + HealthRegen / 2 + "\nSell Price: " + SellPrice);
                embed.WithFooter("Type ;Equip to equip, type ;Sell [Armor Piece] to sell your current armor slot to use this item. Do ;Sell Drop to sell this item.");

                for (int i = 0; i < droppedHelmets.Count; i++)
                {
                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }
                droppedLeggings.Add(new Leggings(Context.User.Id, 0, imageURL, itemName, SellPrice, Rarity, Armor / 2, Health / 2, HealthRegen / 2));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else if (get == 5)
            {
                EmbedBuilder embed = new EmbedBuilder();
                Random rando = new Random();
                string itemName = Items.mod_names[rando.Next(0, Items.mod_names.Count - 1)] + " Boots";
                string imageURL = Items.boot_icons[rando.Next(0, Items.boot_icons.Length - 1)];
                string Rarity = "Common";
                if (forceLoot == 0)
                {
                    int roll = rando.Next(0, 1000);
                    if (roll == 747)
                        Rarity = "OmegaPoggers";
                    else if (roll < 600)
                        Rarity = "Common";
                    else if (roll < 900)
                        Rarity = "Uncommon";
                    else if (roll < 930)
                        Rarity = "Rare";
                    else if (roll < 950)
                        Rarity = "Very Rare";
                    else if (roll < 992)
                        Rarity = "Legendary";
                    else if (roll < 1000)
                        Rarity = "Mythic";
                }

                else if (forceLoot == 1) Rarity = "Common";
                else if (forceLoot == 2) Rarity = "Uncommon";
                else if (forceLoot == 3) Rarity = "Rare";
                else if (forceLoot == 4) Rarity = "Very Rare";
                else if (forceLoot == 5) Rarity = "Legendary";
                else if (forceLoot == 6) Rarity = "Mythic";
                else if (forceLoot == 7) Rarity = "OmegaPoggers";
                uint Armor = 0;
                uint Health = 0;
                uint HealthRegen = 0;
                uint SellPrice = 0;
                if (Rarity == "Common")
                {
                    int _roll = rando.Next(1, 8);
                    Armor = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(8, 9 + (monsterLevel / 7) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(1, 2 + (monsterLevel / 7) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(0, 50) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.LightGrey);
                }

                else if (Rarity == "Uncommon")
                {
                    int _roll = rando.Next(2, 15);
                    Armor = (uint)rando.Next(4, 5 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(3, 4 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(30, 150) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Green);
                }
                else if (Rarity == "Rare")
                {
                    int _roll = rando.Next(4, 30);
                    Armor = (uint)rando.Next(5, 6 + (monsterLevel / 3) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(13, 14 + (monsterLevel / 3) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(8, 9 + (monsterLevel / 3) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(140, 200) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Blue);
                }
                else if (Rarity == "Very Rare")
                {
                    int _roll = rando.Next(8, 48);
                    Armor = (uint)rando.Next(7, 8 + (monsterLevel / 4) * rando.Next(1, _roll));
                    Health = (uint)rando.Next(16, 17 + (monsterLevel / 4) * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + (monsterLevel / 4) * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(125, 350) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Magenta);
                }
                else if (Rarity == "Legendary")
                {
                    int _roll = rando.Next(40, 180);
                    Armor = (uint)rando.Next(8, 9 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(2000, 2500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Orange);
                }
                else if (Rarity == "Mythic")
                {
                    int _roll = rando.Next(50, 350);
                    Armor = (uint)rando.Next(10, 11 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(20, 21 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(8000, 12500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Red);
                }
                else if (Rarity == "OmegaPoggers")
                {
                    int _roll = rando.Next(200, 1250);
                    Armor = (uint)rando.Next(15, 16 + monsterLevel * rando.Next(1, _roll));
                    Health = (uint)rando.Next(25, 26 + monsterLevel * rando.Next(1, _roll));
                    HealthRegen = (uint)rando.Next(28, 29 + monsterLevel * rando.Next(1, _roll));
                    SellPrice = (uint)rando.Next(100000, 112500) * (1 + Data.Data.GetData_Luck(Context.User.Id));
                    embed.WithColor(Discord.Color.Gold);
                }

                embed.WithAuthor(monsterName + " dropped a " + itemName, Context.User.GetAvatarUrl());
                embed.WithImageUrl(imageURL);
                embed.WithDescription("Rarity: " + Rarity + "\n\n" + "Armor: " + Armor / 2 + "\n" + "Health: " +
                    Health / 2 + "\n" + "Health Regeneration: " + HealthRegen / 2 + "\nSell Price: " + SellPrice);
                embed.WithFooter("Type ;Equip to equip, type ;Sell [Armor Piece] to sell your current armor slot to use this item. Do ;Sell Drop to sell this item.");

                for (int i = 0; i < droppedHelmets.Count; i++)
                {
                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }
                droppedBoots.Add(new Boots(Context.User.Id, 0, imageURL, itemName, SellPrice, Rarity, Armor / 2, Health / 2, HealthRegen / 2));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("equip"), Alias("EQUIP"), Summary("Equip the item that dropped.")]
        public async Task EquipDrop([Remainder] string option = "")
        {
            Helmet helmet = null;
            Chestplate chestplate = null;
            Gauntlets gauntlets = null;
            Belt belt = null;
            Leggings leggings = null;
            Boots boots = null;
            for (int i = 0; i < droppedHelmets.Count; i++)
            {
                if (droppedHelmets[i].OwnerID == Context.User.Id)
                {
                    helmet = droppedHelmets[i];
                    break;
                }
            }
            for (int i = 0; i < droppedChestplates.Count; i++)
            {
                if (droppedChestplates[i].OwnerID == Context.User.Id)
                {
                    chestplate = droppedChestplates[i];
                    break;
                }
            }
            for (int i = 0; i < droppedGauntlets.Count; i++)
            {
                if (droppedGauntlets[i].OwnerID == Context.User.Id)
                {
                    gauntlets = droppedGauntlets[i];
                    break;
                }
            }
            for (int i = 0; i < droppedBelt.Count; i++)
            {
                if (droppedBelt[i].OwnerID == Context.User.Id)
                {
                    belt = droppedBelt[i];
                    break;
                }
            }
            for (int i = 0; i < droppedLeggings.Count; i++)
            {
                if (droppedLeggings[i].OwnerID == Context.User.Id)
                {
                    leggings = droppedLeggings[i];
                    break;
                }
            }
            for (int i = 0; i < droppedBoots.Count; i++)
            {
                if (droppedBoots[i].OwnerID == Context.User.Id)
                {
                    boots = droppedBoots[i];
                    break;
                }
            }
            if (helmet == null && chestplate == null && gauntlets == null && belt == null && leggings == null && boots == null)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Nothing to loot!");
                embed.Color = Color.Red;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            if (option == string.Empty || option == "")
            {
                if ((helmet != null && Data.Data.GetHelmet(Context.User.Id) != null) ||
                   (chestplate != null && Data.Data.GetChestplate(Context.User.Id) != null) ||
                   (gauntlets != null && Data.Data.GetGauntlet(Context.User.Id) != null) ||
                   (leggings != null && Data.Data.GetLeggings(Context.User.Id) != null) ||
                   (boots != null && Data.Data.GetBoots(Context.User.Id) != null))
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("This item slot already have an item equipped.");
                    embed.WithDescription("If you want to replace it, type ;equip Replace \nIf you want to sell the equipped one" +
                        "type ;sell [Armor piece (Helmet, Chestplate, Gauntlets, Leggings, Boots)] and then try equipping the item again." +
                        "\nIf you loot a new item during this, the last dropped item wil be lost.");
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    return;
                }
                else
                {
                    if (helmet != null)
                    {
                        await Data.Data.SetHelmet(Context.User.Id, helmet);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Helmet equipped!");
                        embed.Color = Color.Teal;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, helmet.Health, 0, 0, helmet.Health);
                        for (int i = 0; i < droppedHelmets.Count; i++)
                        {
                            if (droppedHelmets[i].OwnerID == Context.User.Id)
                            {
                                droppedHelmets.Remove(droppedHelmets[i]);
                                break;
                            }
                        }
                    }
                    else if (chestplate != null)
                    {
                        await Data.Data.SetChestplate(Context.User.Id, chestplate);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Chestplate equipped!");
                        embed.Color = Color.Teal;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, chestplate.Health, 0, 0, chestplate.Health);
                        for (int i = 0; i < droppedChestplates.Count; i++)
                        {
                            if (droppedChestplates[i].OwnerID == Context.User.Id)
                            {
                                droppedChestplates.Remove(droppedChestplates[i]);
                                break;
                            }
                        }
                    }
                    if (gauntlets != null)
                    {
                        await Data.Data.SetGauntlets(Context.User.Id, gauntlets);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Gauntlets equipped!");
                        embed.Color = Color.Teal;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, gauntlets.Health, 0, 0, gauntlets.Health);
                        for (int i = 0; i < droppedGauntlets.Count; i++)
                        {
                            if (droppedGauntlets[i].OwnerID == Context.User.Id)
                            {
                                droppedGauntlets.Remove(droppedGauntlets[i]);
                                break;
                            }
                        }
                    }
                    if (belt != null)
                    {
                        await Data.Data.SetBelt(Context.User.Id, belt);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Belt equipped!");
                        embed.Color = Color.Teal;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, belt.Health, 0, 0, belt.Health);
                        for (int i = 0; i < droppedBelt.Count; i++)
                        {
                            if (droppedBelt[i].OwnerID == Context.User.Id)
                            {
                                droppedBelt.Remove(droppedBelt[i]);
                                break;
                            }
                        }
                    }
                    if (leggings != null)
                    {
                        await Data.Data.SetLeggings(Context.User.Id, leggings);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Leggings equipped!");
                        embed.Color = Color.Teal;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, leggings.Health, 0, 0, leggings.Health);
                        for (int i = 0; i < droppedLeggings.Count; i++)
                        {
                            if (droppedLeggings[i].OwnerID == Context.User.Id)
                            {
                                droppedLeggings.Remove(droppedLeggings[i]);
                                break;
                            }
                        }
                    }
                    if (boots != null)
                    {
                        await Data.Data.SeBoots(Context.User.Id, boots);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Boots equipped!");
                        embed.Color = Color.Teal;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Data.Data.SaveData(Context.User.Id, 0, 0, "", 0, boots.Health, 0, 0, boots.Health);
                        for (int i = 0; i < droppedBoots.Count; i++)
                        {
                            if (droppedBoots[i].OwnerID == Context.User.Id)
                            {
                                droppedBoots.Remove(droppedBoots[i]);
                                break;
                            }
                        }
                    }
                }
            }

            else if (option == "Replace" || option == "replace")
            {
                if (helmet != null)
                {
                    if (Data.Data.GetHelmet(Context.User.Id) != null)
                        await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, Data.Data.GetHelmet(Context.User.Id).Health, 0, 0, 0);
                    await Data.Data.SetHelmet(Context.User.Id, helmet);

                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Helmet equipped!");
                    embed.Color = Color.Teal;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    for (int i = 0; i < droppedHelmets.Count; i++)
                    {
                        if (droppedHelmets[i].OwnerID == Context.User.Id)
                        {
                            droppedHelmets.Remove(droppedHelmets[i]);
                            break;
                        }
                    }
                }
                else if (chestplate != null)
                {
                    if (Data.Data.GetChestplate(Context.User.Id) != null)
                        await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, Data.Data.GetChestplate(Context.User.Id).Health, 0, 0, 0);
                    await Data.Data.SetChestplate(Context.User.Id, chestplate);

                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Chestplate equipped!");
                    embed.Color = Color.Teal;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    for (int i = 0; i < droppedChestplates.Count; i++)
                    {
                        if (droppedChestplates[i].OwnerID == Context.User.Id)
                        {
                            droppedChestplates.Remove(droppedChestplates[i]);
                            break;
                        }
                    }
                }
                if (gauntlets != null)
                {
                    if (Data.Data.GetGauntlet(Context.User.Id) != null)
                        await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, Data.Data.GetGauntlet(Context.User.Id).Health, 0, 0, 0);
                    await Data.Data.SetGauntlets(Context.User.Id, gauntlets);

                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Gauntlets equipped!");
                    embed.Color = Color.Teal;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    for (int i = 0; i < droppedGauntlets.Count; i++)
                    {
                        if (droppedGauntlets[i].OwnerID == Context.User.Id)
                        {
                            droppedGauntlets.Remove(droppedGauntlets[i]);
                            break;
                        }
                    }
                }
                if (belt != null)
                {
                    if (Data.Data.GetBelt(Context.User.Id) != null)
                        await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, Data.Data.GetBelt(Context.User.Id).Health, 0, 0, 0);
                    await Data.Data.SetBelt(Context.User.Id, belt);

                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Belt equipped!");
                    embed.Color = Color.Teal;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    for (int i = 0; i < droppedBelt.Count; i++)
                    {
                        if (droppedBelt[i].OwnerID == Context.User.Id)
                        {
                            droppedBelt.Remove(droppedBelt[i]);
                            break;
                        }
                    }
                }
                if (leggings != null)
                {
                    if (Data.Data.GetLeggings(Context.User.Id) != null)
                        await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, Data.Data.GetLeggings(Context.User.Id).Health, 0, 0, 0);
                    await Data.Data.SetLeggings(Context.User.Id, leggings);

                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Leggings equipped!");
                    embed.Color = Color.Teal;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    for (int i = 0; i < droppedLeggings.Count; i++)
                    {
                        if (droppedLeggings[i].OwnerID == Context.User.Id)
                        {
                            droppedLeggings.Remove(droppedLeggings[i]);
                            break;
                        }
                    }
                }
                if (boots != null)
                {
                    if (Data.Data.GetBoots(Context.User.Id) != null)
                        await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, Data.Data.GetBoots(Context.User.Id).Health, 0, 0, 0);
                    await Data.Data.SeBoots(Context.User.Id, boots);

                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Boots equipped!");
                    embed.Color = Color.Teal;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    for (int i = 0; i < droppedBoots.Count; i++)
                    {
                        if (droppedBoots[i].OwnerID == Context.User.Id)
                        {
                            droppedBoots.Remove(droppedBoots[i]);
                            break;
                        }
                    }
                }
            }
            else
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Invalid");
                embed.WithDescription("If you want to replace an item, type: ;equip Replace.\n" +
                    "If you want to wear this item, type: ;equip.\n" +
                    "If you want to sell an item, type ;sell [Armor piece], ;sell drop to sell this item");
                embed.Color = Color.Teal;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("sell"), Alias("Sell", "sellitem"), Summary("Sell a piece of equipment")]
        public async Task SellEquip([Remainder] string option = "")
        {
            option = option.ToLower();
            if (option == "helmet" || option == "helmets")
            {
                if (Data.Data.GetHelmet(Context.User.Id) != null)
                {
                    Helmet item = Data.Data.GetHelmet(Context.User.Id);
                    await Data.Data.SaveData(Context.User.Id, item.ItemCost, 0, "", 0, 0, 0, 0, 0);
                    await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, item.Health, 0, 0, 0);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Sold " + item.ItemName + " for " + item.ItemCost + " Gold!");
                    embed.Color = Color.Gold;
                    embed.WithUrl(item.WebURL);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.DeleteHelmet(Context.User.Id);
                }
            }
            else if (option == "chestplate" || option == "chestplates")
            {
                if (Data.Data.GetChestplate(Context.User.Id) != null)
                {
                    Chestplate item = Data.Data.GetChestplate(Context.User.Id);
                    await Data.Data.SaveData(Context.User.Id, item.ItemCost, 0, "", 0, 0, 0, 0, 0);
                    await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, item.Health, 0, 0, 0);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Sold " + item.ItemName + " for " + item.ItemCost + " Gold!");
                    embed.Color = Color.Gold;
                    embed.WithUrl(item.WebURL);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.DeleteChestplate(Context.User.Id);
                }
            }
            else if (option == "gauntlet" || option == "gauntlets")
            {
                if (Data.Data.GetGauntlet(Context.User.Id) != null)
                {
                    Gauntlets item = Data.Data.GetGauntlet(Context.User.Id);
                    await Data.Data.SaveData(Context.User.Id, item.ItemCost, 0, "", 0, 0, 0, 0, 0);
                    await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, item.Health, 0, 0, 0);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Sold " + item.ItemName + " for " + item.ItemCost + " Gold!");
                    embed.Color = Color.Gold;
                    embed.WithUrl(item.WebURL);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.DeleteGauntlets(Context.User.Id);
                }
            }
            else if (option == "belt" || option == "belts")
            {
                if (Data.Data.GetBelt(Context.User.Id) != null)
                {
                    Belt item = Data.Data.GetBelt(Context.User.Id);
                    await Data.Data.SaveData(Context.User.Id, item.ItemCost, 0, "", 0, 0, 0, 0, 0);
                    await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, item.Health, 0, 0, 0);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Sold " + item.ItemName + " for " + item.ItemCost + " Gold!");
                    embed.Color = Color.Gold;
                    embed.WithUrl(item.WebURL);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.DeleteBelt(Context.User.Id);
                }
            }
            else if (option == "legging" || option == "leggings")
            {
                if (Data.Data.GetLeggings(Context.User.Id) != null)
                {
                    Leggings item = Data.Data.GetLeggings(Context.User.Id);
                    await Data.Data.SaveData(Context.User.Id, item.ItemCost, 0, "", 0, 0, 0, 0, 0);
                    await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, item.Health, 0, 0, 0);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Sold " + item.ItemName + " for " + item.ItemCost + " Gold!");
                    embed.Color = Color.Gold;
                    embed.WithUrl(item.WebURL);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.DeleteLeggings(Context.User.Id);
                }
            }
            else if (option == "boot" || option == "boots")
            {
                if (Data.Data.GetBoots(Context.User.Id) != null)
                {
                    Boots item = Data.Data.GetBoots(Context.User.Id);
                    await Data.Data.SaveData(Context.User.Id, item.ItemCost, 0, "", 0, 0, 0, 0, 0);
                    await Data.Data.SubtractSaveData(Context.User.Id, 0, 0, "", 0, item.Health, 0, 0, 0);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("Sold " + item.ItemName + " for " + item.ItemCost + " Gold!");
                    embed.Color = Color.Gold;
                    embed.WithUrl(item.WebURL);
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.DeleteBoots(Context.User.Id);
                }
            }
            else if (option == "drop" || option == "itemdrop")
            {
                for (int i = 0; i < droppedHelmets.Count; i++)
                {
                    if (droppedHelmets[i].OwnerID == Context.User.Id)
                    {
                        await Data.Data.SaveData(Context.User.Id, droppedHelmets[i].ItemCost, 0, "", 0, 0, 0, 0, 0);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Sold " + droppedHelmets[i].ItemName + " for " + droppedHelmets[i].ItemCost + " Gold!");
                        embed.Color = Color.Gold;
                        embed.WithUrl(droppedHelmets[i].WebURL);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        droppedHelmets.Remove(droppedHelmets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedChestplates.Count; i++)
                {
                    if (droppedChestplates[i].OwnerID == Context.User.Id)
                    {
                        await Data.Data.SaveData(Context.User.Id, droppedChestplates[i].ItemCost, 0, "", 0, 0, 0, 0, 0);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Sold " + droppedChestplates[i].ItemName + " for " + droppedChestplates[i].ItemCost + " Gold!");
                        embed.Color = Color.Gold;
                        embed.WithUrl(droppedChestplates[i].WebURL);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        droppedChestplates.Remove(droppedChestplates[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedGauntlets.Count; i++)
                {
                    if (droppedGauntlets[i].OwnerID == Context.User.Id)
                    {
                        await Data.Data.SaveData(Context.User.Id, droppedGauntlets[i].ItemCost, 0, "", 0, 0, 0, 0, 0);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Sold " + droppedGauntlets[i].ItemName + " for " + droppedGauntlets[i].ItemCost + " Gold!");
                        embed.Color = Color.Gold;
                        embed.WithUrl(droppedGauntlets[i].WebURL);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        droppedGauntlets.Remove(droppedGauntlets[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBelt.Count; i++)
                {
                    if (droppedBelt[i].OwnerID == Context.User.Id)
                    {
                        await Data.Data.SaveData(Context.User.Id, droppedBelt[i].ItemCost, 0, "", 0, 0, 0, 0, 0);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Sold " + droppedBelt[i].ItemName + " for " + droppedBelt[i].ItemCost + " Gold!");
                        embed.Color = Color.Gold;
                        embed.WithUrl(droppedBelt[i].WebURL);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        droppedBelt.Remove(droppedBelt[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedLeggings.Count; i++)
                {
                    if (droppedLeggings[i].OwnerID == Context.User.Id)
                    {
                        await Data.Data.SaveData(Context.User.Id, droppedLeggings[i].ItemCost, 0, "", 0, 0, 0, 0, 0);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Sold " + droppedLeggings[i].ItemName + " for " + droppedLeggings[i].ItemCost + " Gold!");
                        embed.Color = Color.Gold;
                        embed.WithUrl(droppedLeggings[i].WebURL);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        droppedLeggings.Remove(droppedLeggings[i]);
                        break;
                    }
                }
                for (int i = 0; i < droppedBoots.Count; i++)
                {
                    if (droppedBoots[i].OwnerID == Context.User.Id)
                    {
                        await Data.Data.SaveData(Context.User.Id, droppedBoots[i].ItemCost, 0, "", 0, 0, 0, 0, 0);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithAuthor("Sold " + droppedBoots[i].ItemName + " for " + droppedBoots[i].ItemCost + " Gold!");
                        embed.Color = Color.Gold;
                        embed.WithUrl(droppedBoots[i].WebURL);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        droppedBoots.Remove(droppedBoots[i]);
                        break;
                    }
                }
            }
            else
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Invalid.");
                embed.WithDescription("To sell a piece of equipment you must use the command like:\n``;sell helmet``\n\nThe item slots to sell " +
                    "are:\nHelmet, Chestplate, Gauntlets, Belt, Leggings & Boots");
                embed.Color = Color.Gold;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }
        }

        [Command("equipment"), Alias("EQUIPMENT", "equipdrop"), Summary("Equip the last dropped item.")]
        public async Task Equipment([Remainder] string option = "")
        {
            option = option.ToLower();
            if (option == "helmet" || option == "helmets")
            {
                Helmet item = Data.Data.GetHelmet(Context.User.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Currently equipped helmet");
                if (item != null)
                {
                    embed.WithDescription(item.ItemName + " - " + item.ItemRarity + "\nArmor: " + item.Armor + "\nHealth: " + item.Health + "\nHealth Regeneration: " + item.HealthGainOnDamage + "\nSell Price: " + item.ItemCost);
                    embed.WithImageUrl(item.WebURL);
                    embed.Color = GetRarityColor(item.ItemRarity);
                }
                else
                {
                    embed.WithDescription("No helmet equipped...");
                    embed.Color = Color.Red;
                }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            else if (option == "gauntlet" || option == "gauntlets")
            {
                Gauntlets item = Data.Data.GetGauntlet(Context.User.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Currently Equipped Gauntlets");
                if (item != null)
                {
                    embed.WithDescription(item.ItemName + " - " + item.ItemRarity + "\nArmor: " + item.Armor + "\nHealth: " + item.Health + "\nHealth Regeneration: " + item.HealthGainOnDamage + "\nSell Price: " + item.ItemCost);
                    embed.WithImageUrl(item.WebURL);
                    embed.Color = GetRarityColor(item.ItemRarity);
                }
                else
                {
                    embed.WithDescription("No gauntlets equipped...");
                    embed.Color = Color.Red;
                }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            else if (option == "chestplate" || option == "chestplates")
            {
                Chestplate item = Data.Data.GetChestplate(Context.User.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Currently Equipped Chestplate");
                if (item != null)
                {
                    embed.WithDescription(item.ItemName + " - " + item.ItemRarity + "\nArmor: " + item.Armor + "\nHealth: " + item.Health + "\nHealth Regeneration: " + item.HealthGainOnDamage + "\nSell Price: " + item.ItemCost);
                    embed.WithImageUrl(item.WebURL);
                    embed.Color = GetRarityColor(item.ItemRarity);
                }
                else
                {
                    embed.WithDescription("No chestplate equipped...");
                    embed.Color = Color.Red;
                }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            else if (option == "belt" || option == "belts")
            {
                Belt item = Data.Data.GetBelt(Context.User.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Currently Equipped Belt");
                if (item != null)
                {
                    embed.WithDescription(item.ItemName + " - " + item.ItemRarity + "\nArmor: " + item.Armor + "\nHealth: " + item.Health + "\nHealth Regeneration: " + item.HealthGainOnDamage + "\nSell Price: " + item.ItemCost);
                    embed.WithImageUrl(item.WebURL);
                    embed.Color = GetRarityColor(item.ItemRarity);
                }
                else
                {
                    embed.WithDescription("No belt equipped...");
                    embed.Color = Color.Red;
                }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            else if (option == "legging" || option == "leggings")
            {
                Leggings item = Data.Data.GetLeggings(Context.User.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Currently Equipped Leggings");
                if (item != null)
                {
                    embed.WithDescription(item.ItemName + " - " + item.ItemRarity + "\nArmor: " + item.Armor + "\nHealth: " + item.Health + "\nHealth Regeneration: " + item.HealthGainOnDamage + "\nSell Price: " + item.ItemCost);
                    embed.WithImageUrl(item.WebURL);
                    embed.Color = GetRarityColor(item.ItemRarity);
                }
                else
                {
                    embed.WithDescription("No leggings equipped...");
                    embed.Color = Color.Red;
                }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            else if (option == "boot" || option == "boots")
            {
                Boots item = Data.Data.GetBoots(Context.User.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Currently Equipped Boots");
                if (item != null)
                {
                    embed.WithDescription(item.ItemName + " - " + item.ItemRarity + "\nArmor: " + item.Armor + "\nHealth: " + item.Health + "\nHealth Regeneration: " + item.HealthGainOnDamage + "\nSell Price: " + item.ItemCost);
                    embed.WithImageUrl(item.WebURL);
                    embed.Color = GetRarityColor(item.ItemRarity);
                }
                else
                {
                    embed.WithDescription("No boots equipped...");
                    embed.Color = Color.Red;
                }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }
            EmbedBuilder embebado = new EmbedBuilder();
            embebado.WithAuthor("Current Equipment:");
            Helmet helm = Data.Data.GetHelmet(Context.User.Id);
            Gauntlets gaun = Data.Data.GetGauntlet(Context.User.Id);
            Chestplate ches = Data.Data.GetChestplate(Context.User.Id);
            Belt belt = Data.Data.GetBelt(Context.User.Id);
            Leggings legg = Data.Data.GetLeggings(Context.User.Id);
            Boots boot = Data.Data.GetBoots(Context.User.Id);
            string helmets;
            string gauntlets;
            string chestplate;
            string belts;
            string leggings;
            string boots;
            if (helm != null)
                helmets = helm.ItemName + " - " + helm.ItemRarity + "\nArmor: " + helm.Armor + "\nHealth: " + helm.Health + "\nHealth Regeneration: " + helm.HealthGainOnDamage + "\nSell Price: " + helm.ItemCost;
            else helmets = "No helmet equipped...";
            if (gaun != null)
                gauntlets = gaun.ItemName + " - " + gaun.ItemRarity + "\nArmor: " + gaun.Armor + "\nHealth: " + gaun.Health + "\nHealth Regeneration: " + gaun.HealthGainOnDamage + "\nSell Price: " + gaun.ItemCost;
            else gauntlets = "No gauntlets equipped...";
            if (ches != null)
                chestplate = ches.ItemName + " - " + ches.ItemRarity + "\nArmor: " + ches.Armor + "\nHealth: " + ches.Health + "\nHealth Regeneration: " + ches.HealthGainOnDamage + "\nSell Price: " + ches.ItemCost;
            else chestplate = "No chestplate equipped...";
            if (belt != null)
                belts = belt.ItemName + " - " + belt.ItemRarity + "\nArmor: " + belt.Armor + "\nHealth: " + belt.Health + "\nHealth Regeneration: " + belt.HealthGainOnDamage + "\nSell Price: " + belt.ItemCost;
            else belts = "No belt equipped...";
            if (legg != null)
                leggings = legg.ItemName + " - " + legg.ItemRarity + "\nArmor: " + legg.Armor + "\nHealth: " + legg.Health + "\nHealth Regeneration: " + legg.HealthGainOnDamage + "\nSell Price: " + legg.ItemCost;
            else leggings = "No leggings equipped...";
            if (boot != null)
                boots = boot.ItemName + " - " + boot.ItemRarity + "\nArmor: " + boot.Armor + "\nHealth: " + boot.Health + "\nHealth Regeneration: " + boot.HealthGainOnDamage + "\nSell Price: " + boot.ItemCost;
            else boots = "No boots equipped...";
            embebado.WithDescription("═════════════════════\n" + helmets + "\n═════════════════════\n" + gauntlets + "\n═════════════════════\n" + chestplate + "\n═════════════════════\n" + belts +
                "\n═════════════════════\n" + leggings + "\n═════════════════════\n" + boots + "\n═════════════════════");
            embebado.WithFooter("To view the icons of your item, do ;Equipment [EquipmentType]");
            embebado.Color = Color.LightOrange;
            await Context.Channel.SendMessageAsync("", false, embebado.Build());
            return;
        }


        public Color GetRarityColor(string ItemRarity)
        {
            if (ItemRarity == "Common")
                return Color.LightGrey;
            else if (ItemRarity == "Uncommon")
                return Color.Green;
            else if (ItemRarity == "Rare")
                return Color.Blue;
            else if (ItemRarity == "Very Rare")
                return Color.Magenta;
            else if (ItemRarity == "Legendary")
                return Color.Orange;
            else if (ItemRarity == "Mythic")
                return Color.Red;
            else if (ItemRarity == "OmegaPoggers")
                return Color.DarkGreen;
            else return Color.DarkMagenta;
        }

        [Command("skill"), Alias("skills", "Skill", "Skills", "skillpoint"), Summary("Show Skills")]
        public async Task SkillLevelUp(string option = "", uint amountOfPoints = 1)
        {
            option = option.ToLower();
            if (option == "" || option.Length == 0)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle(Skill + "Skills");
                embed.WithDescription("Current skill points: " + Data.Data.GetData_SkillPoints(Context.User.Id) +
                                      " " + Skill + "\nTo upgrade a skill do -Skill [Stat].\n\n" +
                                      "Stamina: " + Data.Data.GetData_Stamina(Context.User.Id) +
                                      "\nStability: " + Data.Data.GetData_Stability(Context.User.Id) +
                                      "\nDexterity: " + Data.Data.GetData_Dexterity(Context.User.Id) +
                                      "\nStrength: " + Data.Data.GetData_Strength(Context.User.Id) +
                                      "\nLuck: " + Data.Data.GetData_Luck(Context.User.Id));
                embed.Color = Color.Teal;
                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                embed.WithFooter("Use ;skill info for a list of what each skill does.");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            else if (option == "info")
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle(Skill + "Skill Info");
                embed.WithDescription("**Stamina**: Random chance to heal 25% of max health each turn.\n\n" +
                                      "**Stability**: Increases how much health you will regain in the beginning of you turn.\n\n" +
                                      "**Dexterity**: Increases the likelihood of you dodging an enemy attack.\n\n" +
                                      "**Strength**: Increases your critical chance.\n\n" +
                                      "**Luck**: Increases the drop rate of items and the price of it.\n\n");
                embed.Color = Color.Teal;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            else if (Data.Data.GetData_SkillPoints(Context.User.Id) >= amountOfPoints)
            {
                if (option == "stamina" || option == "stability" || option == "dexterity" || option == "strength" || option == "luck")
                {
                    if (option == "stamina")
                    {
                        await Data.Data.SetSkillPoints(Context.User.Id, Data.Data.GetData_SkillPoints(Context.User.Id) - amountOfPoints);
                        await Data.Data.AddStaminaPoints(Context.User.Id, amountOfPoints);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("Stamina increased by " + amountOfPoints + "!");
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        embed.WithFooter("Remaining skill points: " + Data.Data.GetData_SkillPoints(Context.User.Id));
                        embed.Color = Color.Green;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (option == "stability")
                    {
                        await Data.Data.SetSkillPoints(Context.User.Id, Data.Data.GetData_SkillPoints(Context.User.Id) - amountOfPoints);
                        await Data.Data.AddStabilityPoints(Context.User.Id, amountOfPoints);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("Stability increased by " + amountOfPoints + "!");
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        embed.WithFooter("Remaining skill points: " + Data.Data.GetData_SkillPoints(Context.User.Id));
                        embed.Color = Color.Green;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (option == "dexterity")
                    {
                        await Data.Data.SetSkillPoints(Context.User.Id, Data.Data.GetData_SkillPoints(Context.User.Id) - amountOfPoints);
                        await Data.Data.AddDexterityPoints(Context.User.Id, amountOfPoints);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("Dexterity increased by " + amountOfPoints + "!");
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        embed.WithFooter("Remaining skill points: " + Data.Data.GetData_SkillPoints(Context.User.Id));
                        embed.Color = Color.Green;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (option == "strength")
                    {
                        await Data.Data.SetSkillPoints(Context.User.Id, Data.Data.GetData_SkillPoints(Context.User.Id) - amountOfPoints);
                        await Data.Data.AddStrengthPoints(Context.User.Id, amountOfPoints);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("Strength increased by " + amountOfPoints + "!");
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        embed.WithFooter("Remaining skill points: " + Data.Data.GetData_SkillPoints(Context.User.Id));
                        embed.Color = Color.Green;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (option == "luck")
                    {
                        await Data.Data.SetSkillPoints(Context.User.Id, Data.Data.GetData_SkillPoints(Context.User.Id) - amountOfPoints);
                        await Data.Data.AddLuckPoints(Context.User.Id, amountOfPoints);
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithTitle("Luck increased by " + amountOfPoints + "!");
                        embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                        embed.WithFooter("Remaining skill points: " + Data.Data.GetData_SkillPoints(Context.User.Id));
                        embed.Color = Color.Green;
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Invalid.");
                    embed.WithDescription("The subcommands are: Stamina, Stability, Dexterity, Strength and Luck. You may leave it blank or @someone.");
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else
            {
                if (option == "stamina" || option == "stability" || option == "dexterity" || option == "strength" || option == "luck" || option == "charisma")
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle(Skill + "uhh..." + Skill);
                    embed.WithDescription("You do not have enough skill points to do that!");
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Invalid sub command!");
                    embed.WithDescription("The subcommands are: Stamina, Stability, Dexterity, Strength and Luck. You may leave it blank or @someone.");
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }

        [Command("skill"), Alias("SKILL", "skillpoint")]
        public async Task SkillLevelUp(IUser User)
        {
            if (User == null) return;
            var vuser = User as SocketGuildUser;
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle(Skill + "Skills");
            embed.WithDescription("Current Skill Points: " + Data.Data.GetData_SkillPoints(vuser.Id) +
                " " + Skill + "\n\nStamina: " + Data.Data.GetData_Stamina(vuser.Id) +
                "\nStability: " + Data.Data.GetData_Stability(vuser.Id) +
                "\nDexterity: " + Data.Data.GetData_Dexterity(vuser.Id) +
                "\nStrength: " + Data.Data.GetData_Strength(vuser.Id) +
                "\nLuck: " + Data.Data.GetData_Luck(vuser.Id));
            embed.Color = Color.Teal;
            embed.WithThumbnailUrl(vuser.GetAvatarUrl());
            embed.WithFooter("Do ;Skill Info for a list of what each skill does");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("lootbox"), Alias("LOOTBOX", "lootchest", "lootchests", "lb"), Summary("Open your boxes/chets")]
        public async Task Lootboxes([Remainder] string option = "")
        {
            option = option.ToLower();
            if (option == "")
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle("Current Loot Chests");
                embed.WithDescription("Common Chests: " + Data.Data.GetData_CommonBoxCount(Context.User.Id) +
                    "\nUncommon Chests: " + Data.Data.GetData_UncommonBoxCount(Context.User.Id) +
                    "\nRare Chests: " + Data.Data.GetData_RareBoxCount(Context.User.Id) +
                    "\nVery Rare Chests: " + Data.Data.GetData_VeryRareBoxCount(Context.User.Id) +
                    "\nLegendary Chests: " + Data.Data.GetData_LegendaryBoxCount(Context.User.Id) +
                    "\nMythic Chests: " + Data.Data.GetData_MythicBoxCount(Context.User.Id) +
                    "\nOmegaPoggers Chests: " + Data.Data.GetData_OmegaBoxCount(Context.User.Id));
                embed.Color = Color.Gold;
                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                embed.WithFooter("Do ;lootbox [Type] to open.");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            else if (option == "common")
            {
                if (Data.Data.GetData_CommonBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Common Loot Box Opened.");
                    embed.Color = Color.LightGrey;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetCommonBoxCount(Context.User.Id, Data.Data.GetData_CommonBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "Common Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 1);
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("You do not have any loot boxes of this type...");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else if (option == "uncommon")
            {
                if (Data.Data.GetData_UncommonBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Uncommon Loot Box Opened.");
                    embed.Color = Color.Green;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetUncommonBoxCount(Context.User.Id, Data.Data.GetData_UncommonBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "Uncommon Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 2);
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("You do not have any loot boxes of this type...");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else if (option == "rare")
            {
                if (Data.Data.GetData_RareBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Rare Loot Box Opened.");
                    embed.Color = Color.Blue;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetRareBoxCount(Context.User.Id, Data.Data.GetData_RareBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "Rare Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 3);
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("You do not have any loot boxes of this type...");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else if (option == "veryrare")
            {
                if (Data.Data.GetData_VeryRareBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Very Rare Loot Box Opened.");
                    embed.Color = Color.Magenta;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetVeryRareBoxCount(Context.User.Id, Data.Data.GetData_VeryRareBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "Very Rare Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 4);
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("You do not have any loot boxes of this type...");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else if (option == "legendary")
            {
                if (Data.Data.GetData_LegendaryBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Legendary Loot Box Opened!");
                    embed.Color = Color.Orange;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetLegendaryBoxCount(Context.User.Id, Data.Data.GetData_LegendaryBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "Legendary Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 6);
                }
                else
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithTitle("You do not have any loot boxes of this type...");
                    Embed.Color = Color.Red;
                    Embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
            }
            else if (option == "mythic")
            {
                if (Data.Data.GetData_MythicBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Mythic Loot Box Opened!");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetMythicBoxCount(Context.User.Id, Data.Data.GetData_MythicBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "Mythic Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 7);
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("You do not have any loot boxes of this type...");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else if (option == "omega")
            {
                if (Data.Data.GetData_OmegaBoxCount(Context.User.Id) >= 1)
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("Godly Loot Box Opened!");
                    embed.Color = Color.Teal;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Data.Data.SetOmegaBoxCount(Context.User.Id, Data.Data.GetData_OmegaBoxCount(Context.User.Id) - 1);
                    await LootItem(Context, "OmegaPoggers Loot Box", (int)Data.Data.GetData_Level(Context.User.Id), 8);
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle("You do not have any loot boxes of this type...");
                    embed.Color = Color.Red;
                    embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }
    }
}