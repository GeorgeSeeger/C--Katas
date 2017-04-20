using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public class ConnectFour
    {
        private static List<List<string>> Board()
        {
           return new List<List<string>>() { new List<string>(){ },
                                                                             new List<string>(){ },
                                                                             new List<string>(){ },
                                                                             new List<string>(){ },
                                                                             new List<string>(){ },
                                                                             new List<string>(){ },
                                                                             new List<string>(){ } }; 
        } 

        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            var board = Board();
            foreach (var s in piecesPositionList)
            {
                var command = s.Split('_');
                AddToBoard(command, board);
                if (CheckWinner(board))
                {
                    return command[1];
                }
            }
            return "Draw";
        }

        private static void AddToBoard(string[] command, List<List<string>> board )
        {
            var col = command[0];
            var column = FindColumn(col, board);
            column.Add(command[1]);
        }

        private static List<string> FindColumn(string col, List<List<string>> board)
        {
            var colNum = new List<string>() { "A", "B", "C", "D", "E", "F", "G" }.IndexOf(col);
            return board[colNum];
        }

        private static bool CheckWinner(List<List<string>> board)
        {
            //check vertical columns
            foreach (var column in board)
            {
                if (ListHasWinner(column) )
                {
                    return true;
                }
            }

            //check horizontal columns
            for (int i = 0; i < 6; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < 7; j++)
                {
                    row.Add(board[j].Skip(i).Take(1).FirstOrDefault());
                }
                if (ListHasWinner(row))
                {
                    return true;
                }
            }

            //Check the diagonals
            for (int i = 3; i < 8; i++)
            {
                var diagLeft = new List<string>();
                var diagRight = new List<string>();
                var min = new List<int> { i, board.Count() - 1 }.Min();
                for (int j = 0; j <= min; j++)
                {
                    var cell = board[j].Skip(i - j).Take(1).FirstOrDefault();
                    diagLeft.Add(cell);

                    board.Reverse();
                    var cellReversed = board[j].Skip(i - j).Take(1).FirstOrDefault();
                    diagRight.Add(cellReversed);
                    board.Reverse();
                }
                if (ListHasWinner(diagLeft) || ListHasWinner(diagRight))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ListHasWinner(List<string> list)
        {
            var red = "RedRedRedRed";
            var yellow = "YellowYellowYellowYellow";
            var concatList = string.Join("", list.Select(s => s == null ? "null" : s));
            if (concatList.Contains(red) || concatList.Contains(yellow))
            {
                return true;
            }
            return false;
        }

//        public static void Main(string[] args)
//        {
//            List<string> myList = new List<string>()
//            {
//                "C_Yellow",
//                "E_Red",
//                "G_Yellow",
//                "B_Red",
//                "D_Yellow",
//                "B_Red",
//                "B_Yellow",
//                "G_Red",
//                "C_Yellow",
//                "C_Red",
//                "D_Yellow",
//                "F_Red",
//                "E_Yellow",
//                "A_Red",
//                "A_Yellow",
//                "G_Red",
//                "A_Yellow",
//                "F_Red",
//                "F_Yellow",
//                "D_Red",
//                "B_Yellow",
//                "E_Red",
//                "D_Yellow",
//                "A_Red",
//                "G_Yellow",
//                "D_Red",
//                "D_Yellow",
//                "C_Red"
//            };
//
//            var ans = WhoIsWinner(myList);
//            var a = 2;
//        }
    }
}
