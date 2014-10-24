// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgSyndicate : Msg
    {
        public const Int16 Id = _MSG_SYNDICATE;

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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Action;
            public Int32 Param;
        };

        public static Byte[] Create(Int32 Param, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Action = (Int32)Action;
                pMsg->Param = Param;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                MsgInfo* pMsg = null;
                fixed (Byte* pBuf = Buffer)
                    pMsg = (MsgInfo*)pBuf;

                Player Player = Client.User;

                switch ((Action)pMsg->Action)
                {
                    case Action.ApplyJoin:
                        {
                            if (Player.Syndicate != null)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(pMsg->Param, out Target))
                                return;

                            if (Target.Syndicate == null)
                                return;

                            Syndicate.Member Member = Target.Syndicate.GetMemberInfo(pMsg->Param);
                            if (!(Member.Rank == 100 || Member.Rank == 90))
                                return;

                            pMsg->Param = Player.UniqId;
                            Target.Send(Buffer);
                            break;
                        }
                    case Action.InviteJoin:
                        {
                            if (Player.Syndicate == null)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(pMsg->Param, out Target))
                                return;

                            if (Target.Syndicate != null)
                                return;

                            Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Player.UniqId);
                            if (!(Member.Rank == 100 || Member.Rank == 90))
                                return;

                            Player.Syndicate.AddMember(Target);
                            break;
                        }
                    case Action.LeaveSyn:
                        {
                            if (Player.Syndicate == null)
                                return;

                            Syndicate.Info Syn = Player.Syndicate;
                            Syndicate.Member Member = Syn.GetMemberInfo(Player.UniqId);

                            if (Member.UniqId == Syn.Leader.UniqId)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_NO_DISBAND"));
                                return;
                            }

                            if (Member.Donation < 20000)
                            {
                                Player.SendSysMsg(Client.GetStr("STR_SYN_NOTENOUGH_DONATION_LEAVE"));
                                return;
                            }

                            Syn.DelMember(Member, false);
                            World.BroadcastSynMsg(Syn, MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(Client.GetStr("STR_SYN_LEAVE"), Player.Name), MsgTalk.Channel.Syndicate, 0xFFFFFF));
                            break;
                        }
                    case Action.QuerySynName:
                        {
                            Syndicate.Info Syn = null;
                            if (!World.AllSyndicates.TryGetValue((Int16)pMsg->Param, out Syn))
                                return;

                            if (Syn.GetMasterSyn() == null)
                                Player.Send(MsgName.Create(pMsg->Param, Syn.Name, MsgName.Action.Syndicate));
                            else
                            {
                                String[] Names = new String[2];
                                Names[0] = Syn.GetMasterSyn().Name;
                                Names[1] = Syn.Name;
                                Player.Send(MsgName.Create(pMsg->Param, Names, MsgName.Action.Syndicate));
                            }
                            break;
                        }
                    case Action.DonateMoney:
                        {
                            Syndicate.Info Syn = Player.Syndicate;
                            if (Syn == null)
                                return;

                            if (Syn.DonateMoney(Player, pMsg->Param))
                                Player.Send(MsgSynAttrInfo.Create(Player.UniqId, Syn));
                            break;
                        }
                    case Action.QuerySynAttr:
                        {
                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(pMsg->Param, out Target))
                                return;

                            if (Target.Syndicate == null)
                                return;

                            Player.Send(MsgSynAttrInfo.Create(pMsg->Param, Target.Syndicate));
                            Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Target.Syndicate.Announce, MsgTalk.Channel.SynAnnounce, 0x000000));
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", pMsg->Header.Type, pMsg->Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
