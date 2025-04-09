using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLimiter
{
    public static class HCrypter
    {
        public static string Encrypt(bool value, int length)
        {
            Random m = new Random((int)DateTime.Now.ToBinary());
            string s = "";
            int nm = 0;
            for (int i = 0; i < length; i++)
            {
                int n = m.Next(0, short.MaxValue / 2) + (value ? 1 : 0)*short.MaxValue/2;
                nm += n;
                s += (char)n;
            }
            s += (char)Math.Floor((decimal)(nm / length));
            return s;
        }
        public static string Encrypt(string value, int tacts)
        {
            tacts--;
            Random m = new Random((int)DateTime.Now.ToBinary());
            char[] values = value.ToCharArray();
            string s = "";
            for(int i = 0; i < values.Length; i++)
            {
                s += string.Format("{0:X4}", (int)values[i]) + (i == values.Length - 1 ? "" : " ");
            }
            if (tacts > 0)
                s = Encrypt(s, tacts);
            return s;
        }
        public static bool Decrypt(string info, int length)
        {
            char[] chars = info.ToCharArray();
            int nms = 0;
            for(int i = 0; i < length; i++)
            {
                nms += chars[i];
            }
            if(Math.Floor((decimal)(nms/length)) == chars[length])
            {
                return chars[length] >= short.MaxValue / 2;
            }
            else
            {
                Console.WriteLine("Invalid encrypted value!");
                return new Random((int)DateTime.Now.ToBinary()).Next() > int.MaxValue/2;
            }
        }
        public static string Decrypt(string info, int tacts, bool isBoolean)
        {
            tacts--;
            string[] chars = info.Split(' ');
            string s = "";
            for(int i = 0; i < chars.Length; i++)
            {
                s += (char)int.Parse(chars[i], System.Globalization.NumberStyles.HexNumber);
            }
            if (tacts > 0)
                s = Decrypt(s, tacts, false);
            return s;
        }
    }
}
