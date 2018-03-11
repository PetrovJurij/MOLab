using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System;

namespace CoreTests
{
    [TestClass]
    public class MatrixTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixMultiplicationArgumentExceptionTest()
        {
            Matrix A = new Matrix(3, 4);
            Matrix B = new Matrix(5, 4);

            Matrix C = A * B;
        }

        [TestMethod]
        public void MatrixMultiplicationTest()
        {
            Matrix A = new Matrix(new double[,]{ {1,3,2 },
                                                 { 0,4,-1} });
            Matrix B = new Matrix(new double[,] { { 2, 0, -1, 1 }, 
                                                  { 3, -2, 1, 2 },
                                                  { 0, 1, 2, 3 } });

            Matrix C = new Matrix(new double[,] { { 11,-4,6,13},
                                                  {12,-9,2,5 } });

            Assert.AreEqual(C,A*B);
        }

    }
}
