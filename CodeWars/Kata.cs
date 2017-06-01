using System.Linq;
using Xunit;

namespace CodeWars
{
    public class Kata
    {
        public static int[] SortArray(int[] array)
        {
            var sortedOdds = array.Where(i => i % 2 == 1).OrderBy(i => i).ToArray();
            var a = 0;
            return array.Select(i => i % 2 == 1 ? sortedOdds[a++] : i).ToArray();
        }

        [Fact]
        public static void SortedArrayTest()
        {
            Assert.Equal(new int[] { 1, 3, 2, 8, 5, 4 }, Kata.SortArray(new int[] { 5, 3, 2, 8, 1, 4 }));
            Assert.Equal(new int[] { 1, 3, 5, 8, 0 }, Kata.SortArray(new int[] { 5, 3, 1, 8, 0 }));
            Assert.Equal(new int[] { }, Kata.SortArray(new int[] { }));
        }
    }
}
