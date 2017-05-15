using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Squares
    {
        public string Decompose(long n)
        {
            var list = SquareList(n, n - 1);
            list.Reverse();
            return string.Join(" ", list);
        }

        private static List<long> SquareList(long n, long k)
        {
            var answer = new List<long>();
            var remainder = Math.Pow(n, 2);
            answer.Add(k);
            remainder -= Math.Pow(k, 2);
            while (remainder > 0)
            {
                var x = (long) Math.Floor(Math.Sqrt(remainder));
                answer.Add(x);
                remainder -= Math.Pow(x, 2);
                if ( (1 < remainder && remainder < 5) || (x == 1 && remainder > 1 && answer.Last() == 1))
                {
                    answer = SquareList(n, k - 1);
                }
            }
            return answer;
        }
    }
}
