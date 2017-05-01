using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NeuralNetwork
{
    private List<Layer> _layers;

    public NeuralNetwork(int[] layersSize, IActivationFunction activationFunction)
    {
        _layers = new List<Layer>();

        foreach(int layerSize in layersSize)
        {
            _layers.Add(new Layer(layerSize, activationFunction));
        }
        for (int i = layersSize.Length - 1; i > 0; i--)
        {
            _layers[i].ConnectWithLayer(_layers[i - 1]);
        }
    }

    public void RandomWeight(double min, double max)
    {
        for (int i = 1; i < _layers.Count; i++)
        {
            _layers[i].RandowWeight(min, max);
        }
    }

    public double[] CalcluateOutput(double[] inputSiganl)
    {
        _layers[0].SetOutputs(inputSiganl);

        for (int i = 1; i < _layers.Count; i++)
        {
            _layers[i].Calculate();
        }

        return _layers[_layers.Count - 1].GetOutputs();
    }

    public void Learnig(double[] CorrectedOutputs, double[] Inputs, double learningFactor)
    {
        foreach(Layer layer in _layers)
        {
            layer.ResetErrorValue();
        }

        CalcluateOutput(Inputs);

        _layers[_layers.Count - 1].CalculateError(CorrectedOutputs);
        for (int i = _layers.Count - 1; i > 0; i--)
        {
            _layers[i].BackPropagation();
        }

        for (int i = _layers.Count - 1; i > 0; i--)
        {
            _layers[i].CorrectWeight(learningFactor);
        }
    }

    public void foo2()
    {
        for (int i = 1; i < _layers.Count; i++)
        {
            foreach (List<double> l in _layers[i].GetNeuronsIntuptsWeight())
            {
                Console.WriteLine(String.Join(" ", l.ToArray()));
            }
        }
    }

    public void SetNetworkNeuronsInputsWeight(List<List<List<double>>> networkNeuronsWeight)
    {
        for (int i = 1; i < _layers.Count && i <= networkNeuronsWeight.Count; i++)
        {
            _layers[i].SetNeuronsIntuptsWeight(networkNeuronsWeight[i - 1]);
        }
    }

    public List<List<List<double>>> GetNetworkNeuronsInputsWeight()
    {
        List<List<List<double>>> networkNeuronsWeight = new List<List<List<double>>>();

        for (int i = 1; i < _layers.Count; i++)
        {
            networkNeuronsWeight.Add(_layers[i].GetNeuronsIntuptsWeight());
        }
        return networkNeuronsWeight;
    }

    public List<int> GetNetworkLayersSize()
    {
        List<int> layersSize = new List<int>();
        foreach (Layer layer in _layers)
        {
            layersSize.Add(layer.GetLayersSize());
        }
        return layersSize;
    }
}
