


namespace CodeWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CamelCaser
    {

        public static string ToCamelCase(string str)
        {
            var list = str.Split(new char[] {'-', '_'}).ToList();
            return string.Join("", list.Select(s => list.IndexOf(s) > 0 ? Camel(s) : s));
        }

        private static string Camel(string s)
        {
            return s.ElementAt(0).ToString().ToUpper() + s.Substring(1).ToLower();
        }

//        public static void Main(string[] args)
//        {
//            var a = ToCamelCase("the-stealth-warrior");
//            var c = 3;
//        }
    }
}