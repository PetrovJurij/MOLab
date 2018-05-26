using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace OptimizationMethods
{
    class SvenMethod : IOptimisationMethod
    {
        Matrix h;
        Matrix c;
        Dictionary<int, Matrix> Steps = new Dictionary<int, Matrix>();
        FunctionVector functions;
        bool forward=false;

        public Dictionary<string,Matrix> BorderPoints
        {
            get
            {
                Dictionary<string, Matrix> res = new Dictionary<string, Matrix>();
                if (!forward)
                {
                    res.Add("a", Steps[Steps.Count]);
                    res.Add("c", Steps[Steps.Count-1]);
                    res.Add("b", Steps[Steps.Count - 2]);
                }
                else
                {
                    res.Add("b", Steps[Steps.Count]);
                    res.Add("c", Steps[Steps.Count - 1]);
                    res.Add("a", Steps[Steps.Count - 2]);
                }
                return res;
            }
        }



        public SvenMethod() { }
        public SvenMethod(Matrix x0, FunctionVector func)
        {
            c = x0;
            h = new Matrix(c.N,c.M);
            h[0][0] = 3;
            functions = new FunctionVector(func);
        }
        public SvenMethod(Matrix x0, Matrix h, FunctionVector func)
        {
            c = x0;
            this.h = h;
            functions = new FunctionVector(func);
        }

        public void Start()
        {
            int i = 0;
            Matrix a = new Matrix(c);
            Matrix b = new Matrix(c+h);
            Vector Fa = functions.ExecuteFunctions(a);
            Vector Fb = functions.ExecuteFunctions(b);
            Vector Fc = functions.ExecuteFunctions(c);
            Steps.Add(i, c);
            i++;

            if (Fb < Fc)
            {
                forward = true;
                Steps.Add(i, b);
                i++;
                while (Fb < Fc)
                {
                    a = (Matrix)c.Clone();
                    Fa = functions.ExecuteFunctions(a);
                    c = (Matrix)b.Clone();
                    Fc = functions.ExecuteFunctions(c);
                    h = h + h;
                    b = b + h;
                    Fb = functions.ExecuteFunctions(b);
                    Steps.Add(i, b);
                    i++;
                }
            }
            else
            {
                h = -h;
                a = c + h;
                Fa = functions.ExecuteFunctions(a);
                Steps.Add(i, a);
                i++;

                while (Fa < Fc)
                {
                    
                    b = (Matrix)c.Clone();
                    Fb = functions.ExecuteFunctions(b);
                    c = (Matrix)a.Clone();
                    Fc = functions.ExecuteFunctions(c);
                    h = h + h;
                    a = a + h;
                    Fa = functions.ExecuteFunctions(a);
                    Steps.Add(i, a);
                    i++;
                }
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
