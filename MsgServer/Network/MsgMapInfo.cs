// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;

namespace COServer.Network
{
    public unsafe class MsgMapInfo : Msg
    {
        public const Int16 Id = _MSG_MAPINFO;

        [Flags]
        public enum Flags : int
        {
            None = 0x0000,
            PKField = 0x0001,               //No PkPoints, Not Flashing...
            ChangeMap_Disable = 0x0002,     //Unused...
            Record_Disable = 0x0004,        //Do not save this position, save the previous
            PK_Disable = 0x0008,            //Can't Pk
            Booth_Enable = 0x0010,          //Can create booth
            Team_Disable = 0x0020,          //Can't create team
            Teleport_Disable = 0x0040,      //Can't use scroll
            Syn_Map = 0x0080,               //Syndicate Map
            Prison_Map = 0x0100,            //Prison Map
            Wing_Disable = 0x0200,          //Can't fly
            Family = 0x0400,                //Family Map
            MineField = 0x0800,             //Mine Map
            CallNewbie_Disable = 0x1000,    //Unused...
            RebornNow_Enable = 0x2000,      //Blessed reborn
            NewbieProtect = 0x4000,         //Newbie protection
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 MapId;
            public Int32 MiniMap;
            public Int32 Flags;
        };

        public static Byte[] Create(Map Map)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->MapId = Map.UniqId;
                pMsg->MiniMap = Map.Id;
                pMsg->Flags = Map.Flags;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }
    }
}
