using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class FindingOrderInPermutations
    {
        public string MiddlePermutation(string s)
        {
            var sorted = s.OrderBy(c => c).ToList();
            var middleIndex = Factorial(s.Length) / 2;
            var middleWord = "";
            var index = BigInteger.Zero;
            while (middleWord.Length < s.Length) { 
                    foreach (var c in sorted) {
                        if (index + OrderedStartsWith(sorted, c) >= middleIndex) {
                            middleWord += c;
                            sorted.Remove(c);
                            break;
                        }
                        index += OrderedStartsWith(sorted, c);
                    }
                
            }
            return middleWord;
        }

        private BigInteger Factorial(int n)
        {
            return Enumerable.Range(1, n).Aggregate(BigInteger.One, (total, i) => BigInteger.Multiply(total, i));
        }

        private BigInteger OrderedStartsWith(List<char> sorted, char c)
        {
            return Factorial(sorted.Count - 1);
        }

        [Fact]
        public void Tests()
        {
            Assert.Equal("bac", MiddlePermutation("abc"));
            Assert.Equal("bdca", MiddlePermutation("abcd"));
            Assert.Equal("cbxda", MiddlePermutation("abcdx"));
            Assert.Equal("cxgdba", MiddlePermutation("abcdxg"));
            Assert.Equal("dczxgba", MiddlePermutation("abcdxgz"));
        }
}
}
