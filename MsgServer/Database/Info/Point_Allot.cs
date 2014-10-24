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
        public static Int16[,][] AllPointAllot = new Int16[0,0][];

        public static void GetPointAllotInfo()
        {
            try
            {
                Console.Write("Loading point allot informations...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\Point_Allot.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Byte JobCount = BReader.ReadByte();
                    Byte LvlCount = BReader.ReadByte();

                    AllPointAllot = new Int16[250, LvlCount + 1][];
                    for (Int32 i = 0; i < JobCount; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);
                        Byte JobID = BReader.ReadByte();

                        for (Int32 x = 1; x <= LvlCount; x++)
                        {
                            Console.Write("\b{0}", Loading.NextChar());

                            AllPointAllot[JobID, x] = new Int16[4];
                            AllPointAllot[JobID, x][0] = BReader.ReadInt16(); //Strength
                            AllPointAllot[JobID, x][1] = BReader.ReadInt16(); //Agility
                            AllPointAllot[JobID, x][2] = BReader.ReadInt16(); //Vitality
                            AllPointAllot[JobID, x][3] = BReader.ReadInt16(); //Spirit
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
