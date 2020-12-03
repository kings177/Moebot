using Discord;
using Discord.Commands;
using Discord.WebSocket;
using RPG.Resources.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Config
{
    public class RPGProfile : ModuleBase<SocketCommandContext>
    {
        [Command("rpgpfp"), Alias("rpfp"), Summary("Shows RPG profile")]
        public async Task ProfileRPG(IUser User = null)
        {
            var uuser = User as SocketGuildUser;

            if (User == null) uuser = Context.User as SocketGuildUser;

            EmbedBuilder embed = new EmbedBuilder();
            var user = uuser;
            string classType = "";
            string classEmoji = "";
            string UsersClass = Data.Data.GetClass(uuser.Id);

            if (UsersClass == "Archer")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782378943218974781/hiclipart.com_1.png");
                classType = "Archer";
                classEmoji = Archer;
            }
            else if (UsersClass == "Paladin")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782380077311983636/hiclipart.com_2.png");
                classType = "Paladin";
                classEmoji = Paladin;
            }
            else if (UsersClass == "Warrior")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782378958326726676/hiclipart.com.png");
                classType = "Warrior";
                classEmoji = Warrior;
            }
            else if (UsersClass == "Wizard")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782380503834951710/hiclipart.com_1.png");
                classType = "Wizard";
                classEmoji = Wizard;
            }
            else if (UsersClass == "Witch")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782383932989636608/hiclipart.com_2.png");
                classType = "Witch";
                classEmoji = Witch;
            }
            else if (UsersClass == "Rogue")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782382824916254730/hiclipart.com.png");
                classType = "Rogue";
                classEmoji = Rogue;
            }
            else if (UsersClass == "Monk")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782382486507618334/hiclipart.com_4.png");
                classType = "Monk";
                classEmoji = Monk;
            }
            else if (UsersClass == "Assassin")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782380493697843260/hiclipart.com.png");
                classType = "Assassin";
                classEmoji = Assassin;
            }
            else if (UsersClass == "Tamer")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782383216802922496/hiclipart.com.png");
                classType = "Tamer";
                classEmoji = Tamer;
            }
            else if (UsersClass == "Druid")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782381450774315008/hiclipart.com_2.png");
                classType = "Druid";
                classEmoji = Druid;
            }
            else if (UsersClass == "Necromancer")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782383900978053140/hiclipart.com_1.png");
                classType = "Necromancer";
                classEmoji = Necromancer;
            }
            else if (UsersClass == "Berserker")
            {
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782385335187013652/hiclipart.com_3.png");
                classType = "Berserker";
                classEmoji = Berserker;
            }
            else
            {
                embed.WithAuthor("Error.");
                embed.WithFooter("You don't have an account.");
                embed.WithDescription("You're not registered yet, use ``;begin [Class] [Name] [Age]`` to register.");
                embed.Color = Color.Red;

                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            string name = Data.Data.GetData_Name(uuser.Id);
            uint age = Data.Data.GetData_Age(uuser.Id);
            uint coins = Data.Data.GetData_GoldAmount(uuser.Id);
            uint level = Data.Data.GetData_Level(uuser.Id);
            uint health = Data.Data.GetData_Health(uuser.Id);
            uint damage = Data.Data.GetData_Damage(uuser.Id);
            uint currentHealth = Data.Data.GetData_CurrentHealth(uuser.Id);
            uint currentXp = Data.Data.GetData_XP(uuser.Id);
            uint neededXP = (Data.Data.GetData_Level(uuser.Id) * Data.Data.GetData_Level(uuser.Id));
            uint currentArmor = 0;
            uint currentRegen = 0;

            if (Data.Data.GetHelmet(uuser.Id) != null)
            {
                currentArmor += Data.Data.GetHelmet(uuser.Id).Armor;
                currentRegen += Data.Data.GetHelmet(uuser.Id).HealthGainOnDamage;
            }
            if (Data.Data.GetChestplate(uuser.Id) != null)
            {
                currentArmor += Data.Data.GetChestplate(uuser.Id).Armor;
                currentRegen += Data.Data.GetChestplate(uuser.Id).HealthGainOnDamage;
            }
            if (Data.Data.GetGauntlet(uuser.Id) != null)
            {
                currentArmor += Data.Data.GetGauntlet(uuser.Id).Armor;
                currentRegen += Data.Data.GetGauntlet(uuser.Id).HealthGainOnDamage;
            }
            if (Data.Data.GetBelt(uuser.Id) != null)
            {
                currentArmor += Data.Data.GetBelt(uuser.Id).Armor;
                currentRegen += Data.Data.GetBelt(uuser.Id).HealthGainOnDamage;
            }
            if (Data.Data.GetLeggings(uuser.Id) != null)
            {
                currentArmor += Data.Data.GetLeggings(uuser.Id).Armor;
                currentRegen += Data.Data.GetLeggings(uuser.Id).HealthGainOnDamage;
            }
            if (Data.Data.GetBoots(uuser.Id) != null)
            {
                currentArmor += Data.Data.GetBoots(uuser.Id).Armor;
                currentRegen += Data.Data.GetBoots(uuser.Id).HealthGainOnDamage;
            }

            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
            embed.WithAuthor("Profile of: " + user.Username, user.GetAvatarUrl());
            embed.WithColor(40, 200, 150);
            embed.WithFooter("XP needed to level up: " + currentXp + " / " + neededXP);
            embed.WithDescription("Class: " + classType + " " + classEmoji + "\nName: " + name + " " + Dove + "\nAge: " + age + " " + Age + "\n" +
                "\nGold Coins: " + coins + Coins + "\n" + "\n\n" + "Level: " + level + " " + Level + "\n" + "Health: " + currentHealth + "/" + health + " " +
                Health + "\nArmor: " + currentArmor + " " + Armor + "\nHealth Regeneration: " + currentRegen + " " + Regeneration + "\nDamage: " +
                damage + " " + Damage + "\n\n" + Skill + " " + Data.Data.GetData_SkillPoints(uuser.Id) + " Skill Points" + " " + Skill + "\n" +
                "Stamina: " + Data.Data.GetData_Stamina(uuser.Id) +
                "\nStability: " + Data.Data.GetData_Stability(uuser.Id) +
                "\nDexterity: " + Data.Data.GetData_Dexterity(uuser.Id) +
                "\nStrength: " + Data.Data.GetData_Strength(uuser.Id) +
                "\nLuck: " + Data.Data.GetData_Luck(uuser.Id));
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Group("gold"), Alias("golds", "Gold", "Golds"), Summary("Keyword for gold management.")]
        public class GoldGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("me", "my", "wealth", "currency"), Summary("Shows your current gold.")]
            public async Task Me()
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("You have " + Data.Data.GetData_GoldAmount(Context.User.Id) + " Gold coins.");
                embed.WithColor(40, 200, 150);
                embed.Color = Color.Gold;
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            [Command("give"), Alias("gift"), Summary("Give gold to a person.")]
            public async Task Give(IUser User = null, uint Amount = 0)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(40, 200, 150);

                if (User == null)
                {
                    embed.WithAuthor("Error.");
                    embed.WithFooter("Gold Not given.");
                    embed.WithDescription("Enter ``;Gold Give @user [Amount]``");
                    embed.Color = Color.Red;

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    return;
                }
                else if (User.IsBot)
                {
                    embed.WithAuthor("You stupid??");
                    embed.WithFooter("Do you think im a wench?!?!");
                    embed.WithDescription("Why are you trying to give me money you stupid fuck, stop being retarded.");
                    embed.Color = Color.Red;

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    return;
                }
                else if (Amount >= 1)
                {
                    SocketGuildUser Useer = Context.User as SocketGuildUser;
                    if (Context.User.Id != 353453210389839872)
                    {
                        if (Useer == User)
                        {
                            embed.WithAuthor("You're Retarded.");
                            embed.WithFooter("Fucking Moron.");
                            embed.WithDescription("What do you expect by giving money to yourseld?! \nthat the money would duplicate you dumbfuck?!");
                            embed.Color = Color.Red;

                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                            return;
                        }
                        else
                        {
                            bool exist = true;
                            using (var DbContext = new SqliteDbContext())
                            {
                                var query = DbContext.Data.Where(x => x.UserID == User.Id);
                                if (query == null || query.Count() < 1) exist = false;
                            }

                            if (exist)
                            {
                                if (Data.Data.GetData_GoldAmount(Context.User.Id) >= Amount)
                                {
                                    embed.WithAuthor("Success.");
                                    embed.WithFooter("You are now closer to being poor :).");
                                    embed.WithDescription($"{User.Mention}, you received **{Amount}** gold from {Context.User.Mention}");
                                    embed.Color = Color.Gold;

                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    await Data.Data.SaveData(Context.User.Id, (uint)(-Amount), 0, "", 0, 0, 0, 0, 0);
                                    await Data.Data.SaveData(User.Id, (uint)(Amount), 0, "", 0, 0, 0, 0, 0);

                                    return;
                                }
                                else
                                {
                                    embed.WithAuthor("Error.");
                                    embed.WithFooter("You're Poor.");
                                    embed.WithDescription("You don't have this much money man...");
                                    embed.Color = Color.Red;

                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    return;
                                }
                            }
                            else
                            {
                                embed.WithAuthor("Error.");
                                embed.WithFooter("You lost money tho.");
                                embed.WithDescription("They isnt registered in the bank. They must register in orfer to receive money.");
                                embed.Color = Color.Red;

                                await Context.Channel.SendMessageAsync("", false, embed.Build());

                                return;
                            }
                        }
                    }
                    else
                    {
                        embed.WithAuthor("Token of appreciation.");
                        embed.WithFooter("Master is a philanthropist <3 ");
                        embed.WithDescription("My master gave you a token of appreciation. You Recieved: " + User.Username + " " + Amount + " Gold.");
                        embed.Color = Color.Gold;

                        await Data.Data.SaveData(User.Id, Amount, 0, "", 0, 0, 0, 0, 0);
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                else
                {
                    embed.WithAuthor("Error.");
                    embed.WithFooter("Money not given.");
                    embed.WithDescription("The command functions with ``;Gold Gift @User [Amount]``");
                    embed.Color = Color.Red;

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    return;
                }
            }


            [Command("take"), Alias("remove", "tax"), Summary("Taxation is theft.")]
            public async Task Take(IUser User = null, uint Amount = 0)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(40, 200, 150);

                SocketGuildUser Useer = Context.User as SocketGuildUser;

                if (Context.User.Id != 353453210389839872)
                {
                    embed.WithAuthor("What are you doing?");
                    embed.WithFooter("You trying to order me you son of a bitch?!");
                    embed.WithDescription("You're not my master!!!");
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{User.Mention} Master ordered me to collect **{Amount}** gold as taxes from you, long live the state :).");
                    await Data.Data.SaveData(User.Id, (uint)-Amount, 0, "", 0, 0, 0, 0, 0);
                }
            }
        }

        public class SimpleDataContainer
        {
            public ulong ID;
            public uint GoldValue;
            public uint Level;
            public uint Damage;
            public uint Health;
            public uint Gems;
            public uint Age;
            public uint Event;
            public string Name;

            public SimpleDataContainer(ulong iD, uint goldValue, string name, uint level, uint damage, uint health, uint gems, uint age, uint eventD)
            {
                ID = iD;
                GoldValue = goldValue;
                Name = name;
                Level = level;
                Damage = damage;
                Health = health;
                Gems = gems;
                Age = age;
                Event = eventD;
            }

            ~SimpleDataContainer()
            {
                GC.Collect();
            }
        }

        [Command("leaderboard"), Alias("LB", "lb"), Summary("Rank of the richest.")]
        public async Task Leaderboard(string txt = "")
        {
            if (Context.Guild.Id != 773626859585142865)
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.WithAuthor("Error.");
                embed1.WithDescription("The command only work in Master's house.");
                embed1.WithColor(40, 200, 150);
                embed1.Color = Color.Red;
                await Context.Channel.SendMessageAsync("", false, embed1.Build());
                return;
            }

            if (txt == "" || txt == null)
            {
                EmbedBuilder embedu = new EmbedBuilder();
                embedu.WithAuthor("Error.");
                embedu.WithDescription("The command work with: ``;leaderboard [Type]``." +
                    "\n\nTypes:" +
                    "\n" + Coins + " - **Gold**" +
                    "\n" + Level + " - **Level**" +
                    "\n" + Damage + " - **Damage**" +
                    "\n" + Health + " - **Health**" +
                    "\n" + Age + " - **Age**");
                embedu.WithColor(40, 200, 150);
                embedu.Color = Color.Red;
                await Context.Channel.SendMessageAsync("", false, embedu.Build());
                return;
            }

            List<SimpleDataContainer> list = new List<SimpleDataContainer> { };
            EmbedBuilder embed = new EmbedBuilder();
            string output = "";

            if (txt == "Gold" || txt == "gold" || txt == "Coins" || txt == "coins" || txt == Coins)
            {
                foreach (SocketGuildUser users in Context.Guild.Users) list.Add(new SimpleDataContainer(users.Id, Data.Data.GetData_GoldAmount(users.Id), users.Username, 0, 0, 0, 0, 0, 0));
                list.Sort((s2, s1) => s1.GoldValue.CompareTo(s2.GoldValue));
                for (int i = 0; i < 5; ++i) output = output + "\n" + (i + 1) + ".) " + list[i].Name + " - Gold: " + list[i].GoldValue + Coins;
                embed.WithAuthor("Serverwide Leaderboard by Gold Coins");
                embed.Color = Color.Gold;
                embed.WithThumbnailUrl(Context.Guild.GetUser(list[0].ID).GetAvatarUrl());
            }
            else if (txt == "Level" || txt == "level" || txt == "Xp" || txt == "XP" || txt == "xp" || txt == Level)
            {
                foreach (SocketGuildUser users in Context.Guild.Users) list.Add(new SimpleDataContainer(users.Id, 0, users.Username, Data.Data.GetData_Level(users.Id), 0, 0, 0, 0, 0));
                list.Sort((s2, s1) => s1.Level.CompareTo(s2.Level));
                for (int i = 0; i < 5; ++i) output = output + "\n" + (i + 1) + ".) " + list[i].Name + " - Level: " + list[i].Level + Level;
                embed.WithAuthor("Serverwide Leaderboard by Levels");
                embed.Color = Color.DarkTeal;
                embed.WithThumbnailUrl(Context.Guild.GetUser(list[0].ID).GetAvatarUrl());
            }
            else if (txt == "Damage" || txt == "damage" || txt == "Strength" || txt == "strength" || txt == Damage)
            {
                foreach (SocketGuildUser users in Context.Guild.Users) list.Add(new SimpleDataContainer(users.Id, 0, users.Username, 0, Data.Data.GetData_Damage(users.Id), 0, 0, 0, 0));
                list.Sort((s2, s1) => s1.Damage.CompareTo(s2.Damage));
                for (int i = 0; i < 5; ++i) output = output + "\n" + (i + 1) + ".) " + list[i].Name + " - Damage: " + list[i].Damage + Damage;
                embed.WithAuthor("Serverwide Leaderboard by Damage");
                embed.Color = Color.DarkRed;
                embed.WithThumbnailUrl(Context.Guild.GetUser(list[0].ID).GetAvatarUrl());
            }
            else if (txt == "Health" || txt == "health" || txt == "Life" || txt == "life" || txt == Health)
            {
                foreach (SocketGuildUser users in Context.Guild.Users) list.Add(new SimpleDataContainer(users.Id, 0, users.Username, 0, 0, Data.Data.GetData_Health(users.Id), 0, 0, 0));
                list.Sort((s2, s1) => s1.Health.CompareTo(s2.Health));
                for (int i = 0; i < 5; ++i) output = output + "\n" + (i + 1) + ".) " + list[i].Name + " - Health: " + list[i].Health + Health;
                embed.WithAuthor("Serverwide Leaderboard by Health");
                embed.Color = Color.Green;
                embed.WithThumbnailUrl(Context.Guild.GetUser(list[0].ID).GetAvatarUrl());
            }
            else if (txt == "Age" || txt == "age" || txt == Age)
            {
                foreach (SocketGuildUser users in Context.Guild.Users) list.Add(new SimpleDataContainer(users.Id, 0, users.Username, 0, 0, 0, 0, Data.Data.GetData_Age(users.Id), 0));
                list.Sort((s2, s1) => s1.Age.CompareTo(s2.Age));
                for (int i = 0; i < 5; ++i) output = output + "\n" + (i + 1) + ".) " + list[i].Name + " - Age: " + list[i].Age + Age;
                embed.WithAuthor("Serverwide Leaderboard by Age");
                embed.Color = Color.Orange;
                embed.WithThumbnailUrl(Context.Guild.GetUser(list[0].ID).GetAvatarUrl());
            }
            else
            {
                EmbedBuilder embeed = new EmbedBuilder();
                embeed.WithAuthor("Error.");
                embeed.WithDescription("The command work with: ``;leaderboard [Type]``!" +
                    "\n\nTypes:" +
                    "\n" + Coins + " - **Gold**" +
                    "\n" + Level + " - **Level**" +
                    "\n" + Damage + " - **Damage**" +
                    "\n" + Health + " - **Health**" +
                    "\n" + Age + " - **Age**");
                embeed.WithColor(40, 200, 150);
                embeed.Color = Color.Red;
                await Context.Channel.SendMessageAsync("", false, embeed.Build());
                return;
            }

            embed.WithDescription(output);
            embed.WithFooter("List includes top 5 in the server.");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }



        [Command("info"), Alias("Info"), Summary("Info of something.")]
        public async Task Info(string check)
        {
            EmbedBuilder embed = new EmbedBuilder();

            string classEmoji = "";
            string baseStats = "";

            if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }

            else if (check == "" || check == "")
            {
                embed.WithImageUrl("");
                classEmoji = ;
                baseStats = "";
            }
            else
            {
                embed = new EmbedBuilder();
                embed.WithAuthor("uuhh...");
                embed.WithDescription("What the fuck you're talking about?!");
                return;
            }

            embed.WithTitle(classEmoji + " " + check + " Info");
            embed.WithDescription("Class: " + classEmoji + "\nBase stats: " + baseStats);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}

// YE YE YE YE YE YE YE YE YE
