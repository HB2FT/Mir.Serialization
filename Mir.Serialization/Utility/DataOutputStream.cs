//
// Writing section
//
using System;
using System.IO;

namespace Mir.Serialization.Utility
{
    public class DataOutputStream
    {
        private FileStream fs;

        public DataOutputStream(FileStream fs)
        {
            this.fs = fs;
        }

        public void WriteInt(int value)
        {
            fs.WriteByte((byte)((value >>> 24) & 0xFF));
            fs.WriteByte((byte)((value >>> 16) & 0XFF));
            fs.WriteByte((byte)((value >>> 8) & 0xFF));
            fs.WriteByte((byte)((value >>> 0) & 0xFF));
        }

        public void WriteBoolean(bool value)
        {
            fs.WriteByte((byte)(value ? 1 : 0));
        }

        public void WriteFloat(float value)
        {
            WriteInt(BitConverter.SingleToInt32Bits(value));
        }
    }
}
