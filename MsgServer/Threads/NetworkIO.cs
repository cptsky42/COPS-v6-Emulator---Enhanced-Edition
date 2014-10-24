// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Concurrent;
using COServer.Network;
using CO2_CORE_DLL;
using CO2_CORE_DLL.Security.Cryptography;

namespace COServer.Threads
{
    public class NetworkIO
    {
        private struct KeyValuePair
        {
            public Client Key;
            public Byte[] Value;
        }

        private class IOThread
        {
            public Thread IncomingThread;
            public Thread OutgoingThread;
            public ConcurrentQueue<KeyValuePair> IncomingQueue;
            public ConcurrentQueue<KeyValuePair> OutgoingQueue;

            public IOThread()
            {
                IncomingQueue = new ConcurrentQueue<KeyValuePair>();
                IncomingThread = new Thread(NetworkIO.ProcessIncomingPackets);
                IncomingThread.IsBackground = true;

                OutgoingQueue = new ConcurrentQueue<KeyValuePair>();
                OutgoingThread = new Thread(NetworkIO.ProcessOutgoingPackets);
                OutgoingThread.IsBackground = true;

                IncomingThread.Start(this);
                OutgoingThread.Start(this);
            }
        }

        private Thread AccThread;

        private IOThread[] Threads;

        public NetworkIO(Int32 Amount)
        {
            Threads = new IOThread[Amount];
            for (Int32 i = 0; i < Threads.Length; i++)
                Threads[i] = new IOThread();

            AccThread = new Thread(CheckAccServer);
            AccThread.IsBackground = true;
            AccThread.Start();
        }

        public void Receive(Client Client, Byte[] Data)
        { Threads[Client.IO_UID].IncomingQueue.Enqueue(new KeyValuePair() { Key = Client, Value = Data }); }

        public void Send(Client Client, Byte[] Data)
        { Threads[Client.IO_UID].OutgoingQueue.Enqueue(new KeyValuePair() { Key = Client, Value = Data }); }

        public void AddClient(Client Client) { Client.IO_UID = (Byte)MyMath.Generate(0, Threads.Length - 1); }
        public void DelClient(Client Client) { }

        private static void ProcessOutgoingPackets(Object Sender)
        {
            IOThread Thread = Sender as IOThread;
            while (true)
            {
                if (!Thread.OutgoingQueue.IsEmpty)
                {
                    try
                    {
                        KeyValuePair KV;
                        if (Thread.OutgoingQueue.TryDequeue(out KV))
                        {
                            lock (KV.Key.Cipher)
                            {
                                KV.Key.Cipher.Encrypt(ref KV.Value);
                                KV.Key.Socket.Send(KV.Value);
                            }
                        }
                    }
                    catch (Exception Exc) { Program.WriteLine(Exc); }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        private static void ProcessIncomingPackets(Object Sender)
        {
            IOThread Thread = Sender as IOThread;
            while (true)
            {
                if (!Thread.IncomingQueue.IsEmpty)
                {
                    try
                    {
                        KeyValuePair KV;
                        if (Thread.IncomingQueue.TryDequeue(out KV))
                        {
                            Int16 MsgId = (Int16)((KV.Value[0x03] << 8) + KV.Value[0x02]);
                            switch (MsgId)
                            {
                                case MsgRegister.Id:
                                    {
                                        MsgRegister.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgTalk.Id:
                                    {
                                        MsgTalk.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgWalk.Id:
                                    {
                                        MsgWalk.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgItem.Id:
                                    {
                                        MsgItem.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgAction.Id:
                                    {
                                        MsgAction.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgTick.Id:
                                    {
                                        MsgTick.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgName.Id:
                                    {
                                        MsgName.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgFriend.Id:
                                    {
                                        MsgFriend.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgInteract.Id:
                                    {
                                        MsgInteract.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgTeam.Id:
                                    {
                                        MsgTeam.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgAllot.Id:
                                    {
                                        MsgAllot.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgGemEmbed.Id:
                                    {
                                        MsgGemEmbed.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgForge.Id:
                                    {
                                        MsgForge.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgConnect.Id:
                                    {
                                        MsgConnect.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgTrade.Id:
                                    {
                                        MsgTrade.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgMapItem.Id:
                                    {
                                        MsgMapItem.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgPackage.Id:
                                    {
                                        MsgPackage.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgSyndicate.Id:
                                    {
                                        MsgSyndicate.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgMessageBoard.Id:
                                    {
                                        MsgMessageBoard.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgSynMemberInfo.Id:
                                    {
                                        MsgSynMemberInfo.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgNpc.Id:
                                    {
                                        MsgNpc.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgDialog.Id:
                                    {
                                        MsgDialog.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgDataArray.Id:
                                    {
                                        MsgDataArray.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgBless.Id:
                                    {
                                        MsgBless.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgBroadcast.Id:
                                    {
                                        MsgBroadcast.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgNoble.Id:
                                    {
                                        MsgNoble.Process(KV.Key, KV.Value);
                                        break;
                                    }
                                case MsgAccountExt.Id:
                                    {
                                        MsgAccountExt.Process(KV.Key, KV.Value);
                                        break;
                                    };
                                default:
                                    {
                                        Console.WriteLine("Msg[{0}] not implemented yet!", MsgId);
                                        break;
                                    }
                            }
                        }
                    }
                    catch (Exception Exc) { Program.WriteLine(Exc); }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        private void CheckAccServer()
        {
            while (true)
            {
                try
                {
                    if (!Server.AuthSocket.Connected)
                    {
                        lock (Server.AuthSocket)
                        {
                            lock (Server.AuthCrypto)
                            {
                                try { Server.AuthSocket = new TcpClient(Server.AuthIP, Server.AuthPort); }
                                catch
                                {
                                    Console.WriteLine("Can't connect to AccServer!");
                                    Thread.Sleep(5000);
                                    continue;
                                }

                                Server.AuthCrypto = new COCAC();
                                Server.AuthCrypto.GenerateIV(Server.COSAC_PKey, Server.COSAC_GKey);
                            }
                        }
                    }
                }
                catch { }
                Thread.Sleep(1000);
            }
        }
    }
}
