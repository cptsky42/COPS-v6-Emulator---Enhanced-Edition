// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgWalk : Msg
    {
        public const Int16 Id = _MSG_WALK;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Byte Direction;
            public Byte Mode;
            public Int16 Unknow;
        };

        public static Byte[] Create(Int32 UniqId, Byte Direction, Boolean Run)
        {
            try
            {
                MsgInfo* Msg = stackalloc MsgInfo[1];
                Msg->Header.Length = (Int16)sizeof(MsgInfo);
                Msg->Header.Type = Id;

                Msg->UniqId = UniqId;
                Msg->Direction = (Byte)(((MyMath.Generate(100, 255) % 31) * 8) + Direction);
                Msg->Mode = Run ? (Byte)0x00 : (Byte)0x01;
                Msg->Unknow = 0x00;

                Byte[] Out = new Byte[Msg->Header.Length];
                Kernel.memcpy(Out, Msg, Out.Length);
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
                    Map Map = null;

                    if (pMsg->UniqId != Player.UniqId)
                    {
                        Client.Disconnect();
                        return;
                    }

                    Byte Dir = (Byte)(pMsg->Direction % 8);
                    UInt16 NewX = Player.X;
                    UInt16 NewY = Player.Y;

                    switch (Dir)
                    {
                        case 0: { NewY += 1; break; }
                        case 1: { NewX -= 1; NewY += 1; break; }
                        case 2: { NewX -= 1; break; }
                        case 3: { NewX -= 1; NewY -= 1; break; }
                        case 4: { NewY -= 1; break; }
                        case 5: { NewX += 1; NewY -= 1; break; }
                        case 6: { NewX += 1; break; }
                        case 7: { NewX += 1; NewY += 1; break; }
                    }

                    if (Player != null)
                    {
                        //if (!Player.IsAlive() && !Player.IsGhost())
                        //{
                        //    Player.SendSysMsg(Client.GetStr("STR_DIE"));
                        //    return;
                        //}

                        if (World.AllMaps.TryGetValue(Player.Map, out Map))
                        {
                            if (!Map.IsValidPoint(NewX, NewY))
                            {
                                Player.SendSysMsg(Client.GetStr("STR_INVALID_COORDINATE"));
                                Player.KickBack();
                                return;
                            }
                        }

                        Player.PrevX = Player.X;
                        Player.PrevY = Player.Y;

                        Player.X = NewX;
                        Player.Y = NewY;
                        Player.Direction = Dir;
                        Player.Action = (Int16)MsgAction.Emotion.StandBy;

                        Player.IsInBattle = false;
                        Player.MagicIntone = false;
                        Player.Mining = false;

                        Player.Send(Buffer);
                        Player.Screen.Move(Buffer);
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
