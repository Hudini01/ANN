using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Neuron
    {
        public double ErrorValue;
        public double Output;

        private List<NeuronConnection> _inputs;
        private double _biasWeight;
        private IActivationFunction _activationFunction;

        public Neuron(IActivationFunction activationFunction)
        {
            this._activationFunction = activationFunction;
            this.Output = 0.0;
            this._inputs = new List<NeuronConnection>();
            this._biasWeight = 0.0;
            this.ErrorValue = 0.0;
        }

        public void AddInput(Neuron neuron, double weight = 1.0)
        {
            _inputs.Add(new NeuronConnection(neuron, weight));
        }

        public void AddInputs(List<Neuron> neurons)
        {
            foreach (Neuron neuron in neurons)
            {
                AddInput(neuron);
            }
        }

        public void RandomWeight(double min, double max)
        {
            Random rand = new Random();

            foreach(NeuronConnection connection in _inputs)
            {
                connection.Weight = (rand.NextDouble() * (max - min)) + min;
            }

            _biasWeight = (rand.NextDouble() * (max - min)) + min;
        }

        public void CalculateOutput()
        {
            Output = 0.0;
            foreach(NeuronConnection connection in _inputs)
            {
                Output += connection.Weight * connection.Neuron.Output;
            }
            Output += _biasWeight * 1.0;
            Output = _activationFunction.Calculate(Output);
        }

        public void CalculateErrorValue(double correctOutput)
        {
            ErrorValue = correctOutput - Output;
        }

        public void ImprovementWeight(double learningFactor)
        {
            foreach(NeuronConnection connection in _inputs)
            {
                connection.Weight += learningFactor * ErrorValue * _activationFunction.CalculateDerivative(Output) * connection.Neuron.Output;
            }

            _biasWeight += learningFactor * ErrorValue * _activationFunction.CalculateDerivative(Output) * 1.0;
        }

        public void BackPropagation()
        {
            foreach (NeuronConnection con in _inputs)
            {
                con.Neuron.ErrorValue += ErrorValue * con.Weight;
            }
        }

        public List<double> GetIntuptsWeight()
        {
            List<double> inputsWeight = new List<double>();
            foreach (NeuronConnection connection in _inputs)
            {
                inputsWeight.Add(connection.Weight);
            }
            inputsWeight.Add(_biasWeight);

            return inputsWeight;
        }

        public void SetInputsWeight(List<double> inputsWeight)
        {
            for(int i = 0; i < _inputs.Count && i < inputsWeight.Count; i++)
            {
                _inputs[i].Weight = inputsWeight[i];
            }

            _biasWeight = inputsWeight[inputsWeight.Count - 1];
        }
    }
