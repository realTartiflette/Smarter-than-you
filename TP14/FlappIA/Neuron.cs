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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutate a neurone
        /// </summary>
        /// <param name="neurone"> old neurone </param>
        /// <param name="mutate"> apply mutation </param>
        public Neurone(Neurone neurone, bool mutate)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutate the neurone
        /// </summary>
        public void Mutate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mix the neurone with its partner
        /// </summary>
        /// <param name="partner"> the partner to be mixed with </param>
        public void Crossover(Neurone partner)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Apply the front propagation to the current neurone and update its value
        /// </summary>
        /// <param name="prevLayer"></param>
        public void FrontProp(Layer prevLayer)
        {
            throw new NotImplementedException();
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