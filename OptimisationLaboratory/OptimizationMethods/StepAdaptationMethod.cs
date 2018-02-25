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

        public StepAdaptationMethod(){  }

        public StepAdaptationMethod(Matrix x0)
        {
            x = x0;
            int len = x.M == 1 ? x.N : x.M;
            h = new Matrix(1, len);
            for(int i=0;i<len;i++)
            {
                h[0][i] = 4;
            }
        }

        public StepAdaptationMethod(Matrix x0,Matrix h,double Eps)
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
                double f = Fun(x);
                double g = Fun(y);

                if(g<f)
                {
                    x = y;
                    f = g;
                }

                if(g<f&&r>=0.5)
                    r = 2;

                if(g<f&&r<0.5)
                        r = 0.5;

                if (g >= f && r == 2)
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
                    Distance += Math.Pow(x[i][0] - Steps[0][i][0], 2);
                }
                Distance = Math.Sqrt(Distance);
            }
            else
            {
                for (int j = 0; j < x.M; j++)
                {
                    Distance += Math.Pow(x[0][i] - Steps[0][0][i], 2);
                }
                Distance = Math.Sqrt(Distance);
            }
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
