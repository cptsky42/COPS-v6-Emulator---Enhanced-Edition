using System;
using System.Text;
using System.Runtime.InteropServices;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class PackedString
    {
        private String[] Strings = new String[0];

        public PackedString() { }

        public PackedString(Byte* pBuf)
        {
            Strings = new String[pBuf[0]];

            Int32 Pos = 1;
            for (Byte i = 0; i < Strings.Length; i++)
            {
                Byte Length = pBuf[Pos++];

                StringBuilder Builder = new StringBuilder(Length);
                for (Byte j = 0; j < Length; j++)
                    Builder.Append((Char)pBuf[Pos++]);
                Strings[i] = Builder.ToString();
            }
        }

        public Byte GetCount() { return (Byte)Strings.Length; }
        public String[] GetStrings() { return Strings; }

        public void Add(String Str)
        {
            if (Strings.Length < Byte.MaxValue)
            {
                Byte Length = (Byte)Math.Min(Str.Length, Byte.MaxValue);

                String[] Temp = new String[Strings.Length + 1];
                Array.Copy(Strings, 0, Temp, 0, Strings.Length);
                Temp[Strings.Length] = Str.Substring(0, Length);

                Strings = Temp;
            }
        }

        public Int32 GetLength()
        {
            Int32 Length = 1;
            for (Int32 i = 0; i < Strings.Length; i++)
                Length += Strings[i].Length + 1;
            return Length;
        }

        public Byte[] ToBytes()
        {
            Byte[] Buffer = new Byte[GetLength()];
            Int32 Pos = 0;

            Buffer[Pos++] = (Byte)Strings.Length;
            for (Int32 i = 0; i < Strings.Length; i++)
            {
                Buffer[Pos++] = (Byte)Strings[i].Length;
                for (Int32 j = 0; j < Strings[i].Length; j++)
                    Buffer[Pos++] = (Byte)Strings[i][j];
            }
            return Buffer;
        }
    }
}
