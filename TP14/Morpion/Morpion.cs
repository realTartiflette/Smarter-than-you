using System;
using System.Collections.Generic;
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
        public void positions()
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
                if (board[i] == 'o')
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
            for (int i = 0; i < 3; i += 1)
            {
                if (board[i] == 'x' && board[i + 3] == 'x' && board[i + 6] == 'x')
                {
                    res = 2;
                    break;
                }
                if (board[i] == 'o' && board[i + 3] == 'o' && board[i + 6] == 'o')
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
                if (board[i] == 'o')
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
            for (int i = 2; i < 7; i += 2)
            {
                if (board[i] == 'x')
                    x++;
                if (board[i] == 'o')
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

        public int Min(int a, int b)
        {
            int res = a;
            if (b < res)
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

        private List<int> FindAllBlank()
        {
            List<int> res = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == '_')
                    res.Add(i);
            }

            return res;
        }

        public int EvaluateBoard()
        {
            int gameState = stop();
            int res = 0;
            if (gameState == 1)
                res = -1000 + (9 - FindAllBlank().Count);
            else if (gameState == 2)
                res = 1000 - (9 - FindAllBlank().Count);
            else
            {//ligne
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == 'o' && (i + 1) % 3 != 0)
                    {
                        if (i + 1 < 9 && board[i + 1] == 'o')
                            res--;
                        else if (i + 1 < 9 && board[i + 1] == '_')
                        {
                            if (i + 2 < 9 && board[i + 2] == 'o')
                                res--;
                        }
                    }
                    else if (board[i] == 'x' && (i + 1) % 3 != 0)
                    {
                        if (i + 1 < 9 && board[i + 1] == 'x')
                            res++;
                        else if (i + 1 < 9 && board[i + 1] == '_')
                        {
                            if (i + 2 < 9 && board[i + 2] == 'x')
                                res++;
                        }
                    }
                }
                //col
                for (int i = 0; i < 6; i ++)
                {
                    if (board[i] == 'o')
                    {
                        if (i + 3 < 9 && board[i + 3] == 'o')
                            res--;
                        else if (i + 3 < 9 && board[i + 3] == '_')
                        {
                            if (i + 6 < 9 && board[i + 6] == 'o')
                                res--;
                        }
                    }
                    else if (board[i] == 'x')
                    {
                        if (i + 3 < 9 && board[i + 3] == 'x')
                            res++;
                        else if (i + 3 < 9 && board[i + 3] == '_')
                        {
                            if (i + 6 < 9 && board[i + 6] == 'x')
                                res++;
                        }
                    }
                }
                //diago
                if (board[0] == 'o' && board[4] == 'o' || board[2] == 'o' && board[4] == 'o' ||
                    board[6] == 'o' && board[4] == 'o' || board[8] == 'o' && board[4] == 'o')
                    res--;
                else if (board[0] == 'o' && board[4] == '_' && board[8] == 'o' ||
                         board[2] == 'o' && board[4] == '_' && board[6] == 'o')
                    res--;
                if (board[0] == 'x' && board[4] == 'x' || board[2] == 'x' && board[4] == 'x' ||
                    board[6] == 'x' && board[4] == 'x' || board[8] == 'x' && board[4] == 'x')
                    res++;
                else if (board[0] == 'x' && board[4] == '_' && board[8] == 'x' ||
                         board[2] == 'x' && board[4] == '_' && board[6] == 'x')
                    res++;
            }

            return res;

        }
        public (int, int) IAPlay(Game game, bool maximizingIA, uint cdepth)
        {
            int bestMove = -1;
            if (cdepth == 0 || game.FindAllBlank().Count == 0 || game.stop() != 0)
                return (bestMove, game.EvaluateBoard());
            int value;
            if (maximizingIA)
            {
                value = -1000;
                foreach (var blank in FindAllBlank())
                {
                    /*Game copyGame = load_game(game.state(), game.depth);
                    copyGame.depth -= 1;
                    copyGame.board[blank] = 'x';*/
                    //cdepth -= 1;
                    game.board[blank] = 'x';
                    (_, int play) = IAPlay(game, false, cdepth - 1);
                    game.board[blank] = '_';
                    cdepth = game.depth;
                    if (play > value)
                    {
                        value = play;
                        bestMove = blank;
                    }
                }
            }
            else
            {
                value = 1000;
                foreach (var blank in FindAllBlank())
                {
                    /*Game copyGame = load_game(game.state(), game.depth);
                    copyGame.depth -= 1;
                    copyGame.board[blank] = 'o';*/
                    //cdepth -= 1;
                    game.board[blank] = 'o';
                    (_, int play) = IAPlay(game, true, cdepth - 1);
                    cdepth = game.depth;
                    game.board[blank] = '_';
                    value = Min(value, play);
                }
            }
            return (bestMove, value);
                
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
            (int move, _) = IAPlay(this, true, this.depth);
            if (move != -1)
                board[move] = 'x';
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
        
        public void PrintBoard()
        {
            if (!Console.IsOutputRedirected)
            {
                int y = 0;
                int cpt = 0;
                Console.SetCursorPosition(20, y++);
                Console.WriteLine(" ___________");
                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(20, y++);
                    //Console.WriteLine("| " + board[cpt].ToString() + " | " + board[cpt++].ToString() + " | " + board[cpt++].ToString() +  " |");
                    Console.Write("| " + board[cpt]);
                    cpt++;
                    Console.Write(" | " + board[cpt]);
                    cpt++;
                    Console.Write(" | " + board[cpt] + " |");
                    cpt++;
                    Console.SetCursorPosition(20, y++);
                    Console.WriteLine("|___________|");
                }
            }
        }
    }
}