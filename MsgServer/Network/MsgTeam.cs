// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public class MsgTeam : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_TEAM; } }

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

        //--------------- Internal Members ---------------
        private Action __Action = 0;
        private Int32 __Id = 0;
        //------------------------------------------------

        /// <summary>
        /// Action Id.
        /// </summary>
        public Action _Action
        {
            get { return __Action; }
            set { __Action = value; WriteUInt32(4, (UInt32)value); }
        }

        public Int32 Id
        {
            get { return __Id; }
            set { __Id = value; WriteInt32(8, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgTeam(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Action = (Action)BitConverter.ToUInt32(mBuf, 4);
            __Id = BitConverter.ToInt32(mBuf, 8);
        }

        public MsgTeam(Player aPlayer, Action aAction)
            : base(12)
        {
            _Action = aAction;
            Id = aPlayer.UniqId;
        }

        public MsgTeam(Team aTeam, Action aAction)
            : base(12)
        {
            _Action = aAction;
            Id = aTeam.UniqId;
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            if (!World.AllPlayers.ContainsKey(Id))
                return;

            Player player = World.AllPlayers[Id];

            if (player.Map.IsTeam_Disable())
                return;

            switch (_Action)
            {
                case Action.Create:
                    {
                        if (player.Team != null)
                        {
                            player.SendSysMsg(StrRes.STR_HAVE_TEAM);
                            return;
                        }

                        Team team = Team.CreateNew(player);
                        if (team == null)
                            return;

                        player.Team = team;
                        player.Send(new MsgTeam(player, Action.Create));
                        player.AttachStatus(Status.TeamLeader);
                        break;
                    }
                case Action.ApplyJoin:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                            return;

                        if (team.GetMemberAmount() >= Team.MAX_TEAM_AMOUNT)
                        {
                            player.SendSysMsg(StrRes.STR_TEAM_FULL);
                            return;
                        }

                        if (aClient.Player.Team != null)
                        {
                            player.SendSysMsg(StrRes.STR_HAVE_TEAM);
                            return;
                        }

                        if (team.IsClosed())
                        {
                            player.SendSysMsg(StrRes.STR_TEAM_CLOSED);
                            return;
                        }

                        team.Leader.Send(new MsgTeam(aClient.Player, Action.ApplyJoin));
                        aClient.Player.SendSysMsg(StrRes.STR_REQUEST_SENT);
                        break;
                    }
                case Action.Leave:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        team.BroadcastMsg(this);
                        team.DelMember(player, false);
                        break;
                    }
                case Action.AcceptInvite:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                            return;

                        if (team.GetMemberAmount() >= Team.MAX_TEAM_AMOUNT)
                        {
                            player.SendSysMsg(StrRes.STR_TEAM_FULL);
                            return;
                        }

                        if (aClient.Player.Team != null)
                        {
                            player.SendSysMsg(StrRes.STR_HAVE_TEAM);
                            return;
                        }

                        team.AddMember(aClient.Player, true);
                        //Client.Send(new MsgAction(Team.Leader, Team.Leader.Map, (MsgAction.Action)101));
                        break;
                    }
                case Action.Invite:
                    {
                        Team team = aClient.Player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != aClient.Player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        if (team.GetMemberAmount() >= Team.MAX_TEAM_AMOUNT)
                        {
                            player.SendSysMsg(StrRes.STR_TEAM_FULL);
                            return;
                        }

                        Player target = null;
                        if (World.AllPlayers.TryGetValue(Id, out target))
                        {
                            if (target.Team != null)
                                return;

                            target.Send(new MsgTeam(aClient.Player, Action.Invite));
                        }
                        aClient.Player.SendSysMsg(StrRes.STR_INVITATION_SENT);
                        break;
                    }
                case Action.AgreeJoin:
                    {
                        Team team = aClient.Player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != aClient.Player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        if (team.GetMemberAmount() >= Team.MAX_TEAM_AMOUNT)
                        {
                            player.SendSysMsg(StrRes.STR_TEAM_FULL);
                            return;
                        }

                        Player target = null;
                        if (World.AllPlayers.TryGetValue(Id, out target))
                        {
                            if (target.Team != null)
                                return;

                            target.Send(new MsgTeam(team, Action.AgreeJoin));
                            team.AddMember(target, true);
                            //Target.Send(new MsgAction(Team.Leader, Team.Leader.Map, (MsgAction.Action)101));
                        }
                        break;
                    }
                case Action.Dismiss:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.Dismiss(player);
                        break;
                    }
                case Action.KickOut:
                    {
                        Team team = aClient.Player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != aClient.Player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        if (team.GetMemberAmount() >= Team.MAX_TEAM_AMOUNT)
                        {
                            player.SendSysMsg(StrRes.STR_TEAM_FULL);
                            return;
                        }

                        team.BroadcastMsg(this);

                        Player target = null;
                        if (World.AllPlayers.TryGetValue(Id, out target))
                        {
                            if (target.Team == null)
                                return;

                            team.DelMember(target, false);
                        }
                        break;
                    }
                case Action.CloseTeam:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.Close();
                        break;
                    }
                case Action.OpenTeam:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.Open();
                        break;
                    }
                case Action.CloseMoneyAccess:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.CloseMoney();
                        break;
                    }
                case Action.OpenMoneyAccess:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.OpenMoney();
                        break;
                    }
                case Action.CloseItemAccess:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.CloseItem();
                        break;
                    }
                case Action.OpenItemAccess:
                    {
                        Team team = player.Team;
                        if (team == null)
                            return;

                        if (team.UniqId != player.UniqId)
                        {
                            player.SendSysMsg(StrRes.STR_NOT_TEAM_LEADER);
                            return;
                        }

                        team.OpenItem();
                        break;
                    }
                default:
                    {
                        sLogger.Error("Action {0} is not implemented for MsgTeam.", (UInt16)_Action);
                        break;
                    }
            }
        }
    }
}
