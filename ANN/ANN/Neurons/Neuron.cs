using ANN.ActivationFunction;
using ANN.Layers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN.Neurons
{
    public class Neuron
    {
        public IActivationFunction activationFunction;
        public double output;
        public ArrayList inputs;
        public double biasWeight;

        public double errorValue;

        public Neuron(IActivationFunction activationFunction)
        {
            this.activationFunction = activationFunction;
            this.output = 0.0;
            this.inputs = new ArrayList();
            this.biasWeight = 0.0;
            this.errorValue = 0.0;
        }

        public void AddInput(Neuron neuron, double weight = 1.0)
        {
            inputs.Add(new NeuronConnection(neuron, weight));
        }

        public void RandomWeight(double min, double max, Random random)
        {
            foreach(NeuronConnection connection in inputs)
            {
                connection.weight = (random.NextDouble() * (max - min)) + min;
            }

            biasWeight = (random.NextDouble() * (max - min)) + min;
        }

        public void CalculateOutput()
        {
            output = 0.0;
            foreach(NeuronConnection connection in inputs)
            {
                output += connection.weight * connection.neuron.output;
            }
            output += biasWeight * 1.0;
            output = activationFunction.Calculate(output);
        }

        public void CalculateErrorValue(double correctOutput)
        {
            errorValue = correctOutput - output;
        }

        public void ImprovementWeight(double learningFactor)
        {
            foreach(NeuronConnection connection in inputs)
            {
                connection.weight += learningFactor + errorValue * activationFunction.CalculateDerivative(output) * connection.neuron.output;
            }

            biasWeight += learningFactor * errorValue * activationFunction.CalculateDerivative(output) * 1.0;
        }

        public void AddInputs(Layer layer)
        {
            foreach(Neuron neuron in layer.neurons)
            {
                AddInput(neuron);
            }
        }
    }
}
