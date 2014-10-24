// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgTeam : Msg
    {
        public const Int16 Id = _MSG_TEAM;

        public enum Action
        {
            Create = 0, //TS & TC
			ApplyJoin = 1, //TS
			Leave = 2, //TS & TC
			AcceptInvite = 3, //TS
			Invite = 4, //TS & TC
			AgreeJoin = 5, //TS
			Dismiss = 6, //TS & TC
			KickOut = 7, //TS & TC
			CloseTeam = 8, //TS & TC
			OpenTeam = 9, //TS & TC
			CloseMoneyAccess = 10, //TS
			OpenMoneyAccess = 11, //TS
			CloseItemAccess = 12, //TS
			OpenItemAccess = 13, //TS
		};
		
        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Action;
			public Int32 UniqId;
        };

        public static Byte[] Create(Int32 UniqId, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

				pMsg->Action = (Int32)Action;
                pMsg->UniqId = UniqId;

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

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Action Action = (Action)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 UniqId = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
				
				if (!World.AllPlayers.ContainsKey(UniqId))
					return;
				
				Player User = World.AllPlayers[UniqId];
				
				if (World.AllMaps.ContainsKey(User.Map))
				{
					Map Map = World.AllMaps[User.Map];
					if (Map.IsTeam_Disable())
						return;
				}
				
                switch (Action)
                {
                    case Action.Create:
                        {
                            if (User.Team != null)
                            {
                                User.SendSysMsg(Client.GetStr("STR_HAVE_TEAM"));
                                return;
                            }

                            Team Team = Team.CreateNew(User);
                            if (Team == null)
                                return;

							User.Team = Team;
							User.Send(MsgTeam.Create(User.UniqId, Action.Create));

                            User.AddFlag(Player.Flag.TeamLeader);
                            World.BroadcastRoomMsg(User, MsgUserAttrib.Create(User, User.Flags, MsgUserAttrib.Type.Flags), true);
                            break;
                        }
                    case Action.ApplyJoin:
                        {
                            Team Team = User.Team;
                            if (Team == null)
                                return;

                            if (Team.UniqId != User.UniqId)
                                return;

                            if (Team.GetMemberAmount() >= Team._MAX_TEAMAMOUNT)
                            {
                                User.SendSysMsg(Client.GetStr("STR_TEAM_FULL"));
                                return;
                            }

                            if (Client.User.Team != null)
                            {
                                User.SendSysMsg(Client.GetStr("STR_HAVE_TEAM"));
                                return;
                            }

                            if (Team.IsClosed())
                            {
                                User.SendSysMsg(Client.GetStr("STR_TEAM_CLOSED"));
                                return;
                            }

                            Team.Leader.Send(MsgTeam.Create(Client.User.UniqId, Action.ApplyJoin));
                            Client.User.SendSysMsg(Client.GetStr("STR_REQUEST_SENT"));
                            break;
                        }
                    case Action.Leave:
                        {
                            Team Team = User.Team;
                            if (Team == null)
                                return;

                            World.BroadcastTeamMsg(Team, Buffer);
                            Team.DelMember(User, false);
                            break;
                        }
                    case Action.AcceptInvite:
                        {
                            Team Team = User.Team;
                            if (Team == null)
                                return;

                            if (Team.UniqId != User.UniqId)
                                return;

                            if (Team.GetMemberAmount() >= Team._MAX_TEAMAMOUNT)
                            {
                                User.SendSysMsg(Client.GetStr("STR_TEAM_FULL"));
                                return;
                            }

                            if (Client.User.Team != null)
                            {
                                User.SendSysMsg(Client.GetStr("STR_HAVE_TEAM"));
                                return;
                            }

                            Team.AddMember(Client.User, true);
                            //Client.Send(MsgAction.Create(Team.Leader, Team.Leader.Map, (MsgAction.Action)101));
                            break;
                        }
                    case Action.Invite:
                        {
                            Team Team = Client.User.Team;
                            if (Team == null)
                                return;

                            if (Team.UniqId != Client.User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            if (Team.GetMemberAmount() >= Team._MAX_TEAMAMOUNT)
                            {
                                User.SendSysMsg(Client.GetStr("STR_TEAM_FULL"));
                                return;
                            }

                            Player Target = null;
                            if (World.AllPlayers.TryGetValue(UniqId, out Target))
                            {
                                if (Target.Team != null)
                                    return;

                                Target.Send(MsgTeam.Create(Client.User.UniqId, Action.Invite));
                            }
                            Client.User.SendSysMsg(Client.GetStr("STR_INVITATION_SENT"));
                            break;
                        }
                    case Action.AgreeJoin:
                        {
                            Team Team = Client.User.Team;
                            if (Team == null)
                                return;

                            if (Team.UniqId != Client.User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            if (Team.GetMemberAmount() >= Team._MAX_TEAMAMOUNT)
                            {
                                User.SendSysMsg(Client.GetStr("STR_TEAM_FULL"));
                                return;
                            }

                            Player Target = null;
                            if (World.AllPlayers.TryGetValue(UniqId, out Target))
                            {
                                if (Target.Team != null)
                                    return;

                                Target.Send(MsgTeam.Create(Team.UniqId, Action.AgreeJoin));
                                Team.AddMember(Target, true);
                                //Target.Send(MsgAction.Create(Team.Leader, Team.Leader.Map, (MsgAction.Action)101));
                            }
                            break;
                        }
                    case Action.Dismiss:
                        {
                            Team Team = User.Team;
                            if (Team == null)
                                return;

                            if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.Dismiss(User);
                            break;
                        }
                    case Action.KickOut:
                        {
                            Team Team = Client.User.Team;
                            if (Team == null)
                                return;

                            if (Team.UniqId != Client.User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            if (Team.GetMemberAmount() >= Team._MAX_TEAMAMOUNT)
                            {
                                User.SendSysMsg(Client.GetStr("STR_TEAM_FULL"));
                                return;
                            }

                            World.BroadcastTeamMsg(Team, Buffer);

                            Player Target = null;
                            if (World.AllPlayers.TryGetValue(UniqId, out Target))
                            {
                                if (Target.Team == null)
                                    return;

                                Team.DelMember(Target, false);
                            }
                            break;
                        }
                    case Action.CloseTeam:
                        {
					        Team Team = User.Team;
							if (Team == null)
								return;
					
							if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.Close();
                            break;
                        }	
                    case Action.OpenTeam:
                        {
					        Team Team = User.Team;
							if (Team == null)
								return;
					
							if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.Open();
                            break;
                        }	
                    case Action.CloseMoneyAccess:
                        {
					        Team Team = User.Team;
							if (Team == null)
								return;
					
							if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.CloseMoney();
                            break;
                        }	
                    case Action.OpenMoneyAccess:
                        {
					        Team Team = User.Team;
							if (Team == null)
								return;
					
							if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.OpenMoney();
                            break;
                        }		
                    case Action.CloseItemAccess:
                        {
					        Team Team = User.Team;
							if (Team == null)
								return;
					
							if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.CloseItem();
                            break;
                        }	
                    case Action.OpenItemAccess:
                        {
					        Team Team = User.Team;
							if (Team == null)
								return;
					
							if (Team.UniqId != User.UniqId)
                            {
                                User.SendSysMsg(Client.GetStr("STR_NOT_TEAM_LEADER"));
                                return;
                            }

                            Team.OpenItem();
                            break;
                        }					
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", MsgId, (Int16)Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
