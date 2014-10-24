// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgBroadcast : Msg
    {
        public const Int16 Id = _MSG_BROADCAST;

        public enum Action
        {
            None = 0,
            Send = 3,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Action;
            public Int32 Param;
            public Byte StringCount;
            public String Words;
        };

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Action Action = (Action)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 Param = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Byte StringCount = Buffer[0x0C];
                String Words = Program.Encoding.GetString(Buffer, 0x0E, Buffer[0x0D]);

                switch (Action)
                {
                    case Action.Send:
                        {
                            Player Player = Client.User;
                            if (Player.CPs < 5)
                                return;

                            Player.CPs -= 5;
                            Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                            World.BroadcastMsg(Player, MsgTalk.Create(Player, "ALLUSERS", Words, MsgTalk.Channel.Broadcast, 0x000000), true);
                            break;
                        }
                    default:
                        Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", MsgId, (Int16)Action);
                        break;
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

            //    case DEF_REQUEST_BROADCAST:
            //{
            //    char m_cBigChar[500];
            //    //char penis[50];
            //    //memcpy(penis,data,size);
            //    short total;
            //    total = 0;
            //    short page, test;
            //    page = *(data+8);
            //    test = 0;
            //    int count;
            //    count = 0;
            //    switch (*(data+4))
            //    {
            //    case 1://all - to be released // info awaiting
            //        memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //        *(WORD *)(m_cBigChar+2) = DEF_RESPONSE_BROADCAST;

            //        if ((10*page+10) >= DEF_MAX_BROADCASTS)
            //        {
            //            test = DEF_MAX_BROADCASTS;
            //        }
            //        else
            //        {
            //            test = 10*page+10;
            //        }
            //        for (int i = 10*page; i < test; i++)
            //        {
            //            if (pMainServer->m_Broadcast.m_stBroadcastList[i])
            //            {
            //                *(WORD *)(m_cBigChar + 2+2) = page;//page number
            //                *(WORD *)(m_cBigChar + 6+2) = total;//packet number
            //                (*(WORD *)(m_cBigChar + 8+2))++;//total broadcasts
            //                *(int *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112) = pMainServer->m_Broadcast.m_stBroadcastList[i]->id;//broadcast id total
            //                *(int  *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4) = i;//id relative to broadcast list?
            //                *(int  *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4) = pMainServer->m_Broadcast.m_stBroadcastList[i]->charid;//char id
            //                memcpy(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4 + 4, pMainServer->m_Broadcast.m_stBroadcastList[i]->name, strlen(pMainServer->m_Broadcast.m_stBroadcastList[i]->name));//char name
            //                *(int  *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4 + 4 + 16) = pMainServer->m_Broadcast.m_stBroadcastList[i]->spentcps;//spent cps
            //                memcpy(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4 + 4 + 20, pMainServer->m_Broadcast.m_stBroadcastList[i]->text, strlen(pMainServer->m_Broadcast.m_stBroadcastList[i]->text));//message text
            //                if (*(WORD *)(m_cBigChar + 8+2) % 4 == 0)
            //                {
            //                    total++;
            //                    *(WORD *)(m_cBigChar) = 8 + (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2;
            //                    SendPacket(sid, m_cBigChar, 8 +2+ (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2);
            //                    memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //                    *(WORD *)(m_cBigChar+2) = DEF_RESPONSE_BROADCAST;
            //                }
            //            }
            //            else
            //                break;
            //        }
            //        *(WORD *)(m_cBigChar) = 8 + (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2;
            //        if ((*(WORD *)(m_cBigChar + 8+2) != 0) || (*(WORD *)(m_cBigChar + 8+2) == 0) && (total == 0))
            //            SendPacket(sid, m_cBigChar, 8+2 + (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2);
            //        break;
            //    case 2://your own - release soon
            //        memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //        *(WORD *)(m_cBigChar+2) = DEF_RESPONSE_BROADCAST;

            //        for (int i = 0; i < DEF_MAX_BROADCASTS; i++)
            //        {
            //            if ((pMainServer->m_Broadcast.m_stBroadcastList[i]) && (pMainServer->m_Broadcast.m_stBroadcastList[i]->charid == pMainServer->m_pClientList[sid]->CID()))
            //                test++;
            //        }
            //        if ((10*page+10) >= DEF_MAX_BROADCASTS)
            //        {
            //            return;//test = DEF_MAX_BROADCASTS;
            //        }
            //        for (int i = 10*page; i < test; i++)
            //        {
            //            if ((pMainServer->m_Broadcast.m_stBroadcastList[i]) && (pMainServer->m_Broadcast.m_stBroadcastList[i]->charid == pMainServer->m_pClientList[sid]->CID()))
            //            {
            //                *(WORD *)(m_cBigChar + 2+2) = page;//page number
            //                *(WORD *)(m_cBigChar + 6+2) = total;//packet number
            //                (*(WORD *)(m_cBigChar + 8+2))++;//total broadcasts
            //                *(int *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112) = pMainServer->m_Broadcast.m_stBroadcastList[i]->id;//broadcast id total
            //                *(int  *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4) = i;//id relative to broadcast list?
            //                *(int  *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4) = pMainServer->m_Broadcast.m_stBroadcastList[i]->charid;//char id
            //                memcpy(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4 + 4, pMainServer->m_Broadcast.m_stBroadcastList[i]->name, strlen(pMainServer->m_Broadcast.m_stBroadcastList[i]->name));//char name
            //                *(int  *)(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4 + 4 + 16) = pMainServer->m_Broadcast.m_stBroadcastList[i]->spentcps;//spent cps
            //                memcpy(m_cBigChar + 10 +2+ (*(WORD *)(m_cBigChar + 8+2)-1)*112 + 4 + 4 + 4 + 20, pMainServer->m_Broadcast.m_stBroadcastList[i]->text, strlen(pMainServer->m_Broadcast.m_stBroadcastList[i]->text));//message text
            //                if (*(WORD *)(m_cBigChar + 8+2) % 4 == 0)
            //                {
            //                    total++;
            //                    *(WORD *)(m_cBigChar) = 8 + (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2;
            //                    SendPacket(sid, m_cBigChar, 8 +2+ (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2);
            //                    memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //                    *(WORD *)(m_cBigChar+2) = DEF_RESPONSE_BROADCAST;
            //                }
            //            }
            //            else
            //                break;
            //        }

            //        *(WORD *)(m_cBigChar) = 8 + (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2;
            //        if ((*(WORD *)(m_cBigChar + 8+2) != 0) || (*(WORD *)(m_cBigChar + 8+2) == 0) && (total == 0))
            //            SendPacket(sid, m_cBigChar, 8 + (*(WORD *)(m_cBigChar + 8+2))*112 + 6+2);
            //        break;
            //    case 3://send message
            //        if (!pMainServer->m_Broadcast.m_stBroadcastCurrent) 
            //        {
            //            pMainServer->m_Broadcast.sBroadcastAdd(pMainServer->m_pClientList[sid]->CID(), pMainServer->m_pClientList[sid]->CharName(), data+14, *(data+13));
            //            for (int i = 0; i < DEF_MAXCLIENTS; i++)
            //                if (pMainServer->m_pClientList[i])
            //                    SendText(i, DEF_CHATTYPE_NEWBROADCAST, pMainServer->m_pClientList[sid]->CharName(), "ALLUSERS", (data+14));
            //            SendText(sid, DEF_CHATTYPE_TOP3, "SYSTEM", "ALLUSERS", "Congratulations. You have submitted a message for broadcast.", 0x0000FF);
            //        }
            //        else if ((pMainServer->m_Broadcast.m_stBroadcastCurrent) && (timeGetTime() > pMainServer->m_Broadcast.m_stBroadcastCurrent->time) && (!pMainServer->m_Broadcast.m_stBroadcastList[0]))
            //        {
            //            pMainServer->m_Broadcast.BroadcastDelete();
            //            pMainServer->m_Broadcast.sBroadcastAdd(pMainServer->m_pClientList[sid]->CID(), pMainServer->m_pClientList[sid]->CharName(), data+14, *(data+13));
            //            for (int i = 0; i < DEF_MAXCLIENTS; i++)
            //                if (pMainServer->m_pClientList[i])
            //                    SendText(i, DEF_CHATTYPE_NEWBROADCAST, pMainServer->m_pClientList[sid]->CharName(), "ALLUSERS", (data+14));
            //            SendText(sid, DEF_CHATTYPE_TOP3, "SYSTEM", "ALLUSERS", "Congratulations. You have submitted a message for broadcast.", 0x0000FF);
            //        }
            //        else
            //        {
            //            for (int i = 0; i < DEF_MAX_BROADCASTS; i++)
            //            {
            //                count = pMainServer->m_Broadcast.sBroadcastAdd(pMainServer->m_pClientList[sid]->CID(), pMainServer->m_pClientList[sid]->CharName(), data+14, *(data+13));
            //                memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //                *(WORD *)(m_cBigChar) = 22;
            //                *(WORD *)(m_cBigChar+2) = DEF_REQUEST_BROADCAST;
            //                *(WORD *)(m_cBigChar + 2+2) = 3;
            //                *(WORD *)(m_cBigChar + 6+2) = count+1;
            //                SendPacket(sid, m_cBigChar, 22);
            //                SendText(sid, DEF_CHATTYPE_TOP3, "SYSTEM", "ALLUSERS", "Congratulations. You have submitted a message for broadcast.", 0x0000FF);
            //                break;
            //            }
            //        }

            //        break;
            //    case 4:
            //        {
            //            int b;
            //            b = 0;
            //            int timetemp;
            //            for (int i = 0; i < DEF_MAX_BROADCASTS; i++)
            //            {
            //                if (pMainServer->m_Broadcast.m_stBroadcastList[i])
            //                {
            //                    if (pMainServer->m_Broadcast.m_stBroadcastList[i]->id == *(data+8))
            //                    {
            //                        pMainServer->m_Broadcast.m_stBroadcastList[i]->spentcps += 15;
            //                        BROADCAST * bctemp;
            //                        bctemp = pMainServer->m_Broadcast.m_stBroadcastList[i];
            //                        b = i;
            //                        for (int a = i-1; a >= 0; a--)
            //                        {
            //                            if (pMainServer->m_Broadcast.m_stBroadcastList[a])
            //                            {
            //                                if (pMainServer->m_Broadcast.m_stBroadcastList[a]->spentcps <= bctemp->spentcps)
            //                                {
            //                                    pMainServer->m_Broadcast.m_stBroadcastList[a+1] = pMainServer->m_Broadcast.m_stBroadcastList[a];
            //                                    timetemp = pMainServer->m_Broadcast.m_stBroadcastList[a]->time;
            //                                    pMainServer->m_Broadcast.m_stBroadcastList[a]->time = bctemp->time;
            //                                    pMainServer->m_Broadcast.m_stBroadcastList[a] = bctemp;
            //                                    bctemp->time = timetemp;
            //                                    b = a;
            //                                }
            //                                else
            //                                {
            //                                    break;
            //                                }
            //                            }
            //                        }
            //                        break;
            //                    }
            //                }
            //            }
            //            pMainServer->m_pClientList[sid]->SetCPs(pMainServer->m_pClientList[sid]->GetCPs() - 15);
            //            memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //            *(WORD *)(m_cBigChar) = 20;
            //            *(WORD *)(m_cBigChar+2) = DEF_REQUEST_BROADCAST;
            //            *(WORD *)(m_cBigChar+2+2) = 4;
            //            *(WORD *)(m_cBigChar + 6+2) = b+1;
            //            SendPacket(sid, m_cBigChar, 20);
            //            break;
            //        }
            //    case 5:
            //        {
            //            int b;
            //            b = 0;
            //            int timetemp;
            //            for (int i = 0; i < DEF_MAX_BROADCASTS; i++)
            //            {
            //                if (pMainServer->m_Broadcast.m_stBroadcastList[i])
            //                {
            //                    if (pMainServer->m_Broadcast.m_stBroadcastList[i]->id == *(data+8))
            //                    {
            //                        pMainServer->m_Broadcast.m_stBroadcastList[i]->spentcps += 5;
            //                        BROADCAST * bctemp;
            //                        bctemp = pMainServer->m_Broadcast.m_stBroadcastList[i];
            //                        b = i;
            //                        for (int a = i-1; a >= 0; a--)
            //                        {
            //                            if (pMainServer->m_Broadcast.m_stBroadcastList[a])
            //                            {
            //                                if (pMainServer->m_Broadcast.m_stBroadcastList[a]->spentcps <= bctemp->spentcps)
            //                                {
            //                                    pMainServer->m_Broadcast.m_stBroadcastList[a+1] = pMainServer->m_Broadcast.m_stBroadcastList[a];
            //                                    timetemp = pMainServer->m_Broadcast.m_stBroadcastList[a]->time;
            //                                    pMainServer->m_Broadcast.m_stBroadcastList[a]->time = bctemp->time;
            //                                    pMainServer->m_Broadcast.m_stBroadcastList[a] = bctemp;
            //                                    bctemp->time = timetemp;
            //                                    b = a;
            //                                }
            //                                else
            //                                {
            //                                    break;
            //                                }
            //                            }
            //                        }
            //                        break;
            //                    }
            //                }
            //            }
            //            pMainServer->m_pClientList[sid]->SetCPs(pMainServer->m_pClientList[sid]->GetCPs() - 5);
            //            memset(m_cBigChar, 0, sizeof(m_cBigChar));
            //            *(WORD *)(m_cBigChar) = 20;
            //            *(WORD *)(m_cBigChar+2) = DEF_REQUEST_BROADCAST;
            //            *(WORD *)(m_cBigChar+2+2) = 5;
            //            *(WORD *)(m_cBigChar + 6+2) = b+1;
            //            SendPacket(sid, m_cBigChar, 20);
            //            break;
            //        }
            //    }
            //    //HexDisplay(pData, wMsgSize);
            //    break;
            //}
    }
}
