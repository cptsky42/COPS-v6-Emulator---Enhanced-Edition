// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgDice : Msg
    {
        public const Int16 Id = _MSG_DICE;

        public enum Action
        {
            ChipIn = 0,		        //ÏÂ×¢  to server, to client
                                    //¸öÈËÏÂ×¢Í¨¹ýÕâ¸öAction¹ã²¥Í¬²½
                                    //ÐÂ¼ÓÈë¶Ä¾ÖµÄÈËÒ²Í¨¹ýÕâ¸öActionÈ¡µÃÍ¶×¢ÁÐ±í
                                    //dwDataÎªÍ¶ÁË¶àÉÙÇ®£¬ucTypeÎªÍ¶ÄÄÖÖ×¢
                                    // exclude self, ×Ô¼ºÍ¨¹ý_ACTION_CHIPIN_CONFIRM ·µ»Ø
            ChipIn_Confirm = 1,		//ÏÂ×¢È·ÈÏ// only to client
            CancelChip = 2,		    //È¡ÏûÏÂ×¢to server, to client
                                    //dwMoneyÎªÍ¶ÁË¶àÉÙÇ®£¬ucTypeÎªÍ¶ÄÄÖÖ×¢
                                    // exclude self, ×Ô¼ºÍ¨¹ý_ACTION_CANCELCHIP_CONFIRM ·µ»Ø
            CancelChip_Confirm = 3,	//ÍË³öÏÂ×¢È·ÈÏ// only to client
            BeginChip = 4,	        //¿ªÊ¼ÏÂ×¢ // to client µ¹¼ÆÊ±	// ucTime is leave secs
            EndChip = 5,	        //Âò¶¨ÀëÊÖ // to client µ¹¼ÆÊ±5S
            Dice = 6,		        //÷»×ÓÏûÏ¢ // to client µ¹¼ÆÊ±5S // MsgName¹ã²¥Ó®Ç®ÏûÏ¢
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Byte Action;
            public Byte Amount; //(Time)
            public Int32 DiceNpc;

            //Can have more than one
            public Byte Type;
            public UInt32 Data;
        };

        public static Byte[] Create(Int32 Npc, Int32 Time, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Action = (Byte)Action;
                pMsg->Amount = (Byte)Time;
                pMsg->DiceNpc = Npc;
                pMsg->Type = 0;
                pMsg->Data = 0;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(Int32 Npc, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Action = (Byte)Action;
                pMsg->Amount = 0;
                pMsg->DiceNpc = Npc;
                pMsg->Type = 0;
                pMsg->Data = 0;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

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

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
