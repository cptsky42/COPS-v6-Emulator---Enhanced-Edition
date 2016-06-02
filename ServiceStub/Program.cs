// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace COServer
{
    public class Program
    {
        public const String USAGE = (
            "ServiceStub.exe start Server.exe\n" +
            "ServiceStub.exe stop [ServerName]");

        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "start":
                    {
                        if (args.Length != 2)
                            ShowUsage();

                        String filename = String.Format("{0}\\{1}",
                            Application.StartupPath, args[1]);

                        Console.WriteLine("Launching: {0}", filename);
                        Console.WriteLine("Working directory: {0}", Application.StartupPath);

                        Process process = new Process();
                        process.StartInfo.FileName = filename;
                        process.StartInfo.WorkingDirectory = Application.StartupPath;

                        try
                        {
                            process.Start();
                            process.WaitForExit();
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine("Exception: {0}", exc.Message);
                            Environment.Exit(-1);
                        }

                        break;
                    }
                case "stop":
                    {
                        if (args.Length != 1 && args.Length != 2)
                            ShowUsage();

                        String eventName = String.Format("STOP_COPS_{0}SERVER{1}{2}",
                            args.Length == 2 ? "MSG" : "ACC",
                            args.Length == 2 ? "_" : "",
                            args.Length == 2 ? args[1] : "");
                        Console.WriteLine("Signaling {0}", eventName);

                        try
                        {
                            EventWaitHandle handle = EventWaitHandle.OpenExisting(eventName);
                            handle.Set();
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine("Exception: {0}", exc.Message);
                            Environment.Exit(-1);
                        }

                        break;
                    }
                default:
                    ShowUsage();
                    break;
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine(USAGE);
            Environment.Exit(-1);
        }
    }
}
