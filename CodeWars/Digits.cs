using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public class Digits
    {
        public static long NextSmaller(long n)
        {
            var str = n.ToString();
            List<string> perms;
            perms = str.Length > 6
                ? new Permutation().List(str.Substring(str.Length - 6))
                    .Select(s => str.Substring(0, str.Length - 6) + s)
                    .ToList()
                : new Permutation().List(str);
            var list =  perms.Select(s => long.Parse(s)).Where(l => l < n).OrderBy(l => l);
            if (!list.Any() || list.Last().ToString().Length < n.ToString().Length)
            {
                return -1;
            }
            return list.Last();
        }

        public class Permutation
        {
            private readonly List<string> perms;

            public Permutation()
            {
                perms = new List<string>();
            }
            private void Swap(ref char a, ref char b)
            {
                if (a == b) return;

                a ^= b;
                b ^= a;
                a ^= b;
            }

            public List<string> List(string word)
            {
                var list = word.ToCharArray();
                int x = list.Length - 1;
                GetPer(list, 0, x);
                return perms;
            }

            private void GetPer(char[] list, int k, int m)
            {
                if (k == m)
                {
                    perms.Add(string.Join("", list));
                }
                else
                    for (int i = k; i <= m; i++)
                    {
                        Swap(ref list[k], ref list[i]);
                        GetPer(list, k + 1, m);
                        Swap(ref list[k], ref list[i]);
                    }
            }
        }
    }
}
