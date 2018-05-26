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
        FunctionVector functions;

        public NewtonMethod() { }
        public NewtonMethod(Matrix x0,FunctionVector func)
        {
            x = x0;
            Eps = 0.001;
            functions = new FunctionVector(func);
        }
        public NewtonMethod(Matrix x0, double Eps, FunctionVector func)
        {
            x = x0;
            this.Eps = Eps;
            functions = new FunctionVector(func);
        }

        public Matrix Gradient()
        {
            Matrix res =new Matrix(x.N,x.M);

            for (int i = 0; i < x.N; i++)
            {
                Matrix temp1 = new Matrix(x);
                Matrix temp2 = new Matrix(x);
                PenaltyVector funT1;
                PenaltyVector funT2;

                for (int j=0;j<x.M;j++)
                {
                    temp1[i][j] += 0.000001;
                    temp2[i][j] -= 0.000001;
                    funT1 = functions.ExecuteFunctions(temp1);
                    funT2 = functions.ExecuteFunctions(temp2);
                    res[i][j] = (funT1-funT2)/0.000002;
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
                PenaltyVector funT1;
                PenaltyVector funT2;
                PenaltyVector funT3;
                PenaltyVector funX;

                for (int j=0;j<n;j++)
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
                    {
                        funT1 = functions.ExecuteFunctions(temp1);
                        funT2 = functions.ExecuteFunctions(temp2);
                        funX = functions.ExecuteFunctions(x);
                        res[i][j] =( (funT1 - funX) - (funX-funT2)) / Math.Pow(0.00001, 2);
                    }
                    else
                    {
                        funT1 = functions.ExecuteFunctions(temp1);
                        funT2 = functions.ExecuteFunctions(temp2);
                        funT3 = functions.ExecuteFunctions(temp3);
                        funX = functions.ExecuteFunctions(x);
                        res[i][j] = ((funT3 - funT2) - (funT1 - funX)) / Math.Pow(0.00001, 2);
                    }

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
