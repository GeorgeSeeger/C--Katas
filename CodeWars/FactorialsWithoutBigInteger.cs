using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public class FactorialsWithoutBigInteger {
        public static string Factorial(int n) {
            return Enumerable.Range(1, n)
                    .Select(i => new BigInteger(i))
                    .Aggregate((i, a) => BigInteger.Multiply(i, a))
                    .ToString();
        }

        public static string StringAddition(string num1, string num2)
        {
            var temp = new[] {0}.Concat(num1.ToCharArray().Zip(num2.ToCharArray(), (c1, c2) => int.Parse(c1.ToString()) + int.Parse(c2.ToString()))).Reverse();
            var carryTheOne = false;
            return string.Join("", temp.Select(j =>
            {
                j += carryTheOne ? 1 : 0;
                if (j > 9) {
                    carryTheOne = true;
                    return j % 10;
                }
                carryTheOne = false;
                return j;
            }).Reverse()).TrimStart('0');
        }

        public static string StringMultiplication(string a, string b)
        {
            var remainder = "0";
            var i = 0;
            var sums = new List<string>();
            foreach (var c in a.Reverse()) {
                foreach (var cc in b.Reverse()) {
                    var product = int.Parse(c.ToString()) * int.Parse(cc.ToString()) + int.Parse(remainder);
                    remainder = product > 10 ? product.ToString().First().ToString() : "0";
                    sums.Add(product + new string('0', i++));
                }
            }
            return sums.Aggregate(StringAddition);
        }
        [Fact]
        public void Tests() {
            Assert.Equal("1", Factorial(1));
            Assert.Equal("120", Factorial(5));
            Assert.Equal("362880", Factorial(9));
            Assert.Equal("1307674368000", Factorial(15));
        }

        [Fact]
        public void TestAddition() {
            Assert.Equal("10", StringAddition("5", "5"));
            Assert.Equal("120", StringAddition("20", "100"));
            Assert.Equal("198", StringAddition("99", "99"));
        }

        [Fact]
        public void TestMultiplication()
        {
            Assert.Equal("25", StringMultiplication("5", "5"));
            Assert.Equal("125", StringMultiplication("5", "25"));
            Assert.Equal("400", StringMultiplication("4", "100"));
        }
    }
}
