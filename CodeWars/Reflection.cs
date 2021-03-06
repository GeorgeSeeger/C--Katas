﻿using System;
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
        public static string InvokeMethod(string typeName)
        {
            if (typeName == String.Empty) return String.Empty;
            if (typeName == null) return null;
            var type = Type.GetType(typeName);
            if (type == null)
            {
                type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == typeName);
                if (type == null) return null;
            }

            var instance = ConstructObject(type);
            return (string) type.GetMethods().FirstOrDefault(mi => mi.DeclaringType == type).Invoke(instance, new object[] {});
        }

        private static object ConstructObject(Type type)
        {
            var ctors = type.GetConstructors();
            if (type.GetConstructors().FirstOrDefault().GetParameters().Length == 0)
            {
                return ctors.FirstOrDefault().Invoke(null);
            }
            var parameters =
                ctors.Select(c => c.GetParameters().Select(p => ConstructObject(p.ParameterType)));
            return ctors.FirstOrDefault().Invoke(parameters.FirstOrDefault().ToArray());

        }

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

        [Fact]
        public static void InvokeTests()
        {
            Assert.Equal("Holla!", InvokeMethod("InvokeClass"));
            Assert.Equal("yeahhhhh buddy", InvokeMethod("InvokeComplex"));
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

        public class InvokeClass
        {
            public InvokeClass() { }
            public string Output()
            {
                return "Holla!";
            }
        }

        public class InvokeComplex
        {
            public InvokeComplex(Refl a, testClass b) { }

            public string Output()
            {
                return "yeahhhhh buddy";
            }
        }

    }
}
