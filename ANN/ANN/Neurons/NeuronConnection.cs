using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN.Neurons
{
    class NeuronConnection
    {
        public Neuron neuron;
        public double weight;

        public NeuronConnection(Neuron neuron, double weight)
        {
            this.neuron = neuron;
            this.weight = weight;
        }
    }
}
