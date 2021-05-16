using System;

namespace Morpion
{
    class Program
    {
        static void Main(string[] args)
        {
            Play();
        }
        
        // start the game
        static public void Play()
        {
            bool disPasTrois = true;
            uint depth = 2;
            while (disPasTrois)
            {
                Console.WriteLine("1 -> easy | 2 -> intermediate | 3 -> IMPOSSIBLE");
                Console.Write("Please, choose your difficulty: ");
                string userAction = Console.ReadLine();
                if (Int32.TryParse(userAction, out var userDifficulty))
                {
                    if (userDifficulty < 1 || userDifficulty > 3)
                        Console.Error.WriteLine("You must play a number between 1 and 3 inclusive.");
                    if (userDifficulty == 1)
                    {
                        disPasTrois = false;
                        depth = 1;
                    }
                    if (userDifficulty == 2)
                    {
                        disPasTrois = false;
                        depth = 2;
                    }
                    if (userDifficulty == 3)
                    {
                        disPasTrois = false;
                        depth = 5;
                    }
                    
                }
                else
                    Console.Error.WriteLine("You must play a number between 1 and 3 inclusive.");
            }

            Game game = Game.load_game("_________", depth);
            
            bool isOver = false;
            while (!isOver)
            {
                if (!Console.IsErrorRedirected)
                    Console.Clear();
                game.PrintBoard();
                //Console.WriteLine(game.state());
                int res = game.stop();
                if (res != 0)
                {
                    if (res == 1)
                        Console.WriteLine("Player won the game!");
                    else if (res == 2)
                        Console.WriteLine("IA won the game!");
                    else
                        Console.WriteLine("It's a draw!");
                    isOver = true;
                }
                else
                {
                    game.play();
                }
                game.state();
            }
            Console.WriteLine("Do you want to play again? (y, n) ");
            string userGame = Console.ReadLine();
            if (userGame == "y" || userGame == "yes")
                Play();
        }
    }
}