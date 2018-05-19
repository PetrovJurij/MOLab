using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class FunctionVectorTestClass
    {
        [TestMethod]
        public void DelegateIntoFuncCastTest()
        {
            functionsMask[] f = new functionsMask[1];
            f[0]=(Matrix x) => { return x[0][0]; };


            FunctionVector functionVector = new FunctionVector(f);
        }

        [TestMethod]
        public void ExecuteFunctionsCastTest()
        {
            functionsMask[] f = new functionsMask[8];

            f[0] = (Matrix x) => { return x[0][0]; };
            f[1] = (Matrix x) => { return x[0][1]; };
            f[2] = (Matrix x) => { return x[0][2]; };
            f[3] = (Matrix x) => { return x[0][3]; };
            f[4] = (Matrix x) => { return x[0][4]; };
            f[5] = (Matrix x) => { return x[0][5]; };
            f[6] = (Matrix x) => { return x[0][6]; };
            f[7] = (Matrix x) => { return x[0][7]; };

            Matrix arg = new Matrix(1,f.Length);

            for(int i=0;i<f.Length;i++)
            {
                arg[0][i] = i;
            }

            FunctionVector functionVector = new FunctionVector(f);
            Vector res = functionVector.ExecuteFunctions(arg);

            for(int i=0;i<f.Length;i++)
                Assert.AreEqual(res[i], i);
        }
    }
}
