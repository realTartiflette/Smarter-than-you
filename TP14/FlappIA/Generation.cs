using System;
using System.Linq;
using System.Threading;

namespace tp14
{
    public class Generation
    {
        public Bird[] Birds { get; private set; }

        public Generation(int size)
        {
            Birds = new Bird[size];
            for (int i = 0; i < size; i++)
                Birds[i] = new Bird();
            
        }

        public Generation(Bird bird)
        {
            Birds = new[] {bird};
        }

        /// <summary>
        /// Sort function
        /// </summary>
        private void Sort()
        {
            Array.Sort(Birds, (b1, b2) => b1.Score.CompareTo(b2.Score));
        }

        /// <summary>
        /// Select a bird using fitness proportionate selection
        /// </summary>
        /// <param name="fitnessSum"> Sum of all the score of all birds </param>
        /// <returns> Bird chosen </returns>
        /// <exception cref="Exception"> If no bird are found, it should never happen </exception>
        private Bird SelectBird(double fitnessSum)
        {
            Random rnd = new Random();
            Bird myBird = new Bird();
            bool hasBeenChosen = false;
            foreach (var bird in Birds)
            {
                if (rnd.Next(0, 100) < bird.Score / fitnessSum * 100)
                {
                    hasBeenChosen = true;
                    myBird = bird;
                    break;
                }
            }

            if (!hasBeenChosen)
            {
                myBird = GetBestBird();
            }

            return myBird;
        }

        /// <summary>
        /// Create new generation of bird
        /// </summary>
        public void NewGen()
        {
            Generation generation = new Generation(64);
            long fitnessSum = 0;
            foreach (var bird in Birds)
                fitnessSum += bird.Score;
            
            for (int i = 0; i < 32; i++)
            {
                Bird goodBird1 = SelectBird(fitnessSum);
                Bird goodBird2 = SelectBird(fitnessSum);
                generation.Birds[i] = goodBird1.Crossover(goodBird2);
                generation.Birds[i].Mutate();
            }
            Sort();
            for (int i = 0; i < 16; i++)
            {
                
                generation.Birds[32 + i] = Birds[Birds.Length - i - 1];
                generation.Birds[32 + i].Mutate();
                
            }
            
            Generation trashGeneration = new Generation(32);
            fitnessSum = 0;
            foreach (var bird in trashGeneration.Birds)
                fitnessSum += bird.Score;
            for (int i = 0; i < 32; i++)
                trashGeneration.Birds[i] = Birds[Birds.Length - i - 1];
            for (int i = 0; i < 16; i++)
            {
                Bird goodBird1 = trashGeneration.SelectBird(fitnessSum);
                Bird goodBird2 = trashGeneration.SelectBird(fitnessSum);
                generation.Birds[48 + i] = goodBird1.Crossover(goodBird2);
                generation.Birds[48 + i].Mutate();
            }
            

        }

        public void PrintBirdsScore()
        {
            Sort();
            foreach (var bird in Birds)
                Console.Write(bird.Score + " "); // Do not mind the trailing space...
            Console.WriteLine();
            Console.WriteLine("BEST SCORE: " + Birds[^1].Score);
        }

        /// <summary>
        /// Compute average score of all birds
        /// </summary>
        /// <returns></returns>
        public void PrintAvg()
        {
            Console.WriteLine("Average: " + Birds.Sum(bird => bird.Score) / Birds.Length);
        }

        /// <summary>
        /// Select the best bird in the current generation
        /// </summary>
        /// <returns></returns>
        public Bird GetBestBird()
        {
            Sort();
            return Birds[^1];
        }
    }
}