using System.Collections.Generic;

namespace RPG.Resources
{
    public class GenericItem
    {
        public ulong OwnerID { get; set; }
        public uint ItemID { get; set; }
        public string WebURL { get; set; }
        public string ItemName { get; set; }
        public uint ItemCost { get; set; }
        public string ItemRarity { get; set; }
    }

    public class WearableItem
    {
        public ulong OwnerID { get; set; }
        public uint ItemID { get; set; }
        public string WebURL { get; set; }
        public string ItemName { get; set; }
        public uint ItemCost { get; set; }
        public string ItemRarity { get; set; }
        public uint Armor { get; set; }
        public uint Health { get; set; }
        public uint HealthGainOnDamage { get; set; }
    }

    public class Weapon : GenericItem
    {
        public uint Damage { get; set; }
        public uint LifeSteal { get; set; }

        public Weapon(uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint damage, uint lifeSteal)
        {
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Damage = damage;
            LifeSteal = lifeSteal;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Helmet : WearableItem
    {
        public Helmet(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Accessory : WearableItem
    {
        public Accessory(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Chestplate : WearableItem
    {
        public Chestplate(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Gauntlets : WearableItem
    {
        public Gauntlets(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Belt : WearableItem
    {
        public Belt(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Leggings : WearableItem
    {
        public Leggings(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Boots : WearableItem
    {
        public Boots(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Artifact : WearableItem
    {
        public Artifact(ulong ownerID, uint itemID, string url, string itemName, uint itemCost, string itemRarity, uint armor, uint health, uint healthGainOnDamage)
        {
            OwnerID = ownerID;
            ItemID = itemID;
            WebURL = url;
            ItemName = itemName;
            Armor = armor;
            Health = health;
            HealthGainOnDamage = healthGainOnDamage;
            ItemCost = itemCost;
            ItemRarity = itemRarity;
        }
    }

    public class Items
    {
        public const string EmptySlotIcon = "https://cdn.discordapp.com/attachments/542225685695954945/636453041821843486/EmptySlot.png";

        public static List<string> mod_names = new List<string>()
        {

        };

        public static string[] weapon_icons = new string[]
        {

        };

        public static string[] helmet_icons = new string[]
        {

        };

    public static string[] acessorie_icons = new string[]
            {

            };
    public static string[] chestplate_icons = new string[]
        {

        };
        public static string[] gauntlet_icons = new string[]
        {

        };
        public static string[] belt_icons = new string[]
        {

        };
        public static string[] legging_icons = new string[]
        {

        };
        public static string[] boot_icons = new string[]
        {

        };
    }
}
