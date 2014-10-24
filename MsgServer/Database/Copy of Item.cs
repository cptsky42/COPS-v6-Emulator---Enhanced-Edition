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
        public static void GetAllItems()
        {
            try
            {
                Console.Write("Loading all items in memory...  ");

                DirectoryInfo DI = new DirectoryInfo(Program.RootPath + "\\Items\\");
                FileInfo[] ItemFiles = DI.GetFiles("*.item");

                World.AllItems = new Dictionary<Int32, Item>(200000);
                foreach (FileInfo ItemFile in ItemFiles)
                {
                    Console.Write("\b{0}", Loading.NextChar());

                    Xml AMSXml = new Xml(ItemFile.FullName);
                    AMSXml.RootName = "Item";

                    Item Item = new Item(
                        AMSXml.GetValue("Informations", "UniqId", -1),
                        AMSXml.GetValue("Informations", "OwnerUID", -1),
                        (Byte)AMSXml.GetValue("Informations", "Position", 0),
                        AMSXml.GetValue("Informations", "Id", 0),
                        (Byte)AMSXml.GetValue("Informations", "Craft", 0),
                        (Byte)AMSXml.GetValue("Informations", "Bless", 0),
                        (Byte)AMSXml.GetValue("Informations", "Enchant", 0),
                        (Byte)AMSXml.GetValue("Informations", "Gem1", 0),
                        (Byte)AMSXml.GetValue("Informations", "Gem2", 0),
                        (Byte)AMSXml.GetValue("Informations", "Attr", 0),
                        (Int32)AMSXml.GetValue("Informations", "Restrain", 0),
                        (UInt16)AMSXml.GetValue("Informations", "CurDura", 0),
                        (UInt16)AMSXml.GetValue("Informations", "MaxDura", 0));

                    //New WH UID
                    if (Item.Position == 44)
                        Item.Position = 16;

                    if (Item.OwnerUID == 0)
                    {
                        File.Delete(ItemFile.FullName);
                        continue;
                    }

                    if (!World.AllItems.ContainsKey(Item.UniqId))
                        World.AllItems.Add(Item.UniqId, Item);
                    
                    AMSXml = null;
                }
                ItemFiles = null;
                DI = null;

                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
