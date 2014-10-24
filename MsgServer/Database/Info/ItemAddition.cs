// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace COServer
{
    public unsafe partial class Database
    {
        public static ConcurrentDictionary<Int32, ItemBonus> AllBonus = new ConcurrentDictionary<Int32, ItemBonus>();

        public static void GetItemsBonus()
        {
            try
            {
                Console.Write("Loading items bonus...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\itemaddition.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    AllBonus = new ConcurrentDictionary<Int32, ItemBonus>();
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Int32 Id = BReader.ReadInt32();

                        if (AllBonus.ContainsKey(Id))
                            continue;

                        ItemBonus Info = new ItemBonus();

                        BReader.BaseStream.Seek(-4, SeekOrigin.Current);
                        Info.Id = BReader.ReadInt32();
                        Info.Life = BReader.ReadInt16();
                        Info.MaxAtk = BReader.ReadInt16();
                        Info.MinAtk = BReader.ReadInt16();
                        Info.Defence = BReader.ReadInt16();
                        Info.MAtk = BReader.ReadInt16();
                        Info.MDef = BReader.ReadInt16();
                        Info.Dexterity = BReader.ReadInt16();
                        Info.Dodge = BReader.ReadInt16();

                        AllBonus.TryAdd(Info.Id, Info);
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
