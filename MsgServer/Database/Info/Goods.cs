// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace COServer
{
    public unsafe partial class Database
    {
        public static Dictionary<Int32, ShopInfo> AllShops = new Dictionary<Int32, ShopInfo>();

        public static void GetShopsInfo()
        {
            try
            {
                Console.Write("Loading shops informations...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\Goods.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    AllShops = new Dictionary<Int32, ShopInfo>(Count);
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Int32 Id = BReader.ReadInt32();

                        if (AllShops.ContainsKey(Id))
                            continue;

                        ShopInfo Info = new ShopInfo();

                        BReader.BaseStream.Seek(-4, SeekOrigin.Current);
                        Info.Id = BReader.ReadInt32();
                        Info.Name = Program.Encoding.GetString(BReader.ReadBytes(0x10)).Trim((Char)0x00);
                        Info.MoneyType = BReader.ReadByte();

                        Byte ItemAmount = BReader.ReadByte();
                        Info.Items = new List<Int32>(ItemAmount);
                        for (Byte x = 0; x < ItemAmount; x++)
                        {
                            Int32 ItemId = BReader.ReadInt32();
                            if (!Info.Items.Contains(ItemId))
                                Info.Items.Add(ItemId);
                        }

                        AllShops.Add(Info.Id, Info);
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
