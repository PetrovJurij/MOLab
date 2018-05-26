using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace OptimizationMethods
{
    class DIPMethod
    {
        double eps;
        Matrix a;
        Matrix b;
        Dictionary<int, Matrix> Steps = new Dictionary<int, Matrix>();
        FunctionVector functions;

        public DIPMethod() { }
        public DIPMethod(Matrix x0, FunctionVector func)
        {
            SvenMethod sv =new SvenMethod(x0,func);
            sv.Start();
            Dictionary<string, Matrix> borderPoints = sv.BorderPoints;
            a = borderPoints["a"];
            b = borderPoints["b"];
            eps = 0.001;
            functions = new FunctionVector(func);

        }
        public DIPMethod(Matrix x0, double eps, FunctionVector func)
        {
            SvenMethod sv = new SvenMethod(x0, func);
            sv.Start();
            Dictionary<string, Matrix> borderPoints = sv.BorderPoints;
            a = borderPoints["a"];
            b = borderPoints["b"];
            this.eps = eps;
            functions = new FunctionVector(func);
        }
        public DIPMethod(Matrix a,Matrix b,double eps,FunctionVector func)
        {
            this.a = a;
            this.a = b;
            this.eps = eps;
            functions = new FunctionVector(func);
        }


        public void Start()
        {
            Matrix c = (a+b)/2;
            PenaltyVector Fc = functions.ExecuteFunctions(c);
            PenaltyVector Fa = functions.ExecuteFunctions(a);
            PenaltyVector Fb = functions.ExecuteFunctions(b);
            int i = 1;

            do
            {
                Steps.Add(i, c);
                i++;
                Matrix U = (a + c) / 2;
                PenaltyVector Fu = functions.ExecuteFunctions(U);
                if(Fu<Fc)
                {
                    b = (Matrix)c.Clone();
                    c = (Matrix)U.Clone();
                    Fc = functions.ExecuteFunctions(c);
                }
                else
                {
                    Matrix V = (c + b) / 2;
                    PenaltyVector Fv = functions.ExecuteFunctions(V);
                    if(Fv<Fc)
                    {
                        a =(Matrix)c.Clone();
                        Fa = functions.ExecuteFunctions(a);
                        c = (Matrix)V.Clone();
                        Fc = functions.ExecuteFunctions(c);
                    }
                    else
                    {
                        a = (Matrix)U.Clone();
                        Fa = functions.ExecuteFunctions(a);
                        b = (Matrix)V.Clone();
                        Fb = functions.ExecuteFunctions(b);
                    }
                }
            } while ((c-a).normEvcl()>eps);
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
