using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Bases
    {
        public static string Convert(string input, string source, string target)
        {
            if (input == source.Substring(0, 1)) {  return target.Substring(0,1);}
            List<List<int>> temp = new List<List<int>>();
            var output = string.Empty;
            var k = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                temp.Add(new List<int>() { source.IndexOf(input.Substring(k++, 1), StringComparison.Ordinal), i});
            }

            var intermediary = temp.Select(
                    l => BigInteger.Multiply(new BigInteger(l[0]), BigInteger.Pow(source.Length, l[1])))
                .Aggregate(BigInteger.Add);

            for (int j = (int) BigInteger.Log(intermediary, target.Length); j >= 0; j--)
            {
                output+= target.Substring( (int) (intermediary/(BigInteger) Math.Pow(target.Length, j)), 1);
                intermediary %= (BigInteger) Math.Pow(target.Length, j);
            }
            return output;
        }

//        public static void Main(string[] args)
//        {
//            Convert("1010", "01", "0123456789abcdef");
//        }
    }
}

