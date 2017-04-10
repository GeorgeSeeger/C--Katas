using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public class PrimeDecomp
    {

        public static String factors(int lst)
        {
            var primes = new List<int>();
            for (int b = 2; lst > 2; b++)
            {
                while (lst % b == 0)
                {
                    primes.Add(b);
                    lst /= b;
                }
            }
            var factors = primes.GroupBy(i => i).Select(g => new List<int> {g.First(), g.Count()}).ToList();
            return FactorString(factors);
        }

        public static string FactorString(List<List<int>> factors) => $"({string.Join(")(", factors.Select(l => string.Join("**", l)).ToList())})".Replace("**1", "");
    }
}