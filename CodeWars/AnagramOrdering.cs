using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class AnagramOrdering
    {
        public static long ListPosition(string value)
        {
            if (value == "") return 1;
            return PositionOfFirst(value) + ListPosition(value.Substring(1));
        }

        private static long PositionOfFirst(string str)
        {
            var sorted = string.Join("", str.OrderBy(c => c));
            var index = sorted.IndexOf( str[0] );
            return index * (Factorial(str.Length - 1) / ProductOfDistincts(sorted));
        }

        private static long Factorial(int n)
        {
            if (n < 1) return 1;
            return Enumerable.Range(1, n).Aggregate((long) 1, (a, i) => a * i);
        }

        private static long ProductOfDistincts(string str)
        {
            var factorials = str.Where(c => str.Split(c).Length > 2)
                                .Distinct()
                                .Select(c => str.Split(c).Length - 1)
                                .Select(Factorial);
            return factorials.Any() ?
                factorials.Aggregate((a, i) => a * i)
                : 1;
        }

        [Fact]
        public void TestNumberToOrdinal()
        {
            Assert.Equal(1, ListPosition("A"));
            Assert.Equal(2, ListPosition("ABAB"));
            Assert.Equal(1, ListPosition("AAAB"));
            Assert.Equal(4, ListPosition("BAAA"));
            Assert.Equal(60, ListPosition("CBEDA"));
            Assert.Equal(24572, ListPosition("QUESTION"));
            Assert.Equal(10743, ListPosition("BOOKKEEPER"));
            Assert.Equal(1938852339039, ListPosition("MUCHOCOMBINATIONS"));
        }
    }
}
