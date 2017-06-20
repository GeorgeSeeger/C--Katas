using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace CodeWars
{
    public class TotalIncreasingOrDecreasing
    {
        public static BigInteger TotalIncDec(int x)
        {
            if (x == 0) return 1;
            if (x == 1) return 10;
            if (x == 2) return 100;
            var answer = BigInteger.Zero;
            var limit = BigInteger.Pow(10, x);
            var increasing = false;
            var decreasing = false;
            for (var j = 0; j < limit; j++)
            {
                if (increasing && decreasing) continue;
                if (IncreasingDigits(j)){
                    increasing = true;
                    if (!decreasing) answer++;
                }
                if (DecreasingDigits(j)) {
                    decreasing = true;
                    if (!increasing) answer++;
                }
            }
            return answer;
        }

        private static bool IncreasingDigits(BigInteger x)
        {
            var str = x.ToString();
            var i = 0;
            return str.All(c => int.Parse(c.ToString()) <= int.Parse(str.ElementAt(++i < str.Length ? i : str.Length - 1).ToString()));
        }

        private static bool DecreasingDigits(BigInteger x)
        {
            var str = x.ToString();
            var i = 0;
            return str.All(c => int.Parse(c.ToString()) >= int.Parse(str.ElementAt(++i < str.Length ? i : str.Length - 1).ToString()));
        }

        [Fact]
        public static void Tests()
        {
            Assert.Equal(BigInteger.Parse("1"), TotalIncDec(0));
            Assert.Equal(BigInteger.Parse("10"), TotalIncDec(1));
            Assert.Equal(BigInteger.Parse("100"), TotalIncDec(2));
            Assert.Equal(BigInteger.Parse("475"), TotalIncDec(3));
            Assert.Equal(BigInteger.Parse("1675"), TotalIncDec(4));
        }
    }
}
