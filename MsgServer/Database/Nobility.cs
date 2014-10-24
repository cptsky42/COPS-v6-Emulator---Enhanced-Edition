// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using AMS.Profile;

namespace COServer
{
    public partial class Database
    {
        public static void GetNobilityRank()
        {
            try
            {
                Console.Write("Loading nobility rank in memory...  ");

                DirectoryInfo DI = new DirectoryInfo(Program.RootPath + "\\Characters\\");
                FileInfo[] CharFiles = DI.GetFiles("*.chr");

                World.NobilityRank = new Dictionary<Int32, Nobility.Info>(CharFiles.Length);
                foreach (FileInfo CharFile in CharFiles)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    Xml AMSXml = new Xml(CharFile.FullName);
                    AMSXml.RootName = "Character";

                    Nobility.Info Info = new Nobility.Info()
                    {
                        UniqId = AMSXml.GetValue("Informations", "UniqId", -1),
                        Name = AMSXml.GetValue("Informations", "Name", "NULL"),
                        Look = UInt32.Parse(AMSXml.GetValue("Informations", "Look", "0")),
                        Donation = Int64.Parse(AMSXml.GetValue("Informations", "NobilityDonation", "0")),
                        Rank = 0,
                        Position = 0
                    };

                    if (Info.UniqId == -1)
                    {
                        Console.WriteLine(CharFile.FullName.Replace(Program.RootPath + "\\Characters\\", "") + " may be corrupted.");
                        AMSXml = null;
                        continue;
                    }

                    if (!World.NobilityRank.ContainsKey(Info.UniqId) && Info.Donation > 0)
                        World.NobilityRank.Add(Info.UniqId, Info);

                    AMSXml = null;
                }
                CharFiles = null;
                DI = null;

                Nobility.Rank.ResetPosition();
                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
