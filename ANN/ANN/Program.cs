using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            double[][] inputs =
            {
                new double[] {0,0},
                new double[] {1,0},
                new double[] {1,1},
                new double[] {0,1}
            };

            double[][] inputs2 =
            {
                new double[] {4,0.01, 0.01, -1, -1.5},
                new double[] {2,-1,2,2.5,2},
                new double[] {-1,3.5,0.01,-2,1.5}
            };

            double[][] outputs =
            {
                new double [] {0},
                new double [] {1},
                new double [] {0},
                new double [] {1}
            };

            double[][] outputs2 =
            {
                new double [] {1,0,0},
                new double [] {0,1,0},
                new double [] {0,0,1}
            };

            NeuralNetwork nn2 = new NeuralNetwork(new int[] {2, 2 ,1}, new SigmoidFunction(5.0));
            nn2.RandomWeight(-0.1, 0.1);

            nn2.foo2();

            Random r = new Random();

            for (int i = 0; i < 10000; i++)
            {
                int abc = r.Next(0, 4);
                nn2.Learnig(outputs[abc], inputs[abc], 0.1);
            }

            nn2.foo2();

            for (int i = 0; i < 4; i++)
            {
                double[] ans = nn2.CalcluateOutput(inputs[i]);
                Console.WriteLine(inputs[i][0] + " " + inputs[i][1] + " " + ans[0]);

                //Console.WriteLine(inputs[i][0] + " " + inputs[i][1] + " " + inputs[i][2] + " " + inputs[i][3] + " " + inputs[i][4] + " " + ans[0] + " " + ans[1] + " " + ans[2]);
            }
            

            string fileName = @"C:\TEST\test.bin";
            NetworkIO.SaveNetwork(fileName, nn2);
            NeuralNetwork nn = NetworkIO.LoadNetwork(fileName);
            nn.foo2();
            for (int i = 0; i < 4; i++)
            {
                double[] ans = nn.CalcluateOutput(inputs[i]);
                Console.WriteLine(inputs[i][0] + " " + inputs[i][1] + " " + ans[0]);

                //Console.WriteLine(inputs[i][0] + " " + inputs[i][1] + " " + inputs[i][2] + " " + inputs[i][3] + " " + inputs[i][4] + " " + ans[0] + " " + ans[1] + " " + ans[2]);
            }

            Console.ReadKey();
        }
    }
