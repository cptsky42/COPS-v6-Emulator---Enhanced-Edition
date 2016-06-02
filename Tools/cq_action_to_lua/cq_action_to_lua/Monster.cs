using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace CO2Tools
{
    public class Monster : IComparable, IObj
    {
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

        public static Monster[] getAllMonsters()
        {
            List<Monster> monsters = new List<Monster>();
            List<UInt32> validIDs = new List<UInt32>();

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='copsv6';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id` FROM `monstertype`", connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                    validIDs.Add(Convert.ToUInt32(reader["id"]));
            }

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='" + Program.DATABASE + "';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id`, `name`, `action` FROM `cq_monstertype`", connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Monster monster = new Monster();
                    monster.Id = Convert.ToUInt32(reader["id"]);
                    monster.Name = Convert.ToString(reader["name"]);
                    monster.ActionID = Convert.ToUInt32(reader["action"]);

                    if (validIDs.Contains(monster.Id))
                    {
                        Console.WriteLine("Found {0} ({1})...", monster.Name, monster.Id);
                        monsters.Add(monster);
                    }
                }
            }

            monsters.Sort();
            return monsters.ToArray();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Monster monster = obj as Monster;
            return (int)Id - (int)monster.Id;
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
            stream.WriteLine("function Monster{0}_OnDie(self, client)", Id);
            stream.WriteLine("    name = \"{0}\"", Name);
            stream.WriteLine();

            Action.processAction(stream, "    ", this, ActionID);
            stream.WriteLine();
            stream.WriteLine("end");
        }
    }
}
