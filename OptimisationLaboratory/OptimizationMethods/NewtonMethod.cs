using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace OptimisationMethods
{
    public class NewtonMethod : IOptimisationMethod
    {
        double Eps;
        Matrix x;
        Dictionary<int, Matrix> Steps = new Dictionary<int, Matrix>();

        public NewtonMethod() { }

        public NewtonMethod(Matrix x0, double Eps)
        {
            x = x0;
            this.Eps = Eps;
        }

        public NewtonMethod(Matrix x0)
        {
            x = x0;
            Eps = 0.001;
        }

        public Matrix Gradient()
        {
            Matrix res =new Matrix(x.N,x.M);

            for (int i = 0; i < x.N; i++)
            {
                Matrix temp1 = new Matrix(x);
                Matrix temp2 = new Matrix(x);
                for (int j=0;j<x.M;j++)
                {
                    temp1[i][j] += 0.000001;
                    temp2[i][j] -= 0.000001;
                    res[i][j] = (Fun(temp1)-Fun(temp2))/0.000002;
                    temp1[i][j] -= 0.000001;
                    temp2[i][j] += 0.000001;
                }
            }

            return res;
        }

        public Matrix Hesse()
        {
            int n = x.M == 1 ? x.N : x.M;
            Matrix res = new Matrix(n,n);
            bool row = x.M == 1;


            for(int i=0;i<n;i++)
            {
                Matrix temp1 = new Matrix(x);
                Matrix temp2 = new Matrix(x);
                Matrix temp3 = new Matrix(x);

                for(int j=0;j<n;j++)
                {
                    if(row)
                    {
                        temp1[i][0] += 0.00001;
                        if (i == j)
                            temp2[j][0] -= 0.00001;
                        else
                            temp2[j][0] += 0.00001;
                        temp3[i][0] += 0.00001;
                        temp3[j][0] += 0.00001;
                    }
                    else
                    {
                        temp1[0][i] += 0.00001;
                        if (i == j)
                            temp2[0][j] -= 0.00001;
                        else
                            temp2[0][j] += 0.00001;
                        temp3[0][i] += 0.00001;
                        temp3[0][j] += 0.00001;
                    }
                    if (i == j)
                        res[i][j] = (Fun(temp1) - 2*Fun(x) + Fun(temp2)) / Math.Pow(0.00001, 2);
                    else
                        res[i][j] = (Fun(temp3) - Fun(temp2) - Fun(temp1) + Fun(x)) / Math.Pow(0.00001, 2);

                    if (row)
                    {
                        temp1[i][0] -= 0.00001;
                        if (i == j)
                            temp2[j][0] += 0.00001;
                        else
                            temp2[j][0] -= 0.00001;
                        temp3[i][0] -= 0.00001;
                        temp3[j][0] -= 0.00001;
                    }
                    else
                    {
                        temp1[0][i] -= 0.00001;
                        if (i == j)
                            temp2[0][j] += 0.00001;
                        else
                            temp2[0][j] -= 0.00001;
                        temp3[0][i] -= 0.00001;
                        temp3[0][j] -= 0.00001;
                    }
                }
            }

            return res;
        }

        public void Start()
        {
            Matrix grad = Gradient();
            Matrix hesse = Hesse();

            grad.Transform();

            Matrix s = -(hesse.Reverse()*grad);
            int i = 0;
            Steps.Add(i, x);

            while (s.normEvcl()>Eps)
            {
                s.Transform();
                x += s;
                i++;

                grad = Gradient();
                hesse = Hesse();
                grad.Transform();

                s = -(hesse.Reverse() * grad);

                Steps.Add(i, x);
            }
        }

        double Fun(Matrix x)
        {
            return Math.Pow(x[0][0],2)/2+ Math.Pow(x[0][1], 2) + 
                x[0][1]*x[0][0]-9*x[0][0]-18*x[0][1]+72;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Newton method\n");
            sb.Append("Step\tx\t\tfx\n");
            foreach(var step in Steps)
            {
                sb.Append(step.Key+"\t");
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


        public void PlotSurfaces()
        {
            
        }

    }
}
