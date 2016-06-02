using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace CO2Tools
{
    class Item : IComparable, IObj
    {
        public const Int32 MAX_TASKS = 8;

        public UInt32 Id = 0;
        public String Name = "Unknown";
        public UInt32 ActionID = 0;

        public Byte pushTaskId(UInt32 taskId)
        {
            if (taskId == 0)
                return 255;

            if (taskId != 0)
            {
                Console.WriteLine("ITEM ! LINKS NOT 0 FOR {0} [{1}] !", Name, Id);
                return 255;
            }

            return 0;
        }

        public static Item[] getAllItems()
        {
            List<Item> items = new List<Item>();

            //using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='copsv6';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            //{
            //    MySqlCommand cmd = null;
            //    MySqlDataReader reader = null;

            //    connection.Open();

            //    cmd = new MySqlCommand("SELECT `id`, `name`, `task` FROM `itemtype`", connection);
            //    reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Item item = new Item();
            //        item.Id = Convert.ToUInt32(reader["id"]);
            //        item.Name = Convert.ToString(reader["name"]);
            //        item.ActionID = Convert.ToUInt32(reader["task"]);

            //        Console.WriteLine("Found {0} ({1})...", item.Name, item.Id);
            //        items.Add(item);
            //    }
            //}

            using (FileStream stream = new FileStream("./ItemType.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream, Encoding.Default))
                {
                    UInt32 count = reader.ReadUInt32();
                    stream.Seek(count * sizeof(Int32), SeekOrigin.Current);

                    for (UInt32 i = 0; i < count; ++i)
                    {
                        Item item = new Item();

                        item.Id = reader.ReadUInt32();
                        item.Name = Encoding.Default.GetString(reader.ReadBytes(0x10)).Trim('\0');
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt32();
                        item.ActionID = reader.ReadUInt32();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadInt16();
                        reader.ReadInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadByte();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        stream.Seek(128, SeekOrigin.Current); // Description

                        Console.WriteLine("Found {0} ({1})...", item.Name, item.Id);
                        items.Add(item);
                    }
                }
            }

            items.Sort();
            return items.ToArray();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Item item = obj as Item;
            return (int)Id - (int)item.Id;
        }

        public bool hasTask()
        {
            return ActionID != 0;
        }

        public void generateScript(StreamWriter stream)
        {
            generateHeader(stream);
            generateFunction(stream);
        }

        private void generateHeader(StreamWriter stream)
        {
            stream.WriteLine("--");
            stream.WriteLine("-- ------ COPS v6 Emulator - Closed Source ------");
            stream.WriteLine("-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin");
            stream.WriteLine("--");
            stream.WriteLine("-- Generated from official database ({0}@{1})", Program.DATABASE, Program.HOST);
            stream.WriteLine("-- the {0}.", DateTime.Now);
            stream.WriteLine("--");
            stream.WriteLine("-- Please read the WARNING, DISCLAIMER and PATENTS");
            stream.WriteLine("-- sections in the LICENSE file.");
            stream.WriteLine("--");
            stream.WriteLine();
        }

        private void generateFunction(StreamWriter stream)
        {
            stream.WriteLine("function useItem{0}(self, client)", Id);
            stream.WriteLine("    name = \"{0}\"", Name);
            stream.WriteLine("    face = 1"); // TODO
            stream.WriteLine();

            Action.processAction(stream, "    ", this, ActionID);
            stream.WriteLine();
            stream.WriteLine("end");
        }
    }
}
