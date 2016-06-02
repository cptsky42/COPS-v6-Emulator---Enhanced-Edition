using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using System.Globalization;

namespace CO2Tools
{
    public class Program
    {
        public static string HOST = "192.168.1.114";
        public static string USERNAME = "zfserver";
        public static string PASSWORD = "";
        public static string DATABASE = "cq_4351";

        public static ConcurrentDictionary<UInt32, Byte> MISSING_ACTIONS = new ConcurrentDictionary<UInt32, Byte>();

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            try
            {
                DirectoryInfo directory;

                directory = new DirectoryInfo("./Scripts/");
                directory.Empty();

                directory = new DirectoryInfo("./Scripts2/");
                directory.Empty();

                directory = new DirectoryInfo("./Items/");
                directory.Empty();

                directory = new DirectoryInfo("./Monsters/");
                directory.Empty();

                directory = new DirectoryInfo("./Events/");
                directory.Empty();

                Npc[] npcs = new Npc[0];
                Item[] items = new Item[0];
                Monster[] monsters = new Monster[0];
                Event[] events = new Event[0];

                npcs = Npc.getAllNpcs();
                MISSING_ACTIONS.Clear();

                foreach (Npc npc in npcs)
                {
                    if (npc.hasTask())
                    {
                        if (npc.Id == 3215) // stack overflow
                            continue;

                        if (npc.Id == 30154) // stack overflow (Luna)
                            continue;

                        if (npc.Id == 31) // stack overflow, maybe unused anyway...
                            continue;


                        if (npc.Id == 10063) // Shopboy, must implement chkeq
                            continue;

                        if (npc.Id == 7050)//MaitreD'arme
                            continue;

                        if (npc.Id == 10062) //ArtisantMagique
                            continue;

                        if (npc.Id == 10064) //Tinturier, must implement 512
                            continue;

                        if (npc.Id == 10065) // GodlyArtisan, must implement dur check
                            continue;

                        if (npc.Id == 30156) // HouseAdmin, failing at ACTION_MAP_ATTRIB
                            continue;

                        if (npc.Id == 300500) // Eternity, links full...
                            continue;

                        if (npc.Id == 3381) // SurgeonMiracle, must implement look
                            continue;

                        if (npc.Id == 35015) // Etheral
                            continue;

                        // 2rb quest... Missing...
                        if (npc.Id == 3600)
                            continue;

                        if (npc.Id == 10060 || npc.Id == 10061) // Sunfer, CaptainFang
                            continue;

                        /////////////////////////////////////////////////////////////////////

                        if (npc.Id == 300000) // ShanLee, no longer giving DB...
                            continue;

                        if (npc.Id == 10003) // GuildDirector, not fully implemented...
                            continue;

                        if (npc.Id == 47) // PrizeNPC
                            continue;

                        // CPs things...
                        if (npc.Id >= 923 && npc.Id <= 945 || npc.Id == 2070 || npc.Id == 2071 || npc.Id == 3825 || npc.Id == 3952 || npc.Id == 3953)
                            continue;

                        if (npc.Id == 2065) // JewleryLau
                            continue;

                        using (StreamWriter stream = new StreamWriter("./Scripts/" + npc.Id + ".lua", false, Encoding.Default))
                        {
                            Console.WriteLine("Writing {0}.lua...", npc.Id);
                            npc.generateScript(stream);
                        }
                    }
                }

                using (StreamWriter stream = new StreamWriter("./missings.txt", false, Encoding.Default))
                {
                    foreach (UInt32 action in MISSING_ACTIONS.Keys)
                    {
                        stream.WriteLine("{0}:{1}", action, ((ActionID)action).ToString());
                    }
                }

                npcs = new Npc[0];//Npc.getAllDynaNpcs();
                MISSING_ACTIONS.Clear();

                foreach (Npc npc in npcs)
                {
                    if (npc.hasTask())
                    {
                        if (npc.Id == 815 || npc.Id == 816 || npc.Id == 817 || npc.Id == 818)
                            continue;

                        using (StreamWriter stream = new StreamWriter("./Scripts2/" + npc.Id + ".lua", false, Encoding.Default))
                        {
                            Console.WriteLine("Writing {0}.lua...", npc.Id);
                            npc.generateScript(stream);
                        }
                    }
                }

                using (StreamWriter stream = new StreamWriter("./missings2.txt", false, Encoding.Default))
                {
                    foreach (UInt32 action in MISSING_ACTIONS.Keys)
                    {
                        stream.WriteLine("{0}:{1}", action, ((ActionID)action).ToString());
                    }
                }

                //items = Item.getAllItems();
                MISSING_ACTIONS.Clear();

                foreach (Item item in items)
                {
                    if (item.Id != 726011 && item.Id != 726012 && item.Id != 726013 && item.Id != 726014 && item.Id != 726015 &&
                        item.Id != 720010 && item.Id != 720011 && item.Id != 720012 && item.Id != 720013 && item.Id != 720014 && item.Id != 720015 && item.Id != 720016 && item.Id != 720017 &&
                        item.Id != 720027 && item.Id != 720028 &&
                        item.Id != 721020 && item.Id != 721540 && item.Id != 723017 &&
                        item.Id != 725000 && item.Id != 725001 && item.Id != 725002 && item.Id != 725003 && item.Id != 725004 && item.Id != 725005 &&
                        item.Id != 725010 && item.Id != 725012 && item.Id != 725013 && item.Id != 725015 && item.Id != 725016 && item.Id != 725018 && item.Id != 725019 &&
                        item.Id != 725020 && item.Id != 725021 && item.Id != 725022 && item.Id != 725023 && item.Id != 725024 && item.Id != 725025 && item.Id != 725026 && item.Id != 725027 && item.Id != 725028 && item.Id != 725029 &&
                        item.Id != 725030 && item.Id != 725031 && item.Id != 725041 && item.Id != 725042 &&
                        !(item.Id >= 1000000 && item.Id <= 1002050) &&
                        item.Id != 1060020 && item.Id != 1060021 && item.Id != 1060022 && item.Id != 1060023 && item.Id != 1060024 && item.Id != 1060025 && item.Id != 1060026 && item.Id != 1060027 && item.Id != 1060028 && item.Id != 1060029 &&
                        item.Id != 1060030 && item.Id != 1060031 && item.Id != 1060032 && item.Id != 1060033 && item.Id != 1060034 && item.Id != 1060035 && item.Id != 1060036 && item.Id != 1060037 && item.Id != 1060038 &&
                        item.Id != 1060040 && item.Id != 1060050 && item.Id != 1060060 && item.Id != 1060070 && item.Id != 1060080 && item.Id != 1060090 &&
                        item.Id != 1060100 && item.Id != 1060101 && item.Id != 1060102)
                        continue;

                    if (item.Id == 721090)
                        continue;

                    if (item.Id >= 721178 && item.Id <= 721189)
                        continue;

                    if (item.Id == 722559)
                        continue;

                    if (item.hasTask())
                    {
                        using (StreamWriter stream = new StreamWriter("./Items/" + item.Id + ".lua", false, Encoding.Default))
                        {
                            Console.WriteLine("Writing {0}.lua...", item.Id);
                            item.generateScript(stream);
                        }
                    }
                }

                using (StreamWriter stream = new StreamWriter("./items_missings.txt", false, Encoding.Default))
                {
                    foreach (UInt32 action in MISSING_ACTIONS.Keys)
                    {
                        stream.WriteLine("{0}:{1}", action, ((ActionID)action).ToString());
                    }
                }

                //monsters = Monster.getAllMonsters();
                MISSING_ACTIONS.Clear();

                foreach (Monster monster in monsters)
                {
                    if (monster.Id == 15)
                        continue;

                    if (monster.Id >= 21 && monster.Id <= 40)
                        continue;

                    if (monster.Id >= 90 && monster.Id <= 93)
                        continue;

                    if (monster.Id == 3120)
                        continue;

                    if (monster.Id >= 5053 && monster.Id <= 5060)
                        continue;

                    if (monster.hasTask())
                    {
                        using (StreamWriter stream = new StreamWriter("./Monsters/" + monster.Id + ".lua", false, Encoding.Default))
                        {
                            Console.WriteLine("Writing {0}.lua...", monster.Id);
                            monster.generateScript(stream);
                        }
                    }
                }

                using (StreamWriter stream = new StreamWriter("./monsters_missings.txt", false, Encoding.Default))
                {
                    foreach (UInt32 action in MISSING_ACTIONS.Keys)
                    {
                        stream.WriteLine("{0}:{1}", action, ((ActionID)action).ToString());
                    }
                }




                using (StreamWriter stream = new StreamWriter("./OnLogin.lua", false, Encoding.Default))
                {
                    Console.WriteLine("Writing OnLogin.lua...");
                    Login.generateScript(stream);
                }

                //events = Event.getAllEvents();
                MISSING_ACTIONS.Clear();

                foreach (Event e in events)
                {
                    if (e.ActionID == 2030003)
                        continue;

                    if (e.ActionID == 2030007)
                        continue;

                    if (e.ActionID == 2030096)
                        continue;

                    if (e.ActionID == 2030097)
                        continue;

                    using (StreamWriter stream = new StreamWriter("./Events/" + e.Id + ".lua", false, Encoding.Default))
                    {
                        Console.WriteLine("Writing {0}.lua...", e.ActionID);
                        e.generateScript(stream);
                    }
                }

                using (StreamWriter stream = new StreamWriter("./events_missings.txt", false, Encoding.Default))
                {
                    foreach (UInt32 action in MISSING_ACTIONS.Keys)
                    {
                        stream.WriteLine("{0}:{1}", action, ((ActionID)action).ToString());
                    }
                }
            }
            catch (Exception exc) { Console.WriteLine(exc); }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
