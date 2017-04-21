using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Magnets
    {
        public static double Doubles(int maxk, int maxn)
        {
            double sum = 0;
            for (int k = 1; k <= maxk; k++)
            {
                for (int n = 1; n <= maxn; n++)
                {
                    sum += 1 / (k * Math.Pow(n + 1, 2 * k));
                }
            }
            return sum;
        }
    }
}
