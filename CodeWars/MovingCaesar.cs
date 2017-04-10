using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class MovingCaesar
    {
        public static List<string> movingShift(string s, int shift)
        {
            int shiftIndex = shift;
            return FiveSplit(string.Join("", s.Select(c => CharShift(c, shiftIndex++)).ToList()));
        }

        public static string demovingShift(List<string> s, int shift)
        {
            var str = string.Join("", s);
            int shiftIndex = -1 * shift;
            return string.Join("", str.Select(c => CharShift(c, shiftIndex--)).ToList());

        }

        private static char CharShift(char c, int shift)
        {
            if (!IsLetter(c)) {  return c; }
            var upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lower = "abcdefghijklmnopqrstuvwxyz";
            if (upper.IndexOf(c) > -1)
            {
                return Rotate(upper, c, shift);
            }
            return Rotate(lower, c, shift);
        }

        private static char Rotate(string alphabet, char c, int shift)
        {
            var newIndex = (alphabet.IndexOf(c) + shift) % 26;
            newIndex = newIndex < 0 ? 26 + newIndex : newIndex;
            return alphabet.ElementAt(newIndex);
        }
        private static List<string> FiveSplit(string s)
        {
            var chunkLength = (int)Math.Ceiling( s.Length / 5.0);
            var list = Enumerable.Range(0, 4).Select(i => s.Substring(i * chunkLength, chunkLength)).ToList();
            list.Add(s.Substring(4 * chunkLength));
            return list;
        }

        private static bool IsLetter(char c)
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return letters.IndexOf(c) > -1;
        }

//        static void Main(string[] args)
//        {
//            var c = movingShift("I should have known that you would have a perfect answer for me!!!", 1);
//            var d = demovingShift(c, 1);
//            var e = "End";
//        }
    }
}
