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
    /// Background worker(s) that will regenerate monsters.
    /// </summary>
    public class GeneratorThread
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Generator));

        /// <summary>
        /// The global instance of the singleton.
        /// </summary>
        private static volatile GeneratorThread sInstance = null;
        /// <summary>
        /// The global lock for the singleton pattern.
        /// </summary>
        private static object sLock = new object();

        /// <summary>
        /// Get the GeneratorThread singleton. If the object does not exist yet,
        /// it will be created.
        /// </summary>
        public static GeneratorThread Instance
        {
            get
            {
                if (sInstance == null)
                {
                    lock (sLock)
                    {
                        if (sInstance == null)
                            sInstance = new GeneratorThread();
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
        /// Create new worker threads for regenerating monsters.
        /// </summary>
        private GeneratorThread()
        {
            for (Int32 i = 0; i < WORKERS_COUNT; ++i)
            {
                mWorkers[i] = new Thread(GeneratorThread.RegenerateMonsters);
                mWorkers[i].Name = "GeneratorWorker" + i.ToString();
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
        private static void RegenerateMonsters()
        {
            const uint MAX_NPC_PER_ONTIMER = 20;

            uint maxNpc = MAX_NPC_PER_ONTIMER;
            int index = 0;

            sLogger.Info("Worker {0} starting for handling generators.",
                Thread.CurrentThread.ManagedThreadId);

            index = 0;
            while (!Program.Exiting)
            {
                lock (Database.AllGenerators)
                {
                    maxNpc = MAX_NPC_PER_ONTIMER;
                    if (index >= Database.AllGenerators.Count)
                        index = 0;

                    Generator generator = null;
                    for (int count = Database.AllGenerators.Count;
                            !Program.Exiting & index < count; ++index)
                    {
                        generator = Database.AllGenerators[index];

                        maxNpc -= generator.Generate(maxNpc);
                        if (maxNpc <= 0)
                            break;

                        Thread.Yield();
                    }

                    if (index >= Database.AllGenerators.Count)
                        index = 0;
                }

                Thread.Sleep(10);
            }
        }
    }
}
