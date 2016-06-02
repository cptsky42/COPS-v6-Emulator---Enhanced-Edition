// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.IO;
using MoonSharp.Interpreter;

namespace COServer.Script
{
    public class TaskHandler
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(TaskHandler));

        /// <summary>
        /// All tasks of the server.
        /// </summary>
        public static readonly Dictionary<Int32, Task> AllTasks = new Dictionary<Int32, Task>();

        /// <summary>
        /// Load all tasks in memory.
        /// </summary>
        public static void LoadAllTasks()
        {
            String[] scripts = Directory.GetFiles(Program.RootPath + "/Tasks", "*.lua");

            for (Int32 i = 0; i < scripts.Length; i++)
            {
                FileInfo file = new FileInfo(scripts[i]);
                Int32 uid = Int32.Parse(file.Name.Replace(".lua", ""));

                try
                {
                    NpcTask task = new NpcTask(uid, file.FullName);
                    if (!AllTasks.ContainsKey(uid))
                        AllTasks.Add(uid, task);
                }
                catch (SyntaxErrorException exc)
                {
                    sLogger.Error("Failed to load the task {0}. Error: {1}",
                        uid, exc.Message);
                }
            } 
        }
    }
}
