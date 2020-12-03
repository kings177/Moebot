using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using static RPG.Emojis.Emojis;

namespace RPG.Config
{
    public class SettingUp : ModuleBase<SocketCommandContext>
    {
        [Command("start"), Alias("begin", "START"), Summary("Start the RPG session. use ;start [Class] [Name] [Age]")]
        public async Task Start([Remainder] string Input = "None")
        {
            string[] input = Input.Split();

            var vuser = Context.User as SocketGuildUser;


            string UsersRank = Data.Data.GetRank(Context.User.Id);

            if (UsersRank == "Bronze" || UsersRank == "Silver" || UsersRank == "Gold" || UsersRank == "Platinum" || UsersRank == "GrandMaster")
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("Salute.");
                embed.WithColor(40, 200, 150);
                embed.WithFooter("");
                embed.WithDescription("You're already an adventurer.");
                embed.Color = Color.Red;

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else if (input.Length != 3)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor("You were not accepted in the guild " + Context.User.Username + ".", Context.User.GetAvatarUrl());
                embed.WithColor(40, 200, 150);
                embed.WithFooter("Unsuccessful");
                embed.WithDescription("Sorry, But your standards doens't equal the guild's requests. \n" +
                    "When you improve and decide your class, Just type in ``;start [Class] [Name] [Age]``");
                embed.Color = Color.Red;

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                uint x = 0;

                if (uint.TryParse(input[2], out x) && (input[0] == "Archer" || input[0] == "archer" || input[0] == "Paladin" || input[0] == "paladin"
                    || input[0] == "Warrior" || input[0] == "warrior" || input[0] == "Wizard" || input[0] == "wizard" || input[0] == "Witch" || input[0] == "witch"
                    || input[0] == "Rogue" || input[0] == "rogue" || input[0] == "Monk" || input[0] == "monk" || input[0] == "Assassin" || input[0] == "assassin"
                    || input[0] == "Tamer" || input[0] == "tamer" || input[0] == "Druid" || input[0] == "druid" || input[0] == "Necromancer" || input[0] == "necromancer"
                    || input[0] == "Berserker" || input[0] == "berserker"))

                {
                    x = uint.Parse(input[2]);
                    if (x < 1) x = 1;

                    EmbedBuilder embed = new EmbedBuilder();
                    var user = Context.User;


                    if (input[0] == "Archer" || input[0] == "archer")
                    {
                        embed.WithImageUrl("https://cdn.discordapp.com/attachments/773626859585142868/782378943218974781/hiclipart.com_1.png");

                        await Data.Data.SaveData(user.Id, 500, x, input[1], 35, 22, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Archer");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Paladin" || input[0] == "paladin")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782380077311983636/hiclipart.com_2.png?width=259&height=464");

                        await Data.Data.SaveData(user.Id, 520, x, input[1], 15, 55, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Paladin");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Warrior" || input[0] == "warrior")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782378958326726676/hiclipart.com.png?width=266&height=464");

                        await Data.Data.SaveData(user.Id, 500, x, input[1], 20, 50, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Warrior");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Wizard" || input[0] == "wizard")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782380503834951710/hiclipart.com_1.png?width=205&height=464");

                        await Data.Data.SaveData(user.Id, 500, x, input[1], 48, 22, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Wizard");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Witch" || input[0] == "witch")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782383932989636608/hiclipart.com_2.png?width=383&height=464");

                        await Data.Data.SaveData(user.Id, 500, x, input[1], 45, 25, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Witch");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Rogue" || input[0] == "rogue")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782382824916254730/hiclipart.com.png?width=293&height=464");

                        await Data.Data.SaveData(user.Id, 650, x, input[1], 60, 10, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Rogue");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Monk" || input[0] == "monk")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782382486507618334/hiclipart.com_4.png?width=433&height=464");

                        await Data.Data.SaveData(user.Id, 400, x, input[1], 40, 35, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Monk");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Assassin" || input[0] == "assassin")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782380493697843260/hiclipart.com.png?width=220&height=464");

                        await Data.Data.SaveData(user.Id, 650, x, input[1], 65, 5, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Assassin");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Tamer" || input[0] == "tamer")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782383216802922496/hiclipart.com.png?width=284&height=464");

                        await Data.Data.SaveData(user.Id, 550, x, input[1], 65, 5, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Tamer");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Druid" || input[0] == "druid")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782381450774315008/hiclipart.com_2.png?width=231&height=464");


                        await Data.Data.SaveData(user.Id, 300, x, input[1], 48, 25, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Druid");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Necromancer" || input[0] == "necromancer")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782383900978053140/hiclipart.com_1.png?width=248&height=464");

                        await Data.Data.SaveData(user.Id, 350, x, input[1], 65, 12, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Necromancer");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    else if (input[0] == "Berserker" || input[0] == "berserker")
                    {
                        embed.WithImageUrl("https://media.discordapp.net/attachments/773626859585142868/782385335187013652/hiclipart.com_3.png?width=377&height=465");

                        await Data.Data.SaveData(user.Id, 480, x, input[1], 40, 30, 1, 0, 0);
                        await Data.Data.SetClass(Context.User.Id, "Berserker");
                        await Data.Data.DeleteHelmet(Context.User.Id);
                        await Data.Data.DeleteChestplate(Context.User.Id);
                        await Data.Data.DeleteGauntlets(Context.User.Id);
                        await Data.Data.DeleteBelt(Context.User.Id);
                        await Data.Data.DeleteLeggings(Context.User.Id);
                        await Data.Data.DeleteBoots(Context.User.Id);
                        await Data.Data.SetSkillPoints(Context.User.Id, 0);
                        await Data.Data.SetStaminaPoints(Context.User.Id, 0);
                        await Data.Data.SetStabilityPoints(Context.User.Id, 0);
                        await Data.Data.SetDexterityPoints(Context.User.Id, 2);
                        await Data.Data.SetStrengthPoints(Context.User.Id, 4);
                        await Data.Data.SetLuckPoints(Context.User.Id, 0);
                        await Data.Data.SetSmallPotionCount(Context.User.Id, 6);
                        await Data.Data.SetMediumPotionCount(Context.User.Id, 4);
                        await Data.Data.SetLargePotionCount(Context.User.Id, 2);
                        await Data.Data.SetCommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetUncommonBoxCount(Context.User.Id, 2);
                        await Data.Data.SetRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetVeryRareBoxCount(Context.User.Id, 0);
                        await Data.Data.SetLegendaryBoxCount(Context.User.Id, 0);
                        await Data.Data.SetMythicBoxCount(Context.User.Id, 0);
                        await Data.Data.SetOmegaBoxCount(Context.User.Id, 0);
                        await Data.Data.SetWinCount(Context.User.Id, 0);
                        await Data.Data.SetLoseCount(Context.User.Id, 0);
                    }

                    await Data.Data.SetRank(Context.User.Id, "Bronze");
                    await Game.UpdateUserData();

                    embed.WithAuthor("You were accepted in the league!", Context.User.GetAvatarUrl());
                    embed.Color = Color.Green;

                    SocketTextChannel channel = null;

                    foreach (SocketGuildChannel channelsInServer in Context.Guild.Channels)
                    {
                        if (channelsInServer.Name == "adventurer-title")
                            channel = channelsInServer as SocketTextChannel;
                    }

                    if (channel == null)
                    {
                        embed.WithDescription("Yo " + input[1] + ", welcome to the league. i see that you're a " + Data.Data.GetClass(Context.User.Id) +
                            "\nSo... you're actually not in yet, its time to prove your strength boy!\n\n" +
                            "ok, so here we got a training doll, this beauty right here is the newest model that i developed, try your best buddy, go without fear.\n" +
                            "Type ;explore to begin.");
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        embed.WithDescription("Yo " + input[1] + ", welcome to the league. i see that you're a " + Data.Data.GetClass(Context.User.Id) +
                            "\nSo... you're actually not in yet, its time to prove your strength boy!\n\n" +
                            "ok, so here we got a training doll, this beauty right here is the newest model that i developed, try your best buddy, go without fear.\n" +
                            "Type ;explore to begin.");
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor("You were not accepted to the guild " + Context.User.Username + "!", Context.User.GetAvatarUrl());
                    embed.WithColor(40, 200, 150);
                    embed.WithFooter("Signup failed...");
                    embed.WithDescription("You miss typed some shit and the i couldn't figure it out wtf that was, do it again but right this time.\n" +
                        "use ;begin [Class] [Name] [Age] \nto know the available classes type ;classes");
                    embed.Color = Color.Red;
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }

        [Command("classes"), Alias("class", "CLASSES", "CLASS")]
        public async Task ClassList([Remainder] string Input = "None")
        {
            EmbedBuilder ebed = new EmbedBuilder();

            ebed.WithAuthor("those are the clases, if you wat more, go fuck yourself.");
            ebed.WithColor(40, 200, 150);
            ebed.WithFooter("");

            ebed.WithDescription(Archer + "Archer - Damage+++, Defense--\n" +
                Paladin + "Paladin - Damage-, Defense+++\n" +
                Warrior + "Warrior - Damage-, Defense+++\n" +
                Wizard + "Wizard - Damage++, Defense+\n" +
                Witch + "Witch - Damage+++, Defense--\n" +
                Rogue + "Rogue - Damage+++, Defense--\n" +
                Monk + "Monk - Damage++, Defense+\n" +
                Assassin + "Assassin - Damage++++, Defense---\n" +
                Tamer + "Tamer - Damage++++, Defense---\n" +
                Druid + "Druid - Damage+, Defense++\n" +
                Necromancer + "Necromancer - Damage+++, Defense---\n" +
                Berserker + "Berserker - Damage++, Defense++\n" +
                "The differences between them are the beginning stats and skills, but in the high level, the difference isnt that high");
            ebed.Color = Color.Gold;

            await Context.Channel.SendMessageAsync("", false, ebed.Build());
        }


    }
}
