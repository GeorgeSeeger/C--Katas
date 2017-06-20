using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class ToSmallest
    {
        public static long[] Smallest(long n)
        {
            var str = n.ToString();
            return Enumerable.Range(0, str.Length)
                .Select(i => Tuple.Create(i, Enumerable.Range(0, str.Length).ToArray()))
                .Select(
                    t =>
                        t.Item2.Select(
                            j => new[] {long.Parse(SwapCharsAt(str, t.Item1, j).ToString()), t.Item1, j}))
                .SelectMany(r => r)
                .OrderBy(i => i[0])
                .ThenBy(i => i[1])
                .ThenBy(i => i[2])
                .First();
        }

        private static string SwapCharsAt(string str, int i, int j)
        {
            var chars = str.ToCharArray().ToList();
            var temp = chars[i];
            chars.RemoveAt(i);
            chars.Insert(j, temp);
            return new string(chars.ToArray());
        }

        [Fact]
        public static void Tests()
        {
            Assert.Equal(string.Join(", ", Smallest(261235)), "126235, 2, 0");
            Assert.Equal(string.Join(", ", Smallest(209917)), "29917, 0, 1");
            Assert.Equal(string.Join(", ", Smallest(285365)), "238565, 3, 1");
            Assert.Equal(string.Join(", ", Smallest(269045)), "26945, 3, 0");
            Assert.Equal(string.Join(", ", Smallest(296837)), "239687, 4, 1");
        }
    }
}
