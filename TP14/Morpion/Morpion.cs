using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Morpion
{
    public class Game
    {
        // 0 1 2
        // 3 4 5
        // 6 7 8
        public char[] board;
        public uint depth;

        public Game(string board, uint depth)
        {
            this.board = new char[9];
            for (int i = 0; i < 9; i++)
            {
                this.board[i] = board[i];
            }
            this.depth = depth;
        }
        
        // display of the game with the numbers corresponding to the boxes
        // the display is shifted to the right in case you wish to display the board of the current game to its left
        void positions()
        {
            if (!Console.IsOutputRedirected)
            {
                int y = 0;
                Console.SetCursorPosition(20, y++);
                Console.WriteLine(" ___________");
                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(20, y++);
                    Console.WriteLine("| {0} | {1} | {2} |", i * 3, i * 3 + 1, i * 3 + 2);
                    Console.SetCursorPosition(20, y++);
                    Console.WriteLine("|___________|");
                }
            }
        }
        
        public static Game load_game(String board, uint depth)
        {
            return new Game(board, depth);
        }
        
        
        // return the state of the board,
        // if there is a winner, draw or if the game is not over
        // 2 -> IA / 1 -> Player / 3 -> Draw / 0 -> Not finish
        private int CheckLine()
        {
            int res = 0;
            int x = 0;
            int o = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    x = 0;
                    o = 0;
                }
                
                if (board[i] == 'x')
                    x++;
                if (board[i] == '0')
                    o++;

                if (x == 3)
                {
                    res = 2;
                    break;
                }

                if (o == 3)
                {
                    res = 1;
                    break;
                }
            }
            return res;
        }
        
        private int CheckCol()
        {
            int res = 0;
            int x = 0;
            int o = 0;
            for (int i = 0; i < 6; i += 3)
            {
                if (board[i] == 'x')
                    x++;
                if (board[i] == '0')
                    o++;
                
                if (x == 3)
                {
                    res = 2;
                    break;
                }

                if (o == 3)
                {
                    res = 1;
                    break;
                }
            }
            
            return res;
        }

        private int CheckDiagRight()
        {
            int res = 0;
            int x = 0;
            int o = 0;
            for (int i = 0; i < 9; i += 4)
            {
                if (board[i] == 'x')
                    x++;
                if (board[i] == '0')
                    o++;
                
                if (x == 3)
                {
                    res = 2;
                    break;
                }

                if (o == 3)
                {
                    res = 1;
                    break;
                }
            }
            
            return res;
        }
        
        private int CheckDiagLeft()
        {
            int res = 0;
            int x = 0;
            int o = 0;
            for (int i = 0; i < 7; i += 2)
            {
                if (board[i] == 'x')
                    x++;
                if (board[i] == '0')
                    o++;
                
                if (x == 3)
                {
                    res = 2;
                    break;
                }

                if (o == 3)
                {
                    res = 1;
                    break;
                }
            }
            
            return res;
        }

        public int Max(int a, int b)
        {
            int res = a;
            if (b > res)
                res = b;
            return res;
        }
        public int stop()
        {
            int res = 0;
            res = Max(res, CheckLine());
            res = Max(res, CheckCol());
            res = Max(res, CheckDiagLeft());
            res = Max(res, CheckDiagRight());

            if (res == 0)
            {
                bool isOver = true;
                for (int i = 0; i < 9 && isOver; i++)
                {
                    if (board[i] == '_')
                        isOver = false;
                }


                if (isOver)
                    res = 3;
            }
            return res;
            
        }

        public int FindAllBlank()
        {
            int res = 0;
            foreach (var ele in board)
            {
                if (ele == '_')
                    res++;
            }

            return res;
        }

        public int EvaluateBoard()
        {
            throw new NotImplementedException();
        }
        public int IAPlay(Game game, bool maximizingPlayer)
        {
            if (game.depth == 0 || game.FindAllBlank() < 0)
                return game.EvaluateBoard();
            throw new NotImplementedException();
        }
        // start a game turn
        public void play()
        {
            bool isPlayed = false;

            while (!isPlayed)
            {
                Console.WriteLine("Time to play: ");
                string userAction = Console.ReadLine();
                int userNumber;
                if (Int32.TryParse(userAction, out userNumber))
                {
                    if (userNumber < 0 || userNumber > 8)
                        Console.Error.WriteLine("You must play a number between 0 and 8 inclusive.");
                    else if (board[userNumber] != '_')
                        Console.Error.WriteLine("The box is already taken.");
                    else
                    {
                        board[userNumber] = 'o';
                        isPlayed = true;
                    }
                }
                else
                    Console.Error.WriteLine("You must play a number between 0 and 8 inclusive.");
            }
            //IAPlay();
        }
        
        public string state()
        {
            string res = "";
            for (int i = 0; i < board.Length; i++)
            {
                res += board[i];
            }
            return res;
        }
    }
}