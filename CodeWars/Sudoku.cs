using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public class Sudoku {
        private readonly int[][] sudokuData;

        private readonly int SqrtN;

        private readonly int N;

        public Sudoku(int[][] sudokuData) {
            this.sudokuData = sudokuData;
            this.N = sudokuData.Length;
            this.SqrtN = sudokuData.All(a => a.Length == sudokuData.Length)
                ? (int) Math.Sqrt(sudokuData.Length)
                : 0;
        }

        public bool IsValid() {
            if (SqrtN == 0) return false;
            return this.sudokuData.All(IsSelectionValid) //rows
                   && Enumerable.Range(0, N) //columns
                       .Select(i => Enumerable.Range(0, N))
                       .Select((r, i) => r.Select(j => sudokuData[j][i]))
                       .All(IsSelectionValid)
                   && new int[SqrtN] //boxes
                       .Select(r => new int[SqrtN].Select(s => new int[SqrtN].Select(t => Enumerable.Range(0, SqrtN))))
                       .Select((a, i) => a.Select((b, j) => b.Select((c, k) => c.Select(l => sudokuData[i * SqrtN + k][j * SqrtN + l]))))
                       .SelectMany(a => a.Select(b => b.SelectMany(c => c)))
                       .All(IsSelectionValid);

        }

        private bool IsSelectionValid(IEnumerable<int> a) {
            return a.All(i => 1 <= i && i <= N) && a.Sum() == (int) (N / 2.0 * (N + 1)) && a.Count() == a.Distinct().Count();
        }
    }

    public class SudokuTests {
            [Fact]
        public void ThreeByThreeTest() {
            var goodSudoku1 = new Sudoku(
                new int[][] {
                    new int[] {7,8,4, 1,5,9, 3,2,6},
                    new int[] {5,3,9, 6,7,2, 8,4,1},
                    new int[] {6,1,2, 4,3,8, 7,5,9},

                    new int[] {9,2,8, 7,1,5, 4,6,3},
                    new int[] {3,5,7, 8,4,6, 1,9,2},
                    new int[] {4,6,1, 9,2,3, 5,8,7},

                    new int[] {8,7,6, 3,9,4, 2,1,5},
                    new int[] {2,4,3, 5,6,1, 9,7,8},
                    new int[] {1,9,5, 2,8,7, 6,3,4}
                });
            Assert.True(goodSudoku1.IsValid());
        }

        [Fact]
        public void TwoByTwoTest() {
            var goodSudoku2 = new Sudoku(
                new int[][] {
                    new int[] {1,4, 2,3},
                    new int[] {3,2, 4,1},

                    new int[] {4,1, 3,2},
                    new int[] {2,3, 1,4}
                });
            Assert.True(goodSudoku2.IsValid());
        }
    }
}
