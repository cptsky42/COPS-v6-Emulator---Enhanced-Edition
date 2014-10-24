// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using AMS.Profile;

namespace COServer
{
    public partial class Database
    {
        public static void GetAllSyndicates()
        {
            try
            {
                Console.Write("Loading all syndicates in memory...  ");

                DirectoryInfo DI = new DirectoryInfo(Program.RootPath + "\\Syndicates\\");
                FileInfo[] SynFiles = DI.GetFiles("*.syn");

                World.AllSyndicates = new Dictionary<Int16, Syndicate.Info>(SynFiles.Length);
                foreach (FileInfo SynFile in SynFiles)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    Xml AMSXml = new Xml(SynFile.FullName);
                    AMSXml.RootName = "Syndicate";

                    if (AMSXml.GetValue("Informations", "DelFlag", false))
                    {
                        Int16 UniqId = (Int16)AMSXml.GetValue("Informations", "UniqId", -1);
                        AMSXml = null;

                        Syndicate.Delete(UniqId);
                        continue;
                    }

                    Int32[] Enemies = new Int32[5];
                    Int32[] Allies = new Int32[5];

                    for (SByte i = 0; i < 5; i++)
                    {
                        Enemies[i] = AMSXml.GetValue("Informations", "Enemy" + i.ToString(), 0);
                        Allies[i] = AMSXml.GetValue("Informations", "Ally" + i.ToString(), 0);
                    }

                    Syndicate.Info Info = new Syndicate.Info(
                        (Int16)AMSXml.GetValue("Informations", "UniqId", -1),
                        AMSXml.GetValue("Informations", "Name", "NULL"),
                        AMSXml.GetValue("Informations", "Announce", "NULL"),
                        AMSXml.GetValue("Informations", "LeaderUID", -1),
                        AMSXml.GetValue("Informations", "LeaderName", "NULL"),
                        AMSXml.GetValue("Informations", "Money", 0),
                        (Int16)AMSXml.GetValue("Informations", "FealtySynUID", -1),
                        Enemies,
                        Allies,
                        AMSXml);

                    if (Info.Leader.UniqId == 0)
                        Syndicate.Delete(Info.UniqId);

                    if (!World.AllSyndicates.ContainsKey(Info.UniqId))
                        World.AllSyndicates.Add(Info.UniqId, Info);
                }
                SynFiles = null;
                DI = null;

                DI = new DirectoryInfo(Program.RootPath + "\\Characters\\");
                FileInfo[] ChrFiles = DI.GetFiles("*.chr");

                foreach (FileInfo ChrFile in ChrFiles)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    Xml AMSXml = new Xml(ChrFile.FullName);
                    AMSXml.RootName = "Character";

                    Int16 SynUID = (Int16)AMSXml.GetValue("Informations", "SynUID", 0);
                    if (SynUID == 0)
                    {
                        AMSXml = null;
                        continue;
                    }

                    Syndicate.Info Info = null;
                    if (!World.AllSyndicates.TryGetValue(SynUID, out Info))
                    {
                        using (AMSXml.Buffer())
                        {
                            AMSXml.SetValue("Informations", "SynUID", 0);
                            AMSXml.SetValue("Informations", "SynRank", 0);
                            AMSXml.SetValue("Informations", "SynDonation", 0);
                        }
                        AMSXml = null;
                        continue;
                    }

                    Byte Rank = (Byte)AMSXml.GetValue("Informations", "SynRank", 0);
                    Int32 Donation = AMSXml.GetValue("Informations", "SynDonation", 0);

                    Int32 UniqId = AMSXml.GetValue("Informations", "UniqId", -1);
                    String Name = AMSXml.GetValue("Informations", "Name", "NULL");
                    Byte Level = (Byte)AMSXml.GetValue("Informations", "Level", 0);
                    AMSXml = null;

                    if (UniqId == -1)
                        continue;

                    Syndicate.Member Member = new Syndicate.Member(UniqId, Name, Level, Rank, Donation);
                    if (Info.Leader.UniqId == UniqId)
                        Info.Leader = Member;
                    else
                    {
                        if (!Info.Members.ContainsKey(UniqId))
                            Info.Members.Add(UniqId, Member);
                    }
                }
                ChrFiles = null;
                DI = null;

                while (File.Exists(Program.RootPath + "\\Syndicates\\" + World.LastSyndicateUID.ToString() + ".syn") ||
                         World.AllSyndicates.ContainsKey(World.LastSyndicateUID))
                {
                    World.LastSyndicateUID++;
                }

                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
