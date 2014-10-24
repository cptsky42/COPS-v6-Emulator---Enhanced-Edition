// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Threading;
using CO2_CORE_DLL;

namespace COServer.Threads
{
    public class FloorThread
    {
        private WaitCallback Callback;

        public FloorThread()
        {
            Callback = new WaitCallback(Process);
        }

        public Boolean AddToQueue(FloorItem Item) { return ThreadPool.QueueUserWorkItem(Callback, Item); }

        private void Process(Object Obj)
        {
            try
            {
                FloorItem Item = (Obj as FloorItem);
                if (Item == null)
                    return;

                if (Item.Destroyed)
                    return;

                while (!(Item.Money != 0 && Environment.TickCount - Item.DroppedTime > 20000) &&
                    !(Environment.TickCount - Item.DroppedTime > 30000))
                {
                    if (Item.Destroyed)
                        return;
                    Thread.Sleep(100);
                }
                Item.Destroy(true);
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
