using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System;

namespace CoreTests
{
    [TestClass]
    public class VectorsTestClass
    {
        [TestMethod]
        public void VectorBetterCheck()
        {
            Vector a = new PenaltyVector(4);
            Vector b = new PenaltyVector(4);

            a[2] = 2;

            bool res = a > b;
            Assert.AreEqual(true, res);

            b[0] = 1;
            res = a > b;
            Assert.AreEqual(false, res);
        }
    }
}
