using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeakDelegateProject;

namespace UnitTestWeakDelegate
{
    [TestClass]
    public class UnitTestWeakDelegate
    {
        private TestMethodsKit testMethods = new TestMethodsKit();

        private event Action<int, int> twoParamsEvent;
        private event Action<int, int, byte> threeParamsEvent;
        private event Action emptyEvent;

        [TestMethod]
        public void TwoParamsTestMethod()
        {
            twoParamsEvent += (Action<int, int>)new WeakDelegate((Action<int, int>)(testMethods.Sum)).Weak;
            twoParamsEvent.Invoke(3, 7);
            Assert.AreEqual(10, testMethods.resultIntValue);
        }

        [TestMethod]
        public void ThreeParamsTestMethod()
        {
            threeParamsEvent += (Action<int, int, byte>)new WeakDelegate((Action<int, int, byte>)(testMethods.Mul)).Weak;
            threeParamsEvent.Invoke(5, 7, 10);
            Assert.AreEqual(350, testMethods.resultIntValue);
        }

        [TestMethod]
        public void EmptyTestMethod()
        {
            WeakDelegate wraper = new WeakDelegate((Action)(testMethods.EmptyFunction));
            testMethods = null;

            emptyEvent += (Action)wraper.Weak;
            emptyEvent.Invoke();
            Assert.IsTrue(wraper.IsAlive());       
            GC.Collect();
            emptyEvent.Invoke();
            Assert.IsFalse(wraper.IsAlive());
        }
    }
}
