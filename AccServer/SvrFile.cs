// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Text;

namespace COServer
{
    public class SvrInfo
    {
        public String Name;
        public String IPAddress;
        public UInt16 Port;

        public SvrInfo(String File)
        {
            try
            {
                using (FileStream FStream = new FileStream(File, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream, Encoding.ASCII);

                    String Identifier = Encoding.ASCII.GetString(BReader.ReadBytes(3));
                    if (Identifier == "SVR")
                    {
                        FStream.Seek(1, SeekOrigin.Current);
                        Name = Encoding.ASCII.GetString(BReader.ReadBytes(BReader.ReadInt32()));
                        FStream.Seek(1, SeekOrigin.Current);
                        IPAddress = Encoding.ASCII.GetString(BReader.ReadBytes(BReader.ReadInt32()));
                        FStream.Seek(1, SeekOrigin.Current);
                        Port = BReader.ReadUInt16();
                    }
                    else
                        Program.WriteLine("SvrInfo::SvrInfo() -> Failed to read the file '" + File + "'");

                    BReader.Close();
                    BReader = null;
                }
            }
            catch { Program.WriteLine("SvrInfo::SvrInfo() -> Failed to read the file '" + File + "'"); }
        }

        ~SvrInfo()
        {
            Name = null;
            IPAddress = null;
        }
    }
}
