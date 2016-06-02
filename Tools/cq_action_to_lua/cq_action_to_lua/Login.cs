using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CO2Tools
{
    class Login
    {
        public const UInt32 LOGIN_PROMPT = 1000000;

        public static void generateScript(StreamWriter stream)
        {
            generateHeader(stream);
            generateFunction(stream);
        }

        private static void generateHeader(StreamWriter stream)
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

        private static void generateFunction(StreamWriter stream)
        {
            stream.WriteLine("function onLogin(client)");
            stream.WriteLine();

            Action.processAction(stream, "    ", null, LOGIN_PROMPT);
            stream.WriteLine();
            stream.WriteLine("end");
        }
    }
}
