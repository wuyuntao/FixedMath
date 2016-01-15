using NUnit.Framework;
using System;

namespace FixedMath.Tests
{
    public class FixedTest
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
        public void TestParseInt()
        {
            var n = Fixed.Parse("0");
            Assert.AreEqual(Fixed.Zero, n);

            n = Fixed.Parse("1");
            Assert.AreEqual(Fixed.One, n);

            n = Fixed.Parse("-1");
            Assert.AreEqual(Fixed.FromInt(-1), n);

            n = Fixed.Parse("16777216");
            Assert.AreEqual(Fixed.FromInt(16777216), n);

            n = Fixed.Parse("-16777216");
            Assert.AreEqual(Fixed.FromInt(-16777216), n);
        }

        [Test]
        public void TestParseFloat()
        {
            var f = Fixed.Parse("000.0000");
            AssertApproximately(Fixed.Zero, f);

            f = Fixed.Parse("1.4567");
            AssertApproximately(Fixed.FromFloat(1.4567f), f);

            f = Fixed.Parse("-1.4831");
            AssertApproximately(Fixed.FromFloat(-1.4831f), f);

            f = Fixed.Parse("16777216.1345654");
            AssertApproximately(Fixed.FromFloat(16777216), f);

            f = Fixed.Parse("-16777216.1345654");
            AssertApproximately(Fixed.FromFloat(-16777216), f);
        }

        [Test]
        public void TestAdd()
        {
            var a = Fixed.Parse("10.3");
            var b = Fixed.Parse("3.2");

            var sum = a + b;
            AssertApproximately(Fixed.Parse("13.5"), sum);

            sum = a + 3;
            AssertApproximately(Fixed.Parse("13.3"), sum);

            sum = -5 + b;
            AssertApproximately(Fixed.Parse("-1.8"), sum);
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

        [Test]
        public void TestAtan()
        {
            var a = Fixed.FromInt(14);
            var r = Fixed.FromFloat(1.4994888f);
            AssertApproximately(r, FMath.Atan(a));

            a = Fixed.FromFloat(-0.154787f);
            r = Fixed.FromFloat(-0.153568f);
            AssertApproximately(r, FMath.Atan(a));

            a = Fixed.FromFloat(0.7547870f);
            r = Fixed.FromFloat(0.6465577f);
            AssertApproximately(r, FMath.Atan(a));
        }

        [Test]
        public void TestAtan2()
        {
            var y = Fixed.FromInt(0);
            var x = Fixed.FromInt(1);
            var r = Fixed.FromFloat(0);
            AssertApproximately(r, FMath.Atan2(y, x));

            y = Fixed.FromInt(0);
            x = Fixed.FromInt(-1);
            r = Fixed.FromFloat((float)Math.PI);
            AssertApproximately(r, FMath.Atan2(y, x));

            y = Fixed.FromInt(1);
            x = Fixed.FromInt(0);
            r = Fixed.FromFloat((float)Math.PI / 2);
            AssertApproximately(r, FMath.Atan2(y, x));

            y = Fixed.FromFloat(1.54513f);
            x = Fixed.FromFloat(-1.65673f);
            r = Fixed.FromFloat(2.390926f);     // 2.3910351266f is more accurate value
            AssertApproximately(r, FMath.Atan2(y, x));
        }

        [Test]
        public void TestAsin()
        {
            var a = Fixed.FromFloat(0.54646f);
            var r = Fixed.FromFloat(0.578131f);
            AssertApproximately(r, FMath.Asin(a));

            a = Fixed.FromFloat(-0.154787f);
            r = Fixed.FromFloat(-0.155411851f);
            AssertApproximately(r, FMath.Asin(a));
        }

        [Test]
        public void TestAcos()
        {
            var a = Fixed.FromFloat(0.54646f);
            var r = Fixed.FromFloat(0.992664887f);
            AssertApproximately(r, FMath.Acos(a));

            a = Fixed.FromFloat(-0.154787f);
            r = Fixed.FromFloat(1.726208178f);
            AssertApproximately(r, FMath.Acos(a));
        }

        static void AssertApproximately(Fixed expected, Fixed actual)
        {
            Assert.True(Fixed.Approximately(expected, actual));
        }
    }
}
