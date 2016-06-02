// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Timers;

namespace COServer
{
    /// <summary>
    /// A monitor for the networking I/O.
    /// </summary>
    public class NetworkMonitor
    {
        /// <summary>
        /// The title of the console.
        /// </summary>
        private String mTitleFmt;

        /// <summary>
        /// The number of bytes received by the server.
        /// </summary>
        private int mRecvBytes = 0;
        /// <summary>
        /// The number of bytes sent by the server.
        /// </summary>
        private int mSentBytes = 0;

        /// <summary>
        /// The timer that will update the title periodically.
        /// </summary>
        private Timer mTimer;
        /// <summary>
        /// The interval (in ms) of the update event.
        /// </summary>
        private int mInterval;

        /// <summary>
        /// Create a new monitor for the networking I/O.
        /// </summary>
        /// <param name="aInterval">The interval (in ms) of the update event.</param>
        public NetworkMonitor(int aInterval)
        {
            mTitleFmt = Console.Title + " (↑{0:F2} kbps, ↓{1:F2} kbps)";
            mInterval = aInterval;

            mTimer = new Timer(mInterval);
            mTimer.Elapsed += new ElapsedEventHandler(UpdateStats);
            mTimer.Start();
        }

        /// <summary>
        /// Called by the timer.
        /// </summary>
        private void UpdateStats(Object aSender, ElapsedEventArgs e)
        {
            double download = ((mRecvBytes / (double)mInterval) * 8.0) / 1024.0;
            double upload = ((mSentBytes / (double)mInterval) * 8.0) / 1024.0;

            mRecvBytes = 0;
            mSentBytes = 0;

            Console.Title = String.Format(mTitleFmt, upload, download);
        }

        /// <summary>
        /// Signal to the monitor that aLength bytes were sent.
        /// </summary>
        /// <param name="aLength">The number of bytes sent.</param>
        public void Send(int aLength)
        {
            mSentBytes += aLength;
        }

        /// <summary>
        /// Signal to the monitor that aLength bytes were received.
        /// </summary>
        /// <param name="aLength">The number of bytes received.</param>
        public void Receive(int aLength)
        {
            mRecvBytes += aLength;
        }
    }
}
