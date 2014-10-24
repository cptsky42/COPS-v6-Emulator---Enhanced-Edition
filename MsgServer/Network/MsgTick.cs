// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgTick : Msg
    {
        public const Int16 Id = _MSG_TICK;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Timestamp;
            public Int32 Junk1;
            public Int32 Junk2;
            public Int32 Junk3;
            public Int32 Junk4;
            public UInt32 Hash;
        };

        public static Byte[] Create(Entity Entity)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->UniqId = Entity.UniqId;
                pMsg->Timestamp = 0x00;
                pMsg->Junk1 = MyMath.Generate(0x1FFFFFFF, 0x7FFFFFFF);
                pMsg->Junk2 = MyMath.Generate(0x1FFFFFFF, 0x7FFFFFFF);
                pMsg->Junk3 = MyMath.Generate(0x1FFFFFFF, 0x7FFFFFFF);
                pMsg->Junk4 = MyMath.Generate(0x1FFFFFFF, 0x7FFFFFFF);
                pMsg->Hash = 0x00;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Kernel.memcpy(Out, pMsg, Out.Length);
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

                fixed (Byte* pBuf = Buffer)
                {
                    MsgInfo* pMsg = (MsgInfo*)pBuf;
                    Player Player = Client.User;

                    if (pMsg->UniqId != Player.UniqId)
                    {
                        Client.Socket.Disconnect();
                        return;
                    }

                    if (pMsg->Hash != HashName(Player.Name))
                    {
                        Client.Socket.Disconnect();
                        return;
                    }

                    Int32 Timestamp = pMsg->Timestamp ^ pMsg->UniqId;

                    if (Player.LastClientTick == 0)
                        Player.LastClientTick = Timestamp;

                    if (Player.LastClientTick > Timestamp)
                    {
                        Player.SendSysMsg(Client.GetStr("STR_CONNECTION_OFF"));
                        Client.Socket.Disconnect();
                        return;
                    }

                    Player.LastClientTick = Timestamp;
                    Player.LastRcvClientTick = Environment.TickCount;
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        private static UInt32 HashName(String Name)
        {
            if (String.IsNullOrEmpty(Name) || Name.Length < 4)
                return 0x9D4B5703;
            else
            {
                Byte[] Buffer = Program.Encoding.GetBytes(Name);
                fixed (Byte* pBuf = Buffer)
                    return ((UInt16*)pBuf)[0] ^ 0x9823U;
            }
        }
    }
}
