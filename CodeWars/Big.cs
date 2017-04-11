using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Big
    {
        public static string Add(string a, string b)
        {
            var c = a.PadLeft(b.Length, '0');
            var d = b.PadLeft(a.Length, '0');
            string answer = string.Empty;
            bool carryTheOne = false;

            for (int i = d.Length - 1; i >= -1; i--)
            {
                if (i >= 0)
                {
                    var digit = int.Parse(c.Substring(i, 1)) + int.Parse(d.Substring(i, 1));
                    if (carryTheOne)
                    {
                        digit += 1;
                    }
                    if (digit >= 10)
                    {
                        carryTheOne = true;
                        digit -= 10;
                    }
                    else
                    {
                        carryTheOne = false;
                    }
                    answer += digit.ToString();
                }
                else
                {
                    answer += carryTheOne ? "1" : "";
                }
                
            }
            return string.Join("", answer.Reverse());
        }

//        public static void Main(string[] args)
//        {
//            Big.Add("444", "123456");
//        }
    }
}
