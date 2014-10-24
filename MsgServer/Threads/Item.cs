// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Threading;
using CO2_CORE_DLL;

namespace COServer.Threads
{
    public class ItemThread
    {
        public FileStream Stream;
        private Queue<Item> Queue;
        private Thread Thread;

        public ItemThread(FileStream FStream)
        {
            Stream = FStream;
            Queue = new Queue<Item>();
            Thread = new Thread(Process);
            Thread.IsBackground = true;
            Thread.Start();
        }

        ~ItemThread()
        {
            if (Stream != null)
                Stream.Dispose();
            Queue = null;
            Thread = null;
        }

        public void AddToQueue(Item Item) { Queue.Enqueue(Item); }
        public Boolean IsEmpty() { try { return Queue.Count == 0; } catch { return true; } }

        private void Process()
        {
            while (true)
            {
                try
                {
                    if (Queue.Count > 0)
                    {
                        Item Item = Queue.Dequeue();
                        Item.Save(ref Stream);
                    }
                }
                catch { }
                Thread.Sleep(5);
            }
        }
    }
}
