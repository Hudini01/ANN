using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class SigmoidFunction : IActivationFunction
    {
        private double Beta;

        public SigmoidFunction()
        {
            Beta = 1.0;
        }

        public SigmoidFunction(double Beta)
        {
            this.Beta = Beta;
        }

        public double Calculate(double input)
        {
            return (1.0 / (1.0 + Math.Pow(Math.E, -(Beta * input))));
        }

        public double CalculateDerivative(double output)
        {
            return Beta * output * (1.0 - output);
        }
    }
