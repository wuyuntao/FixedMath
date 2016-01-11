using NUnit.Framework;

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

        static void AssertApproximately(Fixed expected, Fixed actual)
		{
			Assert.True(Fixed.Approximately(expected, actual));
		}
	}
}
