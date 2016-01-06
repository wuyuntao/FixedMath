using NUnit.Framework;
using System;

namespace FixedMath.Tests
{
    public class FixedTests
    {
        [Test]
        public void TestFactory()
        {
            var a = Fixed.FromInt(10);
            Assert.AreEqual("10", a.ToString());

            var b = Fixed.FromFloat(3.2f);
            Assert.AreEqual("3.2", b.ToString());

            var c = Fixed.Parse("6.3");
            Assert.AreEqual("6.299999", c.ToString());
        }

        [Test]
        public void TestAdd()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var sum = a + b;
            Assert.AreEqual(sum, Fixed.Parse("13.5"));
        }


        [Test]
        public void TestSub()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var sub = a - b;
            Assert.AreEqual(sub, Fixed.Parse("7.1"));
        }
    }
}
