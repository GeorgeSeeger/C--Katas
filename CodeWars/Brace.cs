using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Brace
    {

        public static bool validBraces(String braces)
        {
            if (braces == string.Empty) {return true;}
            var valid = new Dictionary<string, string>();
            valid.Add("(", ")");
            valid.Add("[", "]");
            valid.Add("{", "}");
            var first = braces.Substring(0, 1);
            if (valid.ContainsKey(first) && braces.Contains(valid[first]))
            {
                var nextBraceIndex = FindNextBrace(braces, valid);
                var remainderInside = braces.Substring(1, nextBraceIndex - 1);
                var remainderOutside = braces.Substring(nextBraceIndex + 1);
                return true && validBraces(remainderInside) && validBraces(remainderOutside);
            }
            return false;
        }

        public static int FindNextBrace(string str, Dictionary<string, string> valid )
        {
            int count = 1;
            var first = str.Substring(0, 1);
            var close = valid[first];
            for (int i = 1; i < str.Length; i++)
            {
                var c = str.Substring(i, 1);
                if (c == first) { count++; }
                else if (c == close) { count--; }
                if (count == 0) { return i; }
            }
            return 0;
        }
    }
}
