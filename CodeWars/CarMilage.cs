using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class CarMilage
    {
        public static int IsInteresting(int number, List<int> awesomePhrases)
        {
            if (EqualsInteresting(number, awesomePhrases))
            {
                return 2;
            }

            var range = Enumerable.Range(number - 2, 5).Except(new[] {number});
            if (range.Any(i => EqualsInteresting(i, awesomePhrases)))
            {
                return 1;
            }
            return 0;
        }

        private static bool EqualsInteresting(int n, List<int> awesomePhrases)
        {
            if ( n < 100)
            {
                return false;
            }
            if ( awesomePhrases.Contains(n) || IsInAscendingSequential(n) || IsInDescendingSequential(n) || HasTrailingZeros(n) || IsPalindrome(n))
            {
                return true;
            }
            return false;
        }

        private static bool IsInAscendingSequential(int n)
        {
            var ascending = "1234567890";
            return ascending.Contains(n.ToString());
        }

        private static bool IsInDescendingSequential(int n)
        {
            var descending = "9876543210";
            return descending.Contains(n.ToString());
        }

        private static bool HasTrailingZeros(int n)
        {
            var nums = "123456789";
            return !n.ToString().Substring(1).Any(c => nums.Contains(c));
        }

        private static bool IsPalindrome(int n)
        {
            return n.ToString() == string.Join("", n.ToString().Reverse());
        }

        [Fact]
        public static void Tests()
        {
            Assert.Equal(0, IsInteresting(3, new List<int>() { 1337, 256 }));
            Assert.Equal(1, IsInteresting(1336, new List<int>() { 1337, 256 }));
            Assert.Equal(2, IsInteresting(1337, new List<int>() { 1337, 256 }));
            Assert.Equal(0, IsInteresting(11208, new List<int>() { 1337, 256 }));
            Assert.Equal(1, IsInteresting(11209, new List<int>() { 1337, 256 }));
            Assert.Equal(2, IsInteresting(11211, new List<int>() { 1337, 256 }));
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
