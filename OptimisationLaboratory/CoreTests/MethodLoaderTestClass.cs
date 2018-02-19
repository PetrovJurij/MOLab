using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System.IO;
using System;

namespace CoreTests
{
    [TestClass]
    public class MethodLoaderTestClass
    {
        [TestMethod]
        public void TestLoadsAllIOptimisationMethods()
        {
            MethodsLoader loader = new MethodsLoader(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")) + "\\GUI\\bin\\Debug\\methods\\");
            var instances=loader.Load();
            foreach (var instance in instances)
                Assert.IsInstanceOfType(instance, typeof(IOptimisationMethod));
        }
    }
}
