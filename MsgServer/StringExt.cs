// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer
{
    public static class StringExt
    {
        /// <summary>
        /// Determine whether a string is valid or not.
        /// </summary>
        /// <param name="aStr">The string to validate.</param>
        /// <returns>True if the string is valid, false otherwise.</returns>
        public static bool IsValid(this String aStr)
        {
            if (aStr == null)
                return false;

            var str = Program.Encoding.GetBytes(aStr);
            for (int i = 0; i < str.Length; ++i)
            {
                var c = str[i];
                if (c >= 0x81 && c <= 0xFE)
                {
                    if (i + 1 >= str.Length)
                        return false;

                    var c2 = aStr[i + 1];
                    if (c2 < 0x40 && c2 > 0x7E && c2 < 0x80 && c2 > 0xFE)
                        return false;
                    else
                        ++i;
                }
                else
                {
                    if (c == 0x80)
                        return false;

                    if (c < ' ')
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determine whether a string is valid for a message or not.
        /// </summary>
        /// <param name="aStr">The string to validate.</param>
        /// <returns>True if the string is valid, false otherwise.</returns>
        public static bool IsValidMsg(this String aStr)
        {
            if (aStr == null)
                return false;

            var str = Program.Encoding.GetBytes(aStr);
            for (int i = 0; i < str.Length; ++i)
            {
                var c = str[i];

                if (c < ' ')
                    return false;

                switch ((char)c)
                {
                    case '\\':
                    case '\'':
                    case '"':
                        return false;
                }
            }

            return aStr.IsValid();
        }

        /// <summary>
        /// Determine whether a string is valid for a name or not.
        /// </summary>
        /// <param name="aStr">The string to validate.</param>
        /// <returns>True if the string is valid, false otherwise.</returns>
        public static bool IsValidName(this String aStr)
        {
            if (aStr == null)
                return false;

            var str = Program.Encoding.GetBytes(aStr);
            for (int i = 0; i < str.Length; ++i)
            {
                var c = str[i];

                if (c < ' ')
                    return false;

                switch ((char)c)
                {
                    case ' ':
                    case ';':
                    case ',':
                    case '/':
                    case '\\':
                    case '=':
                    case '%':
                    case '@':
                    case '\'':
                    case '"':
                    case '[':
                    case ']':
                        return false;
                }
            }

            if (aStr == "None")
                return false;

            if (aStr.Contains("GM"))
                return false;

            if (aStr.Contains("gm"))
                return false;

            if (aStr.Contains("PM"))
                return false;

            if (aStr.Contains("pm"))
                return false;

            if (aStr.Contains("SYSTEM"))
                return false;

            if (aStr.Contains("system"))
                return false;

            if (aStr.Contains("NPC"))
                return false;

            if (aStr.Contains("npc"))
                return false;

            return aStr.IsValid();
        }
    }
}
