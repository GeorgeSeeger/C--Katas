using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Permutation
    {
        private  readonly List<string> perms;

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

        public  List<string> List(string word)
        {
            var list = word.ToCharArray();
            int x = list.Length - 1;
            GetPer(list, 0, x);
            return perms;
        }

        public List<string> HeapsPermutationAlgorithm(string s)
        {
            var c = new int[s.Length];
            var perms = new List<string> { s };

            var i = 0;
            while (i < s.Length)
            {
                if (c[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        perms.Add(SwapStr(perms.Last(), 0, i));
                    }
                    else
                    {
                        perms.Add(SwapStr(perms.Last(), c[i], i));
                    }
                    c[i]++;
                    i = 0;
                }
                else
                {
                    c[i] = 0;
                    i++;
                }
            }
            return perms;
        }

        private string SwapStr(string s, int i, int j)
        {
            var chars = s.ToCharArray();
            var temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
            return new string(chars);

        }

        private  void GetPer(char[] list, int k, int m)
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
