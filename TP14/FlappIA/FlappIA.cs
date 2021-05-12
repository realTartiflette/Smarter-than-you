using System;
using System.Threading;

namespace tp14
{
    internal static class FlappIA
    {
        public static readonly Random Rnd = new Random();

        private static void PlayFromPath(string path)
        {
            var bird = new Bird(path);
            var drawer = new Drawer(Console.WindowWidth, Console.WindowHeight);
            var gen = new Generation(bird);
            var game = new Game(drawer, gen);
            game.Draw();
            while (game.Continue)
            {
                game.Update();
                game.Draw();
                game.Sleep();
            }
        }

        private static int birdGeneration = 128;

        /// <summary>
        ///     The main function:
        ///     - Register the managers
        ///     - Register the drawers
        ///     - Register the birds
        ///     - Initialize the game
        ///     - Play
        /// </summary>
        public static void Main()
        {
            // Hide the cursor
            Console.CursorVisible = false;
            // Create a new generation of bird
            var gen = new Generation(birdGeneration);
            // Initialize the console drawer, which will handle the console output
            var drawer = new Drawer(Console.WindowWidth, Console.WindowHeight);

            // Number of generation per game
            var currentGeneration = 0;
            var maxGeneration = 100;

            while (currentGeneration++ <= maxGeneration)
            {
                var game = new Game(drawer, gen);
                game.Draw();

                while (game.Continue)
                {
                    game.Update();
                    // Comment on these 3 lines if you want to speed up the training!
                    game.Draw();
                    Console.WriteLine("Current generation: " + currentGeneration);
                    game.Sleep();
                }
                
                Console.Clear();
                gen.PrintBirdsScore();
                gen.PrintAvg();
                Thread.Sleep(1000);
                gen.NewGen();
            }

            var bird = gen.GetBestBird();
            bird.Save("BestBird");
            gen.PrintBirdsScore();
            gen.PrintAvg();
            Console.WriteLine("GAME COMPLETE");
            Console.WriteLine("GENERATION: " + currentGeneration);
        }
    }
}