// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using COServer.Entities;
using COServer.Network;
using COServer.Network.Sockets;
using COServer.Script;
using COServer.Security.Cryptography;
using COServer.Workers;
using MoonSharp.Interpreter;

namespace COServer
{
    [MoonSharpUserData]
    public class Client : IDisposable
    {
        public Player Player = null;

        /// <summary>
        /// The name of the account.
        /// </summary>
        public String Account;
        /// <summary>
        /// The unique ID of the account.
        /// </summary>
        public UInt32 AccountID;

        /// <summary>
        /// The current task.
        /// </summary>
        public Task CurTask = null;

        /// <summary>
        /// The data sent by the client for the task.
        /// </summary>
        public String TaskData = "";

        /// <summary>
        /// The IP address of the client.
        /// </summary>
        public String IPAddress { get { return mSocket.IPAddress; } }
        /// <summary>
        /// The port of the client.
        /// </summary>
        public UInt16 Port { get { return mSocket.Port; } }

        /// <summary>
        /// The TCP/IP socket of the client.
        /// </summary>
        private TcpSocket mSocket = null;
        /// <summary>
        /// The cipher for decrypting/encrypting the network trafic.
        /// </summary>
        private TqCipher mCipher = null;

        /// <summary>
        /// The worker processing networking I/O of the client.
        /// </summary>
        private INetworkWorker mNetworkWorker = null;

        /// <summary>
        /// Indicate whether or not the object is disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// Create a new client based on the newly connected socket.
        /// </summary>
        /// <param name="aSocket">The socket of the client.</param>
        public Client(TcpSocket aSocket)
        {
            mSocket = aSocket;
            mCipher = new TqCipher();

            Account = null;
            AccountID = 0;

            mNetworkWorker = null;
            Server.NetworkIO.AddClient(this, ref mNetworkWorker);
        }

        ~Client()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool aDisposing)
        {
            if (!mIsDisposed)
            {
                if (aDisposing)
                {
                    if (Player != null)
                        Player.Dispose();

                    if (mSocket != null)
                        mSocket.Dispose();
                }

                Player = null;
                CurTask = null;
                mNetworkWorker = null;
                mSocket = null;

                mIsDisposed = true;
            }
        }

        /// <summary>
        /// Handle the client response to the exchange request.
        /// </summary>
        /// <param name="aAccountUID">The unique ID of the account.</param>
        /// <param name="aToken">The authentication token.</param>
        public unsafe void handleExchangeResponse(UInt32 aAccountUID, UInt32 aToken)
        {
            lock (mCipher)
            {
                UInt32* accountUID = &aAccountUID, token = &aToken;
                mCipher.GenerateAltKey(*((Int32*)token), *((Int32*)accountUID));
            }
        }

        /// <summary>
        /// Send the message to the client.
        /// </summary>
        /// <param name="aMsg">The message to send to the client.</param>
        public void Send(Msg aMsg)
        {
            Byte[] msg = (Byte[])aMsg;

            Program.NetworkMonitor.Send(msg.Length);
            mNetworkWorker.Send(this, msg);
        }

        /// <summary>
        /// Send the message to the client.
        /// 
        /// /!\ USED ONLY IN NETWORKIO WORKERS /!\
        /// </summary>
        /// <param name="aData">The data to send to the client.</param>
        public void _Send(ref Byte[] aData)
        {
            lock (mCipher)
            {
                mCipher.Encrypt(ref aData, aData.Length);
                mSocket.Send(aData);
            }
        }

        /// <summary>
        /// Handle the received data from the client.
        /// </summary>
        /// <param name="aData">The data sent by the client.</param>
        public void Receive(ref Byte[] aData)
        {
            Program.NetworkMonitor.Receive(aData.Length);

            if (aData.Length < Msg.MIN_SIZE)
                return;

            // decrypt the data
            mCipher.Decrypt(ref aData, aData.Length);

            UInt16 size = 0;
            for (Int32 i = 0; i < aData.Length; i += size)
            {
                size = (UInt16)((aData[i + 0x01] << 8) + aData[i + 0x00]);
                if (size < aData.Length)
                {
                    Msg msg = Msg.Create(aData, i, size);
                    if (msg != null)
                        mNetworkWorker.Process(this, msg);
                }
                else
                {
                    Msg msg = Msg.Create(aData, 0, aData.Length);
                    if (msg != null)
                        mNetworkWorker.Process(this, msg);
                }
            }
        }

        /// <summary>
        /// Disconnect the client.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (Player != null)
                {
                    if (Player.TransformEndTime != 0)
                    {
                        if (Player.CurHP >= Player.MaxHP)
                            Player.CurHP = Player.MaxHP;
                        Double Multiplier = (Double)Player.CurHP / (Double)Player.MaxHP;
                        Player.CalcMaxHP();
                        Player.CurHP = (Int32)(Player.MaxHP * Multiplier);

                        MyMath.GetEquipStats(Player);
                    }
                    //User.mThread.Stop();

                    Database.Save(Player, false);

                    //Mine
                    if (Player.Mining)
                        Player.Mining = false;

                    //Booth
                    if (Player.Booth != null)
                       Player. Booth.Destroy();

                    //Team
                    if (Player.Team != null)
                    {
                        if (Player.Team.Leader.UniqId == Player.UniqId)
                            Player.Team.Dismiss(Player);
                        else
                            Player.Team.DelMember(Player, true);
                        Player.Team = null;
                    }

                    // Pet
                    if (Player.Pet != null)
                    {
                        Player.Pet.Brain.Sleep();
                        Player.Pet.Disappear();
                        Player.Pet = null;
                    }

                    //Deal
                    if (Player.Deal != null)
                        Player.Deal.Release();

                    //Friends
                    foreach (Int32 FriendUID in Player.Friends.Keys)
                    {
                        Player Friend = null;
                        if (!World.AllPlayers.TryGetValue(FriendUID, out Friend))
                            continue;

                        Friend.Send(new MsgFriend(Player.UniqId, Player.Name, MsgFriend.Status.Offline, MsgFriend.Action.FriendOffline));
                    }

                    //Enemies
                    foreach (Player Enemy in World.AllPlayers.Values)
                    {
                        if (!Enemy.Enemies.ContainsKey(Player.UniqId))
                            continue;

                        Enemy.Send(new MsgFriend(Player.UniqId, Player.Name, MsgFriend.Status.Offline, MsgFriend.Action.EnemyOffline));
                    }

                    //Screen
                    if (Player.Screen != null)
                        Player.Screen.Clear(true);

                    Player.Map.DelEntity(Player);

                    if (World.AllPlayers.ContainsKey(Player.UniqId))
                        World.AllPlayers.Remove(Player.UniqId);

                    if (World.AllPlayerNames.ContainsKey(Player.Name))
                        World.AllPlayerNames.Remove(Player.Name);

                    Player.Dispose();
                    Player = null;
                }

                Server.NetworkIO.DelClient(this, ref mNetworkWorker);

                if (mSocket != null && mSocket.IsAlive)
                    mSocket.Disconnect();
            }
            catch (Exception exc) { Console.WriteLine(exc); }
        }
    }
}
