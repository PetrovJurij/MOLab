using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PenaltyVector:Vector
    {
        public PenaltyVector():base()
        { }

        public PenaltyVector(double []x):base(x)
        { }

        public PenaltyVector(int n):base(n)
        { }

        public PenaltyVector(Vector v):base(v)
        { }

        public PenaltyVector(Matrix Mat):base(Mat)
        { }

        public static bool operator >(PenaltyVector a,PenaltyVector b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be one size.");
            if(a.IsColumn != b.IsColumn)
                throw new ArgumentException("Both vectors must be .");
            for (int i = 0; i < a.Length; i++)
                if (a[i] < b[i])
                    return false;
                else if (a[i] > b[i])
                    return true;

            return false;
        }

        public static bool operator <(PenaltyVector a, PenaltyVector b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be one size.");
            if (a.IsColumn != b.IsColumn)
                throw new ArgumentException("Both vectors must be either columns or rows.");
            for (int i = 0; i < a.Length; i++)
                if (a[i] > b[i])
                    return false;
                else if (a[i] < b[i])
                    return true;

            return false;
        }

        public static double operator -(PenaltyVector a, PenaltyVector b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must be one size.");
            if (a.IsColumn != b.IsColumn)
                throw new ArgumentException("Both vectors must be either columns or rows.");
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return a[i]-b[i];

            return a[a.Length] - b[b.Length];
        }
    }
}
