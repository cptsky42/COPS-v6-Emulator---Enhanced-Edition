// * ***************************************
// *              CREDITS
// * ***************************************
// *  Originally created by Jean-Philippe Boivin (CptSky @ e*pvp), Copyright (C) 2010-2011,
// *  Logik, All rights reserved.
// *  
// * ***************************************
// *              SPECIAL THANKS
// * ***************************************
// * Sparkie (Unknownone @ e*pvp)
// * Saint (tao4229 @ e*pvp) [Queue.dll]
// * 
// * ***************************************
 

// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

namespace COServer
{
    public class Program
    {
        [MTAThread]
        static void Main(String[] args)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA", false);

                Console.Title = "COPS v6 - The Return Of The Legend : MsgServer";
                Program.WriteHeader();

                if (!Directory.Exists(Program.RootPath + "\\Debug\\"))
                    Directory.CreateDirectory(Program.RootPath + "\\Debug\\");

                if (!Directory.Exists(Program.RootPath + "\\Log\\"))
                    Directory.CreateDirectory(Program.RootPath + "\\Log\\");

                if (!Directory.Exists(Program.RootPath + "\\Backup\\"))
                    Directory.CreateDirectory(Program.RootPath + "\\Backup\\");

                DateTime Time = DateTime.Now.ToUniversalTime();
                String File = "Msg-" + Time.Year + "-" + Time.Month + "-" + Time.Day + ".log";
                Debuguer = new StreamWriter(Program.RootPath + "\\Debug\\" + File, true);
                Debuguer.AutoFlush = true;
                StartDay = Time.Day;

                Logger = new StreamWriter(Program.RootPath + "\\Log\\MsgServer.log", true);
                Logger.AutoFlush = true;

                Thread TimeThread = new Thread(CheckTime);
                TimeThread.IsBackground = true;
                TimeThread.Start();

                Server.Run();
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            Console.Read();
        }

        public static String RootPath = Environment.CurrentDirectory;
        public static Encoding Encoding = Encoding.GetEncoding("iso-8859-1");

        #if DEBUG
        public static Boolean Debug = true;
        #else
        public static Boolean Debug = false;
        #endif

        private static Assembly Assembly = Assembly.GetExecutingAssembly();
        private static StreamWriter Debuguer = null;
        private static StreamWriter Logger = null;
        private static Int32 StartDay = 0;

        /// <summary>
        /// Write the header of the console. (ASCII Picture)
        /// </summary>
        private static void WriteHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"+-----------------------------------------------------------------------------+");
            Console.WriteLine(@"|                          _                 _ _                              |");
            Console.WriteLine(@"|                         | |               (_) |                             |");
            Console.WriteLine(@"|                         | |     ___   __ _ _| | __                          |");
            Console.WriteLine(@"|                         | |    / _ \ / _` | | |/ /                          |");
            Console.WriteLine(@"|                         | |___| (_) | (_| | |   < _                         |");
            Console.WriteLine(@"|                         \_____/\___/ \__, |_|_|\_(_)                        |");
            Console.WriteLine(@"|                                       __/ |                                 |");
            Console.WriteLine(@"|                                      |___/                                  |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"|                   COPS v6 - MsgServer " + Version + "                   |");
            Console.WriteLine(@"|                          Copyright (C) 2010 - 2012                          |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"+-----------------------------------------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void CheckTime()
        {
            while (true)
            {
                DateTime Time = DateTime.UtcNow;
                if (Time.Day != StartDay)
                {
                    String File = "Msg-" + Time.Year + "-" + Time.Month + "-" + Time.Day + ".log";
                    lock (Debuguer)
                    {
                        Debuguer.Flush();
                        Debuguer.Close();

                        Debuguer = new StreamWriter(Program.RootPath + "\\Debug\\" + File, true);
                        Debuguer.AutoFlush = true;
                    }
                    StartDay = DateTime.Now.Day;
                }
                Thread.Sleep(60 * 1000);
            }
        }

        public static void Restart()
        {
            if (Debuguer != null)
                Debuguer.Close();
            Debuguer = null;

            if (Logger != null)
                Logger.Close();
            Logger = null;

            //try { Server.AuthSocket.Client.Close(); }
            //catch { }

            //try { Server.Socket.Socket.Close(); }
            //catch { }

            Process Process = new Process();
            Process.StartInfo.FileName = RootPath + "\\MsgServer.exe";
            Process.StartInfo.WorkingDirectory = RootPath;
            Process.Start();

            try { Process.GetCurrentProcess().Kill(); }
            catch { Environment.Exit(0); }
        }

        /// <summary>
        /// Write the object in the console and in the debug file.
        /// </summary>
        public static void WriteLine(Object Object)
        {
            Debuguer.WriteLine(String.Format("[{0:R}] ", DateTime.UtcNow) + Object.ToString());
            Console.WriteLine(Object);
        }

        /// <summary>
        /// Write the object in the log file.
        /// </summary>
        public static void Log(Object Object)
        {
            Logger.WriteLine(String.Format("[{0:R}] ", DateTime.UtcNow) + Object);
            Console.WriteLine(Object);
        }

        /// <summary>
        /// Get and return the current version.
        /// </summary>
        public static String Version
        { 
            get
            {
                Version Version = new Version();
                Version = Assembly.GetName().Version;

                return String.Format("{0:0000}.{1:0000}.{2:0000}.{3:0000}", Version.Major, Version.Minor, Version.Build, Version.Revision);
            }
        }

        /// <summary>
        /// Transform the array of bytes in hexadecimal and convert the value in ANSI.
        /// </summary>
        public static Object Dump(Byte[] Bytes)
        {
            String Hex = "";
            foreach (Byte b in Bytes)
                Hex = Hex + b.ToString("X2") + " ";
            String Out = "";
            while (Hex.Length != 0)
            {
                Int32 SubLength = 0;
                if (Hex.Length >= 48)
                    SubLength = 48;
                else
                    SubLength = Hex.Length;
                String SubString = Hex.Substring(0, SubLength);
                Int32 Remove = SubString.Length;
                SubString = SubString.PadRight(60, ' ') + StrHexToAnsi(SubString);
                Hex = Hex.Remove(0, Remove);
                Out = Out + SubString + "\r\n";
            }
            return Out;
        }

        private static String StrHexToAnsi(String StrHex)
        {
            String[] Data = StrHex.Split(new Char[] { ' ' });
            String Ansi = "";
            foreach (String tmpHex in Data)
            {
                if (tmpHex != "")
                {
                    Byte ByteData = Byte.Parse(tmpHex, System.Globalization.NumberStyles.HexNumber);
                    if ((ByteData >= 32) & (ByteData <= 126))
                        Ansi = Ansi + ((Char)(ByteData)).ToString();
                    else
                        Ansi = Ansi + ".";
                }
            }
            return Ansi;
        }
    }
}
