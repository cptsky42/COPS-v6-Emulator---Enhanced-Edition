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
    public static partial class Database
    {
        /// <summary>
        /// All the banned IP addresses (from, to).
        /// </summary>
        private static Tuple<UInt32, UInt32>[] sBannedIPs = new Tuple<UInt32, UInt32>[0];

        /// <summary>
        /// Load the banned IP addresses.
        /// </summary>
        public static void GetBannedIPs()
        {
            if (!File.Exists(Program.RootPath + "/BannedIPs.list"))
            {
                var stream = File.Create(Program.RootPath + "/BannedIPs.list");
                stream.Dispose();
            }

            String[] lines = File.ReadAllLines(Program.RootPath + "/BannedIPs.list");
            sBannedIPs = new Tuple<UInt32, UInt32>[lines.Length];

            for (int i = 0; i < lines.Length; ++i)
            {
                String[] parts = lines[i].Split('.');

                UInt32 from = 0;
                UInt32 to = 0;

                from += Byte.Parse(parts[0]) * 0xFF000000U;
                if (parts[1] != "*")
                {
                    from += Byte.Parse(parts[1]) * 0x00FF0000U;
                    if (parts[2] != "*")
                    {
                        from += Byte.Parse(parts[2]) * 0x0000FF00U;
                        if (parts[3] != "*")
                        {
                            from += Byte.Parse(parts[3]);
                            to = from;
                        }
                        else
                            to = from + 0x000000FFU;
                    }
                    else
                        to = from + 0x0000FFFFU;
                }
                else
                    to = from + 0x00FFFFFFU;

                sBannedIPs[i] = new Tuple<UInt32, UInt32>(from, to);
            }
        }

        /// <summary>
        /// Determine whether or not the IP address is banned.
        /// </summary>
        /// <param name="aIPAddress">The IP address to check.</param>
        /// <returns>TRUE if the IP address is banned. FALSE otherwise.</returns>
        public static Boolean IsBanned(String aIPAddress)
        {
            String[] parts = aIPAddress.Split('.');
            if (parts.Length != 4)
                return false;

            UInt32 ip = (Byte.Parse(parts[0]) * 0xFF000000U) +
                (Byte.Parse(parts[1]) * 0x00FF0000U) +
                (Byte.Parse(parts[2]) * 0x0000FF00U) +
                (Byte.Parse(parts[3]));

            foreach (Tuple<UInt32, UInt32> tuple in sBannedIPs)
            {
                if (ip >= tuple.Item1 && ip <= tuple.Item2)
                    return true;
            }

            return false;
        }
    }
}