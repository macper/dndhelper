using System;
using DnDHelper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DnDHelper.Test
{
    [TestClass]
    public class DamageTest
    {
        [TestMethod]
        public void ParseTest()
        {
            Damage dmg = new Damage();
            Assert.IsTrue(Damage.TryParse("2K6+2[Fizyczne]+1K8+3[Ogień]", out dmg));
            Assert.AreEqual(4, dmg.Elements.Count);
            Assert.AreEqual(2, dmg.Elements.Single(e => e.DamageType == "Fizyczne" && e.MaxValue == 6).Count);
            Assert.AreEqual(2, dmg.Elements.Single(e => e.DamageType == "Fizyczne" && e.MaxValue == 1).Count);
            Assert.AreEqual(1, dmg.Elements.Single(e => e.DamageType == "Ogień" && e.MaxValue == 8).Count);
            Assert.AreEqual(3, dmg.Elements.Single(e => e.DamageType == "Ogień" && e.MaxValue == 1).Count);
        }
    }
}
