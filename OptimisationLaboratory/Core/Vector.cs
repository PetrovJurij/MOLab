using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Vector
    {
        private double[] vec;
        private int n;

        public int Length
        {
            get { return n; }
            private set { }
        }
        public bool IsColumn { get; set; }

        public Vector()
        { n = 0; }

        public Vector(double[] x)
        {
            n = x.Length;
            vec = new double[n];
            x.CopyTo(vec, 0);
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
            IsColumn = v.IsColumn;
        }

        public Vector(Matrix Mat)
        {
            if (Mat.M > 1 && Mat.N > 1)
                throw new ArgumentException("Матрица должна быть " +
                                            "либо столбцом, либо строкой.");

            if (Mat.M > 1)
                IsColumn = false;
            else
                IsColumn = true;

            if (IsColumn)
                vec = Mat[0];
            else
            {
                Mat.Transform();
                vec = Mat[0];
                Mat.Transform();
            }
        }

        public double this[int index]
        {
            get { return vec[index]; }
            set { vec[index] = value; }
        }

        public static bool operator >(Vector a, Vector b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be one size.");
            if (a.IsColumn != b.IsColumn)
                throw new ArgumentException("Both vectors must be either columns or rows.");
            if (a.Magnitude() > b.Magnitude())
                return true;
            return false;
        }
        public static bool operator <(Vector a, Vector b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be one size.");
            if (a.IsColumn != b.IsColumn)
                throw new ArgumentException("Both vectors must be either columns or rows.");
            if (a.Magnitude() < b.Magnitude())
                return true;
            return false;
        }

        public static double operator -(Vector a, Vector b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be one size.");
            if (a.IsColumn != b.IsColumn)
                throw new ArgumentException("Both vectors must be either columns or rows.");
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return a[i] - b[i];

            return a[a.Length] - b[b.Length];
        }

        public double Magnitude()
        {
            double res=0;
            for(int i=0;i<Length;i++)
            {
                res += vec[i] * vec[i];
            }
            res = Math.Sqrt(res);
            return res;
        }

        public object Clone()
        {
            Vector vec=new Vector(this);
            return vec;
        }
    }
}
