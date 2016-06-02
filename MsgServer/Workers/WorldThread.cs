// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Threading;

namespace COServer.Workers
{
    /// <summary>
    /// Background worker(s) that will do periodic works for entities, maps and events.
    /// </summary>
    public class WorldThread
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(World));

        /// <summary>
        /// The global instance of the singleton.
        /// </summary>
        private static volatile WorldThread sInstance = null;
        /// <summary>
        /// The global lock for the singleton pattern.
        /// </summary>
        private static object sLock = new object();

        /// <summary>
        /// Get the WorldThread singleton. If the object does not exist yet,
        /// it will be created.
        /// </summary>
        public static WorldThread Instance
        {
            get
            {
                if (sInstance == null)
                {
                    lock (sLock)
                    {
                        if (sInstance == null)
                            sInstance = new WorldThread();
                        else
                            while (sInstance == null)
                                Thread.Yield();
                    }
                }

                return sInstance;
            }
        }

        /// <summary>
        /// The number of worker threads.
        /// </summary>
        public const Int32 WORKERS_COUNT = 1;

        /// <summary>
        /// The worker threads.
        /// </summary>
        private Thread[] mWorkers = new Thread[WORKERS_COUNT];

        /// <summary>
        /// Create new worker threads for doing periodic works for entities, maps and events.
        /// </summary>
        private WorldThread()
        {
            for (Int32 i = 0; i < WORKERS_COUNT; ++i)
            {
                mWorkers[i] = new Thread(WorldThread.Process);
                mWorkers[i].Name = "WorldWorker" + i.ToString();
                mWorkers[i].Priority = ThreadPriority.Normal;
                mWorkers[i].IsBackground = true;
                mWorkers[i].Start();
            }
        }

        /// <summary>
        /// Join all workers and wait until they finish.
        /// </summary>
        public void Join()
        {
            for (Int32 i = 0; i < WORKERS_COUNT; ++i)
                mWorkers[i].Join();
        }

        /// <summary>
        /// The task of the worker thread. It will regenerate monsters.
        /// </summary>
        private static void Process()
        {
            sLogger.Info("Worker {0} starting for doing periodic works for entities, maps and events.",
                Thread.CurrentThread.ManagedThreadId);

            return;
        }
    }
}
