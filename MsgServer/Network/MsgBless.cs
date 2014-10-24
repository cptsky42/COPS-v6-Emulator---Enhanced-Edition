// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgBless : Msg
    {
        public const Int16 Id = _MSG_BLESS;

        public enum Action
        {
            None = 0,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Action;
            public Int32 Param;
        };

        public static Byte[] Create(Int32 Param, Action Action)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Action = (Int32)Action;
                pMsg->Param = Param;

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
                Action Action = (Action)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 Param = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);

                Player Player = Client.User;

                switch (Action)
                {
                        //0: Request Time That You Can OffTG
                        //1: Request Enter
                        //3: Request OffTG 
                        //4: Request Quit
                        //5: MessageBox: You can't OffTG on this map.
                    case (Action)0:
                        {
                            //Param = Min
                            Player.Send(MsgBless.Create(Player.MaxTrainingTime, (Action)0));
                            break;
                        }
                    case (Action)1:
                        {
                            Player.TrainingTicks = DateTime.UtcNow.Ticks;
                            //Player.Move(601, 50, 50);

                            Player.Send(Buffer);
                            break;
                        }
                    case (Action)3:
                        {
                            Player.Send(MsgBlessInfo.Create(Player));
                           break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", MsgId, (Int16)Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
