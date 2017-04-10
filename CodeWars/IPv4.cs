using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class IPv4
    {
        public static bool is_valid_IP(string ipAddress)
        {
            string validChars = ".0123456789";
            if (ipAddress.Any(c => !validChars.Contains(c)) || ipAddress == string.Empty) {  return false; }

            var bits = ipAddress.Split('.');
            if (bits.Count() != 4) { return false; }
            if (bits.Select(int.Parse).Any(i => i > 255 || i < 0)) { return false; }
            if (bits.Any(s => s.Substring(0,1) == "0" && s.Length > 1)) {  return false; }
            return true;
        }

//        public static void Main(string[] args)
//        {
//            is_valid_IP("12.255.56.1");
//            is_valid_IP("");
//        }
    }
}
