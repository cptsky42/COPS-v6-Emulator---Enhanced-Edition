// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2011 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgDataArray : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_DATAARRAY; } }

		//--------------- Internal Members ---------------
		private Byte __Action = 0;
		private Byte __Amount = 0;
		private Int32[] __Data = new Int32[1];
		//------------------------------------------------

		public Byte Action
		{
			get { return __Action; }
			set { __Action = value; mBuf[4] = value; }
		}

		public Byte Amount
		{
			get { return __Amount; }
			set { __Amount = value; mBuf[5] = value; }
		}

		public Int32[] Data
		{
			get { return __Data; }
			set
			{
				__Data = value;
				for (int i = 0; i < value.Length; ++i)
					WriteInt32(8 + (i * 4), value[i]);
			}
		}

		/// <summary>
		/// Create a message object from the specified buffer.
		/// </summary>
		/// <param name="aBuf">The buffer containing the message.</param>
		/// <param name="aIndex">The index where the message is starting in the buffer.</param>
		/// <param name="aLength">The length of the message.</param>
		internal MsgDataArray(Byte[] aBuf, int aIndex, int aLength)
			: base(aBuf, aIndex, aLength)
		{
			__Action = mBuf[4];
			__Amount = mBuf[5];

			// mBuf[6] & mBuf[7] => Padding

			__Data = new Int32[__Amount];
			for (Byte i = 0; i < __Amount; ++i)
				__Data[i] = BitConverter.ToInt32(mBuf, 8 + (i * 4));
		}


		/// <summary>
		/// Process the message for the specified client.
		/// </summary>
		/// <param name="aClient">The client who sent the message.</param>
		public override void Process(Client aClient)
        {
            try
            {
				switch (Action)
				{
                    case 0:
                        {
                            if (Amount != 5)
                                return;

                            Player player = aClient.Player;

                            Item item = player.GetItemByUID(Data[0]);
                            Item firstTreasure = player.GetItemByUID(Data[1]);
                            Item secondTreasure = player.GetItemByUID(Data[2]);

                            if (item == null || firstTreasure == null || secondTreasure == null)
                                return;

                            if (firstTreasure.Craft != secondTreasure.Craft)
                                return;

                            if ((item.Craft == 0 && firstTreasure.Craft != 1) || (item.Craft != 0 && item.Craft != firstTreasure.Craft))
                                return;

                            if (item.Craft >= 9)
                                return;

                            Int16 MainType = (Int16)(item.Type / 1000);
                            Int16 FirstTreasureType = (Int16)(firstTreasure.Type / 1000);
                            Int16 SecondTreasureType = (Int16)(secondTreasure.Type / 1000);

                            if ((Int16)(MainType / 100) == 4 && (((Int16)(FirstTreasureType / 100) != 4 || (Int16)(SecondTreasureType / 100) != 4)))
                                return;

                            if ((Int16)(MainType / 100) == 5 && (((Int16)(FirstTreasureType / 100) != 5 || (Int16)(SecondTreasureType / 100) != 5)))
                                return;

                            if ((Int16)(MainType / 100) == 9 && (((Int16)(FirstTreasureType / 100) != 9 || (Int16)(SecondTreasureType / 100) != 9)))
                                return;

                            if ((Int16)(MainType / 100) != 4 && (Int16)(MainType / 100) != 5 && (Int16)(MainType / 100) != 9)
                                if (MainType != FirstTreasureType || MainType != SecondTreasureType)
                                    return;

                            if (item.Craft >= 5)
                            {
                                Item firstGem = player.GetItemByUID(Data[3]);
                                Item secondGem = player.GetItemByUID(Data[4]);

                                if (firstGem == null || secondGem == null)
                                    return;

                                player.DelItem(firstGem, true);
                                player.DelItem(secondGem, true);
                            }

                            player.DelItem(firstTreasure, true);
                            player.DelItem(secondTreasure, true);

                            ++item.Craft;
                            player.Send(new MsgItemInfo(item, MsgItemInfo.Action.Update));
                            break;
                        }
					default:
						{
                            sLogger.Error("Action {0} is not implemented for MsgDataArray.", Action);
                            break;
						}
				}
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
