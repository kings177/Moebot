﻿using RPG.Resources.Database;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace RPG.Data
{
    public static class Data
    {


        // PLAYERS STUFF

        public static uint GetData_GoldAmount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.GoldAmount).FirstOrDefault();
            }
        }

        public static string GetData_Name(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return "You need to create an account.";

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Name).FirstOrDefault();
            }
        }

        public static uint GetData_Age(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Age).FirstOrDefault();
            }
        }

        public static uint GetData_Damage(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Damage).FirstOrDefault();
            }
        }

        public static uint GetData_Health(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Health).FirstOrDefault();
            }
        }

        public static uint GetData_Level(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Level).FirstOrDefault();
            }
        }

        public static uint GetData_XP(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.XP).FirstOrDefault();
            }
        }

        public static uint GetData_CurrentHealth(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.CurrentHealth).FirstOrDefault();
            }
        }

        public static int GetData_Hour(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Hour).FirstOrDefault();
            }
        }

        public static int GetData_Minute(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Minute).FirstOrDefault();
            }
        }

        public static int GetData_Second(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Second).FirstOrDefault();
            }
        }

        public static int GetData_Day(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Day).FirstOrDefault();
            }
        }

        public static int GetLastDaily(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.DailyClaimed).FirstOrDefault();
            }
        }

        public static string GetRank(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return "None";

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Rank).FirstOrDefault();
            }
        }

        public static string GetClass(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return "None";

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Class).FirstOrDefault();
            }
        }


        // ITEMS

        public static RPG.Resources.Helmet GetHelmet(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return null;
                {
                    RPG.Resources.Helmet helmet = new RPG.Resources.Helmet
                    (
                        UserID,
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_ModID).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_URL).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_Name).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_Cost).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_Rarity).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_Armor).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_Health).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Helmet_Regen).FirstOrDefault()
                    );

                    if (helmet.ItemName == "0")
                        return null;

                    return helmet;
                }
            }
        }
        public static RPG.Resources.Gauntlets GetGauntlet(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null) return null;
                {
                    RPG.Resources.Gauntlets gauntlets = new RPG.Resources.Gauntlets
                    (
                        UserID,
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_ModID).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_URL).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_Name).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_Cost).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_Rarity).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_Armor).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_Health).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Gauntlet_Regen).FirstOrDefault()
                    );

                    if (gauntlets.ItemName == "0")
                        return null;

                    return gauntlets;
                }
            }
        }

        public static RPG.Resources.Chestplate GetChestplate(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null) return null;
                {
                    RPG.Resources.Chestplate chestplate = new RPG.Resources.Chestplate
                    (
                        UserID,
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_ModID).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_URL).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_Name).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_Cost).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_Rarity).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_Armor).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_Health).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Chestplate_Regen).FirstOrDefault()
                    );

                    if (chestplate.ItemName == "0")
                        return null;

                    return chestplate;
                }
            }
        }

        public static RPG.Resources.Belt GetBelt(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null) return null;
                {
                    RPG.Resources.Belt belt = new RPG.Resources.Belt
                    (
                        UserID,
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_ModID).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_URL).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_Name).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_Cost).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_Rarity).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_Armor).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_Health).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Belt_Regen).FirstOrDefault()
                    );

                    if (belt.ItemName == "0")
                        return null;

                    return belt;
                }
            }
        }

        public static RPG.Resources.Leggings GetLeggings(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null) return null;
                {
                    RPG.Resources.Leggings leggings = new RPG.Resources.Leggings
                    (
                        UserID,
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_ModID).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_URL).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_Name).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_Cost).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_Rarity).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_Armor).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_Health).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Legging_Regen).FirstOrDefault()
                    );

                    if (leggings.ItemName == "0")
                        return null;

                    return leggings;
                }
            }
        }

        public static RPG.Resources.Boots GetBoots(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null) return null;
                {
                    RPG.Resources.Boots boots = new RPG.Resources.Boots
                    (
                        UserID,
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_ModID).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_URL).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_Name).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_Cost).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_Rarity).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_Armor).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_Health).FirstOrDefault(),
                        DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Boot_Regen).FirstOrDefault()
                    );

                    if (boots.ItemName == "0")
                        return null;

                    return boots;
                }
            }
        }

        // SKILLS

        public static uint GetData_SkillPoints(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.SkillPoints).FirstOrDefault();
            }
        }

        public static uint GetData_Stamina(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Stamina).FirstOrDefault();
            }
        }

        public static uint GetData_Stability(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Stability).FirstOrDefault();
            }
        }

        public static uint GetData_Dexterity(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Dexterity).FirstOrDefault();
            }
        }

        public static uint GetData_Strength(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Strength).FirstOrDefault();
            }
        }

        public static uint GetData_Luck(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Luck).FirstOrDefault();
            }
        }

        // POTIONS

        public static uint GetData_SmallPotionCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.SmallPotionCount).FirstOrDefault();
            }
        }

        public static uint GetData_MediumPotionCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.MediumPotionCount).FirstOrDefault();
            }
        }

        public static uint GetData_LargePotionCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.LargePotionCount).FirstOrDefault();
            }
        }

        // CHESTS

        public static uint GetData_CommonBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.CommonBoxCount).FirstOrDefault();
            }
        }

        public static uint GetData_UncommonBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.UncommonBoxCount).FirstOrDefault();
            }
        }

        public static uint GetData_RareBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.RareBoxCount).FirstOrDefault();
            }
        }

        public static uint GetData_VeryRareBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.VeryRareBoxCount).FirstOrDefault();
            }
        }

        public static uint GetData_LegendaryBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.LegendaryBoxCount).FirstOrDefault();
            }
        }

        public static uint GetData_MythicBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.MythicBoxCount).FirstOrDefault();
            }
        }

        public static uint GetData_OmegaBoxCount(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return 0;

                return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.OmegaBoxCount).FirstOrDefault();
            }
        }


        /*                                                SPAM PART TO CONFIG ALL OF THE PREVIOUS STUFF AND SOME OTHERS BULLSHIT                                               */

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public static async Task SetXP(ulong UserID, uint XP)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {

                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();
                        Current.XP = XP;
                        DbContext.Data.Update(Current);
                    }
                }
                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SaveData(ulong UserID, uint GoldAmount, uint Age, string Name, uint Damage, uint Health, uint Level, uint XP, uint CurrentHealth)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);
                if (query != null && query.Count() < 1)
                {
                    DbContext.Data.Add(new UserData
                    {
                        UserID = UserID,
                        GoldAmount = GoldAmount,
                        Age = Age,
                        Name = Name,
                        Damage = Damage,
                        Health = Health,
                        Level = Level,
                        XP = XP,
                        CurrentHealth = Health,
                    });
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();
                        Current.GoldAmount += GoldAmount;
                        Current.Age += Age;
                        Current.Damage += Damage;
                        Current.Health += Health;
                        Current.Level += Level;

                        int predetermineXP = ((int)Current.XP + (int)XP);
                        if (predetermineXP < 0) Current.XP = 0;
                        else Current.XP += XP;

                        int predetermineHealth = ((int)Current.CurrentHealth + (int)CurrentHealth);
                        if (predetermineHealth < 0) Current.CurrentHealth = 0;
                        else Current.CurrentHealth += CurrentHealth;

                        if (Current.CurrentHealth > Current.Health)
                            Current.CurrentHealth = Current.Health;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SubtractSaveData(ulong UserID, uint GoldAmount, uint Age, string Name, uint Damage, uint Health, uint Level, uint XP, uint CurrentHealth)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1) { return; }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        if ((int)Current.GoldAmount - (int)GoldAmount < 0)
                            Current.GoldAmount = 0;
                        else Current.GoldAmount -= GoldAmount;

                        if ((int)Current.Age - (int)Age < 0)
                            Current.Damage = 0;
                        else Current.Damage -= Damage;

                        if ((int)Current.Health - (int)Health < 0)
                            Current.Health = 0;
                        else Current.Health -= Health;

                        if ((int)Current.Level - (int)Level < 0)
                            Current.Level = 0;
                        else Current.Level -= Level;

                        if ((int)Current.XP - (int)XP < 0)
                            Current.XP = 0;
                        else Current.XP -= XP;

                        if ((int)Current.CurrentHealth - (int)CurrentHealth < 0)
                            Current.CurrentHealth = 0;
                        else Current.CurrentHealth -= CurrentHealth;

                        if (Current.CurrentHealth > Current.Health)
                            Current.CurrentHealth = Current.Health;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SubtractSkillPoints(ulong UserID, uint stamina, uint stability, uint dexterity, uint strength, uint luck, uint charisma)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1) { return; }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        if ((int)Current.Stamina - (int)stamina < 0)
                            Current.Stamina = 0;
                        else Current.Stamina -= stamina;

                        if ((int)Current.Stability - (int)stability < 0)
                            Current.Stability = 0;
                        else Current.Stability -= stability;

                        if ((int)Current.Dexterity - (int)dexterity < 0)
                            Current.Dexterity = 0;
                        else Current.Dexterity -= dexterity;

                        if ((int)Current.Strength - (int)strength < 0)
                            Current.Strength = 0;
                        else Current.Strength -= strength;

                        if ((int)Current.Luck - (int)luck < 0)
                            Current.Luck = 0;
                        else Current.Luck -= luck;


                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddSkillPoints(ulong UserID, uint stamina, uint stability, uint dexterity, uint strength, uint luck, uint charisma)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1) { return; }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Stamina += stamina;

                        Current.Stability += stability;

                        Current.Dexterity += dexterity;

                        Current.Strength += strength;

                        Current.Luck += luck;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateTime(ulong UserID, int Hour, int Minute, int Second, int Day)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query == null || query.Count() < 1) { return; }

                UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();
                Current.Hour = Hour; Current.Minute = Minute; Current.Second = Second; Current.Day = Day;
                DbContext.Data.Update(Current);

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetDailyClaimed(ulong UserID, int Date)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);
                if (query == null || query.Count() < 1) { return; }
                UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();
                Current.DailyClaimed = Date;
                DbContext.Data.Update(Current);
                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetRank(ulong UserID, string SetRank)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);
                if (query == null || query.Count() < 1) { return; }
                UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();
                Current.Rank = SetRank;
                DbContext.Data.Update(Current);
                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetClass(ulong UserID, string SetClass)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);
                if (query == null || query.Count() < 1) { return; }
                UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();
                Current.Class = SetClass;
                DbContext.Data.Update(Current);
                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetHelmet(ulong UserID, RPG.Resources.Helmet helmet)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Helmet_ModID = helmet.ItemID;
                        Current.Helmet_Name = helmet.ItemName;
                        Current.Helmet_URL = helmet.WebURL;
                        Current.Helmet_Rarity = helmet.ItemRarity;
                        Current.Helmet_Armor = helmet.Armor;
                        Current.Helmet_Health = helmet.Health;
                        Current.Helmet_Regen = helmet.HealthGainOnDamage;
                        Current.Helmet_Cost = helmet.ItemCost;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteHelmet(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Helmet_ModID = 0;
                        Current.Helmet_Name = "0";
                        Current.Helmet_URL = "0";
                        Current.Helmet_Rarity = "0";
                        Current.Helmet_Armor = 0;
                        Current.Helmet_Health = 0;
                        Current.Helmet_Regen = 0;
                        Current.Helmet_Cost = 0;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetChestplate(ulong UserID, RPG.Resources.Chestplate chestplate)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Chestplate_ModID = chestplate.ItemID;
                        Current.Chestplate_Name = chestplate.ItemName;
                        Current.Chestplate_URL = chestplate.WebURL;
                        Current.Chestplate_Rarity = chestplate.ItemRarity;
                        Current.Chestplate_Armor = chestplate.Armor;
                        Current.Chestplate_Health = chestplate.Health;
                        Current.Chestplate_Regen = chestplate.HealthGainOnDamage;
                        Current.Chestplate_Cost = chestplate.ItemCost;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteChestplate(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Chestplate_ModID = 0;
                        Current.Chestplate_Name = "0";
                        Current.Chestplate_URL = "0";
                        Current.Chestplate_Rarity = "0";
                        Current.Chestplate_Armor = 0;
                        Current.Chestplate_Health = 0;
                        Current.Chestplate_Regen = 0;
                        Current.Chestplate_Cost = 0;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetGauntlets(ulong UserID, RPG.Resources.Gauntlets gauntlets)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Gauntlet_ModID = gauntlets.ItemID;
                        Current.Gauntlet_Name = gauntlets.ItemName;
                        Current.Gauntlet_URL = gauntlets.WebURL;
                        Current.Gauntlet_Rarity = gauntlets.ItemRarity;
                        Current.Gauntlet_Armor = gauntlets.Armor;
                        Current.Gauntlet_Health = gauntlets.Health;
                        Current.Gauntlet_Regen = gauntlets.HealthGainOnDamage;
                        Current.Gauntlet_Cost = gauntlets.ItemCost;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteGauntlets(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Gauntlet_ModID = 0;
                        Current.Gauntlet_Name = "0";
                        Current.Gauntlet_URL = "0";
                        Current.Gauntlet_Rarity = "0";
                        Current.Gauntlet_Armor = 0;
                        Current.Gauntlet_Health = 0;
                        Current.Gauntlet_Regen = 0;
                        Current.Gauntlet_Cost = 0;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetBelt(ulong UserID, RPG.Resources.Belt belt)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Belt_ModID = belt.ItemID;
                        Current.Belt_Name = belt.ItemName;
                        Current.Belt_URL = belt.WebURL;
                        Current.Belt_Rarity = belt.ItemRarity;
                        Current.Belt_Armor = belt.Armor;
                        Current.Belt_Health = belt.Health;
                        Current.Belt_Regen = belt.HealthGainOnDamage;
                        Current.Belt_Cost = belt.ItemCost;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteBelt(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Belt_ModID = 0;
                        Current.Belt_Name = "0";
                        Current.Belt_URL = "0";
                        Current.Belt_Rarity = "0";
                        Current.Belt_Armor = 0;
                        Current.Belt_Health = 0;
                        Current.Belt_Regen = 0;
                        Current.Belt_Cost = 0;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetLeggings(ulong UserID, RPG.Resources.Leggings leggings)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Legging_ModID = leggings.ItemID;
                        Current.Legging_Name = leggings.ItemName;
                        Current.Legging_URL = leggings.WebURL;
                        Current.Legging_Rarity = leggings.ItemRarity;
                        Current.Legging_Armor = leggings.Armor;
                        Current.Legging_Health = leggings.Health;
                        Current.Legging_Regen = leggings.HealthGainOnDamage;
                        Current.Legging_Cost = leggings.ItemCost;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteLeggings(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Legging_ModID = 0;
                        Current.Legging_Name = "0";
                        Current.Legging_URL = "0";
                        Current.Legging_Rarity = "0";
                        Current.Legging_Armor = 0;
                        Current.Legging_Health = 0;
                        Current.Legging_Regen = 0;
                        Current.Legging_Cost = 0;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SeBoots(ulong UserID, RPG.Resources.Boots boots)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Boot_ModID = boots.ItemID;
                        Current.Boot_Name = boots.ItemName;
                        Current.Boot_URL = boots.WebURL;
                        Current.Boot_Rarity = boots.ItemRarity;
                        Current.Boot_Armor = boots.Armor;
                        Current.Boot_Health = boots.Health;
                        Current.Boot_Regen = boots.HealthGainOnDamage;
                        Current.Boot_Cost = boots.ItemCost;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteBoots(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Boot_ModID = 0;
                        Current.Boot_Name = "0";
                        Current.Boot_URL = "0";
                        Current.Boot_Rarity = "0";
                        Current.Boot_Armor = 0;
                        Current.Boot_Health = 0;
                        Current.Boot_Regen = 0;
                        Current.Boot_Cost = 0;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddSkillPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.SkillPoints = Current.SkillPoints + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetSkillPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.SkillPoints = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddStaminaPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Stamina = Current.Stamina + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetStaminaPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Stamina = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddStabilityPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Stability = Current.Stability + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetStabilityPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Stability = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddDexterityPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Dexterity = Current.Dexterity + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetDexterityPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Dexterity = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddStrengthPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Strength = Current.Strength + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetStrengthPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Strength = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddLuckPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Luck = Current.Luck + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetLuckPoints(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.Luck = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddSmallPotionCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.SmallPotionCount = Current.SmallPotionCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetSmallPotionCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.SmallPotionCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddMediumPotionCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.MediumPotionCount = Current.MediumPotionCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetMediumPotionCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.MediumPotionCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddLargePotionCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.LargePotionCount = Current.LargePotionCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetLargePotionCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.LargePotionCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddCommonBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.CommonBoxCount = Current.CommonBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetCommonBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.CommonBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }
        public static async Task AddUncommonBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.UncommonBoxCount = Current.UncommonBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetUncommonBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.UncommonBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddRareBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.RareBoxCount = Current.RareBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetRareBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.RareBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddVeryRareBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.VeryRareBoxCount = Current.VeryRareBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetVeryRareBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.VeryRareBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddLegendaryBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.LegendaryBoxCount = Current.LegendaryBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetLegendaryBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.LegendaryBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddMythicBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.MythicBoxCount = Current.MythicBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetMythicBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.MythicBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddOmegaBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.OmegaBoxCount = Current.OmegaBoxCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetOmegaBoxCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.OmegaBoxCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddWinCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.WinCount = Current.WinCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetWinCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.WinCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task AddLoseCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.LoseCount = Current.LoseCount + amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SetLoseCount(ulong UserID, uint amount)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);

                if (query != null && query.Count() < 1)
                {
                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        Current.LoseCount = amount;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }

        public static void DeleteSave(ulong UserID)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return;
                else DbContext.Data.Remove(DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault());
            }
        }

        public static bool Explored(ulong UserID, string RegionName)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Data.AsQueryable().Where(x => x.UserID == UserID) == null)
                    return false;

                if (RegionName == "Training_Zone")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Training_Zone).FirstOrDefault() == 1;

                else if (RegionName == "Forsaken_Graves")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Forsaken_Graves).FirstOrDefault() == 1;

                else if (RegionName == "Goblins_Lair")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Goblins_Lair).FirstOrDefault() == 1;

                else if (RegionName == "Haunted_Towers")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Haunted_Towers).FirstOrDefault() == 1;

                else if (RegionName == "Lost_Caves")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Lost_Caves).FirstOrDefault() == 1;

                else if (RegionName == "Spooky_Town")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Spooky_Town).FirstOrDefault() == 1;

                else if (RegionName == "Broken_Village")
                    return DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).Select(x => x.Explored_Broken_Village).FirstOrDefault() == 1;
                else
                {
                    Console.WriteLine("Error in loading the regions explorated: " + RegionName);
                    return false;
                }
            }
        }

        public static async Task Explore(ulong UserID, string RegionName)
        {
            using (var DbContext = new SqliteDbContext())
            {
                var query = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID);
                if (query != null && query.Count() < 1)
                {

                }
                else
                {
                    if (query != null)
                    {
                        UserData Current = DbContext.Data.AsQueryable().Where(x => x.UserID == UserID).FirstOrDefault();

                        if (RegionName == "Training_zone")
                            Current.Explored_Training_Zone = 1;

                        else if (RegionName == "Forsaken_Graves")
                            Current.Explored_Forsaken_Graves = 1;
                        else if (RegionName == "Goblins_Lair")
                            Current.Explored_Goblins_Lair = 1;
                        else if (RegionName == "Haunted_Towers")
                            Current.Explored_Haunted_Towers = 1;
                        else if (RegionName == "Lost_Caves")
                            Current.Explored_Lost_Caves = 1;
                        else if (RegionName == "Spooky_Town")
                            Current.Explored_Spooky_Town = 1;
                        else if (RegionName == "Broken_Village")
                            Current.Explored_Broken_Village = 1;

                        DbContext.Data.Update(Current);
                    }
                }

                await DbContext.SaveChangesAsync();
            }
        }
    }
}
