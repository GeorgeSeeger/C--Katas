using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class MyConvert
    {
        public static int ToIntFromBinary(string num)
        {
            if (num.Length == 0) { return 0; }
            var first = num.First();
            var number = first == '1'
                ? Math.Pow(2, num.Length - 1)
                : 0;
            return (int)number + ToIntFromBinary(num.Substring(1));
        }
    }
}
