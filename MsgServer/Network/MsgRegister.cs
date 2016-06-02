// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgRegister : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_REGISTER; } }

        //--------------- Internal Members ---------------
        private String __Account = "";
        private String __Name = "";
        private String __Password = "";
        private UInt16 __Look = 0;
        private UInt16 __Profession = 0;
        private UInt32 __AccountUID = 0;
        //------------------------------------------------

        /// <summary>
        /// Account name.
        /// </summary>
        public String Account
        {
            get { return __Account; }
            set { __Account = value; WriteString(0x04, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Player's name.
        /// </summary>
        public String Name
        {
            get { return __Name; }
            set { __Name = value; WriteString(0x14, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Un-encrypted password of the account.
        /// </summary>
        public String Password
        {
            get { return __Password; }
            set { __Password = value; WriteString(0x24, value, MAX_NAME_SIZE); }
        }

        /// <summary>
        /// Player's look.
        /// </summary>
        public UInt16 Look
        {
            get { return __Look; }
            set { __Look = value; WriteUInt16(0x34, value); }
        }

        /// <summary>
        /// Player's profession.
        /// </summary>
        public Byte Profession
        {
            get { return (Byte)__Profession; }
            set { __Profession = value; WriteUInt16(0x36, value); }
        }

        /// <summary>
        /// Account UID.
        /// </summary>
        public UInt32 AccountUID
        {
            get { return __AccountUID; }
            set { __AccountUID = value; WriteUInt32(0x38, value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgRegister(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Account = Program.Encoding.GetString(mBuf, 0x04, MAX_NAME_SIZE).TrimEnd('\0');
            __Name = Program.Encoding.GetString(mBuf, 0x14, MAX_NAME_SIZE).TrimEnd('\0');
            __Password = Program.Encoding.GetString(mBuf, 0x24, MAX_NAME_SIZE).TrimEnd('\0');
            __Look = BitConverter.ToUInt16(mBuf, 0x34);
            __Profession = BitConverter.ToUInt16(mBuf, 0x36);
            __AccountUID = BitConverter.ToUInt32(mBuf, 0x38);
        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            if (aClient.Account != Account)
            {
                if (Account.StartsWith("NEW"))
                    Account = Account.Substring(3, Account.Length - 3);
                else
                {
                    aClient.Disconnect();
                    return;
                }
            }

            if (aClient.AccountID != AccountUID)
            {
                aClient.Disconnect();
                return;
            }

            Byte face = 67;
            if (Look / 1000 == 2)
                face = 201;

            Boolean isValidName = true;

            if (Name.IndexOfAny(new Char[] { ' ', '[', ']', '.', ',', ':', '*', '?', '"', '<', '>', '|', '/', '\\' }) > -1)
                isValidName = false;

            if (isValidName)
            {
                Boolean isNameInUse = false;
                // TODO check if name is used by a player in the database...

                if (!isNameInUse)
                {
                    aClient.Send(new MsgTalk("SYSTEM", "ALLUSERS", "ANSWER_OK", Channel.Register, 0x000000));
                    if (!Database.CreatePlayer(aClient, Name, (face * 10000) + Look, Profession))
                    {
                        aClient.Disconnect();
                        return;
                    }
                    sLogger.Info("Creation of '{0}', with the account '{1}' ({2}).",
                        Name, Account, aClient.IPAddress);
                }
                else
                    aClient.Send(new MsgTalk("SYSTEM", "ALLUSERS", StrRes.STR_NAME_USED, Channel.Register, Color.Red));
            }
            else
                aClient.Send(new MsgTalk("SYSTEM", "ALLUSERS", StrRes.STR_INVALID_NAME, Channel.Register, Color.Red));
        }
    }
}
