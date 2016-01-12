using NUnit.Framework;
using System;

namespace FixedMath.Tests
{
    public class SimpleTest
    {
        [Test]
        public void TestFactory()
        {
            var a = Fixed.FromInt(10);
            Assert.AreEqual(Fixed.FromInt(10), a);

            var b = Fixed.FromFloat(3.2f);
            Assert.AreEqual(Fixed.FromFloat(3.2f), b);

            var c = Fixed.Parse("6.3");
            AssertApproximately(Fixed.FromFraction(63, 10), c);

            var d = Fixed.FromFraction(3141592, 1000000);
            AssertApproximately(Fixed.FromFloat(3.141592f), d);
        }

        [Test]
        public void TestAdd()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var sum = a + b;
            AssertApproximately(Fixed.Parse("13.5"), sum);
        }

        [Test]
        public void TestSubtract()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var sub = a - b;
            var r = Fixed.Parse("7.1");
            AssertApproximately(r, sub);
        }

        [Test]
        public void TestMultiply()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var mul = a * b;
            AssertApproximately(Fixed.Parse("32.96"), mul);
        }

        [Test]
        public void TestDivide()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var div = a / b;
            var r = Fixed.Parse("3.21875");
            AssertApproximately(r, div);
        }

        [Test]
        public void TestSqrt()
        {
            var a = Fixed.FromInt(9);
            var r = Fixed.FromInt(3);
            AssertApproximately(r, FMath.Sqrt(a));

            a = Fixed.Parse("1546.564654");
            r = Fixed.Parse("39.326386");
            AssertApproximately(r, FMath.Sqrt(a));
        }

        [Test]
        public void TestSin()
        {
            var deg2Rad = Fixed.FromFloat((float)(Math.PI / 180));
            var a = Fixed.FromInt(90) * deg2Rad;
            var r = Fixed.FromInt(1);
            AssertApproximately(r, FMath.Sin(a));

            a = Fixed.FromInt(0) * deg2Rad;
            r = Fixed.FromInt(0);
            AssertApproximately(r, FMath.Sin(a));

            a = Fixed.FromInt(37) * deg2Rad;
            r = Fixed.Parse("0.60181502");
            AssertApproximately(r, FMath.Sin(a));
        }

        [Test]
        public void TestCos()
        {
            var deg2Rad = Fixed.FromFloat((float)(Math.PI / 180));
            var a = Fixed.FromInt(90) * deg2Rad;
            var r = Fixed.FromInt(0);
            AssertApproximately(r, FMath.Cos(a));

            a = Fixed.FromInt(0) * deg2Rad;
            r = Fixed.FromInt(1);
            AssertApproximately(r, FMath.Cos(a));

            a = Fixed.FromInt(37) * deg2Rad;
            r = Fixed.Parse("0.79863551");
            AssertApproximately(r, FMath.Cos(a));
        }

        [Test]
        public void TestTan()
        {
            var deg2Rad = Fixed.FromFloat((float)(Math.PI / 180));
            var a = Fixed.FromInt(45) * deg2Rad;
            var r = Fixed.FromInt(1);
            AssertApproximately(r, FMath.Tan(a));

            a = Fixed.FromInt(0) * deg2Rad;
            r = Fixed.FromInt(0);
            AssertApproximately(r, FMath.Tan(a));

            a = Fixed.FromInt(37) * deg2Rad;
            r = Fixed.Parse("0.75355405");
            AssertApproximately(r, FMath.Tan(a));
        }

        static void AssertApproximately(Fixed expected, Fixed actual)
        {
            Assert.True(Fixed.Approximately(expected, actual));
        }
    }
}
