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

        private Thread IncomingThread1;
        private Thread IncomingThread2;
        private ConcurrentQueue<KeyValuePair> IncomingQueue;

        private Thread OutgoingThread1;
        private Thread OutgoingThread2;
        private ConcurrentQueue<KeyValuePair> OutgoingQueue;

        public NetworkIO()
        {
            IncomingQueue = new ConcurrentQueue<KeyValuePair>();
            IncomingThread1 = new Thread(ProcessIncomingPackets);
            IncomingThread1.IsBackground = true;
            IncomingThread1.Start();
            IncomingThread2 = new Thread(ProcessIncomingPackets);
            IncomingThread2.IsBackground = true;
            IncomingThread2.Start();

            OutgoingQueue = new ConcurrentQueue<KeyValuePair>();
            OutgoingThread1 = new Thread(ProcessOutgoingPackets);
            OutgoingThread1.IsBackground = true;
            OutgoingThread1.Start();
            OutgoingThread2 = new Thread(ProcessOutgoingPackets);
            OutgoingThread2.IsBackground = true;
            OutgoingThread2.Start();
        }

        ~NetworkIO()
        {

        }

        public void Receive(Client Client, Byte[] Data) { IncomingQueue.Enqueue(new KeyValuePair() { Key = Client, Value = Data }); }
        public void Send(Client Client, Byte[] Data) { OutgoingQueue.Enqueue(new KeyValuePair() { Key = Client, Value = Data }); }

        private void ProcessOutgoingPackets()
        {
            while (true)
            {
                if (!OutgoingQueue.IsEmpty)
                {
                    try
                    {
                        KeyValuePair KV;
                        if (OutgoingQueue.TryDequeue(out KV))
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
                Thread.Sleep(1);
            }
        }

        private void ProcessIncomingPackets()
        {
            while (true)
            {
                if (!IncomingQueue.IsEmpty)
                {
                    KeyValuePair KV;
                    if (IncomingQueue.TryDequeue(out KV))
                    {
                        Int16 MsgId = (Int16)((KV.Value[0x03] << 8) + KV.Value[0x02]);
                        switch (MsgId)
                        {
                            case MsgAccount.Id:
                                {
                                    MsgAccount.Process(KV.Key, KV.Value);
                                    break;
                                }
                            case MsgConnect.Id:
                                {
                                    MsgConnect.Process(KV.Key, KV.Value);
                                    break;
                                }
                            case MsgActionExt.Id:
                                {
                                    MsgActionExt.Process(KV.Key, KV.Value);
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Msg[{0}] not implemented yet!", MsgId);
                                    break;
                                }
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}