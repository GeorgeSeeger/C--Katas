using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Roman
    {
        public static int Decode(string roman)
        {
            if (roman == string.Empty) {  return 0; }
            var doubles = new Dictionary<string, int>()
            {
                {"CM", 900}, {"CD", 400},
                {"XC", 90}, {"XL", 40},
                {"IX", 9}, {"IV", 4}
            };

            if (roman.Length > 1 && doubles.Keys.Contains(roman.Substring(0, 2)))
            {
                return doubles[roman.Substring(0, 2)] + Decode(roman.Substring(2));
            }

            var singles = new Dictionary<string, int>() { {"M", 1000}, {"D", 500}, {"C", 100}, {"L", 50}, {"X", 10}, {"V", 5}, {"I", 1} };
            if (singles.Keys.Contains(roman.Substring(0, 1)))
            {
                return singles[roman.Substring(0, 1)] + Decode(roman.Substring(1));
            }
            return 0; //should never happen
        }
        
    }
}
