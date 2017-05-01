using System;
using System.Collections.Generic;
using System.IO;

public static class NetworkIO
{
    public static NeuralNetwork LoadNetwork(string binFilePath)
    {
        using (FileStream stream = new FileStream(binFilePath, FileMode.Open))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int layersCont = reader.ReadInt32();
                List<int> layersSize = new List<int>();
                for (int i = 0; i < layersCont; i++)
                {
                    layersSize.Add(reader.ReadInt32());
                }

                List<List<List<double>>> w = new List<List<List<double>>>();

                for (int i = 1; i < layersCont; i++)
                {
                    w.Add(new List<List<double>>());
                    for (int j = 0; j < layersSize[i]; j++)
                    {
                        w[i - 1].Add(new List<double>());
                        for (int k = 0; k < layersSize[i - 1] + 1; k++)
                        {
                            w[i - 1][j].Add(reader.ReadDouble());
                        }
                    }
                }
                reader.Close();

                foreach (List<List<double>> l in w)
                {
                    foreach (List<double> l1 in l)
                    {
                        Console.WriteLine(String.Join(" ", l1.ToArray()));
                    }
                }

                NeuralNetwork neuralNetwork = new NeuralNetwork(layersSize.ToArray(), new SigmoidFunction(5.0));
                neuralNetwork.SetNetworkNeuronsInputsWeight(w);

                return neuralNetwork;
            }
        }
    }

    public static void SaveNetwork(string binFilePath, NeuralNetwork neuralNetwork)
    {
        using (FileStream stream = new FileStream(binFilePath, FileMode.Create))
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                List<int> layersSize = neuralNetwork.GetNetworkLayersSize();
                List<List<List<double>>> w = neuralNetwork.GetNetworkNeuronsInputsWeight();
                writer.Write(layersSize.Count);

                foreach (int size in layersSize)
                {
                    writer.Write(size);
                }

                foreach (List<List<double>> l in w)
                {
                    foreach (List<double> l1 in l)
                    {
                        foreach (double l2 in l1)
                        {
                            writer.Write(l2);
                        }
                    }
                }
                writer.Close();
            }
        }
    }
}