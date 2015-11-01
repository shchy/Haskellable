using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Haskellable.Code.Monads.Maybe;

namespace Haskellable.Test
{
    [TestClass]
    public class MaybeTest
    {
        [TestMethod]
        public void SomethingClass()
        {
            var test = new Test();
            var mTest = test.ToMaybe();
            Assert.AreEqual(true, mTest.IsSomething);
            Assert.AreEqual(false, mTest.IsNothing);
        }

        [TestMethod]
        public void NothingClass()
        {
            var test = default(Test);
            var mTest = test.ToMaybe();
            Assert.AreEqual(false, mTest.IsSomething);
            Assert.AreEqual(true, mTest.IsNothing);
        }

        [TestMethod]
        public void SomethingStruct()
        {
            var test = 0;
            var mTest = test.ToMaybe();
            Assert.AreEqual(true, mTest.IsSomething);
            Assert.AreEqual(false, mTest.IsNothing);
        }

        [TestMethod]
        public void MaybeOnMaybe()
        {
            var test = new Test();
            var mTest = test.ToMaybe().ToMaybe();
            Assert.AreEqual(true, mTest.IsSomething);
            Assert.AreEqual(false, mTest.IsNothing);
            Assert.IsFalse(mTest is IMaybe<IMaybe<Test>>);
            Assert.IsTrue(mTest is IMaybe<Test>);
        }

        [TestMethod]
        public void MaybeAsClass()
        {
            var test = new Test();
            var mTest = test.ToMaybeAs<Test>();
            Assert.AreEqual(true, mTest.IsSomething);
            Assert.AreEqual(false, mTest.IsNothing);

            var nothing = test.ToMaybeAs<INobady>();
            Assert.AreEqual(false, nothing.IsSomething);
            Assert.AreEqual(true, nothing.IsNothing);
        }

        [TestMethod]
        public void MaybeAsStruct()
        {
            var test = 0;
            var mTest = test.ToMaybeAs<int>();
            Assert.AreEqual(true, mTest.IsSomething);
            Assert.AreEqual(false, mTest.IsNothing);

            var nothing = test.ToMaybeAs<double>();
            Assert.AreEqual(false, nothing.IsSomething);
            Assert.AreEqual(true, nothing.IsNothing);
        }


        [TestMethod]
        public void MaybeNullable()
        {
            int? test = 0;
            var mTest = test.ToMaybe();
            Assert.AreEqual(true, mTest.IsSomething);
            Assert.AreEqual(false, mTest.IsNothing);

            int? testNull = null;
            var mTestNull = testNull.ToMaybe();
            Assert.AreEqual(false, mTestNull.IsSomething);
            Assert.AreEqual(true, mTestNull.IsNothing);


        }




    }

    public interface INobady
    {

    }

    public class Test
    {
        
    }
}
