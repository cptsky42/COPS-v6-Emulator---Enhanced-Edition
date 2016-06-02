using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace CO2Tools
{
    public class Event : IComparable
    {
        public const Byte MAX_EVENT = 100;
        public static Byte LastEventId = 0;

        public UInt32 Id = 0;
        public UInt32 ActionID = 0;

        public static Event[] getAllEvents()
        {
            List<Event> events = new List<Event>();

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='" + Program.DATABASE + "';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id` FROM `cq_action` WHERE `id` >= 2000000 AND `id` < 3000000", connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Event _event = new Event();
                    _event.Id = ++LastEventId;
                    _event.ActionID = Convert.ToUInt32(reader["id"]);

                    Console.WriteLine("Found event {0}...", _event.ActionID);
                    events.Add(_event);
                }
            }

            events.Sort();
            return events.ToArray();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Event e = obj as Event;
            return (int)ActionID - (int)e.ActionID;
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
            stream.WriteLine("function processEvent{0:000}(client)", Id);
            stream.WriteLine();

            Action.processAction(stream, "    ", null, ActionID);
            stream.WriteLine();
            stream.WriteLine("end");
        }
    }
}
