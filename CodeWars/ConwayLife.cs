using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public class ConwayLife {
        private readonly int[,] cells;
        private int[,] newField;
        public int[,] Cells => newField;

        public ConwayLife(int[,] cells) {
            this.cells = cells;
            this.newField = new int[cells.GetLongLength(0) + 2, cells.GetLongLength(1) + 2];
        }

        public ConwayLife NextGeneration() {
            for (var i = 0; i <= newField.GetUpperBound(0); i++) {
                for (var j = 0; j <= newField.GetUpperBound(1); j++) {
                    var neighbours = this.GetNeighbours(i, j);
                    if (ItLives(neighbours, this.GetCellAt(i, j))) newField[i, j] = 1;
                }
            }
            CropNewField();
            return new ConwayLife(this.newField);
        }

        private int GetCellAt(int i, int j) {
            if (i < 1 || i + 1 > this.cells.GetUpperBound(0) || j < 1 || j + 1 > this.cells.GetUpperBound(1)) return 0;
            return this.cells[i, j];
        }

        private bool ItLives(int[] neighbours, int cell) {
            if (cell == 1 && !IsUnderCrowded(neighbours) && !IsOverCrowded(neighbours)) return true;
            return neighbours.Count(k => k == 1) == 3 && cell == 0;
        }

        private void CropNewField() {
            int top = 0;
            int bottom = 0;
            int left = 0;
            int right = 0;
            for (int i = 0; i <= newField.GetUpperBound(0); i++) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(1); j++ ){
                    row.Add(this.newField[i, j]);
                }
                if (row.Contains(1)) {
                    bottom = i;
                    break;
                }
            }
            for (int i = newField.GetUpperBound(0); i >= 0; i--) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(1); j++) {
                    row.Add(this.newField[i, j]);
                }
                if (row.Contains(1)) {
                    top = i;
                    break;
                }
            }
            for (int i = newField.GetUpperBound(1); i >= 0; i--) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(0); j++) {
                    row.Add(this.newField[j, i]);
                }
                if (row.Contains(1)) {
                    right = i;
                    break;
                }
            }
            for (int i = 0; i <= newField.GetUpperBound(1); i++) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(0); j++) {
                    row.Add(this.newField[j, i]);
                }
                if (row.Contains(1)) {
                    left = i;
                    break;
                }
            }
            if (new[] {top, bottom, left, right}.Any(i => i > 0)) {
                var temp = new int[top - bottom + 1, right - left + 1];
                int k = 0;
                int l = 0;
                for (int i = bottom; i <= top; i++) {
                    for (int j = left; j <= right; j++) {
                        temp[k, l] = newField[i, j];
                        l++;
                    }
                    l = 0;
                    k++;
                }
                newField = temp;
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
            var rowLBound = i - 2 < 0 ? 0 : i - 2;
            var rowUBound = i > this.cells.GetUpperBound(0) ? this.cells.GetUpperBound(0) : i ;
            var colLBound = j - 2 < 0 ? 0 : j - 2;
            var colUBound = j > this.cells.GetUpperBound(1) ? this.cells.GetUpperBound(1) : j;
            for (var k = rowLBound; k <= rowUBound; k++) {
                for (var l = colLBound; l <= colUBound; l++) {
                    if (k + 1 != i || l + 1 != j) neighbours.Add(this.cells[k, l]);
                }
            }
            return neighbours.ToArray();
        }

        public static int[,] GetGeneration(int[,] cells, int generation) {
            var field = new ConwayLife(cells);
            for (var i = 0; i < generation; i++) {
                field.NextGeneration();
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
