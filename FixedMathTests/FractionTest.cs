using NUnit.Framework;

namespace FixedMath.Tests
{
    public class FractionTest
    {
        [Test]
        public void TestFactory()
        {
            var a = new Fraction(1234);
            Assert.AreEqual(1234, a.Numerator);
            Assert.AreEqual(1, a.Denominator);

            var b = new Fraction(-123, 142354);
            Assert.AreEqual(-123, b.Numerator);
            Assert.AreEqual(142354, b.Denominator);
        }

        [Test]
        public void TestParse()
        {
            var a = Fraction.Parse("1234/-25763");
            Assert.AreEqual(1234, a.Numerator);
            Assert.AreEqual(-25763, a.Denominator);

            var b = Fraction.Parse("-943.1234");
            Assert.AreEqual(-9431234, b.Numerator);
            Assert.AreEqual(10000, b.Denominator);

            var c = Fraction.Parse("-456487");
            Assert.AreEqual(-456487, c.Numerator);
            Assert.AreEqual(1, c.Denominator);
        }

        [Test]
        public void TestReduce()
        {
            var a = new Fraction(242, 90);
            a.Reduce();

            Assert.AreEqual(121, a.Numerator);
            Assert.AreEqual(45, a.Denominator);

            a = new Fraction(90, -6);
            a.Reduce();

            Assert.AreEqual(-15, a.Numerator);
            Assert.AreEqual(1, a.Denominator);
        }

        [Test]
        public void TestAdd()
        {
            var a = new Fraction(10, 19);
            var b = new Fraction(32, 11);
            var sum = a + b;
            Assert.AreEqual(718, sum.Numerator);
            Assert.AreEqual(209, sum.Denominator);

            a = new Fraction(10, 18);
            b = new Fraction(32, 15);
            sum = a + b;
            Assert.AreEqual(242, sum.Numerator);
            Assert.AreEqual(90, sum.Denominator);
        }

        [Test]
        public void TestSubtract()
        {
            var a = new Fraction(10, 19);
            var b = new Fraction(32, 11);
            var sub = a - b;
            Assert.AreEqual(-498, sub.Numerator);
            Assert.AreEqual(209, sub.Denominator);

            a = new Fraction(10, 18);
            b = new Fraction(32, 15);
            sub = a - b;
            Assert.AreEqual(-142, sub.Numerator);
            Assert.AreEqual(90, sub.Denominator);
        }

        [Test]
        public void TestMultiply()
        {
            var a = new Fraction(10, 19);
            var b = new Fraction(32, 11);
            var mul = a * b;
            Assert.AreEqual(320, mul.Numerator);
            Assert.AreEqual(209, mul.Denominator);

            a = new Fraction(10, -18);
            b = new Fraction(32, 15);
            mul = a * b;
            Assert.AreEqual(32, mul.Numerator);
            Assert.AreEqual(-27, mul.Denominator);
        }

        [Test]
        public void TestDivide()
        {
            var a = new Fraction(10, 19);
            var b = new Fraction(32, 11);
            var div = a / b;
            Assert.AreEqual(55, div.Numerator);
            Assert.AreEqual(304, div.Denominator);

            a = new Fraction(10, -18);
            b = new Fraction(32, 15);
            div = a / b;
            Assert.AreEqual(25, div.Numerator);
            Assert.AreEqual(-96, div.Denominator);
        }
    }
}
