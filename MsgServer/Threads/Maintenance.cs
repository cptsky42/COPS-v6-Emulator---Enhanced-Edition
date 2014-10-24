// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Threading;
using COServer.Network;
using COServer.Entities;

namespace COServer.Threads
{
    public class MaintenanceSystem
    {
        private Thread Thread;

        public MaintenanceSystem()
        {
            Thread = new Thread(Checking);
            Thread.IsBackground = true;
            Thread.Start();
        }

        ~MaintenanceSystem()
        {
            Thread = null;
        }

        private void Checking()
        {
            while (true)
            {
                if (DateTime.UtcNow.Hour == 23 && DateTime.UtcNow.Minute <= 1)
                {
                    Execute(true);
                    break;
                }
                Thread.Sleep(60000);
            }
        }

        public void Execute(Boolean Backup)
        {
            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Server maintenance in 45 seconds! Please log off to avoid lose of data!", MsgTalk.Channel.GM, 0xFF0000));
            Thread.Sleep(15000);
            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Server maintenance in 30 seconds! Please log off to avoid lose of data!", MsgTalk.Channel.GM, 0xFF0000));
            Thread.Sleep(15000);
            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Server maintenance in 15 seconds! Please log off to avoid lose of data!", MsgTalk.Channel.GM, 0xFF0000));
            Thread.Sleep(5000);
            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Server maintenance in 10 seconds! Please log off to avoid lose of data!", MsgTalk.Channel.GM, 0xFF0000));
            Thread.Sleep(5000);
            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "Server maintenance in 5 seconds! Please log off to avoid lose of data!", MsgTalk.Channel.GM, 0xFF0000));
            Thread.Sleep(5000);
            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", "The server will shutdown now! Please log off to avoid lose of data!", MsgTalk.Channel.GM, 0xFF0000));
            Thread.Sleep(2500);

            Server.Socket.OnConnect = Server.Socket.OnDisconnect;

            Player[] Players = new Player[World.AllPlayers.Count];
            World.AllPlayers.Values.CopyTo(Players, 0);

            foreach (Player Player in Players)
                Player.Owner.Socket.Disconnect();

            while (!World.ItemThread.IsEmpty())
                Thread.Sleep(100);

            lock (World.AllItems)
            {
                foreach (Item Item in World.AllItems.Values)
                    Item.Save(ref World.ItemThread.Stream);
            }

            while (!World.SynThread.IsEmpty())
                Thread.Sleep(100);

            try
            {
                if (Backup)
                {
                    String File = Database.Backup.Pack();
                    if (Server.Backup_FTP)
                        Database.Backup.Upload(File, Server.Backup_Server, Server.Backup_Username, Server.Backup_Password);
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
            Program.Restart();
        }
    }
}
