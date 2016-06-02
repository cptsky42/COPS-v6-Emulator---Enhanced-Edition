// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace COServer.Workers
{
    /// <summary>
    /// Background worker(s) that will destroy floor items.
    /// </summary>
    public class ItemDestroyer
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        protected static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(FloorItem));

        /// <summary>
        /// The global instance of the singleton.
        /// </summary>
        private static volatile ItemDestroyer sInstance = null;
        /// <summary>
        /// The global lock for the singleton pattern.
        /// </summary>
        private static object sLock = new object();

        /// <summary>
        /// Get the ItemDestroyer singleton. If the object does not exist yet,
        /// it will be created.
        /// </summary>
        public static ItemDestroyer Instance
        {
            get
            {
                if (sInstance == null)
                {
                    lock (sLock)
                    {
                        if (sInstance == null)
                            sInstance = new ItemDestroyer();
                        else
                            while (sInstance == null)
                                Thread.Yield();
                    }
                }

                return sInstance;
            }
        }

        /// <summary>
        /// The number of background workers.
        /// </summary>
        public const Int32 WORKERS_COUNT = 5;

        /// <summary>
        /// The worker threads.
        /// </summary>
        private Thread[] mWorkers = new Thread[WORKERS_COUNT];

        /// <summary>
        /// The queue of floor items.
        /// </summary>
        private BlockingCollection<FloorItem> mQueue = new BlockingCollection<FloorItem>();

        /// <summary>
        /// Create new worker threads for destroying floor items.
        /// </summary>
        private ItemDestroyer()
        {
            for (Int32 i = 0; i < WORKERS_COUNT; ++i)
            {
                mWorkers[i] = new Thread(ItemDestroyer.Process);
                mWorkers[i].Name = "ItemDestroyer" + i.ToString();
                mWorkers[i].Priority = ThreadPriority.BelowNormal;
                mWorkers[i].IsBackground = true;
                mWorkers[i].Start(this);
            }
        }

        /// <summary>
        /// Add a floor item to the destroyer queue.
        /// </summary>
        /// <param name="aItem">The floor item to add.</param>
        public void AddToQueue(FloorItem aItem) { mQueue.Add(aItem); }

        /// <summary>
        /// Join all workers and wait until they finish.
        /// </summary>
        public void Join()
        {
            for (Int32 i = 0; i < WORKERS_COUNT; ++i)
                mWorkers[i].Join();
        }

        /// <summary>
        /// The task of the worker thread. It will destroy floor items.
        /// </summary>
        private static void Process(Object aSender)
        {
            ItemDestroyer thread = (aSender as ItemDestroyer);
            BlockingCollection<FloorItem> queue = thread.mQueue;

            while (!Program.Exiting || queue.Count == 0)
            {
                FloorItem item = queue.Take();
                if (item == null)
                    continue;

                if (item.Destroyed)
                    continue;

                while (!item.Destroyed && (DateTime.UtcNow - item.DroppedTime).TotalSeconds < 30)
                    Thread.Sleep(100);

                try
                {
                    if (!item.Destroyed)
                        item.Destroy(true);
                }
                catch (Exception exc)
                {
                    sLogger.Error("Something wrong happened while destroying an item !\nException: {0}", exc);
                }
            }
        }
    }
}
