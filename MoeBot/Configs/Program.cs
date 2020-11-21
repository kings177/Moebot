using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using MoeClorito.RPG.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MoeClorito
{
    class Program
    {

        public static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        public static string ApplicationName = "Google Sheets API .NET Connection";


        static void RPG(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            UserCredential credential;

            using (FileStream stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                //file token.json is created automatically when the authorization complete its flow for the first time
                // its store the users access and refresh tokens.

                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            string spreadsheetId = "1aUUwCBo94UClCTHTnon1om-OMYvmAKZ8uUpwpkgwwbk";
            string range = "Runtime Spells!A3:t";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);


            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;

            int loaded = 0;

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Console.WriteLine("Adding new spell to the database: {0}", row[0] as string);

                    loaded++;

                    float damage_min = 0;
                    float damage_max = 0;
                    float crit_chance = 0;
                    float modID = 0;
                    float spell_heal_min = 0;
                    float spell_heal_max = 0;
                    float mana_cost = 0;
                    float stamina_cost = 0;
                    float cast_chance = 0;
                    float spell_turns = 0;
                    bool allowed_in_pvp = false;
                    bool allowed_on_world_bosses = false;

                    try { damage_min = Convert.ToSingle((row[4] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set damage_min of " + row[0] as string); }
                    try { damage_max = Convert.ToSingle((row[5] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set damage_max of " + row[0] as string); }
                    try { crit_chance = Convert.ToSingle((row[6] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set crit_chance of " + row[0] as string); }
                    try { modID = Convert.ToSingle((row[7] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set modID of " + row[0] as string); }
                    try { spell_heal_min = Convert.ToSingle((row[8] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set spell_heal_min of " + row[0]); }
                    try { spell_heal_max = Convert.ToSingle((row[9] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set spell_heal_max of " + row[0]); }
                    try { mana_cost = Convert.ToSingle((row[10] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set mana_cost of " + row[0]); }
                    try { stamina_cost = Convert.ToSingle((row[11] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set stamina_cost of " + row[0]); }
                    try { cast_chance = Convert.ToSingle((row[12] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set cast_chance of " + row[0]); }
                    try { spell_turns = Convert.ToSingle((row[15] as string).Replace("%", "").Replace("x", "")); }
                    catch { Console.WriteLine("Failed to set spell_turns of " + row[0]); }
                    try { allowed_in_pvp = (row[17] as string) == "Yes" ? true : false; }
                    catch { Console.WriteLine("Failed to set allowed_in_pvp of " + row[0] as string); }
                    try { allowed_on_world_bosses = (row[18] as string) == "Yes" ? true : false; }
                    catch { Console.WriteLine("Failed to set allowed_on_world_bosses of " + row[0] as string); }

                    RPG.Resources.Spell spell = new RPG.Resources.Spell
                        (
                            row[0] as string,
                            row[1] as string,
                            row[2] as string,
                            row[3] as string,
                            damage_min,
                            damage_max,
                            crit_chance,
                            modID,
                            spell_heal_min,
                            spell_heal_max,
                            mana_cost,
                            stamina_cost,
                            cast_chance,
                            row[13] as string,
                            row[14] as string,
                            spell_turns,
                            row[16] as string,
                            row[17] as string,
                            allowed_in_pvp,
                            allowed_on_world_bosses
                        );

                    Game.spellDatabase.Add(row[0] as string, spell);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Loaded {0} spells into the spell database in {1}ms", loaded, ts.Milliseconds);

            Console.Read();

            Console.WriteLine("Loading Dependencies...");

            if (File.Exists(@"Resources\MCollection.Monsters"))
            {
                using (StreamReader sr = File.OpenText(@"Resources\MCollection.Monsters"))
                {
                    string s = "";

                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] temp = s.Split('Æ');

                        if (temp[0] == "Monster")
                        {
                            Game.designedEnemiesI.Add(new RPG.Resources.DesignedEnemy(temp[5], temp[6], temp[7], temp[8], temp[9], temp[10], temp[11]));
                        }
                    }
                }
            }
            else
            {
                File.Create(@"Resources\MCollection.Monsters");
                Console.WriteLine("Failed to find a valid monster collection...");
            }

            Main(args);
        }
        static void Main(string[] args) => new MoeBot().MainAsync().GetAwaiter().GetResult();
    }
}
