using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class HeapPermutations
    {
        public string MiddlePermutation(string s)
        {
            var perms = HeapPermutationAlgorithm(s).ToList();
            perms.Sort();
            return perms.ElementAt(perms.Count / 2 - 1);
        }

        private List<string> HeapPermutationAlgorithm(string s)
        {
            var c = new int[s.Length];
            var perms = new List<string> {s};

            var i = 0;
            while (i < s.Length) {
                if (c[i] < i) {
                    if (i % 2 == 0) {
                        perms.Add(Swap(perms.Last(), 0, i));
                    }
                    else {
                        perms.Add(Swap(perms.Last(), c[i], i));
                    }
                    c[i]++;
                    i = 0;
                }
                else {
                    c[i] = 0;
                    i++;
                }
            }
            return perms;
        }

        private string Swap(string s, int i, int j)
        {
            var chars = s.ToCharArray();
            var temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
            return new string(chars);

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
