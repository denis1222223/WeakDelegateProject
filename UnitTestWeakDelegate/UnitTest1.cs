using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestWeakDelegate
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            long before = GC.GetTotalMemory(true);

            long after = GC.GetTotalMemory(true);

        }
    }
}
