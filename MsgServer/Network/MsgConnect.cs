// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgConnect : Msg
    {
        public const Int16 Id = _MSG_CONNECT;
        const Int32 _MAX_LANGUAGE_SIZE = 10;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 AccountUID;
            public Int32 Token;
            public Int16 Constant; //Executable
            public fixed Byte Language[_MAX_LANGUAGE_SIZE];
            public Int32 Version;
        };

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                fixed (Byte* pBuf = Buffer)
                {
                    MsgInfo* pMsg = (MsgInfo*)pBuf;

                    Client.Cipher.GenerateKey(pMsg->Token, pMsg->AccountUID);

                    if (Kernel.cstring(pMsg->Language, _MAX_LANGUAGE_SIZE) == "En")
                        Client.Language = Language.En;
                    else
                        Client.Language = Language.Fr;

                    if (pMsg->Constant != 0x4B)
                    {
                        Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Client.GetStr("STR_WRONG_CLIENT"), MsgTalk.Channel.Entrance, 0x000000));
                        return;
                    }

                    if (Client.Language == Language.Fr && (pMsg->Version ^ 0x200) != Server.Version_FR)
                    {
                        Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Client.GetStr("STR_WRONG_VERSION"), MsgTalk.Channel.Entrance, 0x000000));
                        return;
                    }

                    if (Client.Language == Language.En && (pMsg->Version ^ 0x200) != Server.Version_EN)
                    {
                        Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Client.GetStr("STR_WRONG_VERSION"), MsgTalk.Channel.Entrance, 0x000000));
                        return;
                    }

                    String Character = "@INVALID_CHAR@";
                    Client.Account = "@INVALID_ACC@";

                    lock (World.AllAccounts)
                    {
                        MsgAccountExt.MsgInfo Info = new MsgAccountExt.MsgInfo();
                        if (World.AllAccounts.ContainsKey(pMsg->Token))
                        {
                            Info = World.AllAccounts[pMsg->Token];
                            if (Info.AccountUID == pMsg->AccountUID)
                            {
                                Client.AccLvl = Info.AccLvl;
                                Client.Flags = Info.Flags;

                                Byte[] Tmp = new Byte[0x10];
                                Marshal.Copy((IntPtr)Info.AccountId, Tmp, 0, 0x10);
                                Client.Account = Program.Encoding.GetString(Tmp).TrimEnd((Char)0x00);
                                Marshal.Copy((IntPtr)Info.Character, Tmp, 0, 0x10);
                                Character = Program.Encoding.GetString(Tmp).TrimEnd((Char)0x00);

                                World.AllAccounts.Remove(Info.Token);
                            }
                        }
                    }

                    if (Client.Account != null && Client.Account != "@INVALID_ACC@")
                    {
                        Program.Log("Connection of " + Client.Socket.IpAddress + ", with " + Client.Account + ".");

                        if (Character == "@INVALID_CHAR@")
                            Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "NEW_ROLE", MsgTalk.Channel.Entrance, 0x000000));
                        else
                        {
                            if (!Database.GetPlayerInfo(ref Client, Character))
                            {
                                Client.Socket.Disconnect();
                                return;
                            }

                            if (World.AllPlayers.ContainsKey(Client.User.UniqId))
                                World.AllPlayers[Client.User.UniqId].Disconnect();

                            lock (World.AllPlayers) { World.AllPlayers.Add(Client.User.UniqId, Client.User); }

                            Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "ANSWER_OK", MsgTalk.Channel.Entrance, 0x000000));
                            Client.Send(MsgUserInfo.Create(Client.User));

                            Client.User.SetDefaultFlag();
                            Client.Send(MsgUserAttrib.Create(Client.User, Client.User.Flags, MsgUserAttrib.Type.Flags));

                            DateTime BlessEnd = DateTime.FromBinary(Client.User.BlessEndTime);
                            if (BlessEnd.CompareTo(DateTime.UtcNow) > 0)
                            {
                                TimeSpan Span = new TimeSpan(BlessEnd.Ticks - DateTime.UtcNow.Ticks);
                                Client.Send(MsgUserAttrib.Create(Client.User, (Int64)Span.TotalSeconds, MsgUserAttrib.Type.BlessTime));
                                Client.Send(MsgUserAttrib.Create(Client.User, 0, MsgUserAttrib.Type.TrainingPoints));
                                if (Client.User.Map == 601)
                                    Client.Send(MsgUserAttrib.Create(Client.User, 1, MsgUserAttrib.Type.TrainingPoints));
                            }
                            else
                                Client.User.BlessEndTime = 0;

                            Byte Param = 0;
                            if (Client.User.CurseEndTime != 0)
                                Param++;
                            if (Client.User.BlessEndTime != 0)
                                Param += 2;
                            Client.Send(MsgUserAttrib.Create(Client.User, Param, MsgUserAttrib.Type.SizeAdd));

                            Client.Send(MsgNoble.Create(Client.User));

                            Client.Send(MsgUserAttrib.Create(Client.User, Client.User.TimeAdd, MsgUserAttrib.Type.TimeAdd));

                            Client.Send(MsgTalk.Create("SYSTEM", Client.User.Name, Client.GetStr("STR_LOGIN_INFORMATION"), MsgTalk.Channel.Normal, 0x000000));
                            Client.Send(MsgTalk.Create("SYSTEM", Client.User.Name, "COPS v6 Emulator : v" + Program.Version, MsgTalk.Channel.Normal, 0x000000));
                            Client.Send(MsgTalk.Create("SYSTEM", Client.User.Name, String.Format(Client.GetStr("STR_SERVER_UPTIME"), String.Format("{0:G}", (DateTime.Now - Server.LaunchTime))), MsgTalk.Channel.Normal, 0x000000));

                            if (Client.User.PremiumEndTime.CompareTo(DateTime.UtcNow) > 0)
                                Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Vous etes encore premium jusqu'au " + String.Format("{0:R}", Client.User.PremiumEndTime), MsgTalk.Channel.Talk, 0xFFFFFF));

                            Int32 PlayersOnline = World.AllPlayers.Count;
                            Client.User.SendSysMsg(String.Format(Client.GetStr("STR_SERVER_INFORMATION"), PlayersOnline, Server.Name));

                            if (PlayersOnline > Server.MaxPlayers)
                                Server.SaveMaxPlayersRecord(PlayersOnline);

                            if (Client.AccLvl > 6 && !World.AllMasters.ContainsKey(Client.User.UniqId))
                                World.AllMasters.Add(Client.User.UniqId, Client.User);

                            foreach (Player Programmer in World.AllMasters.Values)
                                Programmer.SendSysMsg(String.Format("$@ Connection of {0} [{1}]!", Client.User.Name, Client.User.UniqId));

                            if (!Client.User.ToS)
                            {
                                Client.Send(MsgAction.Create(Client.User, (Int32)MsgAction.Command.AcceptToS, MsgAction.Action.PostCmd));
                                Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Vous serez déconnecté si vous n'acceptez pas les termes!", MsgTalk.Channel.Talk, 0xFFFFFF));
                            }

                            Client.User.ConnectionTime = Environment.TickCount;
                        }
                    }
                    else
                        Client.Socket.Disconnect();
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
