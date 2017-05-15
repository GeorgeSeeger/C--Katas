using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class Factorials
    {
        public static string Factorial(int n)
        {
            return Enumerable.Range(1, n)
                    .Select(i => new BigInteger(i))
                    .Aggregate((i, a) => BigInteger.Multiply(i, a))
                    .ToString();
        }

        [Fact]
        public void Tests()
        {
            Assert.Equal("1", Factorial(1));
            Assert.Equal("120", Factorial(5));
            Assert.Equal("362880", Factorial(9));
            Assert.Equal("1307674368000", Factorial(15));
        }
    }
}
