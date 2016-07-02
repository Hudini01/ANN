using ANN.ActivationFunction;
using ANN.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN.Network
{
    class NeuralNetwork
    {
        public Layer inputLayer, outputLayer;
        private Layer[] hidenLayers;

        public NeuralNetwork(int inputLayerSize, int[] hidenLayersSize, int outputLayerSize, IActivationFunction activationFunction)
        {
            this.inputLayer = new Layer(inputLayerSize, activationFunction);
            this.outputLayer = new Layer(outputLayerSize, activationFunction);

            hidenLayers = new Layer[hidenLayersSize.Length];

            for (int i = 0; i < hidenLayersSize.Length; i++)
            {
                hidenLayers[i] = new Layer(hidenLayersSize[i], activationFunction);
            }

            this.outputLayer.ConnectWithLayer(hidenLayers[hidenLayersSize.Length - 1]);

            for (int i = hidenLayersSize.Length - 1; i > 0; i--)
            {
                hidenLayers[i].ConnectWithLayer(hidenLayers[i - 1]);
            }

            this.hidenLayers[0].ConnectWithLayer(inputLayer);
        }

        public void RandomWeight(double Min, double Max)
        {
            Random rand = new Random();

            outputLayer.RandowWeight(Min, Max, rand);

            for (int i = 0; i < hidenLayers.Length; i++)
            {
                hidenLayers[i].RandowWeight(Min, Max, rand);
            }
        }

        public double[] CalcluateOutput(double[] InputSiganl)
        {
            inputLayer.SetOutputs(InputSiganl);

            for (int i = 0; i < hidenLayers.Length; i++)
            {
                hidenLayers[i].Calculate();
            }

            outputLayer.Calculate();

            return outputLayer.GetOutputs();
        }

        public void Learnig(double[] CorrectedOutputs, double[] Inputs, double learningFactor)
        {
            inputLayer.ResetErrorValue();
            for (int i = 0; i < hidenLayers.Length; i++)
                hidenLayers[i].ResetErrorValue();
            outputLayer.ResetErrorValue();

            CalcluateOutput(Inputs);

            outputLayer.CalculateError(CorrectedOutputs);

            outputLayer.BackPropagation();

            for (int i = hidenLayers.Length - 1; i > 0; i--)
                hidenLayers[i].BackPropagation();

            outputLayer.CorrectWeight(learningFactor);

            for (int i = 0; i < hidenLayers.Length; i++)
                hidenLayers[i].CorrectWeight(learningFactor);
        }
    }
}
