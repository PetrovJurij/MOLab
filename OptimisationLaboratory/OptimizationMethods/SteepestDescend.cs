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
        FunctionVector functions;
        
        public SteepestDescend(){ }
        public SteepestDescend(Matrix x0,FunctionVector func)
        {
            x = x0;
            Eps = 0.001;
            functions = new FunctionVector(func);
        }
        public SteepestDescend(Matrix x0, double Eps,FunctionVector func)
        {
            x = x0;
            this.Eps = Eps;
            functions = new FunctionVector(func);
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
                    SAM = new StepAdaptationMethod(x, g, 0.01,functions);
                }catch(ArgumentException)
                {
                    g.Transform();
                    SAM = new StepAdaptationMethod(x,g,0.01,functions);
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
                    Vector vec1 = new PenaltyVector(functions.ExecuteFunctions(temp1));
                    Vector vec2 = new PenaltyVector(functions.ExecuteFunctions(temp2));
                    res[i][j] = (vec1 - vec2) / 0.000002;
                    temp1[i][j] -= 0.000001;
                    temp2[i][j] += 0.000001;
                }
            }

            return res;
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

                Vector res = functions.ExecuteFunctions(step.Value);
                for (int i = 0; i < res.Length; i++)
                {
                    sb.Append("\t");
                    sb.Append(res[i]);
                    sb.Append("\t");
                }
            }

            return sb.ToString();
        }
    }
}
