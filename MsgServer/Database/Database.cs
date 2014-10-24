using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CO2_CORE_DLL;
using CO2_CORE_DLL.IO;
using CO2_CORE_DLL.Security.Cryptography;

namespace COServer
{
    public unsafe static class Database2
    {
        public static ItemType AllItems = new ItemType();
        public static MagicType AllMagics = new MagicType();

        private static COFAC Cipher = new COFAC();
        //LevExp = 8F7

        public static void GetItemsInfo()
        {
            try
            {
                Console.Write("Loading items informations...  ");

                String TmpFile = Path.GetTempFileName();
                Cipher.GenerateKey(0x3297);

                using (FileStream Reader = new FileStream(Program.RootPath + "\\Database\\ItemType.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (FileStream Writer = new FileStream(TmpFile, FileMode.Open, FileAccess.Write, FileShare.Read))
                {
                    Byte[] Buffer = new Byte[Kernel.MAX_BUFFER_SIZE];

                    Int32 Length = Reader.Read(Buffer, 0, Buffer.Length);
                    while (Length > 0)
                    {
                        fixed (Byte* pBuffer = Buffer)
                            Cipher.Decrypt(pBuffer, Length);
                        Writer.Write(Buffer, 0, Length);

                        Length = Reader.Read(Buffer, 0, Buffer.Length);
                    }
                }

                AllItems.LoadFromTxt(TmpFile);
                File.Delete(TmpFile);

                Console.WriteLine("Ok!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static void GetMagicsInfo()
        {
            try
            {
                Console.Write("Loading magics informations...  ");
                AllMagics.LoadFromDat(Program.RootPath + "\\Database\\MagicType.dat");
                Console.WriteLine("Ok!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
