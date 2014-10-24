// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Concurrent;

namespace COServer
{
    public partial class Database
    {
        public static void GetAllMaps()
        {
            try
            {
                Console.Write("Loading all maps in memory...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\map.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    World.AllMaps = new ConcurrentDictionary<Int16, Map>();
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Int16 UniqId = BReader.ReadInt16();

                        if (World.AllMaps.ContainsKey(UniqId))
                            continue;

                        BReader.BaseStream.Seek(17, SeekOrigin.Current);

                        UInt16 Width = BReader.ReadUInt16();
                        UInt16 Height = BReader.ReadUInt16();

                        Boolean[,] Accessible = new Boolean[Width, Height];
                        for (UInt16 x = 0; x < Width; x++)
                        {
                            for (UInt16 y = 0; y < Height; y++)
                                Accessible[x, y] = BReader.ReadBoolean();
                        }

                        UInt16[,] Heights = new UInt16[Width, Height];
                        for (UInt16 x = 0; x < Width; x++)
                        {
                            for (UInt16 y = 0; y < Height; y++)
                                Heights[x, y] = BReader.ReadUInt16();
                        }

                        BReader.BaseStream.Seek(-(Width * Height), SeekOrigin.Current); //Access
                        BReader.BaseStream.Seek(-(Width * Height) * sizeof(UInt16), SeekOrigin.Current); //Heights
                        BReader.BaseStream.Seek(-21, SeekOrigin.Current);

                        Map Map = new Map(UniqId, Accessible, Heights, Width, Height);
                        Map.Id = BReader.ReadUInt16();
                        Map.Flags = BReader.ReadInt32();
                        Map.Weather = BReader.ReadByte();
                        Map.PortalX = BReader.ReadUInt16();
                        Map.PortalY = BReader.ReadUInt16();
                        Map.RebornMap = BReader.ReadInt16();
                        Map.Color = BReader.ReadUInt32();

                        BReader.BaseStream.Seek(((Width * Height) * (sizeof(Boolean) + sizeof(UInt16)) + 4), SeekOrigin.Current);

                        World.AllMaps.TryAdd(UniqId, Map);
                        Accessible = null;
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
