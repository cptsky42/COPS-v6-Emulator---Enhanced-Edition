// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using COServer.Entities;
using MySql.Data.MySqlClient;

namespace COServer
{
    public unsafe partial class Database
    {
        public static ConcurrentDictionary<Int32, MonsterInfo> AllMonsters = new ConcurrentDictionary<Int32, MonsterInfo>();

        public static void GetMonstersInfo()
        {
            try
            {
                Console.Write("Loading monsters informations...  ");

                #if MYSQL_DB
                String Connection = "SERVER=localhost;" +
                                    "PORT=3306;" +
                                    "DATABASE=coserver;" +
                                    "USERNAME=root;" +
                                    "PASSWORD=root;";

                String Query = "SELECT * FROM `cq_monstertype`;";
                using (MySqlDataAdapter Adapter = new MySqlDataAdapter(Query, Connection))
                {
                    using (DataSet DS = new DataSet())
                    {
                        Adapter.Fill(DS, "src");

                        AllMonsters = new ConcurrentDictionary<Int32, MonsterInfo>();
                        for (Int32 i = 0; i < DS.Tables["src"].Rows.Count; i++)
                        {
                            Console.Write("\b{0}", Loading.NextChar());
                            DataRow Data = DS.Tables["src"].Rows[i];
                           
                            Int32 Id = (Int32)Data["Id"];

                            if (AllMonsters.ContainsKey(Id))
                                continue;

                            MonsterInfo Info = new MonsterInfo();

                            Info.Id = Id;
                            Info.Name = (String)Data["Name"];
                            Info.Type = (Byte)Data["Type"];
                            Info.Look = (Int16)Data["Look"];
                            Info.Level = (Int16)Data["Level"];
                            Info.Life = (Int32)Data["Life"];
                            Info.MinAtk = (Int32)Data["MinAtk"];
                            Info.MaxAtk = (Int32)Data["MaxAtk"];
                            Info.Defence = (Int32)Data["Defense"];
                            Info.Dodge = (Byte)Data["Dodge"];
                            Info.AtkRange = (Byte)Data["AtkRange"];
                            Info.ViewRange = (Byte)Data["ViewRange"];
                            Info.AtkSpeed = (Int32)Data["AtkSpeed"];
                            Info.MoveSpeed = (Int32)Data["MoveSpeed"];
                            Info.MagicType = (UInt16)Data["MagicType"];
                            Info.MagicDef = (UInt16)Data["MagicDefense"];
                            Info.DropMoney = (Int32)Data["DropMoney"];
                            Info.DropHP = (Int32)Data["DropHP"];
                            Info.DropMP = (Int32)Data["DropMP"];
                            Info.BattleLvl = (Int16)Data["BattleLvl"];

                            Info.Drops = new List<Drop>();

                            AllMonsters.TryAdd(Info.Id, Info);
                        }
                    }
                }
                #else
                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\MonsterType.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    AllMonsters = new ConcurrentDictionary<Int32, MonsterInfo>();
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Int32 Id = BReader.ReadInt32();

                        if (AllMonsters.ContainsKey(Id))
                            continue;

                        MonsterInfo Info = new MonsterInfo();

                        BReader.BaseStream.Seek(-4, SeekOrigin.Current);
                        Info.Id = BReader.ReadInt32();
                        Info.Name = Program.Encoding.GetString(BReader.ReadBytes(0x10)).Trim((Char)0x00);
                        Info.Type = BReader.ReadByte();
                        Info.Look = BReader.ReadInt16();
                        Info.Level = BReader.ReadInt16();
                        Info.Life = BReader.ReadInt32();
                        Info.MinAtk = BReader.ReadInt32();
                        Info.MaxAtk = BReader.ReadInt32();
                        Info.Defence = BReader.ReadInt32();
                        Info.Dodge = BReader.ReadByte();
                        Info.AtkRange = BReader.ReadByte();
                        Info.ViewRange = BReader.ReadByte();
                        Info.AtkSpeed = BReader.ReadInt32();
                        Info.MoveSpeed = BReader.ReadInt32();
                        Info.MagicType = BReader.ReadUInt16();
                        Info.MagicDef = BReader.ReadUInt16();
                        Info.DropMoney = BReader.ReadInt32();
                        Info.DropHP = BReader.ReadInt32();
                        Info.DropMP = BReader.ReadInt32();
                        Info.BattleLvl = BReader.ReadInt16();

                        Info.Drops = new List<Drop>();

                        AllMonsters.TryAdd(Info.Id, Info);
                    }
                    BReader.Close();
                    BReader = null;
                }
                #endif

                String[] Lines = File.ReadAllLines(Program.RootPath + "\\Drops.txt");
                foreach (String Line in Lines)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    String[] Parts = Line.Split('\t');

                    Int32 UID = Int32.Parse(Parts[0]);
                    if (!AllMonsters.ContainsKey(UID))
                        continue;

                    Int32 Count = Int32.Parse(Parts[1]);
                    for (Int32 i = 0; i < Count; i++)
                    {
                        String[] KV = Parts[2 + i].Split(':');
                        Int32 ItemId = Int32.Parse(KV[0]);
                        Double Rate = Double.Parse(KV[1]);

                        AllMonsters[UID].Drops.Add(new Drop() { Id = ItemId, Rate = Rate });
                    }
                }

                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
