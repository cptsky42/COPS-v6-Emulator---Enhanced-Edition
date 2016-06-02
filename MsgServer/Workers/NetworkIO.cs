// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Concurrent;
using System.Threading;
using COServer.Network;

namespace COServer.Workers
{
    /// <summary>
    /// A background worker processing all networking requests.
    /// </summary>
    public interface INetworkWorker
    {
        /// <summary>
        /// Unique ID of the worker.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Process the specified message for the client.
        /// </summary>
        /// <param name="aClient">The client associated to the incoming message.</param>
        /// <param name="aMsg">The message to process.</param>
        void Process(Client aClient, Msg aMsg);

        /// <summary>
        /// Send the specified message to the client.
        /// </summary>
        /// <param name="aClient">The client to which the message must be sent.</param>
        /// <param name="aData">The data of the message to send.</param>
        void Send(Client aClient, Byte[] aData);
    }

    public class NetworkIO
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        protected static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Msg));

        /// <summary>
        /// An incoming packet.
        /// </summary>
        private struct IncomingPacket
        {
            /// <summary>
            /// Client who sent the packet.
            /// </summary>
            public Client Client;
            /// <summary>
            /// The packet.
            /// </summary>
            public Msg Msg;
        }

        /// <summary>
        /// An outgoing packet.
        /// </summary>
        private struct OutgoingPacket
        {
            /// <summary>
            /// Client who will receive the packet.
            /// </summary>
            public Client Client;
            /// <summary>
            /// Data of the packet.
            /// </summary>
            public Byte[] Data;
        }

        /// <summary>
        /// A background worker.
        /// </summary>
        private class Worker : INetworkWorker
        {
            /// <summary>
            /// Unique ID of the worker.
            /// </summary>
            public int Id { get; private set; }

            /// <summary>
            /// Background thread processing incoming packets.
            /// </summary>
            private Thread mIncomingThread;
            /// <summary>
            /// Background thread
            /// </summary>
            private Thread mOutgoingThread;

            /// <summary>
            /// Queue for all incoming packets.
            /// </summary>
            public BlockingCollection<IncomingPacket> mIncomingQueue;
            /// <summary>
            /// Queue for all outgoing packets.
            /// </summary>
            public BlockingCollection<OutgoingPacket> mOutgoingQueue;

            /// <summary>
            /// Create a new worker.
            /// </summary>
            /// <param name="aId">The unique ID of the worker to be created.</param>
            public Worker(int aId)
            {
                Id = aId;

                mIncomingQueue = new BlockingCollection<IncomingPacket>();
                mIncomingThread = new Thread(ProcessIncomingPackets);
                mIncomingThread.Name = "IncomingWorker" + aId.ToString();
                mIncomingThread.IsBackground = true;

                mOutgoingQueue = new BlockingCollection<OutgoingPacket>();
                mOutgoingThread = new Thread(ProcessOutgoingPackets);
                mOutgoingThread.Name = "OutgoingWorker" + aId.ToString();
                mOutgoingThread.IsBackground = true;

                mIncomingThread.Start(this);
                mOutgoingThread.Start(this);
            }

            /// <summary>
            /// Process the specified message for the client.
            /// </summary>
            /// <param name="aClient">The client associated to the incoming message.</param>
            /// <param name="aMsg">The message to process.</param>
            public void Process(Client aClient, Msg aMsg)
            {
                mIncomingQueue.Add(new IncomingPacket() { Client = aClient, Msg = aMsg });
            }

            /// <summary>
            /// Send the specified message to the client.
            /// </summary>
            /// <param name="aClient">The client to which the message must be sent.</param>
            /// <param name="aData">The data of the message to send.</param>
            public void Send(Client aClient, Byte[] aData)
            {
                mOutgoingQueue.Add(new OutgoingPacket() { Client = aClient, Data = aData });
            }

            /// <summary>
            /// Process outgoing packets.
            /// </summary>
            private static void ProcessOutgoingPackets(Object aSender)
            {
                Worker thread = aSender as Worker;
                while (true)
                {
                    try
                    {
                        OutgoingPacket packet = thread.mOutgoingQueue.Take();
                        {
                            packet.Client._Send(ref packet.Data);
                        }
                    }
                    catch (Exception exc)
                    {
                        sLogger.Error("Failed to pop an element from the outgoing queue...\nException: {0}", exc);
                    }
                }
            }

            /// <summary>
            /// Process incoming packets.
            /// </summary>
            private static void ProcessIncomingPackets(Object aSender)
            {
                Worker thread = aSender as Worker;
                while (true)
                {
                    try
                    {
                        IncomingPacket packet = thread.mIncomingQueue.Take();
                        {
                            try
                            {
                                packet.Msg.Process(packet.Client);
                            }
                            catch (Exception exc)
                            {
                                sLogger.Error("Something wrong happened while processing a message !\nException: {0}", exc);

                                if (packet.Client != null && Program.Debug)
                                    packet.Client.Disconnect();
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        sLogger.Error("Failed to pop an element from the incoming queue...\nException: {0}", exc);
                    }
                }
            }
        }

        /// <summary>
        /// All the background workers.
        /// </summary>
        private Worker[] mWorkers;
        /// <summary>
        /// A map of workers and the number of clients on each worker.
        /// </summary>
        private ConcurrentDictionary<int, int> mClients;

        /// <summary>
        /// Create a new set of workers to process networking I/O.
        /// </summary>
        /// <param name="aCount">The number of workers to create.</param>
        public NetworkIO(Byte aCount)
        {
            mWorkers = new Worker[aCount];
            mClients = new ConcurrentDictionary<int, int>();

            for (int workerId = 0; workerId < mWorkers.Length; ++workerId)
            {
                mWorkers[workerId] = new Worker(workerId);
                mClients.TryAdd(workerId, 0);
            }
        }

        /// <summary>
        /// Assign the client to one worker.
        /// </summary>
        /// <param name="aClient">The client to assign.</param>
        /// <param name="aWorker">The worker that will handle the networking I/O of the client.</param>
        public void AddClient(Client aClient, ref INetworkWorker aWorker)
        {
            if (aWorker != null)
                return;

            int workerId = 0;
            int lastNbClients = int.MaxValue;
            foreach (var kv in mClients)
            {
                if (kv.Value < lastNbClients)
                {
                    workerId = kv.Key;
                    lastNbClients = kv.Value;
                }
            }

            sLogger.Debug("{0}:{1} will use the I/O worker n° {2}.",
                aClient.IPAddress,
                aClient.Port,
                workerId);

            bool success = false;
            while (!success)
            {
                int nbClients;
                if (!mClients.TryGetValue(workerId, out nbClients))
                    break;

                success = mClients.TryUpdate(workerId, nbClients + 1, nbClients);
            }

            aWorker = mWorkers[workerId];
        }

        /// <summary>
        /// Unassign the client to a worker.
        /// </summary>
        /// <param name="aClient">The client to unassign.</param>
        /// <param name="aWorker">The worker handling the networking I/O of the client.</param>
        public void DelClient(Client aClient, ref INetworkWorker aWorker)
        {
            if (aWorker == null)
                return;

            bool success = false;
            while (!success)
            {
                int nbClients;
                if (!mClients.TryGetValue(aWorker.Id, out nbClients))
                    break;

                success = mClients.TryUpdate(aWorker.Id, nbClients - 1, nbClients);
            }

            aWorker = null;
        }
    }
}
