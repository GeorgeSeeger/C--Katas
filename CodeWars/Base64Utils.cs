using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public static class Base64Utils {
        public static string ToBase64(string s) {
            return string.Join("", s.ToCharArray()
                                    .Select(c => (int)c)
                                    .Select(b => Convert.ToString(b, 2))
                                    .Select(str => str.PadLeft(8, '0'))
                                    .SelectMany(str => str)
                                    .Select((c, i) => new IndexHolder { Index = i , IntValue = int.Parse( c.ToString() ) })
                                    .GroupBy(x => x.Index / 6)
                                    .Select(PadWithZeros)
                                    .Select(x => string.Join("", x.Select(v => v.IntValue)))
                                    .Select(str => Convert.ToInt32(str, 2))
                                    .Select(Codes)
                                    .Select((x, i) => new IndexHolder { Index = i, CharValue = x})
                                    .GroupBy(g => g.Index / 4)
                                    .Select(g => string.Join("", g.Select(v => v.CharValue)).PadRight(4, '=')));
        }

        public static string FromBase64(string s) {
            return string.Join("", s.Replace("=", "")
                                    .Select(CodeFor)
                                    .Select(i => Convert.ToString(i, 2))
                                    .Select(str => str.PadLeft(6, '0'))
                                    .SelectMany(str => str)
                                    .Select((c, i) => new IndexHolder(){ CharValue = c, Index = i})
                                    .GroupBy(ih => ih.Index / 8)
                                    .Where(g => g.Count() == 8)
                                    .Select(g => string.Join("", g.Select(ih => ih.CharValue)))
                                    .Select(str => (char) Convert.ToInt32(str, 2)));
        }

        private static char Codes(int i) {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[i];
        }

        private static int CodeFor(char c) {
            return  "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".IndexOf(c);
        }

        private static IEnumerable<IndexHolder> PadWithZeros(IEnumerable<IndexHolder> group, int size = 6) {
            var list = new List<IndexHolder>(group);
            while (list.Count < size) {
                list.Add(new IndexHolder {IntValue = 0});
            }
            return list;
        }

        [Fact]
        public static void SampleValueEncodeTest()
        {
            Assert.Equal(ToBase64("this is a string!!"), "dGhpcyBpcyBhIHN0cmluZyEh");
            Assert.Equal(ToBase64("ee"), "ZWU=");
            Assert.Equal(ToBase64("e"), "ZQ==");
            Assert.Equal(ToBase64(""), "");
        }

        [Fact]
        public static void SampleValueDecodeTest() {
            Assert.Equal("this is a string!!", FromBase64("dGhpcyBpcyBhIHN0cmluZyEh"));
            Assert.Equal("ee", FromBase64("ZWU="));
            Assert.Equal("e", FromBase64("ZQ=="));
            Assert.Equal("", FromBase64(""));
        }
    }

    class IndexHolder {
        public int Index;
        public int IntValue;
        public string StringValue;
        public char CharValue;
    }

    
}
