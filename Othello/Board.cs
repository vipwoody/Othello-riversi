using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    class Board
    {
        // 0 = white, 1 = black, 8 = null
        public int[][] board;
        // Player0 = white, Player1 = black
        int currentPlayer;
        // default value for the empty square - BE SURE ITS THE SAME HERE AND IN PROGRAM CLASS
        int emptySquare = 7;

        // Board constructor
        public Board(int[][] board, int currentPlayer)
        {
            this.board = new int[board.Length][];
            for (int i = 0; i < board[0].Length; ++i)
                this.board[i] = new int[board[0].Length];

            for (int r = 0; r < board.Length; ++r)
            {
                for (int c = 0; c < board[r].Length; ++c)
                    this.board[r][c] = board[r][c];
            }

            this.currentPlayer = currentPlayer;
        }

        // Helper function to display the current board state
        public void DisplayBoard()
        {
            Console.WriteLine("\n   0 1 2 3 4 5 6 7");
            for (int i = 0; i < this.board.Length; ++i)
            {
                Console.Write(i + "| ");
                for (int j = 0; j < this.board[i].Length; ++j)
                {
                    if (this.board[i][j] == 7)
                    {
                        Console.Write(". ");
                    }
                    else if (this.board[i][j] == 1)//black
                    {
                        Console.Write("B ");
                    }
                    else if (this.board[i][j] == 0)//black
                    {
                        Console.Write("W ");
                    }
                }
                Console.Write("|\n");
            }
        }

        // This function gets any possible moves that a given
        // player can make in a turn, it checks only for valid
        // moves
        public List<List<int>> GetPossibleMoves(int player)
        {
            // Create possible moves list
            List<List<int>> possibleMoves = new List<List<int>>();
            bool foundMultiple = false;

            for (int r = 0; r < board.Length; ++r)
            {
                for (int c = 0; c < board[r].Length; ++c)
                {
                    // If the current spot equals the current players piece
                    if (board[r][c] == player)
                    {
                        // Look for opposing player
                        #region Look Up
                        int nr = r - 1, nc = c;
                        while (nr >= 0)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1-player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr - 1, nc2 = nc;
                                while (nr2 >= 0)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 -= 1;
                                        nc2 -= 0;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Up-Right
                        nr = r - 1;
                        nc = c + 1;
                        while (nr >= 0 && nc < board[0].Length)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr - 1, nc2 = nc + 1;
                                while (nr2 >= 0 && nc2 < board[0].Length)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 -= 1;
                                        nc2 += 1;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Right
                        nr = r;
                        nc = c + 1;
                        while (nc < board[0].Length)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr, nc2 = nc + 1;
                                while (nc2 < board[0].Length)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 -= 0;
                                        nc2 += 1;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Down-Right
                        nr = r + 1;
                        nc = c + 1;
                        while (nr < board.Length && nc < board[0].Length)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr + 1, nc2 = nc + 1;
                                while (nr2 < board.Length && nc2 < board[0].Length)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 += 1;
                                        nc2 += 1;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Down
                        nr = r + 1;
                        nc = c;
                        while (nr < board.Length)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr + 1, nc2 = nc;
                                while (nr2 < board.Length)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 += 1;
                                        nc2 += 0;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Down-Left
                        nr = r + 1;
                        nc = c - 1;
                        while (nr < board.Length && nc >= 0)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr + 1, nc2 = nc - 1;
                                while (nr2 < board.Length && nc2 >= 0)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 += 1;
                                        nc2 -= 1;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Left
                        nr = r;
                        nc = c - 1;
                        while (nc >= 0)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr, nc2 = nc - 1;
                                while (nc2 >= 0)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1 - player)
                                    {
                                        nr2 += 0;
                                        nc2 -= 1;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion

                        #region Look Up-Left
                        nr = r - 1;
                        nc = c - 1;
                        while (nr >= 0 && nc >= 0)
                        {
                            if (board[nr][nc] == emptySquare || board[nr][nc] == player)
                                break;
                            // If opposing player is found
                            else if (board[nr][nc] == 1 - player)
                            {
                                // Keep looking until end or empty square
                                int nr2 = nr - 1, nc2 = nc - 1;
                                while (nr2 >= 0 && nc2 >= 0)
                                {
                                    // Found a possible move
                                    if (board[nr2][nc2] == emptySquare)
                                    {
                                        // Check in each already found possible move
                                        foreach (List<int> outer in possibleMoves)
                                        {
                                            // If the destination point already exists
                                            // This means that one destination affects multiple
                                            // initial points (rows, columns or diagonals)
                                            // Therefore, add the new initial point to the list
                                            if (outer[0] == nc2 && outer[1] == nr2)
                                            {
                                                outer.Add(c);
                                                outer.Add(r);
                                                foundMultiple = true;
                                            }
                                        }
                                        // If a multiple was found, don't add a new entry
                                        if (!foundMultiple)
                                        {
                                            List<int> t = new List<int>();
                                            t.Add(nc2);
                                            t.Add(nr2);
                                            t.Add(c);
                                            t.Add(r);
                                            possibleMoves.Add(t);
                                        }
                                        foundMultiple = false;
                                        break;
                                    }
                                    else if (board[nr2][nc2] == player)
                                        break;
                                    else if (board[nr2][nc2] == 1-player)
                                    {
                                        nr2 -= 1;
                                        nc2 -= 1;
                                    }
                                }
                                break;
                            }
                        }
                        #endregion
                    }
                }
            }

            return possibleMoves;
        }

        // This is a helper function simply to display which
        // moves are allowed given a list of possible moves
        public void DisplayPossibleMoves(List<int[]> pm)
        {
            Console.WriteLine();
            Console.WriteLine("Possible Moves:");
            for (int i = 0; i < pm.Count; ++i)
            {
                for (int j = 0; j < pm[i].Length; ++j)
                {
                    if (j % 2 == 0)
                        Console.Write("x:");
                    else
                        Console.Write(" y:");
                    Console.Write(pm[i][j]);
                }
                Console.WriteLine();
            }
        }

        // This Function makes a move given a certain player,
        // the location the move starts from,
        // and the location the move ends at
        public Board MakeMove(List<int> move, int player)
        {
            // Create temporary board
            int[][] temp = new int[this.board.Length][];
            for (int i = 0; i < this.board[0].Length; ++i)
                temp[i] = new int[this.board[0].Length];

            // Fill it with the current board values
            for (int r = 0; r < this.board.Length; ++r)
            {
                for (int c = 0; c < this.board[r].Length; ++c)
                    temp[r][c] = this.board[r][c];
            }

            // Set new game piece
            temp[move[1]][move[0]] = player;
            bool done = false;
            int indexCtr = 2;

            // Toggle all taken pieces
            int nmx = move[0], nmy = move[1];
            int mx = move[2], my = move[3];
            int incr = 0;
            // This whole section is for toggling the pieces
            // my = move-y          mx = move-x
            // nmy = new-move-y     nmx = new-move-x
            // my is strictly greater than nmy
            while (!done)
            {
                if (my > nmy)
                {
                    // up-left
                    if (mx > nmx)
                    {
                        for (int i = my; i >= nmy; --i)
                        {
                            temp[i][mx + (incr--)] = player;
                        }
                    }
                    // up-right
                    else if (mx < nmx)
                    {
                        for (int i = my; i >= nmy; --i)
                        {
                            temp[i][mx + (incr++)] = player;
                        }
                    }
                    // up
                    else
                    {
                        for (int i = nmy; i <= my; ++i)
                        {
                            for (int j = nmx; j <= mx; ++j)
                                temp[i][j] = player;
                        }
                    }
                }
                // nmy is strictly greater than my
                else if (my < nmy)
                {
                    // down-left
                    if (mx > nmx)
                    {
                        for (int i = my; i <= nmy; ++i)
                        {
                            temp[i][mx + (incr--)] = player;
                        }
                    }
                    // down-right
                    else if (mx < nmx)
                    {
                        for (int i = my; i <= nmy; ++i)
                        {
                            temp[i][mx + (incr++)] = player;
                        }
                    }
                    // down
                    else
                    {
                        for (int i = my; i <= nmy; ++i)
                        {
                            for (int j = mx; j <= nmx; ++j)
                                temp[i][j] = player;
                        }
                    }
                }
                else
                {
                    // Left
                    if (mx > nmx)
                    {
                        for (int j = nmx; j <= mx; ++j)
                            temp[my][j] = player;
                    }
                    // Right
                    else if (mx < nmx)
                    {
                        for (int j = mx; j <= nmx; ++j)
                            temp[my][j] = player;
                    }
                    // This case is a last resort failure case,
                    // should and never does get reached
                    else
                    {
                        Console.WriteLine("Somehow both x and y are the same");
                        Console.WriteLine("DEBUG DAMNIT!");
                        Console.ReadLine();
                    }
                }
                // If there are multiple initial points
                // Increment the index and reset the incr variable
                if (indexCtr < move.Count)
                {
                    mx = move[indexCtr++];
                    my = move[indexCtr++];
                    incr = 0;
                }
                // Else if there are no more initial points, exit loop
                else
                    done = true;
            }
            

            Board b = new Board(temp, player);
            return b;
        }

        // This function evaluates the current board based on
        // a certain player, all it does is count how many
        // onboard pieces belong to the given player
        public int Evaluate(int player)
        {
            int points = 0;

            for (int i = 0; i < this.board.Length; ++i)
            {
                for (int j = 0; j < this.board[0].Length; ++j)
                {
                    if (board[i][j] == player)
                        ++points;
                }
            }
            return points;
        }

        // Returns which player this board belongs to
        public int GetCurrentPlayer() { return currentPlayer; }

        // Checks if an end game state is reached
        public bool IsGameOver(int player)
        {
            if (this.GetPossibleMoves(player).Count <= 0)
                return true;
            else
                return false;
        }
    }
}
