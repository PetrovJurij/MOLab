using System;
using System.Collections.Generic;
using Core;
using System.Text;

namespace OptimizationMethods
{
    public class StepAdaptationMethod:IOptimisationMethod
    {
        double Eps;
        Matrix h;
        Matrix x;
        Dictionary<int, Matrix> Steps = new Dictionary<int, Matrix>();
        public Matrix LastStep;
        public double Distance;
        private FunctionVector functions;

        public StepAdaptationMethod(){  }

        public StepAdaptationMethod(Matrix x0,FunctionVector func)
        {
            x = x0;
            int len = x.M == 1 ? x.N : x.M;
            h = new Matrix(1, len);
            for(int i=0;i<len;i++)
            {
                h[0][i] = 4;
            }

            functions = new FunctionVector(func);
        }

        public StepAdaptationMethod(Matrix x0,Matrix h,double Eps, FunctionVector func)
        {
            x = x0;

            this.Eps = Eps;

            bool IsColumn = h.M == 1;
            int ZerosCount = 0;
            if(x.M!=h.M||x.N!=h.M)
            {
                throw new ArgumentException("Размер вектора аргументов и " +
                    "вектора шага должны быть одинаковы");
            }

            if (IsColumn)
            {
                for (int i = 0; i < h.M; i++)
                {
                    if (h[1][i] != 0)
                        break;
                    else
                        ZerosCount++;
                }
                if (ZerosCount == h.M)
                    throw new ArgumentException("Шаг должен быть не нулевым.");
            }
            else
            {
                for (int i = 0; i < h.N; i++)
                {
                    if (h[i][1] != 0)
                        break;
                    else
                        ZerosCount++;
                }
                if (ZerosCount == h.N)
                    throw new ArgumentException("Шаг должен быть не нулевым.");
            }
            this.h = (Matrix)h.Clone();

            functions = new FunctionVector(func);
        }

        public void Start()
        {
            double r = 0;
            Matrix h0;
            int i = 0;

            while(h.normEvcl()>Eps)
            {
                Steps.Add(i, x);
                h0 = h;
                Matrix y = x + h0;

                Vector f = new PenaltyVector(functions.ExecuteFunctions(x));
                Vector g = new PenaltyVector(functions.ExecuteFunctions(y));
                

                if(g<f)
                {
                    x = y;
                    f = g;
                }

                if(g<f&&r>=0.5)
                    r = 2;

                if(g<f&&r<0.5)
                        r = 0.5;

                if (g < f && r == 2)
                    r = 0.25;
                else
                    r = -0.5;

                h = h * r;
                x = x + h;
                i++;
            }
            LastStep = Steps[i];
            bool IsColumn = x.M == 1;
            if (IsColumn)
            {
                for (int j = 0; j < x.N; j++)
                {
                    Distance +=(x[i][0] - Steps[0][i][0])*(x[i][0] - Steps[0][i][0]);
                }
                Distance = Math.Sqrt(Distance);
            }
            else
            {
                for (int j = 0; j < x.M; j++)
                {
                    Distance += (x[0][i] - Steps[0][0][i])*(x[0][i] - Steps[0][0][i]);
                }
                Distance = Math.Sqrt(Distance);
            }


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
                for(int i=0;i<res.Length;i++)
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
