using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Haskellable.Test
{
    [TestClass]
    public class GuardsTest
    {
        const string TEST = "あたしの年収低すぎ！？";

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(TEST, Helper(500));
            Assert.AreEqual("1200", Helper(1200));
        }

        string Helper(int v)
        {
            return v
                .ToGuards()
                .Where(x => new { number = x, stringOfNumber = x.ToString() })
                .When(a => a.number < 1000, a => TEST)
                .Return(a => a.stringOfNumber);
        }
    }
}
