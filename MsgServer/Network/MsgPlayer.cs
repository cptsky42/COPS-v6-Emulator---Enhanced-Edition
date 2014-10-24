// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgPlayer : Msg
    {
        public const Int16 Id = _MSG_PLAYER;

        public struct MsgInfo
        {
            public MsgHeader Header;
        };

        public static Byte[] Create(Player Entity)
        {
            try
            {
                if (Entity.Name == null || Entity.Name.Length > _MAX_NAMESIZE)
                    return null;

                Syndicate.Member SynMember = null;
                if (Entity.Syndicate != null)
                    SynMember = Entity.Syndicate.GetMemberInfo(Entity.UniqId);

                Byte[] Out = new Byte[85 + Entity.Name.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Entity.UniqId;
                    *((Int32*)(p + 8)) = (Int32)Entity.Look;
                    *((Int64*)(p + 12)) = (Int64)Entity.Flags;
                    if (SynMember != null)
                    {
                        *((Int16*)(p + 20)) = (Int16)Entity.Syndicate.UniqId;
                        *((Byte*)(p + 22)) = (Byte)0x00; //Unknow
                        *((Byte*)(p + 23)) = (Byte)SynMember.Rank;
                    }
                    *((Int32*)(p + 24)) = (Int32)Entity.GetGarmentTypeID();
                    *((Int32*)(p + 28)) = (Int32)Entity.GetHeadTypeID();
                    *((Int32*)(p + 32)) = (Int32)Entity.GetArmorTypeID();
                    *((Int32*)(p + 36)) = (Int32)Entity.GetRightHandTypeID();
                    *((Int32*)(p + 40)) = (Int32)Entity.GetLeftHandTypeID();
                    *((Int32*)(p + 44)) = (Int32)0x00; //Unknow
                    *((UInt16*)(p + 48)) = (UInt16)Entity.CurHP;
                    *((Int16*)(p + 50)) = (Int16)Entity.Level;
                    *((UInt16*)(p + 52)) = (UInt16)Entity.X;
                    *((UInt16*)(p + 54)) = (UInt16)Entity.Y;
                    *((Int16*)(p + 56)) = (Int16)Entity.Hair;
                    *((Byte*)(p + 58)) = (Byte)Entity.Direction;
                    *((Byte*)(p + 59)) = (Byte)Entity.Action;
                    *((Int16*)(p + 60)) = (Int16)Entity.Metempsychosis;
                    *((Int16*)(p + 62)) = (Int16)Entity.Level;
                    *((Int32*)(p + 64)) = (Int32)0x00; //Unknow
                    *((Int32*)(p + 68)) = (Int32)Entity.Nobility.Rank;
                    *((Int32*)(p + 72)) = (Int32)Entity.UniqId;
                    *((Int32*)(p + 76)) = (Int32)Entity.Nobility.Position;
                    *((Byte*)(p + 80)) = (Byte)0x01; //String Count
                    *((Byte*)(p + 81)) = (Byte)Entity.Name.Length;
                    for (Byte i = 0; i < Entity.Name.Length; i++)
                        *((Byte*)(p + 82 + i)) = (Byte)Entity.Name[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Monster Entity)
        {
            try
            {
                if (Entity.Name == null || Entity.Name.Length > _MAX_NAMESIZE)
                    return null;

                Byte[] Out = new Byte[85 + Entity.Name.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Entity.UniqId;
                    *((Int32*)(p + 8)) = (Int32)Entity.Look;
                    *((Int64*)(p + 12)) = (Int64)Entity.Flags;
                    *((Int16*)(p + 20)) = (Int16)0x00;
                    *((Byte*)(p + 22)) = (Byte)0x00;
                    *((Byte*)(p + 23)) = (Byte)0x00;
                    *((Int32*)(p + 24)) = (Int32)0x00;
                    *((Int32*)(p + 28)) = (Int32)0x00;
                    *((Int32*)(p + 32)) = (Int32)0x00;
                    *((Int32*)(p + 36)) = (Int32)0x00;
                    *((Int32*)(p + 40)) = (Int32)0x00;
                    *((Int32*)(p + 44)) = (Int32)0x00;
                    *((UInt16*)(p + 48)) = (UInt16)Entity.CurHP;
                    *((Int16*)(p + 50)) = (Int16)Entity.Level;
                    *((UInt16*)(p + 52)) = (UInt16)Entity.X;
                    *((UInt16*)(p + 54)) = (UInt16)Entity.Y;
                    *((Int16*)(p + 56)) = (Int16)0x00;
                    *((Byte*)(p + 58)) = (Byte)Entity.Direction;
                    *((Byte*)(p + 59)) = (Byte)Entity.Action;
                    *((Int16*)(p + 60)) = (Int16)0x00;
                    *((Int16*)(p + 62)) = (Int16)Entity.Level;

                    *((Byte*)(p + 80)) = (Byte)0x01; //String Count
                    *((Byte*)(p + 81)) = (Byte)Entity.Name.Length;
                    for (Byte i = 0; i < Entity.Name.Length; i++)
                        *((Byte*)(p + 82 + i)) = (Byte)Entity.Name[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
