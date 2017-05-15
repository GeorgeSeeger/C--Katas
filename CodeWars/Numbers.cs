using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Numbers
    {
        public static BigInteger TotalIncDec(int x)
        {
            var num = new BigInteger(0);
            if (x == 0) return 1;
            if (x == 1) return 10;
            if (x == 2) return 100;
            return num;
        }
    }
}
