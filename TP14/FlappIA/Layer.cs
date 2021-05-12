using System;

namespace tp14
{
    public class Layer
    {
        public Neurone[] Neurones { get; }

        /// <summary>
        /// Create new layer of neurones
        /// </summary>
        /// <param name="size"> Dimension of the current layer </param>
        /// <param name="prevSize"> Dimension of the previous layer </param>
        public Layer(int size, int prevSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutate a layer
        /// </summary>
        /// <param name="layer"> Layer to mutate </param>
        /// <param name="mutate"> Apply mutation </param>
        public Layer(Layer layer, bool mutate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutate the current layer
        /// </summary>
        public void Mutate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mix the current layer with a partner layer
        /// </summary>
        /// <param name="partner"></param>
        public void Crossover(Layer partner)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Apply the propagation function to the layer
        /// </summary>
        /// <param name="prevLayer"> Dimension of the previous layer</param>
        public void FrontProp(Layer prevLayer)
        {
            throw new NotImplementedException();
        }
    }
}