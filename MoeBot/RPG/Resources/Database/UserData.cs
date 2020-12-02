using System.ComponentModel.DataAnnotations;

namespace MoeClorito.RPG.Resources.Database
{
    public class UserData
    {
        [Key]
        public ulong UserID { get; set; }
        public uint GoldAmount { get; set; }
        public uint Age { get; set; }
        public string Name { get; set; }
        public uint Damage { get; set; }
        public uint Health { get; set; }
        public uint Level { get; set; }
        public uint XP { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Day { get; set; }
        public int DailyClaimed { get; set; }
        public string Class { get; set; }
        public string Rank { get; set; }
        public uint FlatHealthRegen { get; set; }


        // skills
        public string Learned_Spells { get; set; }
        public string Equipped_Spell_1 { get; set; }
        public string Equipped_Spell_2 { get; set; }
        public string Equipped_Spell_3 { get; set; }
        public string Equipped_Spell_4 { get; set; }


        // Helmets
        public string Helmet_Name { get; set; }
        public string Helmet_URL { get; set; }
        public string Helmet_Rarity { get; set; }
        public uint Helmet_Level { get; set; }
        public uint Helmet_ModID { get; set; }
        public uint Helmet_Armor { get; set; }
        public uint Helmet_Health { get; set; }
        public uint Helmet_Regen { get; set; }
        public uint Helmet_Cost { get; set; }


        // Chestplates
        public string Chestplate_Name { get; set; }
        public string Chestplate_URL { get; set; }
        public string Chestplate_Rarity { get; set; }
        public uint Chestplate_Level { get; set; }
        public uint Chestplate_ModID { get; set; }
        public uint Chestplate_Armor { get; set; }
        public uint Chestplate_Health { get; set; }
        public uint Chestplate_Regen { get; set; }
        public uint Chestplate_Cost { get; set; }


        // Gauntlets
        public string Gauntlet_Name { get; set; }
        public string Gauntlet_URL { get; set; }
        public string Gauntlet_Rarity { get; set; }
        public uint Gauntlet_Level { get; set; }
        public uint Gauntlet_ModID { get; set; }
        public uint Gauntlet_Armor { get; set; }
        public uint Gauntlet_Health { get; set; }
        public uint Gauntlet_Regen { get; set; }
        public uint Gauntlet_Cost { get; set; }


        // Belts
        public string Belt_Name { get; set; }
        public string Belt_URL { get; set; }
        public string Belt_Rarity { get; set; }
        public uint Belt_Level { get; set; }
        public uint Belt_ModID { get; set; }
        public uint Belt_Armor { get; set; }
        public uint Belt_Health { get; set; }
        public uint Belt_Regen { get; set; }
        public uint Belt_Cost { get; set; }


        // Leggings
        public string Legging_Name { get; set; }
        public string Legging_URL { get; set; }
        public string Legging_Rarity { get; set; }
        public uint Legging_Level { get; set; }
        public uint Legging_ModID { get; set; }
        public uint Legging_Armor { get; set; }
        public uint Legging_Health { get; set; }
        public uint Legging_Regen { get; set; }
        public uint Legging_Cost { get; set; }


        // Boots
        public string Boot_Name { get; set; }
        public string Boot_URL { get; set; }
        public string Boot_Rarity { get; set; }
        public uint Boot_Level { get; set; }
        public uint Boot_ModID { get; set; }
        public uint Boot_Armor { get; set; }
        public uint Boot_Health { get; set; }
        public uint Boot_Regen { get; set; }
        public uint Boot_Cost { get; set; }


        // Artifact
        public string Artifact_Name { get; set; }
        public string Artifact_URL { get; set; }
        public string Artifact_Rarity { get; set; }
        public uint Artifact_Level { get; set; }
        public uint Artifact_ModID { get; set; }
        public uint Artifact_ModValue { get; set; }
        public uint Artifact_Armor { get; set; }
        public uint Artifact_Health { get; set; }
        public uint Artifact_Regen { get; set; }
        public uint Artifact_Damage { get; set; }
        public uint Artifact_Cost { get; set; }


        // Weapon
        public string Weapon_Name { get; set; }
        public string Weapon_Efficient_Class { get; set; }
        public string Weapon_URL { get; set; }
        public string Weapon_Rarity { get; set; }
        public uint Weapon_Level { get; set; }
        public uint Weapon_ModID { get; set; }
        public uint Weapon_ModValue { get; set; }
        public uint Weapon_Armor { get; set; }
        public uint Weapon_Health { get; set; }
        public uint Weapon_Regen { get; set; }
        public uint Weapon_Damage { get; set; }
        public uint Weapon_Cost { get; set; }


        // Stats
        public uint SkillPoints { get; set; }
        public uint Stamina { get; set; }
        public uint Stability { get; set; }
        public uint Dexterity { get; set; }
        public uint Strength { get; set; }
        public uint Luck { get; set; }


        // Potions
        public uint SmallPotionCount { get; set; }
        public uint MediumPotionCount { get; set; }
        public uint LargePotionCount { get; set; }


        // Boxes
        public uint CommonBoxCount { get; set; }
        public uint UncommonBoxCount { get; set; }
        public uint RareBoxCount { get; set; }
        public uint VeryRareBoxCount { get; set; }
        public uint LegendaryBoxCount { get; set; }
        public uint MythicBoxCount { get; set; }
        public uint OmegaBoxCount { get; set; }



        // PvP
        public uint WinCount { get; set; }
        public uint LoseCount { get; set; }


        // Exploration
        public uint Explored_Training_Zone { get; set; }
        public uint Explored_Forsaken_Graves { get; set; }
        public uint Explored_Goblins_Lair { get; set; }
        public uint Explored_Haunted_Towers { get; set; }
        public uint Explored_Lost_Caves { get; set; }
        public uint Explored_Spooky_Town { get; set; }
        public uint Explored_Broken_Village { get; set; }


        // Bank
        public uint StoredGold { get; set; }


        // Achievement Info
        public uint Total_Monsters_Slain { get; set; }
        public uint Total_Gold_Obtained { get; set; }
        public uint Total_Damage_Taken { get; set; }
        public uint Total_Items_Found { get; set; }
        public uint Total_Damage_Dealt { get; set; }
        public uint Total_Bosses_Slain { get; set; }
        public uint Total_Bosses_Encountered { get; set; }
        public uint Total_Rare_Creatures_Slain { get; set; }
        public uint Total_Explorations { get; set; }
        public uint Total_Lore_Found { get; set; }
        public uint Total_Nothings_Found { get; set; }
        public uint Total_Loot_Found_Exploring { get; set; }
        public uint Total_Areas_Discovered { get; set; }
        public uint Total_PvP_Damage_Dealt { get; set; }
        public uint Total_Deaths { get; set; }
        public uint Total_XP_Gained { get; set; }
        public uint Total_XP_Lost { get; set; }
        public uint Total_Reborns { get; set; }
        public uint Total_Gambles_Won { get; set; }
        public uint Total_Gambles_Lost { get; set; }
        public uint Total_Gold_Stolen { get; set; }
    }
}
