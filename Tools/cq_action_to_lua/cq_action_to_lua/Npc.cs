using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace CO2Tools
{
    class Npc : IComparable, IObj
    {
        public const Int32 MAX_TASKS = 8;

        public UInt32 Id = 0;
        public String Name = "Unknown";
        public Task[] Tasks = new Task[MAX_TASKS];

        public UInt32[] Links = new UInt32[Byte.MaxValue];
        private Byte LastIndex = 1;

        public Byte pushTaskId(UInt32 taskId)
        {
            if (taskId == 0)
                return 255;

            if (LastIndex == 255)
            {
                Console.WriteLine("WARNING ! LINKS FULL FOR {0} [{1}] !", Name, Id);
                return 255;
            }

            for (Int32 i = 0; i < Byte.MaxValue; ++i)
            {
                // avoid duplicate tasks
                if (Links[i] == taskId)
                    return (Byte)i;
            }

            Links[LastIndex] = taskId;
            return LastIndex++;
        }

        public static Npc[] getAllNpcs()
        {
            List<Npc> npcs = new List<Npc>();

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='" + Program.DATABASE + "';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id`, `name`, `task0`, `task1`, `task2`, `task3`, `task4`, `task5`, `task6`, `task7` FROM `cq_npc`", connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Npc npc = new Npc();
                    npc.Id = Convert.ToUInt32(reader["id"]);
                    npc.Name = Convert.ToString(reader["name"]);

                    Console.WriteLine("Found {0} ({1})...", npc.Name, npc.Id);

                    for (Int32 i = 0; i < MAX_TASKS; ++i)
                    {
                        UInt32 taskId = Convert.ToUInt32(reader["task" + i]);
                        if (taskId != 0)
                            npc.Tasks[i] = Task.getTask(taskId);
                    }

                    npcs.Add(npc);
                }
            }

            npcs.Sort();
            return npcs.ToArray();
        }

        public static Npc[] getAllDynaNpcs()
        {
            List<Npc> npcs = new List<Npc>();

            using (MySqlConnection connection = new MySqlConnection("Server=" + Program.HOST + ";Database='" + Program.DATABASE + "';Username='" + Program.USERNAME + "';Password='" + Program.PASSWORD + "';"))
            {
                MySqlCommand cmd = null;
                MySqlDataReader reader = null;

                connection.Open();

                cmd = new MySqlCommand("SELECT `id`, `name`, `task0`, `task1`, `task2`, `task3`, `task4`, `task5`, `task6`, `task7` FROM `cq_dynanpc` WHERE `type` = 02 OR `type` = 10 OR `type` = 26", connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Npc npc = new Npc();
                    npc.Id = Convert.ToUInt32(reader["id"]);
                    npc.Name = Convert.ToString(reader["name"]);

                    Console.WriteLine("Found {0} ({1})...", npc.Name, npc.Id);

                    for (Int32 i = 0; i < MAX_TASKS; ++i)
                    {
                        UInt32 taskId = Convert.ToUInt32(reader["task" + i]);
                        if (taskId != 0)
                            npc.Tasks[i] = Task.getTask(taskId);
                    }

                    npcs.Add(npc);
                }
            }

            npcs.Sort();
            return npcs.ToArray();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Npc npc = obj as Npc;
            return (int)Id - (int)npc.Id;
        }

        private const string GET_MONEY_FCT = "getMoney(client)";
        private const string GET_PROFESSION_FCT = "getProfession(client)";
        private const string GET_SEX_FCT = "getSex(client)";
        private const string GET_PKPOINTS_FCT = "getPkPoints(client)";
        private const string IS_MARRIED_FCT = "isMarried(client)";

        public bool hasTask()
        {
            foreach (Task task in Tasks)
            {
                if (task != null)
                    return true;
            }
            return false;
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
            stream.WriteLine("function processTask{0}(client, idx)", Id);
            stream.WriteLine("    name = \"{0}\"", Name);
            stream.WriteLine("    face = 1"); // TODO
            stream.WriteLine();

            generateActivation(stream); // idx0
            stream.WriteLine();

            for (Int32 i = 1; i < Byte.MaxValue; ++i)
            {
                UInt32 taskId = Links[i];
                if (taskId == 0)
                    continue;

                Task task = Task.getTask(taskId);
                if (task == null)
                    continue;

                stream.WriteLine("    elseif (idx == {0}) then", i);
                stream.WriteLine();

                //if (Id == 10003 && (i == 15 || i == 20 || i == 21 || i == 28 || i == 35)) // skip few subtasks for guild manager...
                //{
                //    stream.WriteLine("        -- kind of too complex...");
                //    stream.WriteLine();
                //    continue;
                //}

                bool addedValidation = generateTestTask(stream, task, true);
                Action.processAction(stream, addedValidation ? "            " : "        ", this, task.IdNext);

                stream.WriteLine();

                if (addedValidation)
                {
                    stream.WriteLine("        end");
                    stream.WriteLine();
                }
            }

            stream.WriteLine("    end");
            stream.WriteLine();
            stream.WriteLine("end");
        }

        private void generateActivation(StreamWriter stream)
        {
            stream.WriteLine("    if (idx == 0) then");
            stream.WriteLine();

            int TASKS_LENGTH = 0;
            foreach (Task task in Tasks)
                if (task != null)
                    ++TASKS_LENGTH;

            int taskCount = 0;
            int validationCount = 0;
            foreach (Task task in Tasks)
            {
                if (task == null)
                    continue;

                string ident = "        ";

                bool addedValidation = generateTestTask(stream, task, taskCount++ == 0);
                if (addedValidation)
                {
                    ident = "            ";
                    ++validationCount;
                }

                // last action after validations
                if (!addedValidation && validationCount > 0)
                {
                    stream.WriteLine();
                    stream.WriteLine("        else");
                    stream.WriteLine();
                    ident = "         ";
                }

                Action.processAction(stream, ident, this, task.IdNext);

                if ((!addedValidation && validationCount > 0) || (addedValidation && TASKS_LENGTH == 1))
                {
                    stream.WriteLine();
                    stream.WriteLine("        end");
                }
            }
        }

        private bool generateTestTask(StreamWriter stream, Task task, bool isFirst)
        {
            const Int32 VAL_SKIPTEST = 999;

            // no validation at all...
            if (String.IsNullOrEmpty(task.ItemName1) && String.IsNullOrEmpty(task.ItemName2) &&
                task.Money == 0 && task.Profession == 0 && (task.Sex == 0 || task.Sex == VAL_SKIPTEST) &&
                task.MinPk == -100000 && task.MaxPk == 100000 && task.Team == VAL_SKIPTEST && 
                task.Marriage == -1)
                return false;

            if (isFirst)
                stream.Write("        if ");
            else
            {
                stream.WriteLine();
                stream.Write("        elseif ");
            }

            if (!String.IsNullOrEmpty(task.ItemName1))
            {
                stream.Write("hasTaskItem(client, \"{0}\") and ", task.ItemName1);

                if (!String.IsNullOrEmpty(task.ItemName2))
                    stream.Write("hasTaskItem(client, \"{0}\") and ", task.ItemName2);
            }

            stream.Write("({0} >= {1})", GET_MONEY_FCT, task.Money);

            if (task.Profession != 0)
                stream.Write(" and ({0} == {1})", GET_PROFESSION_FCT, task.Profession);

            #if OLD_SEX_VALIDATION // commented in Demon source
            if (task.Sex != 0)
                stream.Write(" and ((1 << {0}) & {1} == 0)", GET_SEX_FCT, task.Sex);
            #endif

            //if (task.Sex != VAL_SKIPTEST)
            //    stream.Write(" and ({0} == {1})", GET_SEX_FCT, task.Sex);

            if (task.MinPk != -100000 && task.MaxPk != 100000) // NOT IN REAL CO2 SOURCE ! BUT ADD SHITTY VALIDATION !
                stream.Write(" and ({0} >= {1} and {2} <= {3})", GET_PKPOINTS_FCT, task.MinPk, GET_PKPOINTS_FCT, task.MaxPk);

            if (task.Team != VAL_SKIPTEST)
            {
                /*			switch(pTask->GetInt(TASKDATA_TEAM))
                            {
                            case 0:
                            case 1:
                                {
                                    int nMembers	=this->GetTeamMembers();
                                    if (nMembers != 0 && nMembers != 1)
                                        return false;
                                }
                                break;

                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                {
                                    if (this->GetTeamMembers() != m_Info.nTeam)
                                        return false;
                                }
                                break;

                            case 6:		// no team members check
                                break;

                            case 7:		// couple
                                {
                                    CTeam* pTeam	=this->GetTeam();
                                    if (!pTeam || !pTeam->IsCoupleTeam())
                                        return false;
                                }
                                break;

                            case 8:		// brother
                                {
                                    CTeam* pTeam	=this->GetTeam();
                                    if (!pTeam || !pTeam->IsBrotherTeam())
                                        return false;
                                }
                                break;

                            default:
                                return false;
                            } // switch
                 */
        }
/*
		if(pTask->GetInt(TASKDATA_METEMPSYCHOSIS) && this->GetMetempsychosis() < pTask->GetInt(TASKDATA_METEMPSYCHOSIS))
		{
			if (g_cSyndicate.GetLevel(this->GetSyndicateID()) != m_Info.nSyndicate)
				return false;
		}
*/
            if (task.Marriage != -1)
                stream.Write(" and ({0}{1})", task.Marriage == 0 ? "not " : "", IS_MARRIED_FCT);

            stream.WriteLine(" then");
            stream.WriteLine();

            return true;
        }
    }
}
