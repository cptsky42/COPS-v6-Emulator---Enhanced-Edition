// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace COServer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Spawn
    {
        public Int16 MapId;
        public UInt16 StartX;
        public UInt16 StartY;
        public UInt16 AddX;
        public UInt16 AddY;
        public Int32 MaxNPC;
        public Int32 Rest_Secs;
        public Int32 Max_Per_Gen;
        public Int32 NpcType;
    }

    public unsafe partial class Database
    {
        public static Spawn[] AllSpawns = new Spawn[0];

        public static void GetSpawnsInfo()
        {
            try
            {
                Console.Write("Loading spawns informations...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\generator.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    AllSpawns = new Spawn[Count];
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);

                        Spawn Info = new Spawn();
                        Info.MapId = BReader.ReadInt16();
                        Info.StartX = BReader.ReadUInt16();
                        Info.StartY = BReader.ReadUInt16();
                        Info.AddX = BReader.ReadUInt16();
                        Info.AddY = BReader.ReadUInt16();
                        Info.MaxNPC = BReader.ReadInt32();
                        Info.Rest_Secs = BReader.ReadInt32();
                        Info.Max_Per_Gen = BReader.ReadInt32();
                        Info.NpcType = BReader.ReadInt32();

                        AllSpawns[i] = Info;
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
