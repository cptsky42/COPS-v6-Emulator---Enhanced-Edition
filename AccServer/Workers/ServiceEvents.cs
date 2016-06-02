// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace COServer.Workers
{
    public class ServiceEventsListener
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Global lock for the instance.
        /// </summary>
        private static object sLock = new object();
        /// <summary>
        /// Global instance.
        /// </summary>
        private static volatile ServiceEventsListener sInstance = null;

        /// <summary>
        /// Create the service events listener if it is not created yet.
        /// </summary>
        public static void Create()
        {
            if (sInstance == null)
            {
                lock (sLock)
                {
                    if (sInstance == null)
                        sInstance = new ServiceEventsListener();
                }
            }
        }

        /// <summary>
        /// The worker thread.
        /// </summary>
        private Thread mThread;

        /// <summary>
        /// Create a new worker thread for listening for a stop request.
        /// </summary>
        private ServiceEventsListener()
        {
            mThread = new Thread(Listen);
            mThread.Name = "ServiceEvents";
            mThread.IsBackground = true;
            mThread.Start();
        }

        /// <summary>
        /// Listen for a stop request.
        /// </summary>
        private void Listen()
        {
            String eventName = "STOP_COPS_ACCSERVER";
            sLogger.Info("Listening for event {0}.", eventName);

            var users = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var rule = new EventWaitHandleAccessRule(users,
                EventWaitHandleRights.Synchronize | EventWaitHandleRights.Modify,
                AccessControlType.Allow);
            var security = new EventWaitHandleSecurity();
            security.AddAccessRule(rule);

            bool createdNew = false;
            EventWaitHandle handle = new EventWaitHandle(
                false, EventResetMode.AutoReset,
                eventName,
                out createdNew, security);
            handle.WaitOne();

            sLogger.Info("Received stop request !");
            Server.Stop();
        }
    }
}
