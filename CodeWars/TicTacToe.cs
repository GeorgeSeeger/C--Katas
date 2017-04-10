using System.Linq;

namespace CodeWars
{
    public class TicTacToe
    {
        public int IsSolved(int[,] board)
        {
            var results = new[] {CheckRows(board), CheckColumns(board), CheckDiagonals(board)}.Max();
            if (results == 0 && CheckFull(board)) { return -1; }
            return results;
        }

        private int CheckRows(int[,] board)
        {
            int[] row;
            for(int i = 0; i < 3; i++)
            {
                row = new int[] {board[0, i], board[1, i], board[2, i] };
                if (row[0] == row[1] && row[1] == row[2] && row[0] != 0)
                {
                    return row[0];
                }
            }
            return 0;
        }

        private int CheckColumns(int[,] board)
        {
            int[] column;
            for (int i = 0; i < 3; i++)
            {
                column = new int[] { board[i, 0], board[i, 1], board[i, 2] };
                if (column[0] == column[1] && column[1] == column[2] && column[0] != 0)
                {
                    return column[0];
                }
            }
            return 0;
        }

        private int CheckDiagonals(int[,] board)
        {
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[1, 1] != 0)
            {
                return board[0, 0];
            }
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[1, 1] != 0)
            {
                return board[0, 0];
            }
            return 0;
        }

        private bool CheckFull(int[,] board)
        {
            foreach (int i in board)
            {
                if ( i == 0) {  return  false;}
            }
            return true;
        }
    }
}
