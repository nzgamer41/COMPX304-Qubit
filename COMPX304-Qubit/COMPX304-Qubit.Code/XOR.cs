using System;
using System.Collections.Generic;
using System.Text;

namespace COMPX304_Qubit
{
    public static class XOR
    {
        public static string Encrypt(string message, string key)
        {
            //convert message to binary
            //convert key to binary
            //repeat key in message
            //XOR
            //???
            //profit
            string data = StringToBinary(message);
            string keyy = StringToBinary(key);
            int dataLen = data.Length;
            int keyLen = keyy.Length;
            char[] output = new char[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (char)(data[i] ^ keyy[i % keyLen]);
            }

            return new string(output);
        }

        public static string Decrypt(string message, string key)
        {
            string keyy = StringToBinary(key);
            int dataLen = message.Length;
            int keyLen = keyy.Length;
            char[] output = new char[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (char)(message[i] ^ keyy[i % keyLen]);
            }

            string tempData = new string(output);
            string data = BinaryToString(tempData);
            return new string(data);
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
    }
}
