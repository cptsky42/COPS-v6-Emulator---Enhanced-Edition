// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgForge : Msg
    {
        public const Int16 Id = _MSG_FORGE;

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
        };

        public static Byte[] Create()
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;


                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        //The goods build up 　　Melts decayed is mysterious! Builds up the system to provide a promotion special
        //stage prop, the low quality equipment even common item potential opportunity for you: So long as puts in
        //these stage props or the equipment builds up in the fence, has the possibility builds up everybody to
        //need builds up the stone (one kind to be possible red to use for to promote equipment supplement rank
        //general stage prop)! Builds up the guide: 1st, the permission puts in builds up in the stove, and will
        //promote to build up turns into the power the goods:
        //  a, stage prop: Capsicum anomalum, meteor, meteor tear, each kind of gem;
        //                 (the following also open some special duty stage prop) 　
        //  b, equipment: Besides holiday clothing's all equipments, equip cannot have the supplement rank;
        //                 (following will open holiday clothing also to promote to build up turns into power) 
        //  Attention: Ordinary quality equipment, only then kills obtains strangely, has not repaired the
        //  durableness is lower than 50% only then to allow the participation to build up;
        //
        //  Attention: May also put in besides the above other stage prop equipment builds up in the fence, but
        //  will not enhance builds up the probability;
        //  (the following opening) 2, above tally the condition stage prop equipment to drive from the
        //  knapsack to builds up in the fence; Builds up fence most supports to put in the total is 20
        //  stage prop equipments; 3rd, the click builds up on fence's " Determines " The button, in builds
        //  up above the fence will appear this time builds up builds up the rank and builds up turns into
        //  the power;
        //4th, builds up the level definitions this time to build up may produce " + several "
        //     Builds up the stone (only to open at present red to builds up +1 builds up stone red);
        //5th, builds up turns into the power to explain that this time builds up the success probability;
        //6th, in can see builds up the rank and builds up turns into the power in the situation, clicks on
        //     " Builds up " The button, builds up in fence's goods vanishing, if builds up the success, appears
        //     in the knapsack +1 red builds up the stone; Small skill: 1st, is clicking on " Builds up " Before the button, if wants to cancel builds up, may click builds up fence upper right " Cancels " The button finished this time builds up the operation; 2nd, is clicking on " Determines " Before the button, if wants to replace the partial stage props, may click on " once more; Determines " Latter (this time builds up on fence not to demonstrate that builds up rank and builds up probability), to builds up in fence's stage prop equipment to carry on the replacement operation;

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 Unknow1 = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int16 Unknow2 = (Int16)((Buffer[0x09] << 8) + Buffer[0x08]);
                Int16 Count = (Int16)((Buffer[0x0B] << 8) + Buffer[0x0A]);
                Int32[] ItemsUID = new Int32[Count];

                Int32 Pos = 12;
                for (Int32 i = 0; i < Count; i++)
                {
                    ItemsUID[i] = (Int32)((Buffer[Pos + 3] << 24) + (Buffer[Pos + 2] << 16) + (Buffer[Pos + 1] << 8) + Buffer[Pos + 0]);
                    Pos += 4;
                }

                Console.WriteLine(Program.Dump(Buffer));
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
