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
        public static void GetAllWeaponSkills()
        {
            try
            {
                Console.Write("Loading all weapon skills in memory...  ");

                DirectoryInfo DI = new DirectoryInfo(Program.RootPath + "\\WeaponSkills\\");
                FileInfo[] WeaponSkillFiles = DI.GetFiles("*.ws");

                World.AllWeaponSkills = new Dictionary<Int32, WeaponSkill>(WeaponSkillFiles.Length);
                foreach (FileInfo WeaponSkillFile in WeaponSkillFiles)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    Xml AMSXml = new Xml(WeaponSkillFile.FullName);
                    AMSXml.RootName = "WeaponSkill";

                    WeaponSkill WeaponSkill = new WeaponSkill(
                        AMSXml.GetValue("Informations", "UniqId", -1),
                        AMSXml.GetValue("Informations", "OwnerUID", -1),
                        (Int16)AMSXml.GetValue("Informations", "Type", 0),
                        (Byte)AMSXml.GetValue("Informations", "Level", 0),
                        AMSXml.GetValue("Informations", "Exp", 0),
                        (Byte)AMSXml.GetValue("Informations", "OldLevel", 0),
                        AMSXml.GetValue("Informations", "Unlearn", false));

                    if (WeaponSkill.OwnerUID == 0)
                        WeaponSkill.Delete(WeaponSkill.UniqId);

                    if (!World.AllWeaponSkills.ContainsKey(WeaponSkill.UniqId))
                        World.AllWeaponSkills.Add(WeaponSkill.UniqId, WeaponSkill);

                    AMSXml = null;
                }
                WeaponSkillFiles = null;
                DI = null;

                while (File.Exists(Program.RootPath + "\\WeaponSkills\\" + World.LastWeaponSkillUID.ToString() + ".ws") ||
                         World.AllItems.ContainsKey(World.LastWeaponSkillUID))
                {
                    World.LastWeaponSkillUID++;
                }

                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
