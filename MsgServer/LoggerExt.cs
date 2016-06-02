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
    /// <summary>
    /// Extensions to the log4net logger interface.
    /// </summary>
    public static class LoggerExt
    {
        /// <summary>
        /// Logs a formatted message string with the log4net.Core.Level.Debug level.
        /// </summary>
        /// <param name="aLogger">The logger used to log the message.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments of the message.</param>
        public static void Debug(this log4net.ILog aLogger, String format, params object[] args)
        {
            aLogger.DebugFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted message string with the log4net.Core.Level.Info level.
        /// </summary>
        /// <param name="aLogger">The logger used to log the message.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments of the message.</param>
        public static void Info(this log4net.ILog aLogger, String format, params object[] args)
        {
            aLogger.InfoFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted message string with the log4net.Core.Level.Warn level.
        /// </summary>
        /// <param name="aLogger">The logger used to log the message.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments of the message.</param>
        public static void Warn(this log4net.ILog aLogger, String format, params object[] args)
        {
            aLogger.WarnFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted message string with the log4net.Core.Level.Error level.
        /// </summary>
        /// <param name="aLogger">The logger used to log the message.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments of the message.</param>
        public static void Error(this log4net.ILog aLogger, String format, params object[] args)
        {
            aLogger.ErrorFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted message string with the log4net.Core.Level.Fatal level.
        /// </summary>
        /// <param name="aLogger">The logger used to log the message.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments of the message.</param>
        public static void Fatal(this log4net.ILog aLogger, String format, params object[] args)
        {
            aLogger.FatalFormat(format, args);
        }
    }
}
