using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Layer
{
    public List<Neuron> neurons;

    public Layer(int neuronNumbers, IActivationFunction activationFunction)
    {
        this.neurons = new List<Neuron>();

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
            neuron.AddInputs(layer.neurons);
        }
    }

    public void RandowWeight(double min, double max)
    {
        foreach(Neuron neuron in neurons)
        {
            neuron.RandomWeight(min, max);
        }
    }

    public void SetOutputs(double []outputs)
    {
        for(int i = 0; i < neurons.Count; i++)
        {
            (neurons[i]).Output = outputs[i];
        }
    }

    public double[] GetOutputs()
    {
        double[] outputs = new double[neurons.Count];
        for (int i = 0; i < neurons.Count;i++)
        {
            outputs[i] = (neurons[i]).Output;
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
            neuron.ErrorValue = 0.0;
        }
    }

    public void CalculateError(double[] correctOutputs)
    {
        for (int i = 0; i < neurons.Count; i++)
        {
            ((Neuron)neurons[i]).CalculateErrorValue(correctOutputs[i]);
        }
    }

    public void CorrectWeight(double learningFactor)
    {
        foreach (Neuron neuron in neurons)
        {
            neuron.ImprovementWeight(learningFactor);
        }
    }

    public void BackPropagation()
    {
        foreach (Neuron neuron in neurons)
        {
            neuron.BackPropagation();
        }
    }

    public int GetLayersSize()
    {
        return neurons.Count;
    }

    public List<List<double>> GetNeuronsIntuptsWeight()
    {
        List<List<double>> neuronsInputsWeight = new List<List<double>>();
        foreach (Neuron neuron in neurons)
        {
            neuronsInputsWeight.Add(neuron.GetIntuptsWeight());
        }
        return neuronsInputsWeight;
    }

    public void SetNeuronsIntuptsWeight(List<List<double>> neuronsInputsWeight)
    {
        for(int i = 0; i < neurons.Count && i < neuronsInputsWeight.Count; i++)
        {
            neurons[i].SetInputsWeight(neuronsInputsWeight[i]);
        }
    }
}
