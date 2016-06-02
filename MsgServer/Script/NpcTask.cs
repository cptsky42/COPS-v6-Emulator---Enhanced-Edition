// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using MoonSharp.Interpreter;

namespace COServer.Script
{
    public class NpcTask : Task
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(NpcTask));

        /// <summary>
        /// The unique ID of the task.
        /// </summary>
        private readonly Int32 mId;

        /// <summary>
        /// Create a new NPC task using the specified Lua script.
        /// </summary>
        /// <param name="aId">The unique ID of the task</param>
        /// <param name="aPath">The path of the Lua script</param>
        public NpcTask(Int32 aId, String aPath)
            : base(aPath, "processTask" + aId)
        {
            mId = aId;
        }

        /// <summary>
        /// Execute the task using the specified arguments.
        /// </summary>
        /// <param name="aClient">The client executing the task</param>
        /// <param name="aArgs">The arguments of the task</param>
        public override void Execute(Client aClient, params object[] aArgs)
        {
            int idx = (Byte)aArgs[0];

            sLogger.Info("Executing NPC task {0} with client={1}, idx={2}",
                mId, aClient, idx);

            try
            {
                sLogger.Debug("Calling function {0}", mFct);

                var env = GetEnvironment();
                var res = env.Call(env.Globals[mFct], aClient, idx);
            }
            catch (ScriptRuntimeException exc)
            {
                sLogger.Error("Moon# returned an error while calling {0}.\n{1}",
                    mFct, exc.Message);
            }
        }
    }
}
