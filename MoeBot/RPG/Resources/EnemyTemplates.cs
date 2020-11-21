using System;


namespace RPG.Resources
{
    public struct Enemy
    {
        public string WebURL { get; set; }
        public uint MaxHealth { get; set; }
        public uint MinHealth { get; set; }
        public uint MaxDamage { get; set; }
        public uint MinDamage { get; set; }
        public uint MaxLevel { get; set; }
        public uint MinLevel { get; set; }
        public uint CurrentHealth { get; set; }
        public uint MaxGoldDrop { get; set; }
        public uint MinGoldDrop { get; set; }
        public uint MaxXpDrop { get; set; }
        public uint MinXpDrop { get; set; }
        public string Name { get; set; }
        public bool IsDead { get; set; }
        public bool IsInCombat { get; set; }

        public Enemy (string url, uint maxHealth, uint minHealth, uint maxDamage, uint minDamage, uint maxLevel, uint minLevel,
            uint maxGoldDrop, uint minGoldDrop, uint maxXPDrop, uint minXPDrop, string name, bool isDead = false, bool isInCombat = false)
        {
            WebURL = url;
            MaxHealth = maxHealth;
            MinHealth = minHealth;
            CurrentHealth = MaxHealth;
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            MaxLevel = maxLevel;
            MinLevel = minLevel;
            MaxGoldDrop = maxGoldDrop;
            MinGoldDrop = minGoldDrop;
            MaxXpDrop = maxXPDrop;
            MinXpDrop = minXPDrop;
            Name = name;
            IsDead = isDead;
            IsInCombat = isInCombat;
        }
    }

    public static class EnemyTemplates
    {
        public static Enemy Slime = new Enemy
            ("https://media.discordapp.net/attachments/773626859585142868/779627325032890379/250.png?width=225&height=225",
            20, // Max Health
            10, // Min Health
            5, // Max Damage
            1, // Min Damage
            3, // Max Level
            1, // Min Level
            3, // Max Gold
            1, // Min Gold
            5, // Max XP
            2, // Min XP
            "Slime" // Name

            );

        public static Enemy TrainingDoll = new Enemy
            (
            "https://cdn.discordapp.com/attachments/773626859585142868/779640389564366848/pngwing.com.png",
                50, // Max Health
                50, // Min Health
                1, // Max Damage
                1, // Min Damage
                1, // Max Level
                1, // Min Level
                0, // Max Gold
                0, // Min Gold
                7, // Max XP
                7, // Min XP
                "Training Bot" // Name
            );

        public static Enemy Goblin = new Enemy
            (
                "https://cdn.discordapp.com/attachments/773626859585142868/779624190910398504/PinClipart.com_female-superhero-clipart_1368184.png",
                20, // Max Health
                10, // Min Health
                25, // Max Damage
                5, // Min Damage
                5, // Max Level
                1, // Min Level
                10, // Max Gold
                1, // Min Gold
                3, // Max XP
                1, // Min XP
                "Goblin" // Name
            );

        public static Enemy Imp = new Enemy
            (
            "https://cdn.discordapp.com/attachments/773626859585142868/779643019749883914/f6852fee64f84b73dbd77d3dae0f76fb.png",
                25, // Max Health
                10, // Min Health
                15, // Max Damage
                2, // Min Damage
                5, // Max Level
                1, // Min Level
                3, // Max Gold
                1, // Min Gold
                7, // Max XP
                3, // Min XP
                "Imp" // Name
            );



        public class DesignedEnemy
        {
            public string name = "";
            public string flavor = "";
            public string iconURL = "";
            public int minLevel = 0;
            public int maxLevel = 0;
            public float difficulty = 0f;
            public float tankiness = 0f;
            public bool valid = false;

            public DesignedEnemy(string _name, string _flavor, string _iconURL, string _minLevel, string _maxLevel, string _difficulty, string _tankiness)
            {
                name = _name;

                while (_flavor.Contains("[MonsterName]"))
                {
                    _flavor = _flavor.Replace("[MonsterName]", _name);
                }

                flavor = flavor;
                iconURL = _iconURL;

                bool parsedMinLevel = int.TryParse(_minLevel, out minLevel);
                bool parsedMaxLevel = int.TryParse(_maxLevel, out maxLevel);
                bool parsedDifficulty = float.TryParse(_difficulty, out difficulty);
                bool parsedTankiness = float.TryParse(_tankiness, out tankiness);

                if (!parsedMinLevel)
                    Console.WriteLine("[Designed Enemy] Error parsing min level for " + _name);
                if (!parsedMaxLevel)
                    Console.WriteLine("[Designed Enemy] Error parsing max level for " + _name);
                if (!parsedDifficulty)
                    Console.WriteLine("[Designed Enemy] Error parsing difficulty for " + _name);
                if (!parsedTankiness)
                    Console.WriteLine("[Designed Enemy] Error parsing tankiness for " + _name);

                if (parsedMinLevel && parsedMaxLevel)
                {
                    if (minLevel > maxLevel)
                    {
                        Console.WriteLine("[Designed Enemy] Error, min level is greater than max level for " + _name + ". Min level: " + minLevel + ", Max Level: " + maxLevel);
                    }
                    else if (parsedMinLevel && parsedMaxLevel && parsedDifficulty && parsedTankiness)
                    {
                        Console.WriteLine("[Designed Enemy] Successfully loaded " + _name);
                        valid = true;
                    }
                }

            }
        }
    }
}
