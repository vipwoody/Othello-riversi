using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    class Program
    {
        static void Main(string[] args)
        {
            // Maximum depth the algorithm should search to
            int maxDepth = 4;
            // Board attributes for its size
            int boardRows = 8, boardCols = 8;
            // Default empty square value - BE SURE ITS THE SAME HERE AND IN BOARD CLASS
            int emptySquare = 7;
            // Containers for the miniMax return values
            List<int> miniMaxValues = new List<int>();
            // Container to store and pass on only the move returned by miniMax
            List<int> chosenMove = new List<int>();
            // Initial values for blackPoint and whitePoints
            int blackPoints = 0, whitePoints = 0;
            // String to say who won the game
            string winner = "None";

            #region Create Start
            // Create rows
            int[][] startState = new int[boardRows][];
            // Create cols
            for (int i = 0; i < boardRows; ++i)
                startState[i] = new int[boardCols];

            // fill in the board with start state
            for (int r = 0; r < boardRows; ++r)
            {
                for (int c = 0; c < boardCols; ++c)
                {
                    if (r == 3 && c == 3)
                        startState[r][c] = 1;
                    else if (r == 3 && c == 4)
                        startState[r][c] = 0;
                    else if (r == 4 && c == 3)
                        startState[r][c] = 0;
                    else if (r == 4 && c == 4)
                        startState[r][c] = 1;
                    else 
                        startState[r][c] = emptySquare;
                }
            }
            #endregion

            Board board = new Board(startState, 1);

            board.DisplayBoard();

            #region MiniMax Driver Section
            while (true)
            {
                // Player 1 (Black) Moves first

                miniMaxValues = MiniMax(board, 1, 1, maxDepth, 0);
                //miniMaxValues = MiniMaxMove(board, 1, 1, maxDepth, 0);
                //miniMaxValues = Unpredictable(board, 1);
                //miniMaxValues = Greedy(board, 1);
                //miniMaxValues = Human(board, 1);

                if (((miniMaxValues[1] == -1 || miniMaxValues[2] == -1 || miniMaxValues[3] == -1 || miniMaxValues[4] == -1) &&
                    board.IsGameOver(0)) || //other player cannot move also
                    (board.Evaluate(1) == 0) || (board.Evaluate(0) == 0)// one player or the other have 0 point
                    )
                {
                    blackPoints = board.Evaluate(1);
                    whitePoints = board.Evaluate(0);
                    break;
                }
                else if(miniMaxValues[1] != -1 && miniMaxValues[2] != -1 && miniMaxValues[3] != -1 && miniMaxValues[4] != -1)
                {
                    // New move coordinates
                    bool skip = true;
                    chosenMove.Clear();
                    foreach (int val in miniMaxValues)
                    {
                        if (skip)
                        {
                            skip = false;
                            continue;
                        }
                        chosenMove.Add(val);
                    }
                    board = board.MakeMove(chosenMove, 1);
                    board.DisplayBoard();
                }

                // Player 2 (White) Moves second

                //miniMaxValues = MiniMax(board, 0, 0, maxDepth, 0);
                //miniMaxValues = MiniMaxMove(board, 0, 0, maxDepth, 0);
                //miniMaxValues = Unpredictable(board, 0);
                miniMaxValues = Greedy(board, 0);
                //miniMaxValues = Human(board, 0);

                if (((miniMaxValues[1] == -1 || miniMaxValues[2] == -1 || miniMaxValues[3] == -1 || miniMaxValues[4] == -1) &&
                    board.IsGameOver(1)) || //other player cannot move also
                    (board.Evaluate(1) == 0) || (board.Evaluate(0) == 0)// one player or the other have 0 point
                    )
                {
                    blackPoints = board.Evaluate(1);
                    whitePoints = board.Evaluate(0);
                    break;
                }
                else if (miniMaxValues[1] != -1 && miniMaxValues[2] != -1 && miniMaxValues[3] != -1 && miniMaxValues[4] != -1)
                {
                    // New move coordinates
                    bool skip = true;
                    chosenMove.Clear();
                    foreach (int val in miniMaxValues)
                    {
                        if (skip)
                        {
                            skip = false;
                            continue;
                        }
                        chosenMove.Add(val);
                    }
                    board = board.MakeMove(chosenMove, 0);
                    board.DisplayBoard();
                }
            }
            #endregion

            // End game output
            if (blackPoints > whitePoints)
                winner = "Black Wins!";
            else if (whitePoints > blackPoints)
                winner = "White Wins!";
            else
                winner = "It's a Draw!";

            Console.WriteLine("Game Over!");
            Console.WriteLine("Black: " + blackPoints + "   White: " + whitePoints);
            Console.WriteLine(winner);

            #region System("pause") Equivalent
            Console.WriteLine();
            Console.Write("Press Enter To Exit");
            Console.ReadLine();
            #endregion
        }

        static List<int> Human(Board board, int player)
        {
            List<int> humanReturn = new List<int>();
            humanReturn.Add(board.Evaluate(player));
            humanReturn.Add(-1);
            humanReturn.Add(-1);
            humanReturn.Add(-1);
            humanReturn.Add(-1);

            if (board.GetPossibleMoves(player).Count == 0)
            {
                return humanReturn;
            }

            while (true)
            {
                Console.Write("enter x-position:");
                humanReturn[1] = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.Write("enter y-position:");
                humanReturn[2] = int.Parse(Console.ReadLine());
                Console.WriteLine();

                foreach (List<int> move in board.GetPossibleMoves(player))
                {
                    if (humanReturn[1] == move[0] && humanReturn[2] == move[1])
                    {
                        Board newBoard = board.MakeMove(move, player);
                        humanReturn[3] = move[2];
                        humanReturn[4] = move[3];
                        return humanReturn;
                    }
                }
                Console.WriteLine("invalid move!");
            }
        }

        static List<int> MiniMax(Board board, int player, int callingPlayer, int maxDepth, int currentDepth)
        {
            // This array contains: bestScore, bestMoveX, bestMoveY in that order
            List<int> miniMaxReturn = new List<int>();
            // This array contains: bestScore, bestMoveX, bestMoveY in that order
            List<int> currentReturn = new List<int>();

            // Check for recursion end
            if(board.IsGameOver(player) || currentDepth == maxDepth)
            {
                miniMaxReturn.Add(board.Evaluate(callingPlayer));
                miniMaxReturn.Add(-1);
                miniMaxReturn.Add(-1);
                miniMaxReturn.Add(-1);
                miniMaxReturn.Add(-1);

                return miniMaxReturn;
            }

            // Depending on the player we choose a starting bestScore
            // Negative if currentPlayer so we always chose a higher scored board
            if (board.GetCurrentPlayer() == player)
                miniMaxReturn.Add(-int.MaxValue);
            // Positive for opponent so that they always choose a lower scored
            // board
            else
                miniMaxReturn.Add(int.MaxValue);

            // Contains default moveX, moveY
            miniMaxReturn.Add(-1);
            miniMaxReturn.Add(-1);
            miniMaxReturn.Add(-1);
            miniMaxReturn.Add(-1);

            Board newBoard;
            // Recurse for each move
            foreach (List<int> move in board.GetPossibleMoves(player))
            {
                // Get new board configuration
                newBoard = board.MakeMove(move, player);

                // Recurse till return
                currentReturn = MiniMax(newBoard, 1-player, callingPlayer, maxDepth, currentDepth+1);

                // Update the best score based on which player is playing
                if (board.GetCurrentPlayer() == player)
                {
                    if (currentReturn[0] > miniMaxReturn[0])
                    {
                        miniMaxReturn.Clear();
                        miniMaxReturn.Add(currentReturn[0]);
                        foreach (int val in move)
                            miniMaxReturn.Add(val);
                    }
                }
                else
                {
                    if (currentReturn[0] < miniMaxReturn[0])
                    {
                        miniMaxReturn.Clear();
                        miniMaxReturn.Add(currentReturn[0]);
                        foreach (int val in move)
                            miniMaxReturn.Add(val);
                    }
                }
            }

            return miniMaxReturn;
        }

        static List<int> Greedy(Board board, int player) 
        {
            List<int> greedyReturn = new List<int>(); // 0: points after move, 1: move X, 2: move Y, 3: helper X, 4: helper Y 
            greedyReturn.Add(board.Evaluate(player));
            greedyReturn.Add(-1);
            greedyReturn.Add(-1);
            greedyReturn.Add(-1);
            greedyReturn.Add(-1);

            List<List<int>> possibilities = board.GetPossibleMoves(player);

            if (possibilities.Count == 0)
            {
                return greedyReturn;
            }

            Board newBoard;

            foreach (List<int> move in board.GetPossibleMoves(player))
            {
                // Get new board configuration
                newBoard = board.MakeMove(move, player);

                if (newBoard.Evaluate(player) > greedyReturn[0])//if better than last possible move
                {
                    greedyReturn.Clear();//clear the last one
                    greedyReturn.Add(newBoard.Evaluate(player));
                    foreach (int val in move)
                        greedyReturn.Add(val);
                }
            }
            return greedyReturn;
        }

        static List<int> Unpredictable(Board board, int player)//randomly choose a move
        {
            List<int> UnpredictableReturn = new List<int>(); // 0: points after move, 1: move X, 2: move Y, 3: helper X, 4: helper Y 

            List<List<int>> possibilities = board.GetPossibleMoves(player);

            if (possibilities.Count == 0)
            {
                UnpredictableReturn.Add(board.Evaluate(player));
                UnpredictableReturn.Add(-1);
                UnpredictableReturn.Add(-1);
                UnpredictableReturn.Add(-1);
                UnpredictableReturn.Add(-1);

                return UnpredictableReturn;
            }

            Random random = new Random();
            int randomMove = random.Next(0, possibilities.Count);
            List<int> move = possibilities[randomMove];

            UnpredictableReturn.Add(board.MakeMove(move, player).Evaluate(player));
            foreach (int val in move)
                UnpredictableReturn.Add(val);

            return UnpredictableReturn;
        }

        static List<int> MiniMaxMove(Board board, int player, int callingPlayer, int maxDepth, int currentDepth)//MiniMax base on number of moves
        {
            // This array contains: bestScore, bestMoveX, bestMoveY in that order
            List<int> miniMaxReturn = new List<int>();
            // This array contains: bestScore, bestMoveX, bestMoveY in that order
            List<int> currentReturn = new List<int>();

            // Check for recursion end
            if (board.IsGameOver(player) || currentDepth == maxDepth)
            {
                miniMaxReturn.Add(board.GetPossibleMoves(callingPlayer).Count);
                miniMaxReturn.Add(-1);
                miniMaxReturn.Add(-1);
                miniMaxReturn.Add(-1);
                miniMaxReturn.Add(-1);

                return miniMaxReturn;
            }


            // Depending on the player we choose a starting bestScore
            // Negative if currentPlayer so we always chose a higher scored board
            if (board.GetCurrentPlayer() == player)
                miniMaxReturn.Add(-int.MaxValue);
            // Positive for opponent so that they always choose a lower scored
            // board
            else
                miniMaxReturn.Add(int.MaxValue);

            // Contains default moveX, moveY
            miniMaxReturn.Add(-1);
            miniMaxReturn.Add(-1);
            miniMaxReturn.Add(-1);
            miniMaxReturn.Add(-1);

            Board newBoard;
            // Recurse for each move
            foreach (List<int> move in board.GetPossibleMoves(player))
            {
                // Get new board configuration
                newBoard = board.MakeMove(move, player);

                // Recurse till return
                currentReturn = MiniMaxMove(newBoard, 1 - player, callingPlayer, maxDepth, currentDepth + 1);

                // Update the best score based on which player is playing
                if (board.GetCurrentPlayer() == player)
                {
                    if (currentReturn[0] > miniMaxReturn[0])
                    {
                        miniMaxReturn.Clear();
                        miniMaxReturn.Add(currentReturn[0]);
                        //miniMaxReturn[0] = currentReturn[0];
                        foreach(int val in move)
                            miniMaxReturn.Add(val);
                    }
                }
                else
                {
                    if (currentReturn[0] < miniMaxReturn[0])
                    {
                        miniMaxReturn.Clear();
                        miniMaxReturn.Add(currentReturn[0]);
                        //miniMaxReturn[0] = currentReturn[0];
                        foreach (int val in move)
                            miniMaxReturn.Add(val);
                    }
                }
            }

            return miniMaxReturn;
        }
    }
}
