using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Haskellable.Test
{
    [TestClass]
    public class GuardsTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("1", ToStringByGuards(1));
            Assert.AreEqual("2", ToStringByGuards(2));
            Assert.AreEqual("3", ToStringByGuards(3));
            Assert.AreEqual("4", ToStringByGuards(4));
            Assert.AreEqual("not match", ToStringByGuards(0));
            Assert.AreEqual("1", ToStringByGuardsWithWhere(1));
            Assert.AreEqual("2", ToStringByGuardsWithWhere(2));
            Assert.AreEqual("3", ToStringByGuardsWithWhere(3));
            Assert.AreEqual("4", ToStringByGuardsWithWhere(4));
            Assert.AreEqual("not match", ToStringByGuardsWithWhere(5));
        }

        string ToStringByGuards(int v)
        {
            return
                v.ToGuards()
                .When(1, "1")
                .When(2, _ => _.ToString())
                .When(a => a == 3, "3")
                .When(a => a == 4, _ => _.ToString())
                .Return("not match");
        }

        string ToStringByGuardsWithWhere(int v)
        {
            return
                v.ToGuards()
                .Where(a => new { v = a, s = a.ToString() })
                .When(a => a.v == 1, "1")
                .When(a => a.v == 2, _ => _.s)
                .When(a => a.v == 3, "3")
                .When(a => a.v == 4, _ => _.s)
                .Return("not match");
        }



    }
}
