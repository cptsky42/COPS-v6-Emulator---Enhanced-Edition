// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgAllot : Msg
    {
        public const Int16 Id = _MSG_ALLOT;

        [StructLayout(LayoutKind.Sequential , Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Byte Strength;
            public Byte Agility;
            public Byte Vitality;
            public Byte Spirit;
        };

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

                    UInt16 Points = (UInt16)(pMsg->Strength + pMsg->Agility + pMsg->Vitality + pMsg->Spirit);
                    Player Player = Client.User;

                    if (Player.AddPoints < Points)
                    {
                        Program.Log(String.Format("[CRIME] {0} ({1}) : ALLOT_CHEAT", Player.Name, Player.UniqId));
                        Player.SendSysMsg(Client.GetStr("STR_ALLOT_CHEAT"));
                        return;
                    }

                    Player.Strength += pMsg->Strength;
                    Player.Agility += pMsg->Agility;
                    Player.Vitality += pMsg->Vitality;
                    Player.Spirit += pMsg->Spirit;
                    Player.AddPoints -= (UInt16)(pMsg->Strength + pMsg->Agility + pMsg->Vitality + pMsg->Spirit);

                    MyMath.GetHitPoints(Player, true);
                    MyMath.GetMagicPoints(Player, true);
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
