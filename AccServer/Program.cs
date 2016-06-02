// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using log4net.Config;

namespace COServer
{
    public class Program
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Program));

        [MTAThread]
        static void Main(String[] args)
        {
            try
            {
                Console.Title = "COPS v6 - The Return Of The Legend : AccServer Enhanced";
                Program.WriteHeader();

                if (!Directory.Exists(Program.RootPath + "/Log/"))
                    Directory.CreateDirectory(Program.RootPath + "/Log/");

                // log4net configuration
                XmlConfigurator.ConfigureAndWatch(new FileInfo(Program.RootPath + "/AccServer.config"));

                NetworkMonitor = new NetworkMonitor(5000);
                Server.Run();
            }
            catch (Exception Exc) { Console.WriteLine(Exc); }

            while (true)
                Console.Read();
        }

        public static readonly String RootPath = Environment.CurrentDirectory;
        public static Encoding Encoding = Encoding.GetEncoding("Windows-1252");

        public static NetworkMonitor NetworkMonitor = null;

        #if DEBUG
        public static Boolean Debug = true;
        #else
        public static Boolean Debug = false;
        #endif

        private static Assembly sAssembly = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Write the header of the console. (ASCII Picture)
        /// </summary>
        private static void WriteHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"+-----------------------------------------------------------------------------+");
            Console.WriteLine(@"|   ____ ___  ____  ____     _____ __  __ _   _ _        _  _____ ___  ____   |");
            Console.WriteLine(@"|  / ___/ _ \|  _ \/ ___|   | ____|  \/  | | | | |      / \|_   _/ _ \|  _ \  |");
            Console.WriteLine(@"| | |  | | | | |_) \___ \   |  _| | |\/| | | | | |     / _ \ | || | | | |_) | |");
            Console.WriteLine(@"| | |__| |_| |  __/ ___) |  | |___| |  | | |_| | |___ / ___ \| || |_| |  _ <  |");
            Console.WriteLine(@"|  \____\___/|_|   |____/   |_____|_|  |_|\___/|_____/_/   \_\_| \___/|_| \_\ |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"|                                                     by Jean-Philippe Boivin |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"|                   COPS v6 - AccServer Enhanced " + Version + "                  |");
            Console.WriteLine(@"|                      Copyright (C) 2010-2012, 2014-2015                     |");
            Console.WriteLine(@"|                                                                             |");
            Console.WriteLine(@"+-----------------------------------------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public static void Exit(bool aRestart)
        {
            if (aRestart)
            {
                Process process = new Process();
                #if _X64
                process.StartInfo.FileName = RootPath + "/AccServer64.exe";
                #else
                process.StartInfo.FileName = RootPath + "/AccServer.exe";
                #endif
                process.StartInfo.WorkingDirectory = RootPath;
                process.Start();
            }

            Environment.Exit(0);
        }

        /// <summary>
        /// Get and return the current version.
        /// </summary>
        public static String Version
        {
            get
            {
                Version Version = new Version();
                Version = sAssembly.GetName().Version;

                return String.Format("{0:0}.{1:0}.{2:0}.{3:0}", Version.Major, Version.Minor, Version.Build, Version.Revision);
            }
        }
    }
}
