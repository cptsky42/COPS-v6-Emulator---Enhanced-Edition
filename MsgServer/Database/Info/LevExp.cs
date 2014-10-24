// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace COServer
{
    public unsafe partial class Database
    {
        public static UInt64[] AllLevExp = new UInt64[0];
        public static Int32[] AllLevTime = new Int32[0];

        public static void GetLevelsInfo()
        {
            try
            {
                Console.Write("Loading levels informations...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\LevExp.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Byte Count = BReader.ReadByte();

                    AllLevExp = new UInt64[Count + 1];
                    AllLevTime = new Int32[Count + 1];
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Int32 Id = BReader.ReadByte();
                        AllLevExp[Id] = BReader.ReadUInt64();
                        AllLevTime[Id] = BReader.ReadInt32();
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
