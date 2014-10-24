// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Threading;
using CO2_CORE_DLL;

namespace COServer.Threads
{
    public class SynThread
    {
        private struct SavingInfo
        {
            public Syndicate.Info Syn;
            public String Entry;
            public Object Value;
        }

        private Queue<SavingInfo> Queue;
        private Thread Thread;

        public SynThread()
        {
            Queue = new Queue<SavingInfo>();
            Thread = new Thread(Process);
            Thread.IsBackground = true;
            Thread.Start();
        }

        ~SynThread()
        {
            Queue = null;
            Thread = null;
        }

        public void AddToQueue(Syndicate.Info Syn, String Entry, Object Value) { Queue.Enqueue(new SavingInfo() { Syn = Syn, Entry = Entry, Value = Value }); }
        public Boolean IsEmpty() { try { return Queue.Count == 0; } catch { return true; } }

        private void Process()
        {
            while (true)
            {
                try
                {
                    if (Queue.Count > 0)
                    {
                        SavingInfo Info = Queue.Dequeue();
                        Info.Syn.Save(Info.Entry, Info.Value);
                    }
                }
                catch { }
                Thread.Sleep(5);
            }
        }
    }
}
