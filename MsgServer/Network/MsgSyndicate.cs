// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgSyndicate : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_SYNDICATE; } }

        public enum Action
        {
            None = 0,
            ApplyJoin = 1,			    // ÉêÇë¼ÓÈëºÚÉç»á, id
            InviteJoin = 2,			    // ÑûÇë¼ÓÈëºÚÉç»á, id
            LeaveSyn = 3,			    // ÍÑÀëºÚÉç»á
            KickOut_Member = 4,			// ¿ª³ýºÚÉç»á³ÉÔ±, name
            QuerySynName = 6,			// ²éÑ¯°ïÅÉÃû×Ö
            SetAlly = 7,			    // ½áÃË				// to client, npc(npc_id is syn_id, same follow)
            ClearAlly = 8,		    	// ½â³ý½áÃË			// to client, npc
            SetAntagonize = 9,			// Ê÷µÐ				// to client, npc
            ClearAntagonize = 10,		// ½â³ýÊ÷µÐ			// to client, npc
            DonateMoney = 11,			// °ïÖÚ¾èÇ®
            QuerySynAttr = 12,			// ²éÑ¯°ïÅÉÐÅÏ¢		// to server
            SetSyn = 14,		    	// Ìí¼Ó°ïÅÉID		// to client
            UniteSubSyn = 15,			// ºÏ²¢ÌÃ¿Ú // to client // dwData±»ºÏ²¢, targetÊÇÖ÷ÈË
            UniteSyn = 16,			    // ºÏ²¢°ïÅÉ // to client // dwData±»ºÏ²¢, targetÊÇÖ÷ÈË
            SetWhiteSyn = 17,			// °×°ï°ïÅÉID // Î´±»Õ¼ÁìÇë·¢ID_NONE
            SetBlackSyn = 18,			// ºÚ°ï°ïÅÉID // Î´±»Õ¼ÁìÇë·¢ID_NONE
            DestroySyn = 19,			// ÊÀ½ç¹ã²¥£¬É¾³ý°ïÅÉ¡£
            SetMantle = 20,            // ÊÀ½ç¹ã²¥£¬Åû·ç // add huang 2004.1.1       

            //_APPLY_ALLY = 21,			// ÉêÇë½áÃË			// to server&client, idTarget=SynLeaderID
            //_CLEAR_ALLY = 22,			// Çå³ý½áÃË			// to server

            //_SET_ANTAGONIZE = 23,			//Ê÷µÐ client to server
            //_CLEAR_ANTAGONIZE = 24,			//½â³ýÊ÷µÐ client to server

            //NPCMSG_CREATE_SYN = 101,			// Í¨ÖªNPC·þÎñÆ÷Ìí¼Ó°ïÅÉ	// to npc
            //NPCMSG_DESTROY_SYN = 102,			// Í¨ÖªNPC·þÎñÆ÷É¾³ý°ïÅÉ	// to npc
            //KICKOUT_MEMBER_INFO_QUERY = 110,	//°ïÖ÷²éÑ¯ÉêÇë¿ª³ýµÄ³ÉÔ±
            //KICKOUT_MEMBER_AGREE = 111,	//°ïÖ÷Í¬Òâ¿ª³ý»áÔ±
            //KICKOUT_MEMBER_NOAGREE = 112,	//°ïÖ÷²»Í¬Òâ¿ª³ý»áÔ±
            //SYNMEMBER_ASSIGN = 113,			//°ïÅÉ³ÉÔ±±àÖÆ	
            //SYN_CHANGE_NAME = 114,			// °ïÅÉ¸ÄÃû
            //SYN_CHANGE_SUBNAME = 114,		//·ÖÍÅ¸ÄÃû
            //SYN_CHANGE_SUBSUBNAME = 115,		//·Ö¶Ó¸ÄÃû
            //SYN_DEMISE = 116,		//ìøÈÃ
            //SYN_SET_ASSISTANT = 117,		//ÉèÖÃ¸±°ïÖ÷
            //SYN_SET_TEAMLEADER = 118,		//ÉèÖÃ°ïÅÉ¶Ó³¤
            //SYN_SET_PUBLISHTIME = 119,		//ÉèÖÃ¹«¸æÊ±¼ä
        };

        //--------------- Internal Members ---------------
        private Action __Action = (Action)0;
        private UInt32 __Data = 0;
        //------------------------------------------------

        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt32(4, (UInt32)value); }
        }

        public UInt32 Data
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(8, value); }
        }

        public UInt32 TargetId
        {
            get { return __Data; }
            set { __Data = value; WriteUInt32(8, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgSyndicate(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Action = (Action)BitConverter.ToUInt32(mBuf, 4);
            __Data = BitConverter.ToUInt32(mBuf, 8);
        }

        public MsgSyndicate(UInt32 aData, Action aAction)
            : base(12)
        {
            _Action = aAction;
            Data = aData;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            Player player = aClient.Player;

            switch (_Action)
            {
                case Action.ApplyJoin:
                    {
                        if (player.Syndicate != null)
                            return;

                        Player target = null;
                        if (!World.AllPlayers.TryGetValue((Int32)TargetId, out target))
                            return;

                        if (target.Syndicate == null)
                            return;

                        Syndicate.Member member = target.Syndicate.GetMemberInfo((Int32)TargetId);
                        if (member.Rank != Syndicate.Rank.Leader && member.Rank != Syndicate.Rank.SubLeader &&
                            member.Rank != Syndicate.Rank.BranchMgr && member.Rank != Syndicate.Rank.InternMgr && member.Rank != Syndicate.Rank.DeputyMgr)
                            return;

                        TargetId = (UInt32)player.UniqId;
                        target.Send(this);
                        break;
                    }
                case Action.InviteJoin:
                    {
                        if (player.Syndicate == null)
                            return;

                        Player target = null;
                        if (!World.AllPlayers.TryGetValue((Int32)TargetId, out target))
                            return;

                        if (target.Syndicate != null)
                            return;

                        Syndicate.Member member = player.Syndicate.GetMemberInfo(player.UniqId);
                        if (member.Rank != Syndicate.Rank.Leader && member.Rank != Syndicate.Rank.SubLeader &&
                            member.Rank != Syndicate.Rank.BranchMgr && member.Rank != Syndicate.Rank.InternMgr && member.Rank != Syndicate.Rank.DeputyMgr)
                            return;

                        player.Syndicate.AddMember(target);
                        break;
                    }
                case Action.LeaveSyn:
                    {
                        if (player.Syndicate == null)
                            return;

                        Syndicate syn = player.Syndicate;
                        Syndicate.Member member = syn.GetMemberInfo(player.UniqId);

                        if (member.Id == syn.Leader.Id)
                        {
                            player.SendSysMsg(StrRes.STR_NO_DISBAND);
                            return;
                        }

                        if (member.Proffer < 20000)
                        {
                            player.SendSysMsg(StrRes.STR_SYN_NOTENOUGH_DONATION_LEAVE);
                            return;
                        }

                        syn.LeaveSyn(player, false, true);
                        World.BroadcastSynMsg(syn, new MsgTalk("SYSTEM", "ALLUSERS", String.Format(StrRes.STR_SYN_LEAVE, player.Name), Channel.Syndicate, Color.White));
                        break;
                    }
                case Action.QuerySynName:
                    {
                        Syndicate syn = null;
                        if (!World.AllSyndicates.TryGetValue((UInt16)Data, out syn))
                            return;

                        if (syn.FealtySynUID == 0)
                            player.Send(new MsgName(Data, syn.Name, MsgName.NameAct.Syndicate));
                        else
                        {
                            String[] names = new String[2];
                            names[0] = syn.GetMasterSyn().Name;
                            names[1] = syn.Name;
                            player.Send(new MsgName((UInt32)Data, names, MsgName.NameAct.Syndicate));
                        }
                        break;
                    }
                case Action.DonateMoney:
                    {
                        Syndicate syn = player.Syndicate;
                        if (syn == null)
                            return;

                        if (syn.Donate(player, Data))
                            player.Send(new MsgSynAttrInfo(player.UniqId, syn));
                        break;
                    }
                case Action.QuerySynAttr:
                    {
                        Player target = null;
                        if (!World.AllPlayers.TryGetValue((Int32)TargetId, out target))
                            return;

                        Syndicate.SynchroInfo(target, true);
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgSyndicate.", (UInt32)_Action);
                        break;
                    }
            }
        }
    }
}
