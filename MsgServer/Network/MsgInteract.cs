// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer.Network
{
    public unsafe class MsgInteract : Msg
    {
        public const Int16 Id = _MSG_INTERACT;

        public enum Action
        {
            None = 0,
            Steal = 1, //Works?
            Attack = 2, //Attack Miss -> Param = 0
            Heal = 3, //Works?
            Poison = 4, //Works?
            Assassinate = 5, //Works?
            Freeze = 6, //Works?
            Unfreeze = 7, //Works?
            Court = 8,
            Marry = 9,
            Divorce = 10, //Works?
            PresentMoney = 11, //Works?
            PresentItem = 12, //Works?
            SendFlowers = 13,
            Kill = 14,
            JoinSyn = 15, //Works?
            AcceptSynMember = 16, //Works?
            KickOutSynMember = 17, //Works?
            PresentPower = 18, //Works?
            QueryInfo = 19, //Works?
            RushAtk = 20, //Works?
            MagicAttack = 21,
            AbortMagic = 22,
            ReflectWeapon = 23,
            Bump = 24, //Dash
            Shoot = 25,
            ReflectMagic = 26,			
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Timestamp;
            public Int32 SenderUID;
            public Int32 TargetUID;
            public UInt16 TargetX;
            public UInt16 TargetY;
            public Int32 Action;
            public Int32 Param; //{ Data } || { MagicType, MagicLevel }
            public Int32 Unknow;
        };

        public static Byte[] Create(Entity Sender, Entity Target, Int32 Param, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Timestamp = Environment.TickCount;
                pMsg->SenderUID = Sender.UniqId;
                if (Target != null)
                {
                    pMsg->TargetUID = Target.UniqId;
                    pMsg->TargetX = Target.X;
                    pMsg->TargetY = Target.Y;
                }
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

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 Timestamp = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 SenderUID = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Int32 TargetUID = (Int32)((Buffer[0x0F] << 24) + (Buffer[0x0E] << 16) + (Buffer[0x0D] << 8) + Buffer[0x0C]);
                UInt16 TargetX = (UInt16)((Buffer[0x11] << 8) + Buffer[0x10]);
                UInt16 TargetY = (UInt16)((Buffer[0x13] << 8) + Buffer[0x12]);
                Action Action = (Action)((Buffer[0x17] << 24) + (Buffer[0x16] << 16) + (Buffer[0x15] << 8) + Buffer[0x14]);
                Int32 Param = (Int32)((Buffer[0x1B] << 24) + (Buffer[0x1A] << 16) + (Buffer[0x19] << 8) + Buffer[0x18]);

                Int32 Interval = 0;
                Player Player = Client.User;
                Interval = Timestamp - Player.LastAttackTick;
                Player.LastAttackTick = Timestamp;
                Player.MagicIntone = false;

                if (Interval / 100 == Player.PrevAtkInterval / 100)
                    Player.BotCheck++;
                else
                {
                    Player.PrevAtkInterval = Interval;
                    Player.BotCheck = 0;
                }

                switch (Action)
                {
                    case Action.Attack:
                        {
                            Player.Action = (Int16)MsgAction.Emotion.StandBy;
                            if (Player.UniqId != SenderUID)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, TargetX, TargetY, Player.AtkRange))
                                return;

                            Player.TargetUID = TargetUID;
                            Player.AtkType = (Int32)Action;
                            Player.IsInBattle = true;
                            Player.MagicType = -1;
                            Player.MagicLevel = 0;

                            if (Entity.IsPlayer(TargetUID))
                            {
                                Player Target = null;
                                if (!World.AllPlayers.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, Player.AtkRange))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.PvP(Player, Target);
                            }
                            else if (Entity.IsMonster(TargetUID))
                            {
                                Monster Target = null;
                                if (!World.AllMonsters.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, Player.AtkRange))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.PvM(Player, Target);
                            }
                            else if (Entity.IsTerrainNPC(TargetUID))
                            {
                                TerrainNPC Target = null;
                                if (!World.AllTerrainNPCs.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, Player.AtkRange))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.PvE(Player, Target);
                            }
                            else
                            {
                                Player.IsInBattle = false;
                                return;
                            }
                            break;
                        }
                    case Action.Court:
                        {
                            if (Player.UniqId != SenderUID)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(TargetUID, out Target))
                                return;

                            if (Target.Map != Player.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, 17))
                                return;

                            if (!(Player.Look % 10 < 3 && Target.Look % 10 > 2) && !(Player.Look % 10 > 2 && Target.Look % 10 < 3))
                                return;

                            if (Target.Spouse != "Non" && Target.Spouse != "None" && Target.Spouse != "")
                                return;

                            Target.Send(Buffer);
                            break;
                        }
                    case Action.Marry:
                        {
                            if (Player.UniqId != SenderUID)
                                return;

                            Player Target = null;
                            if (!World.AllPlayers.TryGetValue(TargetUID, out Target))
                                return;

                            if (Target.Map != Player.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, 17))
                                return;

                            if (!(Player.Look % 10 < 3 && Target.Look % 10 > 2) && !(Player.Look % 10 > 2 && Target.Look % 10 < 3))
                                return;

                            if (Target.Spouse != "Non" && Target.Spouse != "None" && Target.Spouse != "")
                                return;

                            Player.Spouse = Target.Name;
                            Player.Send(MsgName.Create(Player.UniqId, Player.Spouse, MsgName.Action.Spouse));

                            Target.Spouse = Player.Name;
                            Target.Send(MsgName.Create(Target.UniqId, Target.Spouse, MsgName.Action.Spouse));

                            World.BroadcastMapMsg(Player.Map, MsgName.Create((Player.X | Player.Y << 16), "1122", MsgName.Action.Fireworks));
                            World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", String.Format(Client.GetStr("STR_MARRY"), Player.Name, Target.Name), MsgTalk.Channel.GM, 0xFF0000));
                            break;
                        }
                    case Action.MagicAttack:
                        {
                            Player.Action = (Int16)MsgAction.Emotion.StandBy;

                            MagicType.Entry MagicInfo = new MagicType.Entry();
                            UInt16 MagicType = (UInt16)(Param);

                            //uint16_t encrypt_skilllevel(uint8_t p_skilllevel, int p_timestamp)
                            //{
                            //    return (uint16_t)((p_skilllevel | ((p_timstamp << 8) & 0xff00)) ^ 0x3721);
                            //}

                            if (Player.UniqId != SenderUID)
                                return;

                            TargetUID = (Int32)((((UInt32)TargetUID & 0xffffe000) >> 13) | (((UInt32)TargetUID & 0x1fff) << 19));
                            TargetUID ^= 0x5F2D2463;
                            TargetUID ^= SenderUID;
                            TargetUID -= 0x746F4AE6;

                            TargetX ^= (UInt16)((UInt32)SenderUID & 0xffff);
                            TargetX ^= 0x2ED6;
                            TargetX = (UInt16)((TargetX << 1) | ((TargetX & 0x8000) >> 15));
                            TargetX -= 0x22EE;

                            TargetY ^= (UInt16)((UInt32)SenderUID & 0xffff);
                            TargetY ^= 0xB99B;
                            TargetY = (UInt16)((TargetY << 5) | ((TargetY & 0xF800) >> 11));
                            TargetY -= 0x8922;

                            MagicType ^= (UInt16)0x915D;
                            MagicType ^= (UInt16)SenderUID;
                            MagicType = (UInt16)(MagicType << 3 | MagicType >> 13);
                            MagicType -= (UInt16)0xEB42;

                            Magic Magic = Player.GetMagicByType((Int16)MagicType);
                            if (Magic == null)
                                return;

                            if (!Database2.AllMagics.TryGetValue((Magic.Type * 10) + Magic.Level, out MagicInfo))
                                return;

                            Player.TargetUID = TargetUID;
                            Player.AtkType = (Int32)Action;
                            Player.IsInBattle = true;
                            Player.MagicType = (Int16)MagicType;
                            Player.MagicLevel = Magic.Level;

                            if (TargetUID == 0)
                            {
                                Battle.Magic.Use(Player, null, TargetX, TargetY);
                                if (Player.Map != 1039)
                                    Player.IsInBattle = false;
                                return;
                            }

                            if (Entity.IsPlayer(TargetUID))
                            {
                                Player Target = null;
                                if (!World.AllPlayers.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, (Int32)MagicInfo.Distance))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.Magic.Use(Player, Target, TargetX, TargetY);
                            }
                            else if (Entity.IsMonster(TargetUID))
                            {
                                Monster Target = null;
                                if (!World.AllMonsters.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, (Int32)MagicInfo.Distance))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.Magic.Use(Player, Target, TargetX, TargetY);
                            }
                            else if (Entity.IsTerrainNPC(TargetUID))
                            {
                                TerrainNPC Target = null;
                                if (!World.AllTerrainNPCs.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, (Int32)MagicInfo.Distance))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.Magic.Use(Player, Target, TargetX, TargetY);
                            }

                            if (Player.Map != 1039)
                                Player.IsInBattle = false;
                            break;
                        }
                    case Action.Shoot:
                        {
                            Player.Action = (Int16)MsgAction.Emotion.StandBy;
                            if (Player.UniqId != SenderUID)
                                return;

                            Player.TargetUID = TargetUID;
                            Player.AtkType = (Int32)Action;
                            Player.IsInBattle = true;
                            Player.MagicType = -1;
                            Player.MagicLevel = 0;

                            if (Entity.IsPlayer(TargetUID))
                            {
                                Player Target = null;
                                if (!World.AllPlayers.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, Player.AtkRange))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.PvP(Player, Target);
                            }
                            else if (Entity.IsMonster(TargetUID))
                            {
                                Monster Target = null;
                                if (!World.AllMonsters.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, Player.AtkRange))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.PvM(Player, Target);
                            }
                            else if (Entity.IsTerrainNPC(TargetUID))
                            {
                                TerrainNPC Target = null;
                                if (!World.AllTerrainNPCs.TryGetValue(TargetUID, out Target))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }

                                if (!MyMath.CanSee(Player.X, Player.Y, Target.X, Target.Y, Player.AtkRange))
                                {
                                    Player.IsInBattle = false;
                                    return;
                                }
                                Battle.PvE(Player, Target);
                            }
                            else
                            {
                                Player.IsInBattle = false;
                                return;
                            }
                            break;
                        }
                    case (Action)30:
                        {
                            //GouDeSaintNuage
                            Player.Send(MsgInteract.Create(Player, null, 200, (Action)30));
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
