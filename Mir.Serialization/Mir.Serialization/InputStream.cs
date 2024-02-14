//
// Reading section
//
using System;
using System.IO;

namespace Mir.Serialization
{
    public class InputStream
    {
        private FileStream fs;

        public InputStream(FileStream fs)
        {
            this.fs = fs;
        }

        public int ReadInt()
        {
            int ch1 = fs.ReadByte();
            int ch2 = fs.ReadByte();
            int ch3 = fs.ReadByte();
            int ch4 = fs.ReadByte();

            if ((ch1 | ch2 | ch3 | ch4) < 0) throw new System.Exception("Integer okunamadı.");

            return ((ch1 << 24) + (ch2 << 16) + (ch3 << 8) + (ch4 << 0));
        }

        public bool ReadBoolean()
        {
            int ch = fs.ReadByte();

            if (ch < 0) throw new System.Exception("Boolean okunamadı.");

            return (ch != 0);
        }

        public float ReadFloat()
        {
            return BitConverter.UInt32BitsToSingle((uint)ReadInt());
        }
    }
}
