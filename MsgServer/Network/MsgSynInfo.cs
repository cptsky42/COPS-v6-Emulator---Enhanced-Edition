// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgSynInfo : Msg
    {
        public const Int16 Id = _MSG_SYNINFO;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Donation;
            public Int32 Fund;
            public Int32 Members;
            public String Leader;
        };

        public static Byte[] Create(Int32 SenderUID, Syndicate.Info Syndicate)
        {
            try
            {
                Syndicate.Member Sender = null;
                if (Syndicate != null)
                {
                    if (Syndicate.Leader.Name == null || Syndicate.Leader.Name.Length > _MAX_NAMESIZE)
                        return null;

                    if (Syndicate.Leader.UniqId == SenderUID)
                        Sender = Syndicate.Leader;
                    else if (!Syndicate.Members.TryGetValue(SenderUID, out Sender))
                        return null;
                }

                Byte[] Out = new Byte[40];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    if (Syndicate != null)
                    {
                        *((Int32*)(p + 4)) = (Int32)Syndicate.UniqId;
                        *((Int32*)(p + 8)) = (Int32)Sender.Donation;
                        *((Int32*)(p + 12)) = (Int32)Syndicate.Money;
                        *((Int32*)(p + 16)) = (Int32)Syndicate.Members.Count + 1;
                        *((Byte*)(p + 20)) = (Byte)Sender.Rank;
                        for (Byte i = 0; i < (Byte)Syndicate.Leader.Name.Length; i++)
                            *((Byte*)(p + 21 + i)) = (Byte)Syndicate.Leader.Name[i];
                    }
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
