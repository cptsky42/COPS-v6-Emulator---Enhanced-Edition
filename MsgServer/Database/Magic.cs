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
        public static void GetAllMagics()
        {
            try
            {
                Console.Write("Loading all magics in memory...  ");

                DirectoryInfo DI = new DirectoryInfo(Program.RootPath + "\\Magics\\");
                FileInfo[] MagicFiles = DI.GetFiles("*.mgc");

                World.AllMagics = new Dictionary<Int32, Magic>(MagicFiles.Length);
                foreach (FileInfo MagicFile in MagicFiles)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    Xml AMSXml = new Xml(MagicFile.FullName);
                    AMSXml.RootName = "Magic";

                    Magic Magic = new Magic(
                        AMSXml.GetValue("Informations", "UniqId", -1),
                        AMSXml.GetValue("Informations", "OwnerUID", -1),
                        (Int16)AMSXml.GetValue("Informations", "Type", 0),
                        (Byte)AMSXml.GetValue("Informations", "Level", 0),
                        AMSXml.GetValue("Informations", "Exp", 0),
                        (Byte)AMSXml.GetValue("Informations", "OldLevel", 0),
                        AMSXml.GetValue("Informations", "Unlearn", false));

                    if (Magic.OwnerUID == 0)
                        Magic.Delete(Magic.UniqId);

                    if (!World.AllMagics.ContainsKey(Magic.UniqId))
                        World.AllMagics.Add(Magic.UniqId, Magic);

                    AMSXml = null;
                }
                MagicFiles = null;
                DI = null;

                while (File.Exists(Program.RootPath + "\\Magics\\" + World.LastMagicUID.ToString() + ".mgc") ||
                         World.AllItems.ContainsKey(World.LastMagicUID))
                {
                    World.LastMagicUID++;
                }

                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
