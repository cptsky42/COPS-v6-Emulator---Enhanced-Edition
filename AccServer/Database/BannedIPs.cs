// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;

namespace COServer
{
    public partial class Database
    {
        public static String[] BannedIPs;

        public static void GetBannedIPs()
        {
            try
            {
                if (!File.Exists(Program.RootPath + "\\BannedIPs.list"))
                    File.Create(Program.RootPath + "\\BannedIPs.list");

                BannedIPs = File.ReadAllLines(Program.RootPath + "\\BannedIPs.list");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Boolean IsIPBanned(String IP)
        {
            try
            {
                for (Int32 i = 0; i < BannedIPs.Length; i++)
                {
                    String[] Splitter = BannedIPs[i].Split('.');
                    String[] TheSplit = IP.Split('.');

                    //Internet Protocol Adresse
                    if (BannedIPs[i] == IP)
                        return true;

                    //Internet Protocol with Joker
                    if (Splitter[0] == TheSplit[0])
                    {
                        if (Splitter[1] == "*")
                            return true;
                        else
                            if (Splitter[1] == TheSplit[1])
                            {
                                if (Splitter[2] == "*")
                                    return true;
                                else
                                    if (Splitter[2] == TheSplit[2])
                                    {
                                        if (Splitter[3] == "*")
                                            return true;
                                    }
                            }
                    }
                }
                return false;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return false; }
        }
    }
}