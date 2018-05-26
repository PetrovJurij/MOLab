using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace OptimizationMethods
{
    class SquereInterpolationMethod:IOptimisationMethod
    {
        double eps;
        Matrix a, b, U;
        Dictionary<int, Matrix> Steps = new Dictionary<int, Matrix>();
        FunctionVector functions;

        public SquereInterpolationMethod() { }
        public SquereInterpolationMethod(Matrix x0, FunctionVector func)
        {
            SvenMethod sv = new SvenMethod(x0, func);
            sv.Start();
            Dictionary<string, Matrix> borderPoints = sv.BorderPoints;
            a = borderPoints["a"];
            U = borderPoints["c"];
            b = borderPoints["b"];
            eps = 0.001;
            functions = new FunctionVector(func);

        }
        public SquereInterpolationMethod(Matrix x0, double eps, FunctionVector func)
        {
            SvenMethod sv = new SvenMethod(x0, func);
            sv.Start();
            Dictionary<string, Matrix> borderPoints = sv.BorderPoints;
            a = borderPoints["a"];
            U = borderPoints["c"];
            b = borderPoints["b"];
            this.eps = eps;
            functions = new FunctionVector(func);
        }
        public SquereInterpolationMethod(Matrix a, Matrix b, double eps, FunctionVector func)
        {
            this.a = a;
            this.a = b;
            U = (a + b) / 2;
            this.eps = eps;
            functions = new FunctionVector(func);
        }

        public void Start()
        {
            Matrix V = new Matrix(a.N, a.M);
            Matrix d;
            PenaltyVector Fv;
            PenaltyVector Fa = functions.ExecuteFunctions(a);
            PenaltyVector Fb = functions.ExecuteFunctions(b);
            PenaltyVector Fu = functions.ExecuteFunctions(U);
            int counter = 1;

            bool interpolationFound = SquereInterpolation(a, b, U, V);
            if (interpolationFound)
            {
                d = U - V;
                do
                {
                    interpolationFound = SquereInterpolation(a, b, U, V);
                    if (interpolationFound)
                    {
                        Fv = functions.ExecuteFunctions(V);
                        Fu= functions.ExecuteFunctions(U);
                        d = U - V;
                        bool Sign = false;
                        for (int i = 0; i < d.N; i++)
                        {
                            for (int j = 0; j < d.M; j++)
                            {
                                if (d[i][j] > 0)
                                {
                                    Sign = true;
                                    break;
                                }
                            }
                            if (Sign)
                                break;
                        }
                        if (Fv<Fu)
                        {
                            if(Sign)
                            {
                                a = (Matrix)U.Clone();
                                Fa = functions.ExecuteFunctions(a);
                            }
                            else
                            {
                                b = (Matrix)U.Clone();
                                Fb = functions.ExecuteFunctions(b);
                            }

                            U = (Matrix)V.Clone();
                            Fu = functions.ExecuteFunctions(U);
                        }
                        else
                        {
                            if(Sign)
                            {
                                b = (Matrix)V.Clone();
                                Fb = functions.ExecuteFunctions(b);
                            }
                            else
                            {
                                a = (Matrix)V.Clone();
                                Fa = functions.ExecuteFunctions(a);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                    Steps.Add(counter,U);
                    counter++;
                } while (d.normEvcl() > eps);
            }

        }

        private bool SquereInterpolation(Matrix a, Matrix b, Matrix U, Matrix V)
        {
            V = (Matrix)U.Clone();
            PenaltyVector Fa = functions.ExecuteFunctions(a);
            PenaltyVector Fb = functions.ExecuteFunctions(b);
            PenaltyVector Fu = functions.ExecuteFunctions(U);
            Matrix q = (b - U) * (Fa - Fu);
            Matrix p = (U - a) * (Fb - Fu);
            Matrix s = p + q;

            if (s.normEvcl() <= 0)
                return false;

            V = (p * (U + a) + q * (b + U)) / (s * 2);
            if(V.normEvcl()<=a.normEvcl()||V.normEvcl()>b.normEvcl())
            {
                return false;
            }

            return true;

        }
    }
}
