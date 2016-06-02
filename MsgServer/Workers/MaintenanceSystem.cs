// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Threading;
using COServer.Entities;
using COServer.Network;

namespace COServer.Workers
{
    /// <summary>
    /// Background worker that will handle triggers of maintenance.
    /// </summary>
    public class MaintenanceSystem
    {
        /// <summary>
        /// The global instance of the singleton.
        /// </summary>
        private static volatile MaintenanceSystem sInstance = null;
        /// <summary>
        /// The global lock for the singleton pattern.
        /// </summary>
        private static object sLock = new object();

        /// <summary>
        /// Get the MaintenanceSystem singleton. If the object does not exist yet,
        /// it will be created.
        /// </summary>
        public static MaintenanceSystem Instance
        {
            get
            {
                if (sInstance == null)
                {
                    lock (sLock)
                    {
                        if (sInstance == null)
                            sInstance = new MaintenanceSystem();
                        else
                            while (sInstance == null)
                                Thread.Yield();
                    }
                }

                return sInstance;
            }
        }

        /// <summary>
        /// The worker thread.
        /// </summary>
        private Thread mWorker;

        /// <summary>
        /// Determine whether or not a maintenance was triggered.
        /// </summary>
        private bool mTriggered;
        /// <summary>
        /// Determine whether or not the server must be restarted.
        /// </summary>
        private bool mRestart;

        /// <summary>
        /// Create a new worker thread for handling triggers of maintenance.
        /// </summary>
        private MaintenanceSystem()
        {
            mTriggered = false;
            mRestart = true;

            mWorker = new Thread(Check);
            mWorker.Name = "MaintenanceSystem";
            mWorker.IsBackground = true;
            mWorker.Start();
        }

        /// <summary>
        /// The task of the worker thread. It will check for a trigger.
        /// </summary>
        private void Check()
        {
            while (!Program.Exiting)
            {
                // Environment.TickCount will become negative after ~24.9 days.
                // Shutdown the server before as otherwise the server might
                // have an unexpected behaviour.
                if (Environment.TickCount > 2000000000)
                    Trigger(false);

                if (mTriggered)
                {
                    Execute();
                    break;
                }

                Thread.Sleep(10000); // 10s
            }
        }

        /// <summary>
        /// Trigger a maintenance.
        /// </summary>
        public void Trigger()
        {
            mTriggered = true;
        }

        /// <summary>
        /// Trigger a maintenance.
        /// </summary>
        /// <param name="aRestart">Indicates if the server must be restarted.</param>
        public void Trigger(bool aRestart)
        {
            mTriggered = true;
            mRestart = aRestart;
        }

        /// <summary>
        /// Execute the maintenance.
        /// </summary>
        private void Execute()
        {
            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", "Server maintenance in 15 seconds! Please log off to avoid lose of data!", Channel.GM, Color.Red));
            Thread.Sleep(5000);
            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", "Server maintenance in 10 seconds! Please log off to avoid lose of data!", Channel.GM, Color.Red));
            Thread.Sleep(5000);
            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", "Server maintenance in 5 seconds! Please log off to avoid lose of data!", Channel.GM, Color.Red));
            Thread.Sleep(5000);
            World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", "The server will shutdown now! Please log off to avoid lose of data!", Channel.GM, Color.Red));
            Thread.Sleep(2000);

            Console.WriteLine("Server maintenance !");

            // TODO gracefully shutdown threads...
            Program.Exiting = true;

            Player[] Players = new Player[World.AllPlayers.Count];
            World.AllPlayers.Values.CopyTo(Players, 0);

            Console.WriteLine("Disconnecting all players...");
            foreach (Player Player in Players)
                Player.Client.Disconnect();

            Console.WriteLine("Saving all items...");
            lock (World.AllItems)
            {
                foreach (Item Item in World.AllItems.Values)
                    Item.Save();
            }

            ItemDestroyer.Instance.Join();
            GeneratorThread.Instance.Join();

            Program.Exit(mRestart);
        }
    }
}
