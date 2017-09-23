using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace LCLib.Functions
{
    public static class encryption
    {
        public static String Encrypt(this string text)
        {
            Byte[] bytArray = Encoding.Default.GetBytes(text);
            String fir = "";
            String sec = "";
            for (int i = 0; i < bytArray.Length; i++)
            {
                int tmp = 255 - bytArray[i];
                fir += tmp.ToString("X");
            }
            Byte[] hexArray = Encoding.Default.GetBytes(fir);
            for (int i = 0; i < hexArray.Length; i++)
            {
                int tmp = 0;
                if (hexArray[i] >= 48 && hexArray[i] <= 57)
                {
                    tmp = 105 - hexArray[i];
                }
                else if (hexArray[i] >= 65 && hexArray[i] <= 90)
                {
                    tmp = 155 - hexArray[i];
                }
                else if (hexArray[i] >= 97 && hexArray[i] <= 122)
                {
                    tmp = 219 - hexArray[i];
                }
                sec += (char)tmp;
            }
            return sec.ToLower();
        }
        public static String Decrypt(this string text)
        {
            text = text.ToUpper();
            Byte[] strArray = Encoding.Default.GetBytes(text);
            String fir = "";
            String sec = "";
            String thi = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] >= 48 && strArray[i] <= 57)
                {
                    fir += " " + (105 - strArray[i]);
                }
                else if (strArray[i] >= 65 && strArray[i] <= 90)
                {
                    fir += " " + (155 - strArray[i]);
                }
                else if (strArray[i] >= 97 && strArray[i] <= 122)
                {
                    fir += " " + (219 - strArray[i]);
                }
            }
            String[] bArray = fir.Split(' ');
            for (int i = 1; i < bArray.Length; i++)
            {
                sec += (char)(int.Parse(bArray[i]));
            }
            strArray = Encoding.Default.GetBytes(sec);

            for (int i = 0; i < strArray.Length; i += 2)
            {
                thi += (char)(255 - Int32.Parse((char)strArray[i] + "" + (char)strArray[i + 1], NumberStyles.HexNumber));
            }
            return thi;
        }
    }
}
