// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
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
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA", false);

                Console.Title = "COPS v6 - The Return Of The Legend : MsgServer Enhanced";
                Program.WriteHeader();

                if (!Directory.Exists(Program.RootPath + "/Log/"))
                    Directory.CreateDirectory(Program.RootPath + "/Log/");

                // log4net configuration
                XmlConfigurator.ConfigureAndWatch(new FileInfo(Program.RootPath + "/MsgServer.config"));

                NetworkMonitor = new NetworkMonitor(5000);
                Server.Run();
            }
            catch (Exception exc) { sLogger.Fatal(exc); }
            
            while (true)
                Console.Read();
        }

        public static readonly String RootPath = Environment.CurrentDirectory;
        public static Encoding Encoding = Encoding.GetEncoding("Windows-1252");

        public static bool Exiting = false;

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
            Console.WriteLine(@"|                   COPS v6 - MsgServer Enhanced " + Version + "                  |");
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
                process.StartInfo.FileName = RootPath + "/MsgServer64.exe";
                #else
                process.StartInfo.FileName = RootPath + "/MsgServer.exe";
                #endif
                process.StartInfo.WorkingDirectory = RootPath;
                process.Start();
            }

            Environment.Exit(0);
        }

        /// <summary>
        /// Determine if the server is running as a service (launched by ServiceStub).
        /// </summary>
        /// <returns>True if the server is running as a service, false otherwise.</returns>
        private static bool IsService()
        {
            int id = Process.GetCurrentProcess().Id;
            sLogger.Debug("Process ID: {0}", Process.GetCurrentProcess().Id);

            var search = new ManagementObjectSearcher(
                "root\\CIMV2",
                String.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", id));

            var results = search.Get().GetEnumerator();
            results.MoveNext();
            var queryObj = results.Current;

            var parentId = (uint)queryObj["ParentProcessId"];
            var parent = Process.GetProcessById((int)parentId);
            sLogger.Debug("Process was started by {0}", parent.ProcessName);

            return parent.ProcessName == "ServiceStub";
        }

        /// <summary>
        /// Write the formatted input to the log file. If running in DEBUG, it will be written to the console.
        /// </summary>
        [Obsolete]
        public static void Log(String format, params object[] args)
        {
            String msg = String.Format(format, args);

            if (Debug)
                Console.WriteLine(msg);
            sLogger.Info(String.Format("[{0:R}] ", DateTime.Now.ToUniversalTime()) + msg);
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
