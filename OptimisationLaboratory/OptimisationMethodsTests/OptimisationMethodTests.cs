using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using OptimisationMethods;

namespace OptimisationMethodsTests
{
    [TestClass]
    public class StepAdaptationMethodTests
    {
        [TestMethod]
        public void AlgorithmWorkingTest()
        {
            Matrix x = new Matrix(new double[,] { { 9, 4 } });
            Matrix h = new Matrix(new double[,] { { -9, 5 } });

            
        }


    }

    [TestClass]
    public class SteepestDescendMethodTests
    {
        [TestMethod]
        public void AlgorithmWorkingTest()
        {
            Matrix x = new Matrix(new double[,] { { 9, 4 } });
        }
    }
}
