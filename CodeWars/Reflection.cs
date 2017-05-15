using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public static class Reflection
    {
        //        public static string InvokeMethod(string typeName)
        //        {
        //            if (typeName == String.Empty) return String.Empty;
        //            if (typeName == null) return null;
        //
        //            var t = Type.GetType(typeName);
        //            if (t == null) return null;
        //
        //            var ctors = t.GetConstructors(BindingFlags.Public);
        //            var obj = ctors[0].Invoke(new object[] {});
        //            return obj.
        //        }

        public static string ConcatStringMembers(object testObject)
        {
            if (testObject == null) return string.Empty;
            var type = testObject.GetType();
            var strings = type.GetFields()
                              .Where(pi => pi.FieldType == typeof(string))
                              .Select(pi => (string)pi.GetValue(testObject))
                              .Concat(type.GetMethods()
                                          .Where(mi => mi.DeclaringType == type && mi.ReturnType.IsInstanceOfType("") && mi.GetParameters().Length == 0)
                                          .Select(mi => (string) mi.Invoke(testObject, new object[] {})))
                                .GroupBy(s => s.Length)
                                .OrderByDescending(g => g.Key)
                                .Select(g => g.OrderBy(s => s))
                                .SelectMany(g => g);
            return string.Join("", strings);
        }

        [Fact]
        public static void MemberResultsTest()
        {
            Assert.Equal("Test-OutputStarkTen", ConcatStringMembers(new Refl()));
            Assert.Equal("OutputIt", ConcatStringMembers(new testClass()));
            Assert.Equal("", ConcatStringMembers(null));
        }

        public class Refl
        {

            public string B = "Ten";

            public string Output()
            {
                return "Test-Output";
            }

            public int AddInts(int i1, int i2)
            {
                return i1 + i2;
            }

            public string TonysLastname()
            {
                return "Stark";
            }
        }

        public class testClass
        {
            public string Output1()
            {
                return "Output";
            }

            public string Output2()
            {
                return "It";
            }
        }


    }
}
