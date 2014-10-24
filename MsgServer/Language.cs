// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;

namespace COServer
{
    public enum Language : byte
    {
        Fr = 0,
        En = 1,
    };

    public class STR
    {
        private static Dictionary<String, String>[] StrRes = new Dictionary<String, String>[2];

        public static String Get(String Key)
        {
            if (StrRes[0].ContainsKey(Key))
                return StrRes[0][Key];

            return Key;
        }

        public static String Get(Language Lang, String Key)
        {
            if (StrRes[(Byte)Lang].ContainsKey(Key))
                return StrRes[(Byte)Lang][Key];

            return Key;
        }

        public static void LoadStrRes()
        {
            try
            {
                String[] Lang = new String[] { "Fr", "En" };

                for (Int32 x = 0; x < Lang.Length; x++)
                {
                    String[] Lines = File.ReadAllLines(Program.RootPath + "\\StrRes\\" + Lang[x] + ".lang", Program.Encoding);
                    StrRes[x] = new Dictionary<String, String>(Lines.Length);
                    foreach (String Line in Lines)
                    {
                        String[] Parts = Line.Split('=');

                        if (Parts.Length != 2)
                            continue;

                        if (StrRes[x].ContainsKey(Parts[0]))
                            continue;

                        StrRes[x].Add(Parts[0], Parts[1]);
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
