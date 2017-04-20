namespace CodeWars
{
    using System;
    using System.Numerics;
    using System.Collections.Generic;
    using static System.Linq.Enumerable;

    public class Pascal
    {
        public static List<int> PascalsTriangle(int n)
        {
            int m = n - 1;
            if (m == 0) { return new List<int>() { 1 };}
            var row = new List<int>();
            var nFac = Range(1, m ).ToList();
            for (int k = 0; k <= m; k++)
            {
                var kFac = k == 0 ? new List<int>() {1} : Range(1, k);
                var nkFac = m - k == 0 ? new List<int>() { 1 } : Range(1, m - k);
                var product = nFac.Except(nkFac).Except(kFac).Aggregate(BigInteger.One, (i, j) => i * j)
                                / kFac.Intersect(nkFac).Aggregate(BigInteger.One, (i, j) => i * j );

                row.Add((int) product);
            }
            return PascalsTriangle(n - 1).Concat(row).ToList();
        }

        public static void Main(string[] args)
        {
            var a = PascalsTriangle(20);
        }
    }
}
