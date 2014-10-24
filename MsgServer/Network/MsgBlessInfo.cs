// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgBlessInfo : Msg
    {
        public const Int16 Id = _MSG_BLESSINFO;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int16 Used;
            public Int16 Remaining;
            public Int32 Level;
            public UInt64 Exp;
        };

        public static Byte[] Create(Player Player)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                Int32 Time = 0;

                Int64 Ticks = DateTime.UtcNow.Ticks - Player.TrainingTicks;
                TimeSpan Span = new TimeSpan(Ticks);
                if (Span.TotalMinutes < Time)
                    Time = (Int32)Span.TotalMinutes;

                if (Time > Player.MaxTrainingTime)
                    Time = Player.MaxTrainingTime;

                UInt64 Exp = Player.CalcExpBall((Byte)Player.Level, Player.Exp, Time / 300.0);

                pMsg->Used = (Int16)Time;
                pMsg->Remaining = (Int16)(Player.MaxTrainingTime - Time);
                pMsg->Level = Player.Level;
                pMsg->Exp = Player.Exp + Exp;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
