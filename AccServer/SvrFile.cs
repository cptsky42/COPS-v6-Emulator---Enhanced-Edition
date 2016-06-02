// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.IO;

namespace COServer
{
    /// <summary>
    /// A SvrInfo object contains all the information about a MsgServer.
    /// </summary>
    public class SvrInfo
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(SvrInfo));

        /// <summary>
        /// The name of the server.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The IP address of the server.
        /// </summary>
        public readonly String IPAddress;

        /// <summary>
        /// The port of the server.
        /// </summary>
        public readonly UInt16 Port;

        /// <summary>
        /// Creates a new SvrInfo object by loading the specified SVR file.
        /// </summary>
        /// <param name="aPath">The path of the SVR file to load.</param>
        public SvrInfo(String aPath)
        {
            if (!File.Exists(aPath))
            {
                sLogger.Error("The file does not exist. ({0})", aPath);
                throw new FileNotFoundException("The file does not exist", aPath);
            }

            using (Ini reader = new Ini(aPath))
            {
                Name = reader.ReadString("Server", "Name");
                IPAddress = reader.ReadString("Server", "IPAddress");
                Port = reader.ReadUInt16("Server", "Port");
            }
        }
    }
}
