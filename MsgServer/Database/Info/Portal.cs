// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace COServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PortalInfo
    {
        public Int16 FromMap;
        public UInt16 FromX;
        public UInt16 FromY;
        public Int16 ToMap;
        public UInt16 ToX;
        public UInt16 ToY;
    }

    public unsafe partial class Database
    {
        public static PortalInfo[] AllPortals = new PortalInfo[0];

        public static void GetPortalsInfo()
        {
            try
            {
                Console.Write("Loading portals informations...  ");

                using (FileStream FStream = new FileStream(Program.RootPath + "\\Database\\Portal.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Program.Encoding);
                    BReader.BaseStream.Seek(4, SeekOrigin.Begin);
                    Int32 Count = BReader.ReadInt32();

                    AllPortals = new PortalInfo[Count];
                    for (Int32 i = 0; i < Count; i++)
                    {
                        Console.Write("\b{0}", Loading.NextChar());

                        BReader.BaseStream.Seek(4, SeekOrigin.Current);

                        PortalInfo Info = new PortalInfo();

                        Byte[] Data = BReader.ReadBytes(12);

                        IntPtr pData = Marshal.AllocHGlobal(Data.Length);
                        Marshal.Copy(Data, 0, pData, Data.Length);
                        Info = (PortalInfo)Marshal.PtrToStructure(pData, typeof(PortalInfo));
                        Marshal.FreeHGlobal(pData);

                        AllPortals[i] = Info;
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
