// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2014 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.IO;
using Nini.Ini;

namespace COServer
{
    /// <summary>
    /// INI parser.
    /// </summary>
    public class Ini : IDisposable
    {
        /// <summary>
        /// The Nini document.
        /// </summary>
        private IniDocument mDoc;
        /// <summary>
        /// The stream of the file.
        /// </summary>
        private Stream mStream;
        /// <summary>
        /// The text reader used by Nini.
        /// </summary>
        private TextReader mReader;

        /// <summary>
        /// Indicate whether or not the object is disposed.
        /// </summary>
        private bool mIsDisposed = false;

        /// <summary>
        /// Open the specified file with an INI reader.
        /// </summary>
        public Ini(String aPath)
        {
            mStream = new FileStream(aPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            mReader = new StreamReader(mStream, Program.Encoding);
            mDoc = new IniDocument(mReader, IniFileType.WindowsStyle);
        }

        ~Ini()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool aDisposing)
        {
            if (!mIsDisposed)
            {
                if (aDisposing)
                {
                    if (mReader != null)
                        mReader.Dispose();
                    if (mStream != null)
                        mStream.Dispose();
                }

                mReader = null;
                mStream = null;

                mIsDisposed = true;
            }
        }

        /// <summary>
        /// Determine if the specified key exists in the section.
        /// </summary>
        public Boolean KeyExist(String aSection, String aKey)
        {
            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                return section.Contains(aKey);

            return false;
        }

        /// <summary>
        /// Read the value as a 8-bits signed integer.
        /// </summary>
        public SByte ReadInt8(String aSection, String aKey)
        {
            SByte value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                SByte.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 8-bits signed integer.
        /// </summary>
        public SByte ReadInt8(String aSection, String aKey, SByte aDefault)
        {
            SByte value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                SByte.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 8-bits unsigned integer.
        /// </summary>
        public Byte ReadUInt8(String aSection, String aKey)
        {
            Byte value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Byte.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 8-bits unsigned integer.
        /// </summary>
        public Byte ReadUInt8(String aSection, String aKey, Byte aDefault)
        {
            Byte value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Byte.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 16-bits signed integer.
        /// </summary>
        public Int16 ReadInt16(String aSection, String aKey)
        {
            Int16 value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Int16.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 16-bits signed integer.
        /// </summary>
        public Int16 ReadInt16(String aSection, String aKey, Int16 aDefault)
        {
            Int16 value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Int16.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 16-bits unsigned integer.
        /// </summary>
        public UInt16 ReadUInt16(String aSection, String aKey)
        {
            UInt16 value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                UInt16.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 16-bits unsigned integer.
        /// </summary>
        public UInt16 ReadUInt16(String aSection, String aKey, UInt16 aDefault)
        {
            UInt16 value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                UInt16.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 32-bits signed integer.
        /// </summary>
        public Int32 ReadInt32(String aSection, String aKey)
        {
            Int32 value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Int32.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 32-bits signed integer.
        /// </summary>
        public Int32 ReadInt32(String aSection, String aKey, Int32 aDefault)
        {
            Int32 value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Int32.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 32-bits unsigned integer.
        /// </summary>
        public UInt32 ReadUInt32(String aSection, String aKey)
        {
            UInt32 value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                UInt32.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 32-bits unsigned integer.
        /// </summary>
        public UInt32 ReadUInt32(String aSection, String aKey, UInt32 aDefault)
        {
            UInt32 value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                UInt32.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 64-bits signed integer.
        /// </summary>
        public Int64 ReadInt64(String aSection, String aKey)
        {
            Int64 value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Int64.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 64-bits signed integer.
        /// </summary>
        public Int64 ReadInt64(String aSection, String aKey, Int64 aDefault)
        {
            Int64 value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Int64.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 64-bits unsigned integer.
        /// </summary>
        public UInt64 ReadUInt64(String aSection, String aKey)
        {
            UInt64 value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                UInt64.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 64-bits unsigned integer.
        /// </summary>
        public UInt64 ReadUInt64(String aSection, String aKey, UInt64 aDefault)
        {
            UInt64 value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                UInt64.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 32-bits signed decimal.
        /// </summary>
        public Single ReadFloat(String aSection, String aKey)
        {
            Single value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Single.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 32-bits signed decimal.
        /// </summary>
        public Single ReadFloat(String aSection, String aKey, Single aDefault)
        {
            Single value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Single.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 64-bits signed decimal.
        /// </summary>
        public Double ReadDouble(String aSection, String aKey)
        {
            Double value = 0;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Double.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a 64-bits signed decimal.
        /// </summary>
        public Double ReadDouble(String aSection, String aKey, Double aDefault)
        {
            Double value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Double.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a boolean.
        /// </summary>
        public Boolean ReadBoolean(String aSection, String aKey)
        {
            Boolean value = false;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Boolean.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a boolean.
        /// </summary>
        public Boolean ReadBoolean(String aSection, String aKey, Boolean aDefault)
        {
            Boolean value = aDefault;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                Boolean.TryParse(section.GetValue(aKey), out value);

            return value;
        }

        /// <summary>
        /// Read the value as a string.
        /// </summary>
        [Obsolete]
        public String ReadValue(String aSection, String aKey)
        {
            return ReadString(aSection, aKey);
        }

        /// <summary>
        /// Read the value as a string.
        /// </summary>
        public String ReadString(String aSection, String aKey)
        {
            String value = null;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                value = section.GetValue(aKey);

            return value != null ? value : "";
        }

        /// <summary>
        /// Read the value as a string.
        /// </summary>
        public String ReadString(String aSection, String aKey, String aDefault)
        {
            String value = null;

            IniSection section = mDoc.Sections[aSection];
            if (section != null)
                value = section.GetValue(aKey);

            return value != null ? value : aDefault;
        }
    }
}