using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Librarian
    {
        public static string OkkOokOo(string okkOookk)
        {
            var bytes = okkOookk.Replace("!", "").Split(new[] {"? "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim().Replace(", ", "").ToLower().Replace('k', '1').Replace('o', '0'))
                .Select(s => Convert.ToByte(s, 2)).ToArray();

            return System.Text.Encoding.UTF8.GetString(bytes);
        }

//        public static void Main(string[] args)
//        {
//            OkkOokOo("Ok, Ook, Ooo!");
//        }
    }
}
