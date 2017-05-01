public class NeuronConnection
{
    public Neuron Neuron;
    public double Weight;

    public NeuronConnection(Neuron neuron, double weight)
    {
        this.Neuron = neuron;
        this.Weight = weight;
    }
}
