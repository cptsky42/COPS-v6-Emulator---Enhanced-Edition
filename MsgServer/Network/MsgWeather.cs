// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgWeather : Msg
    {
        public const Int16 Id = _MSG_WEATHER;

        public enum Type
        {
            None = 1,
            Rain = 2,
            Snow = 3,
            RainWind = 4,
            AutumnLeaves = 5,
            CherryBlossomPetals = 7,
            CherryBlossomPetalsWind = 8,
            BlowingCotten = 9,
            Atoms = 10,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Type;
            public Int32 Intensity;
            public Int32 Direction;
            public Int32 Color;
        };

        public static Byte[] Create(Map Map)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Type = Map.Weather;
                pMsg->Intensity = MyMath.Generate(125, 150);
                pMsg->Direction = MyMath.Generate(45, 85);
                pMsg->Color = 0;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
