﻿using System.Linq;
using Xunit;

namespace CodeWars
{
    public class PyramidSlideDown
    {
        public static int LongestSlideDown(int[][] pyramid)
        {
            if (pyramid.Length == 1) return pyramid[0][0];
            var line = pyramid[pyramid.Length - 2];
            var lastLine = pyramid[pyramid.Length - 1];
            return LongestSlideDown(
                pyramid.Take(pyramid.Length - 2)
                       .Concat(new [] {Enumerable.Range(0, line.Length)
                                                 .Select(i => line[i] + lastLine[i] > line[i] + lastLine[i + 1]
                                                                    ? line[i] + lastLine[i]
                                                                    : line[i] + lastLine[i + 1]).ToArray()})
                       .ToArray());
        }


        [Fact]
        public void SmallPyramidTest()
        {
            var smallPyramid = new[]
            {
              new[] {3},
              new[] {7, 4},
              new[] {2, 4, 6},
              new[] {8, 5, 9, 3}
          };

            Assert.Equal(PyramidSlideDown.LongestSlideDown(smallPyramid), 23);
        }

        [Fact]
        public void MediumPyramidTest()
        {
            var mediumPyramid = new[]
            {
              new [] {75},
              new [] {95, 64},
              new [] {17, 47, 82},
              new [] {18, 35, 87, 10},
              new [] {20,  4, 82, 47, 65},
              new [] {19,  1, 23, 75,  3, 34},
              new [] {88,  2, 77, 73,  7, 63, 67},
              new [] {99, 65,  4, 28,  6, 16, 70, 92},
              new [] {41, 41, 26, 56, 83, 40, 80, 70, 33},
              new [] {41, 48, 72, 33, 47, 32, 37, 16, 94, 29},
              new [] {53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14},
              new [] {70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57},
              new [] {91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48},
              new [] {63, 66,  4, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31},
              new [] { 4, 62, 98, 27, 23,  9, 70, 98, 73, 93, 38, 53, 60,  4, 23}
          };

            Assert.Equal(PyramidSlideDown.LongestSlideDown(mediumPyramid), 1074);
        }
    }

}