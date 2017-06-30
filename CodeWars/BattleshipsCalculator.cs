using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public class BattleshipsCalculator {

        public BattleshipsCalculator(int[,] board, int[,] strikes)
        {
            this.Board = board;
            this.Battleships = new Dictionary<int, Battleship>();
            MakeBattleshipNotWar();
            MakeWar(strikes);
        }

        private int[,] Board { get; set; }

        public Dictionary<int, Battleship> Battleships;

        private static int[] BattleshipIndicators => new[] {1, 2, 3};

        private void MakeBattleshipNotWar()
        {
            for (var i = 0; i < Board.GetLength(0); i++) {
                for (int j = 0; j < Board.GetLength(1); j++) {
                    if (BattleshipIndicators.Contains(Board[i, j])) {
                        if (!Battleships.ContainsKey(Board[i, j])) {Battleships.Add(Board[i, j], new Battleship());}
                        Battleships[Board[i, j]].AddBoatAt(new[] {i, j});
                    }
                }
            }
        }

        private void MakeWar(int[,] strikes)
        {
            for (var i = 0; i < strikes.GetLength(0); i++) {
                foreach (var battleship in Battleships.Values) {
                    var coord = new[] {strikes[i, 1] - 1, strikes[i, 0] - 1 };
                    battleship.MissileStrikeAt(coord);
                }
            }
        }

        public Dictionary<string, double> Results() {
            return new Dictionary<string, double> {
                {"sunk", Battleships.Values.Count(b => b.IsSunk())},
                {"damaged", Battleships.Values.Count(b => !b.Unharmed() && !b.IsSunk()) },
                {"notTouched", Battleships.Values.Count(b => b.Unharmed()) },
                {"points", 0.5 * Battleships.Values.Count(b => !b.Unharmed() && !b.IsSunk()) +  Battleships.Values.Count(b => b.IsSunk()) - Battleships.Values.Count(b => b.Unharmed()) }
            };
        }

        
    }

    public class Battleship
    {
        public Battleship()
        {
            this.Coords = new List<int[]>();
            this.Hits = new List<int[]>();
        }

        public List<int[]> Coords { get; }

        public List<int[]> Hits { get; }

        public void AddBoatAt(int[] coord)
        {
            Coords.Add(coord);
        }

        public void MissileStrikeAt(int[] coord)
        {
            if (CoordsContains(coord)) {
                Hits.Add(coord);
            }
        }

        private bool CoordsContains(int[] coord)
        {
            return ListContains(Coords, coord) && !ListContains(Hits, coord);
        }

        private bool ListContains(List<int[]> list, int[] coord)
        {
            foreach (var array in list) {
                if (array[0] == coord[0] && array[1] == coord[1]) {
                    return true;
                }
            }
            return false;
        }

        public bool IsSunk()
        {
            return Hits.Count == Coords.Count;
        }

        public int HitCount()
        {
            return Hits.Count;
        }

        public bool Unharmed()
        {
            return Hits.Count == 0;
        }

        [Fact]
        public void BasicTest1() {
            int[,] board = { { 0, 0, 1, 0 },
                             { 0, 0, 1, 0 },
                             { 0, 0, 1, 0 } };
            int[,] attacks = { { 3, 1 }, { 3, 2 }, { 3, 3 } };
            var result = new BattleshipsCalculator(board, attacks).Results();
            Assert.Equal(1, result["sunk"]);
            Assert.Equal(0, result["damaged"]);
            Assert.Equal(0, result["notTouched"]);
            Assert.Equal(1, result["points"]);
        }

        [Fact]
        public void BasicTest2() {
            int[,] board = { { 3, 0, 1 },
                             { 3, 0, 1 },
                             { 0, 2, 1 },
                             { 0, 2, 0 } };
            int[,] attacks = { { 2, 1 }, { 2, 2 }, { 3, 2 }, { 3, 3 } };
            var result = new BattleshipsCalculator(board, attacks).Results();
            Assert.Equal(1, result["sunk"]);
            Assert.Equal(1, result["damaged"]);
            Assert.Equal(1, result["notTouched"]);
            Assert.Equal(0.5, result["points"]);
        }
    }
}
