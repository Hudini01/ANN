using ANN.ActivationFunction;
using ANN.Neurons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN.Layers
{
    class Layer
    {
        public ArrayList neurons;

        public Layer(int neuronNumbers, IActivationFunction activationFunction)
        {
            this.neurons = new ArrayList();

            for(int i = 0; i < neuronNumbers; i++)
            {
                addNeuron(new Neuron(activationFunction));
            }
        }

        private void addNeuron(Neuron neuron)
        {
            neurons.Add(neuron);
        }

        public void ConnectWithLayer(Layer layer)
        {
            foreach(Neuron neuron in neurons)
            {
                neuron.AddInputs(layer);
            }
        }

        public void RandowWeight(double min, double max, Random random)
        {
            foreach(Neuron neuron in neurons)
            {
                neuron.RandomWeight(min, max, random);
            }
        }

        public void SetOutputs(double []outputs)
        {
            for(int i = 0; i < neurons.Count; i++)
            {
                ((Neuron)neurons[i]).output = outputs[i];
            }
        }

        public double[] GetOutputs()
        {
            double[] outputs = new double[neurons.Count];
            for (int i = 0; i < neurons.Count;i++)
            {
                outputs[i] = ((Neuron)neurons[i]).output;
            }

            return outputs;
        }

        public void Calculate()
        {
            foreach(Neuron neuron in neurons)
            {
                neuron.CalculateOutput();
            }
        }

        public void ResetErrorValue()
        {
            foreach (Neuron neuron in neurons)
            {
                neuron.errorValue = 0.0;
            }
        }

        public void SetOutputs(double[] correctOutputs)
        {
            for (int i = 0; i < neurons.Count; i++)
            {
                ((Neuron)neurons[i]).CalculateErrorValue(correctOutputs[i])
;
            }
        }

        public void CorrectWeight(double learningFactor)
        {
            foreach (Neuron neuron in neurons)
            {
                neuron.ImprovementWeight(learningFactor);
            }
        }

    }
}
