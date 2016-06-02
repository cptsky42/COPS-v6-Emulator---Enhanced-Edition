using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace CO2Tools
{
    class Task
    {
        public UInt32 IdNext;
        public UInt32 IdNext_Fail;
        public String ItemName1;
        public String ItemName2;
        public UInt32 Money;
        public UInt32 Profession;
        public Int32 Sex;
        public Int32 MinPk;
        public Int32 MaxPk;
        public UInt32 Team;
        public UInt32 Metempsychosis;
        //public SByte Query;
        public SByte Marriage;
        //public SByte ClientActive;

        public static Task getTask(UInt32 taskId)
        {
            if (taskId == 0)
                return null;

            Task task = new Task();

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='" + Program.DATABASE + "';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id_next`, `id_nextfail`, `itemname1`, `itemname2`, `money`, `profession`, `sex`, `min_pk`, `max_pk`, `team`, `metempsychosis`, `marriage` FROM `cq_task` WHERE `id` = " + taskId, connection);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    task.IdNext = Convert.ToUInt32(reader["id_next"]);
                    task.IdNext_Fail = Convert.ToUInt32(reader["id_nextfail"]);
                    task.ItemName1 = Convert.ToString(reader["itemname1"]);
                    task.ItemName2 = Convert.ToString(reader["itemname2"]);
                    task.Money = Convert.ToUInt32(reader["money"]);
                    task.Profession = Convert.ToUInt32(reader["profession"]);
                    task.Sex = Convert.ToInt32(reader["sex"]);
                    task.MinPk = Convert.ToInt32(reader["min_pk"]);
                    task.MaxPk = Convert.ToInt32(reader["max_pk"]);
                    task.Team = Convert.ToUInt32(reader["team"]);
                    task.Metempsychosis = Convert.ToUInt32(reader["metempsychosis"]);
                    task.Marriage = Convert.ToSByte(reader["marriage"]);
                }
                else
                {
                    Console.WriteLine("Missing task {0}", taskId);
                    return null;
                }
            }

            return task;
        }
    }
}
