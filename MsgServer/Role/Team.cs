// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011, 2014
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public class Team
    {
        private const Int32 GUIDING_TIME = 60 * 60;
        private const Int32 RANGE_EXP_SHARE = 32;
        private const Int32 RANGE_TEAM_STATUS = 12;
        private const Int32 MAX_TEAM_EXP_TIMES = 360;

        public const Int32 MAX_TEAM_AMOUNT = 4;

        private readonly Int32 mUID;
        private readonly Player mLeader;
        private Boolean mJoinForbidden;
        private Boolean mMoneyForbidden;
        private Boolean mItemForbidden;

        public readonly Player[] Members = new Player[MAX_TEAM_AMOUNT];

        public Int32 UniqId { get { return mUID; } }
        public Player Leader { get { return mLeader; } }
        public Boolean MoneyForbidden { get { return mMoneyForbidden; } }
        public Boolean ItemForbidden { get { return mItemForbidden; } }

        private Team(Player aLeader)
        {
            mUID = aLeader.UniqId;
            mLeader = aLeader;

            mJoinForbidden = false;
            mMoneyForbidden = false;
            mItemForbidden = true;

            for (Int32 i = 0; i < Members.Length; i++)
                Members[i] = null;
        }

        public static Team CreateNew(Player aLeader)
        {
            return new Team(aLeader);
        }

        public void LeaderPosition()
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

                        Member.Send(new MsgAction(Leader, 0, (MsgAction.Action)101));
                    }
                }
            }
        }

        public Boolean AddMember(Player Player, Boolean Send)
        {
            if (IsClosed())
                return false;

            Int32 OldAmount = GetMemberAmount();
            if (OldAmount >= MAX_TEAM_AMOUNT)
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
                Leader.Send(new MsgTeamMember(Player, MsgTeamMember.Action.AddMember));
                Player.Send(new MsgTeamMember(Leader, MsgTeamMember.Action.AddMember));
                for (Int32 i = 0; i < Members.Length; i++)
                {
                    Player Member = Members[i];

                    if (Member == null)
                        continue;

                    if (Player.UniqId == Member.UniqId)
                        continue;

                    Member.Send(new MsgTeamMember(Player, MsgTeamMember.Action.AddMember));
                    Player.Send(new MsgTeamMember(Member, MsgTeamMember.Action.AddMember));
                }
                Player.Send(new MsgTeamMember(Player, MsgTeamMember.Action.AddMember));
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
                Leader.Send(new MsgTeam(Member, MsgTeam.Action.Leave));
                Member.Send(new MsgTeam(Leader, MsgTeam.Action.Leave));
                for (Int32 i = 0; i < Members.Length; i++)
                {
                    if (Members[i] == null)
                        continue;

                    if (Member.UniqId == Members[i].UniqId)
                        continue;

                    Members[i].Send(new MsgTeam(Member, MsgTeam.Action.Leave));
                    Member.Send(new MsgTeam(Members[i], MsgTeam.Action.Leave));
                }
            }
            return true;
        }

        public void Dismiss(Player Leader)
        {
            if (Leader == null)
                return;

            if (Leader.UniqId != mUID)
                return;

            foreach (Player Member in Members)
            {
                if (Member == null)
                    continue;

                if (Member.UniqId == Leader.UniqId)
                    continue;

                Member.Send(new MsgTeam(this, MsgTeam.Action.Dismiss));
                Member.Team = null;
            }

            Leader.Send(new MsgTeam(this, MsgTeam.Action.Dismiss));
            Leader.DetachStatus(Status.TeamLeader);
            Leader.Team = null;
        }

        public Int32 GetMemberByIndex(Int32 Index)
        {
            if (Index >= GetMemberAmount() || Index < 0)
                return -1;

            return Members[Index].UniqId;
        }

        public Player GetLeader() { return mLeader; }

        public Int32 GetMemberAmount()
        {
            Int32 Count = 0;
            foreach (Player Member in Members)
            {
                if (Member != null)
                    Count++;
            }
            return Math.Min(Count, MAX_TEAM_AMOUNT);
        }

        public void Open() { mJoinForbidden = false; }
        public void Close() { mJoinForbidden = true; }
        public Boolean IsClosed() { return mJoinForbidden; }

        public void OpenMoney() { mMoneyForbidden = false; }
        public void CloseMoney() { mMoneyForbidden = true; }

        public void OpenItem() { mItemForbidden = false; }
        public void CloseItem() { mItemForbidden = true; }

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

                if (Math.Abs(Leader.X - Target.X) > RANGE_EXP_SHARE ||
                    Math.Abs(Leader.Y - Target.Y) > RANGE_EXP_SHARE)
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

                if (Math.Abs(Member.X - Target.X) > RANGE_EXP_SHARE ||
                    Math.Abs(Member.Y - Target.Y) > RANGE_EXP_SHARE)
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

            if (Math.Abs(Leader.X - Target.X) > RANGE_EXP_SHARE ||
                Math.Abs(Leader.Y - Target.Y) > RANGE_EXP_SHARE)
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

                if (Math.Abs(Members[i].X - Target.X) > RANGE_EXP_SHARE ||
                    Math.Abs(Members[i].Y - Target.Y) > RANGE_EXP_SHARE)
                    continue; // out of share range

                ValidMembers.Add(Members[i]);
            }

            foreach (Player Member in ValidMembers)
            {
                UInt32 AddExp = Battle.AdjustExp(Exp, Member, Target);

                //Max Exp
                if (AddExp > Member.Level * MAX_TEAM_EXP_TIMES)
                    AddExp = (UInt32)(Member.Level * MAX_TEAM_EXP_TIMES);

                //Double Exp
                if (Member.Mate == Killer.Name)
                    AddExp *= 2;

                if (IsTeamWithNewbie(Target))
                    AddExp *= 2;

                if (Member.Profession >= 133 && Member.Profession <= 135)
                    AddExp *= 2;

                Member.SendSysMsg(StrRes.STR_TEAM_EXPERIENCE, Member.AddExp(AddExp, true));
            }
            ValidMembers.Clear();
        }

        public Boolean IsTeamMember(Player Player)
        {
            if (Player.UniqId == mLeader.UniqId)
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
            if (UniqId == mLeader.UniqId)
                return true;

            for (Int32 i = 0; i < GetMemberAmount(); i++)
            {
                if (GetMemberByIndex(i) == UniqId)
                    return true;
            }
            return false;
        }

        public void BroadcastMsg(Msg aMsg)
        {
            if (mLeader != null)
                mLeader.Send(aMsg);

            foreach (Player member in Members)
            {
                if (member == null)
                    continue;

                member.Send(aMsg);
            }
        }
    }
}
