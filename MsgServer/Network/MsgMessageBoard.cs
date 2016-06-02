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
    public class MsgMessageBoard : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_MESSAGEBOARD; } }

        public enum Action : byte
        {
            Del = 1,			        // to server					// no return
            GetList = 2,		    	// to server: index(first index)
            List = 3,		        	// to client: index(first index), name, words, time...
            GetWords = 4,	    		// to server: index(for get)	// return by MsgTalk
        };

        //--------------- Internal Members ---------------
        private UInt16 __Index = 0;
        private Channel __Channel = 0;
        private Action __Action = (Action)0;
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        public UInt16 Index
        {
            get { return __Index; }
            set { __Index = value; WriteUInt16(4, value); }
        }

        public Channel Channel
        {
            get { return __Channel; }
            set { __Channel = value; WriteUInt16(6, (UInt16)value); }
        }

        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; mBuf[8] = (Byte)value; }
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
        internal MsgMessageBoard(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Index = BitConverter.ToUInt16(mBuf, 4);
            __Channel = (Channel)BitConverter.ToUInt16(mBuf, 6);
            __Action = (Action)mBuf[8];
            __StrPacker = new StringPacker(this, 9);
        }

        public MsgMessageBoard(UInt16 aIndex, Channel aChannel, String[] aParams, Action aAction)
            : base((UInt16)(10 + aParams.Select(p => p.Length + 1).Sum()))
        {
            Index = aIndex;
            Channel = aChannel;
            _Action = aAction;

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
            Player player = aClient.Player;
            MessageBoard board = null;

            switch (Channel)
            {
                case Channel.MsgTrade:
                    board = World.TradeBoard;
                    break;
                case Channel.MsgFriend:
                    board = World.FriendBoard;
                    break;
                case Channel.MsgTeam:
                    board = World.TeamBoard;
                    break;
                case Channel.MsgSyn:
                    board = World.SynBoard;
                    break;
                case Channel.MsgOther:
                    board = World.OtherBoard;
                    break;
                case Channel.MsgSystem:
                    board = World.SystemBoard;
                    break;
                default:
                    break;
            }

            switch (_Action)
            {
                case Action.Del:
                    {
                        if (Params.Length != 1)
                            return;

                        String author = Params[0];
                        if (author != player.Name && !(player.IsGM || player.IsPM))
                            return;

                        MessageBoard.MessageInfo message = board.GetMsgInfoByAuthor(author);
                        board.Delete(message);
                        break;
                    }
                case Action.GetList:
                    {
                        String[] list = board.GetList(Index);
                        player.Send(new MsgMessageBoard(Index, Channel, list, Action.List));
                        break;
                    }
                case Action.GetWords:
                    {
                        if (Params.Length != 1)
                            return;

                        String author = Params[0];
                        String words = board.GetWords(author);

                        player.Send(new MsgTalk(author, player.Name, words, Channel, Color.White));
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgMessageBoard.", (Byte)_Action);
                        break;
                    }
            }
        }
    }
}
