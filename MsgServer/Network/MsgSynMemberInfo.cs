// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgSynMemberInfo : Msg
    {
        public const Int16 Id = _MSG_SYNMEMBERINFO;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Donation;
            public Byte Rank;
            public String Name;
        };

        public static Byte[] Create(Syndicate.Member Member)
        {
            try
            {
                Byte[] Out = new Byte[28];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Member.Donation;
                    *((Byte*)(p + 8)) = (Byte)Member.Rank;
                    for (Byte i = 0; i < (Byte)Member.Name.Length; i++)
                        *((Byte*)(p + 9 + i)) = (Byte)Member.Name[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 Donation = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Byte Rank = (Byte)Buffer[0x08];
                String Name = Program.Encoding.GetString(Buffer, 0x09, 0x10).TrimEnd((Char)0x00);

                Player Player = Client.User;

                if (Player.Syndicate == null)
                    return;

                Syndicate.Member Member = Player.Syndicate.GetMemberInfo(Name);
                if (Member == null)
                    return;

                Player.Send(MsgSynMemberInfo.Create(Member));
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
