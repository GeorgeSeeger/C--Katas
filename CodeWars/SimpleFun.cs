using System;
using System.Linq;
using Xunit;

namespace CodeWars
{
    public class SimpleFun
    {
        public int RectangleRotation(int a, int b)
        {
            //rotate the coordinate base to the rows of diagonals
            var root2 = Math.Sqrt(2);
            var diagNum = Math.Floor(1 + b / root2);
            var sides = (int) Math.Floor(a / root2);
            var diagRows = 2 * sides + 1;
            var biggestRow = (int) Math.Floor(b / root2) % 2 == 0 ? sides % 2 : (sides + 1) % 2;
            return (int) Enumerable.Range(0, diagRows)
                                   .Select(i => i % 2 == biggestRow ? diagNum : diagNum - 1)
                                   .Aggregate((j, sum) => j + sum);
        }

        [Fact]
        public void RectRotationTests()
        {
            Assert.Equal(5, RectangleRotation(2, 2));
            Assert.Equal(23, RectangleRotation(6, 4));
            Assert.Equal(65, RectangleRotation(2, 30));
            Assert.Equal(49, RectangleRotation(8, 6));
            Assert.Equal(333, RectangleRotation(16, 20));
        }
    }
}
