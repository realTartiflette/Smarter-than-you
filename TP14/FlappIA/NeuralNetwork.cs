using System;
using System.IO;

namespace tp14
{
    public class NeuralNetwork
    {
        private Layer[] Layers { get; }
       
        /// <summary>
        /// Create a neural network
        /// </summary>
        /// <param name="layerDimension"> Dimension of the neural network </param>
        public NeuralNetwork(int[] layerDimension)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new neural network with mutation
        /// </summary>
        /// <param name="neuralNetwork"> old neural network </param>
        /// <param name="mutate"> apply mutation </param>
        public NeuralNetwork(NeuralNetwork neuralNetwork, bool mutate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Feed the neural network with the current bird's state
        /// </summary>
        /// <param name="input"></param>
        public void Feed(double[] input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Apply the front propagation to the neural network
        /// </summary>
        public void FrontProp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mix the neural network with a partner
        /// </summary>
        /// <param name="partner"> the partner to be mixed with </param>
        public void Crossover(NeuralNetwork partner)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutate the current neural network
        /// </summary>
        public void Mutate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tell the bird if it should jump
        /// </summary>
        /// <returns></returns>
        public bool ShouldJump()
        {
            return Layers[^1].Neurones[0].Value > 0.5;
        }

        /// <summary>
        /// Save the neural network in a file
        /// </summary>
        /// <param name="path"> path of the file </param>
        public void Save(string path)
        {
            var format = "";
            foreach (var layer in Layers)
            {
                format = layer.Neurones.Length + " " + format;
                format += '\n';
                foreach (var neurone in layer.Neurones)
                {
                    format = format + '\n' + neurone.Bias + " ";
                    foreach (var weight in neurone.Weights) 
                        format = format + weight + " ";
                }
            }

            format += "\n\n\n";
            File.WriteAllText(path, format);
        }

        /// <summary>
        /// Get information of the neural network from a file
        /// </summary>
        /// <param name="format"> string of the while file </param>
        /// <param name="i"> current position in the file </param>
        /// <returns></returns>
        private static int[] GetSizes(string format, ref int i)
        {
            var nbLayers = 0;
            for (var j = 0; format[j] != 0 && format[j] != '\n'; j++)
                if (format[j] == ' ')
                    nbLayers++;

            var sizes = new int[nbLayers];
            nbLayers--;

            while (format[i] != 0 && format[i] != '\n') //Sizes
            {
                var end = i;
                while (format[end] != ' ') //Layer's Size
                    end++;

                sizes[nbLayers] = int.Parse(format.Substring(i, end - i));

                nbLayers--;
                i = end + 1;
            }

            i++;
            return sizes;
        }

        /// <summary>
        /// create a neural network from a file
        /// </summary>
        /// <param name="path"> path of the file </param>
        /// <returns> fully working brain </returns>
        public static NeuralNetwork Restore(string path)
        {
            var format = File.ReadAllText(path);
            var i = 0;
            var sizes = GetSizes(format, ref i);
            var network = new NeuralNetwork(sizes);
            i++;

            var layer = 0;
            while (format[i] != 0 && format[i] != '\n') //Layers
            {
                var neurone = 0;
                while (format[i] != 0 && format[i] != '\n') //Neurones
                {
                    var j = i;
                    while (format[j] != ' ') //Biais
                        j++;
                    
                    network.Layers[layer].Neurones[neurone].Bias = double.Parse(format.Substring(i, j - i));
                    i = j + 1;

                    var weight = 0;
                    while (format[i] != 0 && format[i] != '\n') //Weights
                    {
                        j = i;
                        while (format[j] != ' ')
                            j++;
                        network.Layers[layer].Neurones[neurone].Weights[weight] =
                            double.Parse(format.Substring(i, j - i));
                        i = j + 1;
                        weight++;
                    }

                    neurone++;
                    i++;
                }

                layer++;
                i++;
            }

            return network;
        }
    }
}