// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Entities;

namespace COServer.Network
{
    public unsafe class MsgDataArray : Msg
    {
        public const Int16 Id = _MSG_DATAARRAY;

        [StructLayout(LayoutKind.Sequential , Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Unknow; //K = 1280? (Probably Action, but not used on CO2)
            public Int32 MainUID;
            public Int32 FirstTreasureUID;
            public Int32 SecondTreasureUID;
            public Int32 FirstGemUID;
            public Int32 SecondGemUID;
        };

        public static Byte[] Create(Int32 MainUID, Int32 FirstTreasureUID, Int32 SecondTreasureUID, Int32 FirstGemUID, Int32 SecondGemUID)
        {
            try
            {
                MsgInfo* pMsg = stackalloc MsgInfo[1];
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->Unknow = 1280;
                pMsg->MainUID = MainUID;
                pMsg->FirstTreasureUID = FirstTreasureUID;
                pMsg->SecondTreasureUID = SecondTreasureUID;
                pMsg->FirstGemUID = FirstGemUID;
                pMsg->SecondGemUID = SecondGemUID;

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
                Int32 Unknow = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 MainUID = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Int32 FirstTreasureUID = (Int32)((Buffer[0x0F] << 24) + (Buffer[0x0E] << 16) + (Buffer[0x0D] << 8) + Buffer[0x0C]);
                Int32 SecondTreasureUID = (Int32)((Buffer[0x13] << 24) + (Buffer[0x12] << 16) + (Buffer[0x11] << 8) + Buffer[0x10]);
                Int32 FirstGemUID = (Int32)((Buffer[0x17] << 24) + (Buffer[0x16] << 16) + (Buffer[0x15] << 8) + Buffer[0x14]);
                Int32 SecondGemUID = (Int32)((Buffer[0x1B] << 24) + (Buffer[0x1A] << 16) + (Buffer[0x19] << 8) + Buffer[0x18]);

                Player Player = Client.User;

                Item MainItem = Player.GetItemByUID(MainUID);
                Item FirstTreasure = Player.GetItemByUID(FirstTreasureUID);
                Item SecondTreasure = Player.GetItemByUID(SecondTreasureUID);

                if (MainItem == null || FirstTreasure == null || SecondTreasure == null)
                    return;

                if (FirstTreasure.Craft != SecondTreasure.Craft)
                    return;

                if ((MainItem.Craft == 0 && FirstTreasure.Craft != 1) || (MainItem.Craft != 0 && MainItem.Craft != FirstTreasure.Craft))
                    return;

                if (MainItem.Craft >= 9)
                    return;

                Int16 MainType = (Int16)(MainItem.Id / 1000);
                Int16 FirstTreasureType = (Int16)(FirstTreasure.Id / 1000);
                Int16 SecondTreasureType = (Int16)(SecondTreasure.Id / 1000);

                if (FirstTreasureType != 730 && SecondTreasureType != 730)
                {
                    if ((Int16)(MainType / 100) == 4 && (((Int16)(FirstTreasureType / 100) != 4 || (Int16)(SecondTreasureType / 100) != 4)))
                        return;

                    if ((Int16)(MainType / 100) == 5 && (((Int16)(FirstTreasureType / 100) != 5 || (Int16)(SecondTreasureType / 100) != 5)))
                        return;

                    if ((Int16)(MainType / 100) == 9 && (((Int16)(FirstTreasureType / 100) != 9 || (Int16)(SecondTreasureType / 100) != 9)))
                        return;

                    if ((Int16)(MainType / 100) != 4 && (Int16)(MainType / 100) != 5 && (Int16)(MainType / 100) != 9)
                        if (MainType != FirstTreasureType || MainType != SecondTreasureType)
                            return;
                }

                if (MainItem.Craft >= 5)
                {
                    Item FirstGem = Player.GetItemByUID(FirstGemUID);
                    Item SecondGem = Player.GetItemByUID(SecondGemUID);

                    if (FirstGem == null || SecondGem == null)
                        return;

                    Player.DelItem(FirstGem, true);
                    Player.DelItem(SecondGem, true);
                }

                Player.DelItem(FirstTreasure, true);
                Player.DelItem(SecondTreasure, true);

                MainItem.Craft++;
                Player.Send(MsgItemInfo.Create(MainItem, MsgItemInfo.Action.Update));
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
