// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using COServer.Entities;

namespace COServer
{
    public partial class Database
    {
        public static void GetAllNPCs()
        {
            try
            {
                Console.Write("Loading all npcs in memory...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\npc.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    World.AllNPCs = new Dictionary<Int32, NPC>(Count);
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Int32 UniqId = BReader.ReadInt32();

                        if (Entity.IsTerrainNPC(UniqId))
                        {
                            if (World.AllTerrainNPCs.ContainsKey(UniqId))
                                continue;

                            TerrainNPC NPC = new TerrainNPC(
                                UniqId,
                                Program.Encoding.GetString(BReader.ReadBytes(0x10)).TrimEnd((Char)0x00),
                                BReader.ReadByte(),
                                BReader.ReadInt16(),
                                BReader.ReadInt16(),
                                BReader.ReadUInt16(),
                                BReader.ReadUInt16(),
                                BReader.ReadByte(),
                                BReader.ReadByte(),
                                BReader.ReadInt16(),
                                BReader.ReadInt32(),
                                BReader.ReadInt16(),
                                BReader.ReadInt16());

                            World.AllTerrainNPCs.Add(UniqId, NPC);
                            if (World.AllMaps.ContainsKey(NPC.Map))
                                World.AllMaps[NPC.Map].AddEntity(NPC);
                        }
                        else
                        {
                            if (World.AllNPCs.ContainsKey(UniqId))
                                continue;

                            NPC NPC = new NPC(
                                UniqId,
                                Program.Encoding.GetString(BReader.ReadBytes(0x10)).TrimEnd((Char)0x00),
                                BReader.ReadByte(),
                                BReader.ReadInt16(),
                                BReader.ReadInt16(),
                                BReader.ReadUInt16(),
                                BReader.ReadUInt16(),
                                BReader.ReadByte(),
                                BReader.ReadByte());
                            BReader.BaseStream.Seek(10, SeekOrigin.Current);

                            World.AllNPCs.Add(UniqId, NPC);
                            if (World.AllMaps.ContainsKey(NPC.Map))
                                World.AllMaps[NPC.Map].AddEntity(NPC);
                        }
                    }
                    BReader.Close();
                    BReader = null;
                }

                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
