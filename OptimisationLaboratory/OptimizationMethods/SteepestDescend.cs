using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizationMethods
{
    public class SteepestDescend:IOptimisationMethod
    {
        double Eps;
        Matrix x;
        Dictionary<int, Matrix> Steps=new Dictionary<int, Matrix>();
        
        public SteepestDescend(){ }

        public SteepestDescend(Matrix x0)
        {
            x = x0;
            Eps = 0.001;
        }

        public SteepestDescend(Matrix x0, double Eps)
        {
            x = x0;
            this.Eps = Eps;
        }

        public void Start()
        {
            Matrix g=new Matrix(x.N,x.M);
            Matrix s=new Matrix(x.N,x.M);
            do
            {
                StepAdaptationMethod SAM;
                g = Gradient();
                try
                {
                    SAM = new StepAdaptationMethod(x, g, 0.01);
                }catch(ArgumentException)
                {
                    g.Transform();
                    SAM = new StepAdaptationMethod(x,g,0.01);
                }
                SAM.Start();
                Matrix r = (x-SAM.LastStep)*g.Reverse();
                try
                {
                    s = -r * g;
                }catch(ArgumentException)
                {
                    g.Transform();
                    s = -r * g;
                }
                x = x + s;

            } while(s.normEvcl()>Eps);




        }

        public Matrix Gradient()
        {
            Matrix res = new Matrix(x.N, x.M);

            for (int i = 0; i < x.N; i++)
            {
                Matrix temp1 = new Matrix(x);
                Matrix temp2 = new Matrix(x);
                for (int j = 0; j < x.M; j++)
                {
                    temp1[i][j] += 0.000001;
                    temp2[i][j] -= 0.000001;
                    res[i][j] = (Fun(temp1) - Fun(temp2)) / 0.000002;
                    temp1[i][j] -= 0.000001;
                    temp2[i][j] += 0.000001;
                }
            }

            return res;
        }

        double Fun(Matrix x)
        {
            return Math.Pow(x[0][0], 2) / 2 + Math.Pow(x[0][1], 2) + 
                x[0][1] * x[0][0] - 9 * x[0][0] - 18 * x[0][1] + 72;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Newton method\n");
            sb.Append("Step\tx\t\tfx\n");
            foreach (var step in Steps)
            {
                sb.Append(step.Key + "\t");
                for (int i = 0; i < step.Value.M; i++)
                {
                    sb.Append(step.Value[0][i]);
                    sb.Append(" ");
                }

                sb.Append("\t");
                sb.Append(Fun(step.Value));
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
