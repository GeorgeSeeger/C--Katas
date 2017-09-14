using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public class ConwayLife {
        private readonly int[,] cells;
        private int[,] newCells;
        public int[,] Cells => cells;

        public ConwayLife(int[,] cells) {
            this.cells = cells;
            this.newCells = new int[cells.GetLongLength(0) + 2, cells.GetLongLength(1) + 2];
        }

        public ConwayLife NextGeneration() {
            for (var i = 0; i <= newCells.GetUpperBound(0); i++) {
                for (var j = 0; j <= newCells.GetUpperBound(1); j++) {
                    var neighbours = this.GetNeighbours(i, j);
                    if (ItLives(neighbours, this.GetCellAt(i, j))) newCells[i, j] = 1;
                }
            }
            CropNewField();
            return new ConwayLife(this.newCells);
        }

        private int GetCellAt(int i, int j) {
            if (i < 1 || i - 1 > this.cells.GetUpperBound(0) || j < 1 || j - 1 > this.cells.GetUpperBound(1)) return 0;
            return this.cells[i - 1, j - 1];
        }

        private bool ItLives(int[] neighbours, int cell) {
            if (cell == 1 && !IsUnderCrowded(neighbours) && !IsOverCrowded(neighbours)) return true;
            return neighbours.Count(k => k == 1) == 3 && cell == 0;
        }

        private void CropNewField() {
            var top = 0;
            var bottom = 0;
            var left = 0;
            var right = 0;
            for (int i = 0; i <= newCells.GetUpperBound(0); i++) {
                var row = new List<int>();
                for (int j = 0; j <= newCells.GetUpperBound(1); j++) {
                    row.Add(this.newCells[i, j]);
                }
                if (row.Contains(1)) {
                    bottom = i;
                    break;
                }
            }
            for (int i = newCells.GetUpperBound(0); i >= 0; i--) {
                var row = new List<int>();
                for (int j = 0; j <= newCells.GetUpperBound(1); j++) {
                    row.Add(this.newCells[i, j]);
                }
                if (row.Contains(1)) {
                    top = i;
                    break;
                }
            }
            for (int i = newCells.GetUpperBound(1); i >= 0; i--) {
                var row = new List<int>();
                for (int j = 0; j <= newCells.GetUpperBound(0); j++) {
                    row.Add(this.newCells[j, i]);
                }
                if (row.Contains(1)) {
                    right = i;
                    break;
                }
            }
            for (int i = 0; i <= newCells.GetUpperBound(1); i++) {
                var row = new List<int>();
                for (int j = 0; j <= newCells.GetUpperBound(0); j++) {
                    row.Add(this.newCells[j, i]);
                }
                if (row.Contains(1)) {
                    left = i;
                    break;
                }
            }
            CropBetween(top, bottom, left, right);
        }

        private void CropBetween(int top, int bottom, int left, int right) {
            if (new[] { top, bottom, left, right }.Any(i => i > 0)) {
                var temp = new int[top - bottom + 1, right - left + 1];
                int k = 0;
                int l = 0;
                for (int i = bottom; i <= top; i++) {
                    for (int j = left; j <= right; j++) {
                        temp[k, l] = newCells[i, j];
                        l++;
                    }
                    l = 0;
                    k++;
                }
                newCells = temp;
            }
            else {
                newCells = new int[0, 0];
            }
        }

        private bool IsUnderCrowded(IEnumerable<int> neighbours) {
            return neighbours.Count(i => i == 1) < 2;
        }

        private bool IsOverCrowded(IEnumerable<int> neighbours) {
            return neighbours.Count(i => i == 1) > 3;
        }

        private int[] GetNeighbours(int i, int j) {
            var neighbours = new List<int>();
          for (var k = i - 1; k <= i + 1; k++) {
                for (var l = j - 1; l <= j + 1; l++) {
                    if (k != i || l != j) neighbours.Add(this.GetCellAt(k, l));
                }
            }
            return neighbours.ToArray();
        }

        public static int[,] GetGeneration(int[,] cells, int generation) {
            var field = new ConwayLife(cells);
            for (var i = 0; i < generation; i++) {
                field = field.NextGeneration();
            }
            return field.Cells;
        }
    }

    public class ConwayLifeTests {
        [Fact]
        public void Test() {
            var test = new ConwayLife(new int[,] {{1, 0, 0}, {0, 1, 1}, {1, 1, 0}}).NextGeneration();
            Assert.Equal(new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 1, 1 } }, test.Cells);
        }
    }
}
