// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Ionic.Zip;
using Ionic.Zlib;

namespace COServer
{
    public partial class Database
    {
        public class Backup
        {
            const Int32 BIG_BUFFER = 2048; //2 KiB

            public static String Pack()
            {
                try
                {
                    String Time = DateTime.UtcNow.ToString("yyyy-MM-dd");
                    using (ZipFile Zip = new ZipFile(Program.RootPath + "\\Backup\\" + Time + ".zip", Program.Encoding))
                    {
                        Zip.CompressionLevel = CompressionLevel.Level9;
                        Zip.CompressionMethod = CompressionMethod.Deflate;
                        Zip.EmitTimesInWindowsFormatWhenSaving = true;

                        Zip.AddDirectory(Program.RootPath, "");

                        List<ZipEntry> Entries = new List<ZipEntry>();
                        foreach (ZipEntry Entry in Zip.Entries)
                        {
                            if (Entry.FileName.ToLower().Contains("backup"))
                                Entries.Add(Entry);
                        }

                        Zip.RemoveEntries(Entries);
                        Zip.Save();
                    }
                    return Program.RootPath + "\\Backup\\" + Time + ".zip";
                }
                catch (Exception Exc) { Program.WriteLine(Exc); return null; }
            }

            public static void Unpack(String File, String Path)
            {
                using (ZipFile Zip = new ZipFile(File, Program.Encoding))
                {
                    Zip.ExtractAll(Path, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            public static void Upload(String File, String Server, String Username, String Password)
            {
                FileInfo Info = new FileInfo(File);
                String URI = Server + Info.Name;
                FtpWebRequest FtpReq;

                FtpReq = (FtpWebRequest)FtpWebRequest.Create(new Uri(URI));
                FtpReq.Credentials = new NetworkCredential(Username, Password);
                #if !__MonoCS__
                FtpReq.KeepAlive = false;
                #endif
                FtpReq.Method = WebRequestMethods.Ftp.UploadFile;
                FtpReq.UseBinary = true;
                FtpReq.ContentLength = Info.Length;

                Byte[] Buffer = new Byte[BIG_BUFFER];
                using (FileStream FStream = Info.OpenRead())
                {
                    try
                    {
                        using (Stream Stream = FtpReq.GetRequestStream())
                        {

                            Int32 Length = FStream.Read(Buffer, 0, BIG_BUFFER);
                            while (Length != 0)
                            {
                                Stream.Write(Buffer, 0, Length);
                                Length = FStream.Read(Buffer, 0, BIG_BUFFER);
                            }
                        }
                    }
                    catch (Exception Exc) { Program.WriteLine(Exc); }
                }
            }
        }
    }
}