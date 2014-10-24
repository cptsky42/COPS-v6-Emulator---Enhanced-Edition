// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;

namespace COServer
{
    public unsafe partial class Database
    {
        public static PrizeInfo[] AllPrizes = new PrizeInfo[0];

        public static void GetPrizesInfo()
        {
            try
            {
                Console.Write("Loading prizes informations...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\lottery.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    AllPrizes = new PrizeInfo[Count];
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);

                        PrizeInfo Info = new PrizeInfo();
                        Info.Id = BReader.ReadInt32();
                        Info.Rank = BReader.ReadByte();
                        Info.Chance = BReader.ReadByte();
                        Info.Name = Program.Encoding.GetString(BReader.ReadBytes(0x20)).Trim((Char)0x00);
                        Info.Item = BReader.ReadInt32();
                        Info.Hole_Num = BReader.ReadByte();
                        Info.Addition_Lev = BReader.ReadByte();

                        AllPrizes[i] = Info;
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
