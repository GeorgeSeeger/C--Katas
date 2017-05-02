using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class IntPart
    {
        public static Dictionary<int, List<List<int>>> MemoizedParts = new Dictionary<int, List<List<int>>>
        {
            {1, new List<List<int>> {new List<int> {1}}},
            {2, new List<List<int>> {new List<int> {2}, new List<int> { 1, 1} } }
        };

        public static string Part(long n)
        {
            var prod = Partition((int) n).Select(li => li.Aggregate((i, a) => i * a)).OrderBy(i=> i).Distinct();
            var median = prod.Count() % 2 == 1
                ? prod.Skip(prod.Count() / 2).Take(1).First()
                : prod.Skip(prod.Count() / 2 - 1).Take(2).Average();
            return $"Range: {prod.Max() - prod.Min()} Average: {prod.Average():F2} Median: {median:F2}";
        }

        private static List<List<int>> Partition(int n)
        {
            if (MemoizedParts.ContainsKey(n)){ return MemoizedParts[n]; }
            if (MemoizedParts.ContainsKey(n - 1))
            {
                var newList = new List<List<int>>() {new List<int>() {n}};
                for (var i = n -1; i > 0; i--)
                {
                    var min = i;
                    var newPart = MemoizedParts[n - i].Where(li => !li.Any(j => j > min))
                                                      .Select(li => new[] {i}.Concat(li).ToList())
                                                      .ToList();
                    newList = newList.Concat(newPart).ToList();
                    
                }
                MemoizedParts.Add(n, newList);
                return newList;
            }
            for (int j = MemoizedParts.Keys.Max(); j <= n; j++)
            {
                Partition(j);
            }
            return MemoizedParts[n];
        }

        [Fact]
        public static void Tests()
        {
            Assert.Equal( "Range: 1 Average: 1.50 Median: 1.50", Part(2));
            Assert.Equal("Range: 2 Average: 2.00 Median: 2.00", Part(3) );
            Assert.Equal("Range: 3 Average: 2.50 Median: 2.50", Part(4));
            Assert.Equal("Range: 5 Average: 3.50 Median: 3.50", Part(5));
        }
        public static void Main(string[] args)
        {
            
        }
    }
}
