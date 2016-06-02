// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgName : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_NAME; } }

        public enum NameAct
        {
            Fireworks = 1,
            CreateSyn = 2,
            Syndicate = 3,
            ChangeTitle = 4,
            DelRole = 5,
            Spouse = 6,
            QueryNPC = 7,           //To client, To Server
            Wanted = 8,             //To client
            MapEffect = 9,          //To client
            RoleEffect = 10,        //To client
            MemberList = 11,        //To client, To Server, dwData is index
            KickOut_SynMem = 12,
            QueryWanted = 13,
            QueryPoliceWanted = 14,
            PoliceWanted = 15,
            QuerySpouse = 16,
            AddDice_Player = 17,	//BcastClient(INCLUDE_SELF) Ôö¼Ó÷»×ÓÍæ¼Ò// dwDataÎª÷»×ÓÌ¯ID // To Server ¼ÓÈë ÐèÒªÔ­ÏûÏ¢·µ»Ø
            DelDice_Player = 18,    //BcastClient(INCLUDE_SELF) É¾³ý÷»×ÓÍæ¼Ò// dwDataÎª÷»×ÓÌ¯ID // To Server Àë¿ª ÐèÒªÔ­ÏûÏ¢·µ»Ø
            DiceBonus = 19,         //dwDataÎªMoney
            Sound = 20,
            SynEnemie = 21,	
            SynAlly = 22,
        };

        //--------------- Internal Members ---------------
        private UInt32 __Data = 0; // Data, TargetId, (PosX, PosY)
        private NameAct __Type = (NameAct)0;
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(4, value); }
        }

        public UInt32 TargetId
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(4, value); }
        }

        public UInt16 PosX
        {
            get { return (UInt16)((__Data >> 16) & 0x0000FFFFU); }
            set { __Data = (UInt32)((value << 16) | (__Data & 0x0000FFFFU)); WriteUInt16(4, value); }
        }

        public UInt16 PosY
        {
            get { return (UInt16)(__Data & 0x0000FFFFU); }
            set { __Data = (UInt32)((__Data & 0xFFFF0000U) | (value)); WriteUInt16(6, value); }
        }

        public NameAct Type
        {
            get { return __Type; }
            set { __Type = value; mBuf[8] = (Byte)value; }
        }

        public String[] Params
        {
            get { return (String[])__StrPacker; }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgName(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Data = BitConverter.ToUInt32(mBuf, 4);
            __Type = (NameAct)mBuf[8];
            __StrPacker = new StringPacker(this, 9);
        }

        public MsgName(UInt16 aPosX, UInt16 aPosY, String aParam, NameAct aType)
            : base((UInt16)(12 + aParam.Length))
        {
            PosX = aPosX;
            PosY = aPosY;
            Type = aType;

            __StrPacker = new StringPacker(this, 9);
            __StrPacker.AddString(aParam);
        }

        public MsgName(UInt32 aData, String aParam, NameAct aType)
            : base((UInt16)(12 + aParam.Length))
        {
            Data = aData;
            Type = aType;

            __StrPacker = new StringPacker(this, 9);
            __StrPacker.AddString(aParam);
        }

        public MsgName(Int32 aData, String aParam, NameAct aType)
            : base((UInt16)(12 + aParam.Length))
        {
            Data = (UInt32)aData;
            Type = aType;

            __StrPacker = new StringPacker(this, 9);
            __StrPacker.AddString(aParam);
        }

        public MsgName(UInt32 aData, String[] aParams, NameAct aType)
            : base((UInt16)(12 + aParams.Select(p => p.Length + 1).Sum()))
        {
            Data = aData;
            Type = aType;

            __StrPacker = new StringPacker(this, 9);
            foreach (String param in aParams)
                __StrPacker.AddString(param);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            switch (Type)
            {
                case NameAct.MemberList:
                    {
                        Player player = aClient.Player;
                        if (player == null)
                            return;

                        if (player.Syndicate == null)
                            return;

                        Syndicate syn = player.Syndicate;
                        String[] members = syn.GetMemberList();

                        int index = (int)(Data * 10);
                        int count = members.Length - index;
                        if (count > 10)
                            count = 10;

                        members = members.Skip(index).Take(count).ToArray();

                        player.Send(new MsgName(Data + 1, members, NameAct.MemberList));
                        break;
                    }
                case NameAct.QuerySpouse:
                    {
                        Player player = aClient.Player;
                        if (player == null)
                            return;

                        Player target = null;
                        if (!World.AllPlayers.TryGetValue((Int32)TargetId, out target))
                            return;

                        player.Send(new MsgName(target.UniqId, target.Mate, NameAct.QuerySpouse));
                        break;
                    }
                default:
                    {
                        sLogger.Error("Type {0} is not implemented for MsgName.", (Byte)Type);
                        break;
                    }
            }
        }
    }
}
