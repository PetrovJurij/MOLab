using System;
using System.Collections.Generic;
using Core;

namespace OptimizationMethods
{
    public class Huk_DjivsMethod:IOptimisationMethod
    {
        double eps;
        Matrix x;
        Dictionary<int, Matrix> Steps = new Dictionary<int, Matrix>();
        FunctionVector functions;

        public Huk_DjivsMethod(){ }
        public Huk_DjivsMethod(Matrix x0,FunctionVector func)
        {
            x = x0;
            functions = new FunctionVector(func);
        }
        public Huk_DjivsMethod(Matrix x0,double eps,FunctionVector func)
        {
            x = x0;
            this.eps = eps;
            functions = new FunctionVector(func);
        }

        public void Start()
        {
            PenaltyVector fx = new PenaltyVector(functions.ExecuteFunctions(x));
            
            double sigma = 2;
            int i = 0;

            while(sigma>=eps)
            {
                i++;
                sigma = sigma / 2;
                Matrix y = IterationSearch(x, sigma);
                PenaltyVector fy = new PenaltyVector(functions.ExecuteFunctions(y));

                while (fx<fy)
                {
                    Matrix z = (Matrix)((2 * y - x).Clone());
                    PenaltyVector fz = new PenaltyVector(functions.ExecuteFunctions(z));
                    x = y;
                    fx = fy;
                    y = IterationSearch(z, sigma);
                    fy = new PenaltyVector(functions.ExecuteFunctions(y));
                }

                Steps.Add(i,y);
            }
        }

        private Matrix IterationSearch(Matrix x, double sigma)
        {
            int n = x.N;
            Matrix y = (Matrix)x.Clone();
            Matrix u = (Matrix)x.Clone();
            PenaltyVector fy = new PenaltyVector(functions.ExecuteFunctions(y));
            PenaltyVector fu = new PenaltyVector(functions.ExecuteFunctions(u));

            for(int i=0;i<n;i++)
            {
                u[0][i] = y[0][i] + sigma;
                fu = new PenaltyVector(functions.ExecuteFunctions(u));
                if(fu<fy)
                {
                    y[0][i] = u[0][i];
                    fy = (PenaltyVector)fu.Clone();
                    continue;
                }
                u[0][i] = y[0][i] - sigma;
                fu = new PenaltyVector(functions.ExecuteFunctions(u));
                if(fu<fy)
                {
                    y[0][i] = u[0][i];
                    fy = (PenaltyVector)fu.Clone();
                }
                else
                {
                    u[0][i] = y[0][i];
                    fu = (PenaltyVector)fy.Clone();
                }
            }

            return y;
        }
    }
}
