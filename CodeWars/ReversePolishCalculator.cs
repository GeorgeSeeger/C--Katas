using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodeWars
{
    public class ReversePolishCalculator
    {
        public double evaluate(String expr)
        {
            if (expr == "") return 0.0;

            var input = new Queue<string>(expr.Split(' '));
            var stack = new Stack<double>();
            var operators = new[] { "+", "-", "*", "/" };
            while (input.Count > 0)
            {
                var token = input.Dequeue();
                if (operators.Contains(token))
                {
                    var a = stack.Pop();
                    var b = stack.Pop();
                    stack.Push(Calculate(token, b, a));
                }
                else
                {
                    stack.Push(double.Parse(token));
                }
            }
            return stack.Last();
        }

        private double Calculate(string token, double b, double a)
        {
            switch (token)
            {
                case "+":
                    return b + a;
                case "-":
                    return b - a;
                case "*":
                    return b * a;
                case "/":
                    return b / a;
                default:
                    return 0.0;
            }
        }

        [Fact]
        public void Tests()
        {
            Assert.Equal(4, evaluate("1 3 +"));
            Assert.Equal(-2, evaluate("1 3 -"));
            Assert.Equal(3, evaluate("1 3 *"));
            Assert.Equal(2, evaluate("4 2 /"));
            Assert.Equal(3.6, evaluate("3.6"));
        }
    }
}
