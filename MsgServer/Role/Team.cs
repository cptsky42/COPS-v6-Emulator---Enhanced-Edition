// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public class Team
    {
        private const Int32 _GUIDING_TIME = 60 * 60;
        private const Int32 _RANGE_EXPSHARE = 32;
        private const Int32 _RANGE_TEAM_STATUS = 12;
        private const Int32 _MAX_TEAM_EXP_TIMES = 360;

        public const Int32 _MAX_TEAMAMOUNT = 4;

        private Int32 m_UniqId = -1;
        private Player m_Leader = null;
        private Boolean m_JoinForbidden;
        private Boolean m_MoneyForbidden;
        private Boolean m_ItemForbidden;

        public Player[] Members;

        public Int32 UniqId { get { return m_UniqId; } }
        public Player Leader { get { return m_Leader; } }
        public Boolean MoneyForbidden { get { return m_MoneyForbidden; } }
        public Boolean ItemForbidden { get { return m_ItemForbidden; } }

        public Team()
        {
            m_JoinForbidden = false;
            m_MoneyForbidden = false;
            m_ItemForbidden = true;

            Members = new Player[_MAX_TEAMAMOUNT];
            for (Int32 i = 0; i < Members.Length; i++)
                Members[i] = null;
        }

        ~Team()
        {
            m_Leader = null;
            Members = null;
        }

        public static Team CreateNew(Player Leader)
        {
            Team Team = new Team();
            if (Team == null)
                return null;

            if (!Team.Create(Leader))
            {
                Team = null;
                return null;
            }

            return Team;
        }

        public Boolean Create(Player Leader)
        {
            if (Leader == null)
                return false;

            m_UniqId = Leader.UniqId;
            m_Leader = Leader;
            return true;
        }

        public void LeaderXY()
        {
            if (Leader != null)
            {
                lock (Members)
                {
                    foreach (Player Member in Members)
                    {
                        if (Member == null)
                            continue;

                        if (Member.Map != Leader.Map)
                            continue;

                        Member.Send(MsgAction.Create(Leader, 0, (MsgAction.Action)101));
                    }
                }
            }
        }

        public Boolean AddMember(Player Player, Boolean Send)
        {
            if (IsClosed())
                return false;

            Int32 OldAmount = GetMemberAmount();
            if (OldAmount >= _MAX_TEAMAMOUNT)
                return false;

            if (Player == null)
                return false;

            if (IsTeamMember(Player))
                return false;

            for (Int32 i = 0; i < Members.Length; i++)
            {
                if (Members[i] == null)
                {
                    Members[i] = Player;
                    break;
                }
            }
            Player.Team = this;

            if (Send)
            {
                Leader.Send(MsgTeamMember.Create(Player, MsgTeamMember.Action.AddMember));
                Player.Send(MsgTeamMember.Create(Leader, MsgTeamMember.Action.AddMember));
                for (Int32 i = 0; i < Members.Length; i++)
                {
                    Player Member = Members[i];

                    if (Member == null)
                        continue;

                    if (Player.UniqId == Member.UniqId)
                        continue;

                    Member.Send(MsgTeamMember.Create(Player, MsgTeamMember.Action.AddMember));
                    Player.Send(MsgTeamMember.Create(Member, MsgTeamMember.Action.AddMember));
                }
                Player.Send(MsgTeamMember.Create(Player, MsgTeamMember.Action.AddMember));
            }
            return true;
        }

        public Boolean DelMember(Player Member, Boolean Send)
        {
            if (Member == null)
                return false;

            if (!IsTeamMember(Member))
                return false;

            for (Int32 i = 0; i < Members.Length; i++)
            {
                if (Members[i] == null)
                    continue;

                if (Member.UniqId == Members[i].UniqId)
                    Members[i] = null;
            }
            Member.Team = null;

            if (Send)
            {
                Leader.Send(MsgTeam.Create(Member.UniqId, MsgTeam.Action.Leave));
                Member.Send(MsgTeam.Create(Leader.UniqId, MsgTeam.Action.Leave));
                for (Int32 i = 0; i < Members.Length; i++)
                {
                    if (Members[i] == null)
                        continue;

                    if (Member.UniqId == Members[i].UniqId)
                        continue;

                    Members[i].Send(MsgTeam.Create(Member.UniqId, MsgTeam.Action.Leave));
                    Member.Send(MsgTeam.Create(Members[i].UniqId, MsgTeam.Action.Leave));
                }
            }
            return true;
        }

        public void Dismiss(Player Leader)
        {
            if (Leader == null)
                return;

            if (Leader.UniqId != m_UniqId)
                return;

            foreach (Player Member in Members)
            {
                if (Member == null)
                    continue;

                if (Member.UniqId == Leader.UniqId)
                    continue;

                Member.Send(MsgTeam.Create(m_UniqId, MsgTeam.Action.Dismiss));
                Member.Team = null;
            }

            Leader.Send(MsgTeam.Create(m_UniqId, MsgTeam.Action.Dismiss));
            Leader.DelFlag(Player.Flag.TeamLeader);
            World.BroadcastRoomMsg(Leader, MsgUserAttrib.Create(Leader, Leader.Flags, MsgUserAttrib.Type.Flags), true);
            Leader.Team = null;
        }

        public Int32 GetMemberByIndex(Int32 Index)
        {
            if (Index >= GetMemberAmount() || Index < 0)
                return -1;

            return Members[Index].UniqId;
        }

        public Player GetLeader() { return m_Leader; }

        public Int32 GetMemberAmount()
        {
            Int32 Count = 0;
            foreach (Player Member in Members)
            {
                if (Member != null)
                    Count++;
            }
            return Math.Min(Count, _MAX_TEAMAMOUNT);
        }

        public void Open() { m_JoinForbidden = false; }
        public void Close() { m_JoinForbidden = true; }
        public Boolean IsClosed() { return m_JoinForbidden; }

        public void OpenMoney() { m_MoneyForbidden = false; }
        public void CloseMoney() { m_MoneyForbidden = true; }

        public void OpenItem() { m_ItemForbidden = false; }
        public void CloseItem() { m_ItemForbidden = true; }

        public Boolean IsTeamWithNewbie(Monster Target)
        {
            if (Target == null)
                return false;

            Int32 MonsterLvl = Target.Level;

            if (GetLeader() != null)
            {
                Player Leader = GetLeader();
                if (!Leader.IsAlive())
                    goto Members;

                if (Leader.Map != Target.Map)
                    goto Members;

                if (Math.Abs(Leader.X - Target.X) > _RANGE_EXPSHARE ||
                    Math.Abs(Leader.Y - Target.Y) > _RANGE_EXPSHARE)
                    goto Members; // out of share range

                if (Leader.Level + 20 < MonsterLvl)
                    return true;
            }

        Members:
            for (Int32 i = 0; i < GetMemberAmount(); i++)
            {
                Player Member = Members[i];
                if (Member == null)
                    continue;

                if (!Member.IsAlive())
                    continue;

                if (Member.Map != Target.Map)
                    continue;

                if (Math.Abs(Member.X - Target.X) > _RANGE_EXPSHARE ||
                    Math.Abs(Member.Y - Target.Y) > _RANGE_EXPSHARE)
                    continue; // out of share range

                if (Member.Level + 20 < MonsterLvl)
                    return true;
            }
            return false;
        }

        public void AwardMemberExp(Player Killer, Monster Target, Int32 Exp)
        {
            if (!IsTeamMember(Killer.UniqId))
                return;

            Int32 MonsterLvl = Target.Level;

            List<Player> ValidMembers = new List<Player>();
            if (Killer.UniqId == Leader.UniqId || Killer.Map != Leader.Map)
                goto Members;

            if (!Leader.IsAlive())
                goto Members;

            if (Math.Abs(Leader.X - Target.X) > _RANGE_EXPSHARE ||
                Math.Abs(Leader.Y - Target.Y) > _RANGE_EXPSHARE)
                goto Members; // out of share range

            ValidMembers.Add(Leader);

        Members:
            for (Int32 i = 0; i < Members.Length; i++)
            {
                if (Members[i] == null)
                    continue;

                if (Killer.UniqId == Members[i].UniqId || Killer.Map != Members[i].Map)
                    continue;

                if (!Members[i].IsAlive())
                    continue;

                if (Math.Abs(Members[i].X - Target.X) > _RANGE_EXPSHARE ||
                    Math.Abs(Members[i].Y - Target.Y) > _RANGE_EXPSHARE)
                    continue; // out of share range

                ValidMembers.Add(Members[i]);
            }

            foreach (Player Member in ValidMembers)
            {
                Int32 AddExp = Battle.AdjustExp(Exp, Member, Target);

                //Max Exp
                if (AddExp > Member.Level * _MAX_TEAM_EXP_TIMES)
                    AddExp = Member.Level * _MAX_TEAM_EXP_TIMES;

                //Double Exp
                if (Member.Spouse == Killer.Name)
                    AddExp *= 2;

                if (IsTeamWithNewbie(Target))
                    AddExp *= 2;

                if (Member.Profession >= 133 && Member.Profession <= 135)
                    AddExp *= 2;

                Member.SendSysMsg(String.Format(STR.Get("STR_TEAM_EXPERIENCE"), Member.AddExp(AddExp, true)));
            }
            ValidMembers.Clear();
        }

        public Boolean IsTeamMember(Player Player)
        {
            if (Player.UniqId == m_Leader.UniqId)
                return true;

            for (Int32 i = 0; i < GetMemberAmount(); i++)
            {
                if (GetMemberByIndex(i) == Player.UniqId)
                    return true;
            }
            return false;
        }

        public Boolean IsTeamMember(Int32 UniqId)
        {
            if (UniqId == m_Leader.UniqId)
                return true;

            for (Int32 i = 0; i < GetMemberAmount(); i++)
            {
                if (GetMemberByIndex(i) == UniqId)
                    return true;
            }
            return false;
        }
    }
}
