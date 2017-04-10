using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class JosephusSurvivor
    {
        public static int JosSurvivor(int n, int k)
        {
            if (n ==1 ) { return 1;}
            return (JosSurvivor( n - 1, k) + k - 1) % n + 1;
        }
    }
}
