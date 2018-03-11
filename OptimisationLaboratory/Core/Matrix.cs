using System;
using System.Text;

namespace Core
{
    public class Matrix
    {
        private double[][] Mat;

        private int n, m;

        public Matrix()
        { N = 0; M = 0; }

        public Matrix(double[][] Mat)
        {
            N = Mat.Length;
            M = Mat[0].Length;
            this.Mat = new double[Mat.Length][];

            for (int i = 0; i < this.Mat.Length; i++)
                this.Mat[i] = new double[Mat[i].Length];

            Mat.CopyTo(this.Mat, 0);
        }

        public Matrix(double[,] Mat)
        {
            N = Mat.GetUpperBound(0)+1;
            M = Mat.Length/ (Mat.GetUpperBound(0)+1);
            this.Mat = new double[N][];

            for (int i = 0; i < N; i++)
            {
                this.Mat[i] = new double[M];
                for (int j = 0; j < M; j++)
                {
                    this.Mat[i][j] = Mat[i, j];
                }
            }
        }

        public Matrix(int n, int m)
        {
            N = n;
            M = m;
            Mat = new double[n][];
            for (int i = 0; i < n; i++)
            {
                Mat[i] = new double[m];
                for(int j=0;j<m;j++)
                    if (i == j)
                        Mat[i][j] = 1;
            }
        }


        public Matrix(Matrix M)
        {
            N = M.N; this.M = M.M;
            Mat = new double[N][];
            for (int i = 0; i < N; i++)
            {
                Mat[i] = new double[this.M];
                M.Mat[i].CopyTo(Mat[i],0);
            }
        }
        
        public double[] this[int index]
        {
            get
            {
                return Mat[index];
            }
        }

        public static Matrix operator*(Matrix A, Matrix B)
        {
            if (A.M != B.N) throw new ArgumentException("Количество столбцов первой матрицы должно совпадать " +
                                                        "с количеством строк второй матрицы");
            Matrix C = new Matrix(A.N,B.M);

            for(int i=0;i<A.N;i++)
                for(int j=0;j<B.M;j++)
                {
                    double summ = 0;

                    for(int l=0;l<B.N;l++)
                        summ +=A[i][l]*B[l][j];

                    C[i][j] = summ;
                }

            return C;
        }

        public static Matrix operator *(Matrix A, double b)
        {
           Matrix C = new Matrix(A.N, A.M);

            for (int i = 0; i < A.N; i++)
                for (int j = 0; j < A.M; j++)
                {
                    C[i][j] = A[i][j]*b;
                }
            return C;
        }

        public static Matrix operator *(double a, Matrix B)
        {
            return B * a;
        }

        public static Matrix operator -(Matrix A)
        {
            Matrix C = new Matrix(A.N, A.M);
            for(int i=0;i<A.N;i++)
            {
                for(int j=0;j<A.M;j++)
                {
                    C[i][j] = -A[i][j];
                }
            }
            return C;
        }

        public static Matrix operator -(Matrix A,Matrix B)
        {
            if (A.N != B.N || A.M != B.M)
                throw new ArgumentException("Матрицы должны быть одинаковых размеров");

            Matrix C = new Matrix(A.N, A.M);
            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.M; j++)
                {
                    C[i][j] = A[i][j]-B[i][j];
                }
            }
            return C;
        }

        public static Matrix operator+(Matrix A,Matrix B)
        {
            if(A.N!=B.N||A.M!=B.M)  throw new ArgumentException("Количество строк и столбцов матрицы должны совпадать");
            Matrix C = new Matrix(A.N, B.M);

            for(int i=0;i<C.N;i++)
                for(int j=0;j<C.M;j++)
                    C[i][j] = A[i][j] + B[i][j];
            return C;
        }


        public int N { get { return n; } set { if (value > 0) n = value; } }
        public int M { get { return m; } set { if (value > 0) m = value; } }

        public void Reverse()
        {
            if (N != M)
                throw new InvalidOperationException();

            Matrix ReverseM = new Matrix(N,M);

            for (int i = 0; i < N; i++)
            {
                double a1 = Mat[i][i];

                for (int j=0;j<M;j++)
                {
                    Mat[i][j] /= a1;

                    for (int l=0;l<M;l++)
                        ReverseM.Mat[i][j]/=a1;
                }
            }            
        }

        public void Transform()
        {
            Matrix temp = new Matrix(M, N);
            

            for(int i=0;i<M;i++)
            {
                for(int j=0;j<N;j++)
                {
                    temp[i][j] = Mat[j][i];
                }
            }
            M = temp.M;
            N = temp.N;
            Mat = temp.Mat;
        }



        public double normEvcl()
        {
            double res = 0;
            if(N== 1||M == 1)
            {
                int counter = N == 1 ? M : N;

                for (int i = 0; i < counter; i++)
                    if (N == 1)
                        res += Math.Pow(Mat[0][i], 2);
                    else
                        res += Math.Pow(Mat[i][0], 2);
                res = Math.Sqrt(res);
            }
            else
            {

                for(int i=0;i<N;i++)
                    for(int j=0;j<M;j++)
                        res +=Math.Pow(Mat[i][j],2);

                res = Math.Sqrt(res);
            }
            return res;
        }

        public double Determinant()
        {
            if(N!=M) throw new InvalidOperationException("Детерминант определен только для матриц с одинацовым количеством строк и столбцов");

            if (N  == 2) return Mat[0][0] * Mat[1][1] - Mat[1][0] * Mat[0][1];

            Matrix tempM = (Matrix)Clone();
            Matrix NewM = new Matrix(N - 1, N - 1);

            for (int i=1;i<N;i++)
                for (int j = 1; j < N; j++)
                    NewM[i - 1][j - 1] = Mat[i][j] - Mat[0][j] * Mat[i][0] / Mat[0][0];

            return Mat[0][0]*NewM.Determinant();
        }

        public override bool Equals(object obj)
        {
            Matrix B;
            
            try
            {
                B = (Matrix)obj;
            }catch(InvalidCastException ex)
            {
                return false;
            }
            
            if(N!=B.N||M!=B.M)
                return false;

            for(int i=0;i<N;i++)
                for(int j=0;j<M;j++)
                {
                    if (Mat[i][j] != B[i][j])
                        return false;
                }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int maxLength = 0;
            for(int i=0;i<N;i++)
                for(int j=0;j<M;j++)
                    if(maxLength<Mat[i][j].ToString().Length)
                        maxLength= Mat[i][j].ToString().Length;

            string formatString=String.Format("{0}0,{1}{2}","{",maxLength,"}");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    sb.AppendFormat(formatString, Mat[i][j]);
                    sb.Append(" ");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public object Clone()
        {
            Matrix B = new Matrix(N,M);

            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    B[i][j] = Mat[i][j];

            return B;
        }
    }
}
