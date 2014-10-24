// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgUserAttrib : Msg
    {
        public const Int16 Id = _MSG_USERATTRIB;

        public enum Type
        {
            None = -1,
            Life = 0,
            MaxLife = 1,
            Mana = 2,
            MaxMana = 3,
            Money = 4,
            Exp = 5,
            PkPoints = 6,
            Profession = 7,
            SizeAdd = 8, //0 -> None / 1 -> Cursed / 2 -> Blessed / 3 -> Cursed & Blessed (Important for these effect!)
            Energy = 9,
            AddPoints = 11,
            Look = 12,
            Level = 13,
            Spirit = 14,
            Vitality = 15,
            Strength = 16,
            Agility = 17,
            BlessTime = 18,
            DblExpTime = 19,
            // 20 = GuildDonation (when you attack the GW pole for example, this is sent with your new donation)
            CurseTime = 21,
            TimeAdd = 22, //Cyclone & Superman (0 = 0sec / 1 = 1sec / 2 = 2 sec / 3 = 3sec)
            Metempsychosis = 23,
            Flags = 26,
            Hair = 27,
            XP = 28,
            LuckyTime = 29,
            CPs = 30,
            TrainingPoints = 32, //0 -> Show / 1 -> In Offline TG (No Points) / 2 -> Out Offline TG (Points) / 3 -> Add 10 pts / 4 -> 100%
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Count;
            public Int32 Type; //Type(n)
            public Int64 Param; //Param(n)
        };

        public static Byte[] Create(Entity Entity, Int64 Param, Type Type)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->UniqId = Entity.UniqId;
                pMsg->Count = 0x01;
                pMsg->Type = (Int32)Type;
                pMsg->Param = Param;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
