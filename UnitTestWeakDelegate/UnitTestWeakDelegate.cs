using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeakDelegateProject;

namespace UnitTestWeakDelegate
{
    [TestClass]
    public class UnitTestWeakDelegate
    {
        private event Action<int, int> testEvent;
        private event Action<int, int, byte> testEventThreeParams;
        private event Action sampleEvent;

        [TestMethod]
        public void TwoParamsTestMethod()
        {
            var testMethods = new TestMethodsKit();
            testEvent += (Action<int, int>)new WeakDelegate((Action<int, int>)(testMethods.Sum)).Weak;
            testEvent.Invoke(3, 7);
            Assert.AreEqual(10, testMethods.resultIntValue);
        }

        [TestMethod]
        public void ThreeParamsTestMethod()
        {
            var testMethods = new TestMethodsKit();
            testEventThreeParams += (Action<int, int, byte>)new WeakDelegate((Action<int, int, byte>)(testMethods.Mul)).Weak;
            testEventThreeParams.Invoke(5, 7, 10);
            Assert.AreEqual(350, testMethods.resultIntValue);
        }

        [TestMethod]
        public void EmptyTestMethod()
        {
            var testMethods = new TestMethodsKit();
            WeakDelegate wraper = new WeakDelegate((Action)(testMethods.NullFunc));
            sampleEvent += (Action)wraper.Weak;
            sampleEvent.Invoke();
            Assert.IsTrue(wraper.IsAlive());
            testMethods = null;
            GC.Collect();
            sampleEvent.Invoke();
            Assert.IsFalse(wraper.IsAlive());
        }
    }
}
