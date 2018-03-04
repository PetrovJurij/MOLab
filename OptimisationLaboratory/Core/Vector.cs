using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Vector
    {
        private double[] vec;
        private int n;

        public Vector()
        { n = 0; }

        public Vector(double []x)
        {
            n = x.Length;
            vec = new double[n];
            x.CopyTo(vec,0);
        }

        public Vector(int n)
        {
            this.n = n;

            vec = new double[n];
            for (int i = 0; i < n; i++)
                vec[i] = 0;
        }

        public Vector(Vector v)
        {
            n = v.n;
            v.vec.CopyTo(vec, 0);
        }

        public Vector(Matrix Mat)
        {
            if (Mat.M > 1 && Mat.N > 1)
                throw new ArgumentException("Матрица должна быть " +
                                            "либо столбцом, либо строкой.");
        }

        public double this[int index]
        {
            get { return vec[index]; }
        }



    }
}
