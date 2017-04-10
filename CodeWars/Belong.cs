using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Belong
    {
        public static object FindTheNotFittingElement(object[] series)
        {
            Console.WriteLine(string.Format("[ {0} ]", string.Join(", ", series.ToList())));
            var findOfSameElements = series.GroupBy(i => i);
            if (findOfSameElements.Count() == 2)
            {
                return findOfSameElements.OrderBy(g => g.Count()).First().ToList()[0];
            }

            var groupBySameType = series.GroupBy(e => e.GetType());
            if (groupBySameType.Count() == 2)
            {
                return groupBySameType
                    .Select(g => new object[] {g.Count(), g.First()})
                    .OrderBy(a => a[0])
                    .First()[1];
            }
            var charType = 'a'.GetType();
            var intType = 2.GetType();
            if (groupBySameType.Count() == 1 && series.First().GetType() == charType)
            {
                int n;
                var groupByPossibleNumbers = series.GroupBy(e => int.TryParse(e.ToString(), out n));
                if (groupByPossibleNumbers.Count() == 2)
                {
                    return groupByPossibleNumbers
                        .Select(g => new object[] { g.Count(), g.First() })
                        .OrderBy(a => a[0])
                        .First()[1];
                }
                var groupByPunctuation = series.GroupBy(e => IsPunctuation((char)e));
                if (groupByPunctuation.Count() == 2)
                {
                    return groupByPunctuation.Select(g => new object[] { g.Count(), g.First() })
                        .OrderBy(a => a[0])
                        .First()[1];
                }
                var groupBySameCase = series.GroupBy(e => e.ToString() == e.ToString().ToUpper());
                if (groupBySameCase.Count() == 2)
                {
                    return groupBySameCase.Select(g => new object[] { g.Count(), g.First() })
                        .OrderBy(a => a[0])
                        .First()[1];
                }
            }
            if (groupBySameType.Count() == 1 && series.First().GetType() == intType)
            {
                var groupBySign = series.GroupBy(e => (int)e < 0);
                if (groupBySign.Count() == 2)
                {
                    return groupBySign.Select(g => new object[] { g.Count(), g.First() })
                        .OrderBy(a => a[0])
                        .First()[1];
                }

                var groupByEvens = series.GroupBy(e => (int) e % 2 == 0);
                if (groupByEvens.Count() == 2)
                {
                    return groupByEvens.Select(g => new object[] { g.Count(), g.First() })
                        .OrderBy(a => a[0])
                        .First()[1];
                }
            }

                return findOfSameElements.Select(g => new object[] { g.Count(), g.First() })
                        .OrderBy(a => a[0])
                        .First()[1];
        }
        private static bool IsPunctuation(char e)
        {
            return !"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".Contains(e.ToString());
        }
//        public static void Main(string[] args)
//        {
//            FindTheNotFittingElement( new object[] { 'Z', 'L', 'P', 't', 'G' });
//        }
    }
}
