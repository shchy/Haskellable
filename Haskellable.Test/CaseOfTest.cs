using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Haskellable.Test
{
    [TestClass]
    public class CaseOfTest
    {
        [TestMethod]
        public void FromClass()
        {
            Assert.AreEqual("Kami7", HelperForClass(new Mayuyu()));
            Assert.AreEqual("AKB", HelperForClass(new NodoAmeChan()));
            Assert.AreEqual("知らない人", HelperForClass("taro"));
        }

        [TestMethod]
        public void FromStruct()
        {
            Assert.AreEqual("int", HelperForStruct(9));
            Assert.AreEqual("long", HelperForStruct(9L));
            Assert.AreEqual("string", HelperForStruct("moji"));
            Assert.AreEqual("double", HelperForStruct(0.1));
            Assert.AreEqual("?", HelperForStruct(new object()));
        }

        string HelperForClass(object x)
        {
            return x
                .ToCaseOf()
                .Match(NameOfKami7)
                .Match(NameOfAkb)
                .Return("知らない人");
        }

        string HelperForStruct(object x)
        {
            return x
                .ToCaseOf()
                .Match((int y) => "int")
                .Match((long y) => "long")
                .Match((string y) => "string")
                .Match((double y) => "double")
                .Return("?");
        }

        public Func<IAkber, string> NameOfAkb
        {
            get
            {
                return x => "AKB";
            }
        }

        public Func<IKami7, string> NameOfKami7
        {
            get
            {
                return x => "Kami7";
            }
        }

    }

    public interface IAkber
    {
    }

    public interface IKami7
    {
    }

    class Mayuyu : IAkber, IKami7
    {
    }

    class NodoAmeChan : IAkber
    {
    }
}
