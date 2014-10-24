// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using COServer.Network;
using COServer.Entities;
using AMS.Profile;

namespace COServer
{
    public class Magic
    {
        private Int32 UniqIdValue = -1;
        private Int32 OwnerUIDValue = -1;
        private Int16 TypeValue = 0;
        private Byte LevelValue = 0;
        private Int32 ExpValue = 0;
        private Byte OldLevelValue = 0;
        private Boolean UnlearnValue = false;

        public Int32 UniqId { get { return this.UniqIdValue; } }
        public Int32 OwnerUID { get { return this.OwnerUIDValue; } set { this.OwnerUIDValue = value; Save("OwnerUID", value); } }
        public Int16 Type { get { return this.TypeValue; } }
        public Byte Level { get { return this.LevelValue; } set { this.LevelValue = value; Save("Level", value); } }
        public Int32 Exp { get { return this.ExpValue; } set { this.ExpValue = value; Save("Exp", value); } }
        public Byte OldLevel { get { return this.OldLevelValue; } set { this.OldLevelValue = value; Save("OldLevel", value); } }
        public Boolean Unlearn { get { return this.UnlearnValue; } set { this.UnlearnValue = value; Save("Unlearn", value); } }

        public Magic(Int32 UniqId, Int32 OwnerUID, Int16 Type, Byte Level, Int32 Exp, Byte OldLevel, Boolean Unlearn)
        {
            this.UniqIdValue = UniqId;
            this.OwnerUIDValue = OwnerUID;
            this.TypeValue = Type;
            this.LevelValue = Level;
            this.ExpValue = Exp;
            this.OldLevelValue = OldLevel;
            this.UnlearnValue = Unlearn;
        }

        ~Magic()
        {

        }

        public void Save()
        {
            try
            {
                Xml AMSXml = new Xml(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc");
                AMSXml.RootName = "Magic";

                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", "OwnerUID", OwnerUIDValue);
                    AMSXml.SetValue("Informations", "Type", TypeValue);
                    AMSXml.SetValue("Informations", "Level", LevelValue);
                    AMSXml.SetValue("Informations", "Exp", ExpValue);
                    AMSXml.SetValue("Informations", "OldLevel", OldLevelValue);
                    AMSXml.SetValue("Informations", "Unlearn", UnlearnValue);
                }
                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public void Save(String Entry, Int32 Value)
        {
            try
            {
                Xml AMSXml = new Xml(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc");
                AMSXml.RootName = "Magic";

                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", Entry, Value);
                }
                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public void Save(String Entry, Boolean Value)
        {
            try
            {
                Xml AMSXml = new Xml(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc");
                AMSXml.RootName = "Magic";

                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", Entry, Value);
                }
                AMSXml = null;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }

        public static Magic Create(Int32 OwnerUID, Int16 Type, Byte Level, Int32 Exp, Byte OldLevel, Boolean Unlearn)
        {
            try
            {
                Int32 UniqId = World.LastMagicUID;
                World.LastMagicUID++;

                while (File.Exists(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc") ||
                    World.AllMagics.ContainsKey(UniqId))
                {
                    UniqId = World.LastMagicUID;
                    World.LastMagicUID++;
                }

                Magic Magic = new Magic(UniqId, OwnerUID, Type, Level, Exp, OldLevel, Unlearn);
                World.AllMagics.Add(Magic.UniqId, Magic);

                Xml AMSXml = new Xml(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc");
                AMSXml.RootName = "Magic";

                using (AMSXml.Buffer())
                {
                    AMSXml.SetValue("Informations", "UniqId", UniqId);
                    AMSXml.SetValue("Informations", "OwnerUID", OwnerUID);
                    AMSXml.SetValue("Informations", "Type", Type);
                    AMSXml.SetValue("Informations", "Level", Level);
                    AMSXml.SetValue("Informations", "Exp", Exp);
                    AMSXml.SetValue("Informations", "OldLevel", OldLevel);
                    AMSXml.SetValue("Informations", "Unlearn", Unlearn);
                }
                AMSXml = null;
                return Magic;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Delete(Int32 UniqId)
        {
            if (File.Exists(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc"))
                File.Delete(Program.RootPath + "\\Magics\\" + UniqId.ToString() + ".mgc");

            if (World.AllMagics.ContainsKey(UniqId))
            {
                Player Owner = null;
                if (World.AllPlayers.TryGetValue(World.AllMagics[UniqId].OwnerUID, out Owner))
                    Owner.DelMagic(UniqId, true);
                else
                    lock (World.AllMagics) { World.AllMagics.Remove(UniqId); }
            }
        }
    }
}
