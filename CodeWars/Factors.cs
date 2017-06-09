using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class Factors
    {
        public class SumOfDivided
        {
            public static string sumOfDivided(int[] lst)
            {
                return $"({string.Join(")(", lst.Select(PrimeDecomp).SelectMany(a => a).Distinct().OrderBy(i => i).Select(i => new[] { i, lst.Where(j => j % i == 0).Aggregate((k, sum) => k + sum) }).Select(e => string.Join(" ", e)))})";
            }

            private static int[] PrimeDecomp(int n)
            {
                if (n < 0) n *= -1;
                List<int> primes = new List<int>();
                while (n % 2 == 0) {
                    primes.Add(2);
                        n /= 2;
                }
                for (int i = 3; i <= Math.Sqrt(n); i += 2) {
                    while (n % i == 0) {
                        primes.Add(i);
                        n /= i;
                    }
                }
                if (n > 2) primes.Add(n);
                return primes.ToArray();
            }

            [Fact]
            public static void CanDecompPrimeNumbers()
            {
                Assert.Equal(new[] {2, 2}, PrimeDecomp(4));
                Assert.Equal(new[] {3, 3}, PrimeDecomp(9));
                Assert.Equal(new[] {2, 2, 3}, PrimeDecomp(12));
                Assert.Equal(new[] {3, 5}, PrimeDecomp(15));
            }

            [Fact]
            public static void CanSumFactors()
            {
                int[] lst = { 12, 15 };
                Assert.Equal("(2 12)(3 27)(5 15)", sumOfDivided(lst));
            }
        }
    }
}
