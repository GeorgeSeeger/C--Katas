using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class DoubleLinear
    {
        private static List<int> Memo = new List<int> {1};

        public static int DblLinear(int n)
        {
            if (Memo.Count > n) return Memo[n];
            var start = new[] {Memo.Count - 1, n / 3}.Min();
            for (int i = start; i < n; i++)
            {
                var x = Memo[i];
                Memo.Add(Y(x));
                Memo.Add(Z(x));
                Memo = Memo.OrderBy(j => j).Distinct().ToList();
            }
            return Memo[n];
        }

        private static int Y(int x)
        {
            return 2 * x + 1;
        }

        private static int Z(int x)
        {
            return 3 * x + 1;
        }

        [Fact]
        public void Tests()
        {
            Assert.Equal(22, DblLinear(10));
            Assert.Equal(57, DblLinear(20));
            Assert.Equal(91, DblLinear(30));
            Assert.Equal(175, DblLinear(50));
        }
    }
}
