using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars {
    public class ConwayLife {
        private readonly int[,] cells;
        private int[,] newField;

        public ConwayLife(int[,] cells) {
            this.cells = cells;
            this.newField = new int[cells.GetLongLength(0) + 2, cells.GetLongLength(1) + 2];
        }

        public int[,] NextGeneration() {
            for (var i = 0; i <= newField.GetUpperBound(0); i++) {
                for (var j = 0; j <= newField.GetUpperBound(1); j++) {
                    var neighbours = this.GetNeighbours(i, j);
                    if (!IsOverCrowded(neighbours) && !IsUnderCrowded(neighbours)) newField[i, j] = 1;
                }
            }
            CropNewField();
            return this.cells;
        }

        private void CropNewField() {
            var coords = new List<int>();
            //top
            for (int i = 0; i <= newField.GetUpperBound(0); i++) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(1); j++ ){
                    row.Add(this.newField[i, j]);
                }
                if (row.Contains(1)) {
                    coords.Add(i);
                    break;
                }
            }
            //bottom
            for (int i = newField.GetUpperBound(0); i >= 0; i--) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(1); j++) {
                    row.Add(this.newField[i, j]);
                }
                if (row.Contains(1)) {
                    coords.Add(i);
                    break;
                }
            }
            //right
            for (int i = newField.GetUpperBound(1); i >= 0; i--) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(0); j++) {
                    row.Add(this.newField[j, i]);
                }
                if (row.Contains(1)) {
                    coords.Add(i);
                    break;
                }
            }
            //left
            for (int i = 0; i <= newField.GetUpperBound(1); i++) {
                var row = new List<int>();
                for (int j = 0; j <= newField.GetUpperBound(0); j++) {
                    row.Add(this.newField[j, i]);
                }
                if (row.Contains(1)) {
                    coords.Add(i);
                    break;
                }
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
                    if (k != i && l != j) neighbours.Add(this.cells[k, l]);
                }
            }
            return neighbours.ToArray();
        }

        public static int[,] GetGeneration(int[,] cells, int generation) {
            var field = cells;
            for (var i = 0; i < generation; i++) {
                field = new ConwayLife(field).NextGeneration();
            }
            return field;
        }
    }
}
