using System;

namespace tp14
{
    public class Neurone
    {

        /// <summary>
        /// Value of the neurone after the propagation
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Weights of the neurone
        /// </summary>
        public double[] Weights { get; }

        /// <summary>
        /// Bias of the neurones
        /// </summary>
        public double Bias { get; set; }
        
        /// <summary>
        /// Create new neurone
        /// </summary>
        /// <param name="prevSize"> size of the last layer </param>
        public Neurone(int prevSize)
        {
            Random random = new Random();
            Weights = new double[prevSize];
            for (int i = 0; i < Weights.Length; i++)
                Weights[i] = random.Next(-1, 2);
            
            Bias = random.Next(-1, 2);
            Value = 1;
        }

        /// <summary>
        /// Mutate a neurone
        /// </summary>
        /// <param name="neurone"> old neurone </param>
        /// <param name="mutate"> apply mutation </param>
        public Neurone(Neurone neurone, bool mutate)
        {
            Value = 0;
            Weights = neurone.Weights;
            Bias = neurone.Bias;
            
            if (mutate)
                Mutate();
            
        }

        /// <summary>
        /// Random function using Box-Muller transform
        /// </summary>
        /// <returns></returns>
        private static double RandomGaussian()
        {
            var u1 = -2.0 * Math.Log(FlappIA.Rnd.NextDouble());
            var u2 = 2.0 * Math.PI * FlappIA.Rnd.NextDouble();
            return Math.Sqrt(u1) * Math.Cos(u2);
        }
        
        /// <summary>
        /// Apply the mutation depending on a chosen algorithm
        /// </summary>
        /// <param name="probability"> probability of applying the mutation </param>
        /// <returns> true to mutate, false otherwise </returns>
        private bool ShouldMutate(double probability)
        {
            //a modifier par la suite
            bool mutate = false;
            Random rnd = new Random();
            if (rnd.Next(0, 100) < probability * 100)
                mutate = true;
            return mutate;
        }

        /// <summary>
        /// Mutate the neurone
        /// </summary>
        public void Mutate()
        {
            if (ShouldMutate(0.005))
                Bias = RandomGaussian();
            for (int i = 0; i < Weights.Length; i++)
            {
                if (ShouldMutate(0.005))
                    Weights[i] = RandomGaussian();
            }
        }

        /// <summary>
        /// Mix the neurone with its partner
        /// </summary>
        /// <param name="partner"> the partner to be mixed with </param>
        public void Crossover(Neurone partner)
        {
            Random rnd = new Random();
            for (int i = 0; i < Weights.Length; i++)
            {
                if (rnd.Next(0, 2) == 0)
                    Weights[i] = partner.Weights[i];
            }
            //bias not modified
        }

        /// <summary>
        /// Apply the front propagation to the current neurone and update its value
        /// </summary>
        /// <param name="prevLayer"></param>
        public void FrontProp(Layer prevLayer)
        {
            double value = 0;
            for (int i = 0; i < prevLayer.Neurones.Length; i++)
            {
                value += prevLayer.Neurones[i].Value * Weights[i];
            }

            Value = Activation(value + Bias);
        }

        /// <summary>
        /// Activation function
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double Activation(double x)
        {
            return Math.Log(1 + Math.Exp(x));
        }
    }
}