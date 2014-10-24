// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Runtime.InteropServices;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgRegister : Msg
    {
        public const Int16 Id = _MSG_REGISTER;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public fixed Byte Account[0x10];
            public fixed Byte Name[0x10];
            public fixed Byte Password[0x10];
            public Int16 Look;
            public Int16 Profession;
            public Int32 AccountUID;
        };

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null)
                    return;

                if (Buffer.Length != sizeof(MsgInfo))
                    return;

                fixed (Byte* pBuf = Buffer)
                {
                    MsgInfo* pMsg = (MsgInfo*)pBuf;

                    String Account = Kernel.cstring(pMsg->Account, 0x10);
                    String Name = Kernel.cstring(pMsg->Name, 0x10);
                    String Password = Kernel.cstring(pMsg->Password, 0x10);

                    if (Client.Account != Account)
                    {
                        if (Account.StartsWith("NEW"))
                            Account = Account.Substring(3, Account.Length - 3);
                        else
                        {
                            Client.Socket.Disconnect();
                            return;
                        }
                    }

                    if (Account.GetHashCode() != pMsg->AccountUID)
                    {
                        Client.Socket.Disconnect();
                        return;
                    }

                    Byte Face = 67;
                    if (pMsg->Look / 1000 == 2)
                        Face = 201;

                    Boolean ValidName = true;

                    if (Name.IndexOfAny(new Char[] { ' ', '[', ']', '.', ',', ':', '*', '?', '"', '<', '>', '|', '/', '\\' }) > -1)
                        ValidName = false;

                    if (ValidName)
                    {
                        Boolean NameInUse = false;
                        String[] Characters = Directory.GetFiles(Program.RootPath + "\\Characters", "*.chr");
                        foreach (String Character in Characters)
                        {
                            String CharName = Character.Replace(Program.RootPath + "\\Characters\\", "").Replace(".chr", "");
                            if (CharName == Name)
                            {
                                NameInUse = true;
                                break;
                            }
                        }

                        if (!NameInUse)
                        {
                            Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "ANSWER_OK", MsgTalk.Channel.Register, 0x000000));
                            if (!Database.Register(Account, Name, (Face * 10000) + pMsg->Look, (Byte)pMsg->Profession))
                            {
                                Client.Socket.Disconnect();
                                return;
                            }
                            Program.Log("Creation of " + Name + ", with " + Client.Account + " (" + Client.Socket.IpAddress + ").");
                        }
                        else
                            Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Client.GetStr("STR_NAME_USED"), MsgTalk.Channel.Register, 0xFF0000));
                    }
                    else
                        Client.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", Client.GetStr("STR_INVALID_NAME"), MsgTalk.Channel.Register, 0xFF0000));
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
