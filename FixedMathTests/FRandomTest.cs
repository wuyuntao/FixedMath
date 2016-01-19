using NUnit.Framework;

namespace FixedMath.Tests
{
    public class FRandomTest
    {
        [Test]
        public void TestNext()
        {
            var r = new FRandom(1);

            var a = r.Next(107562);
            Assert.AreEqual(78325, a);

            var b = r.Next(-102324, 12034);
            Assert.AreEqual(-28803, b);
        }

        [Test]
        public void TestNextFixed()
        {
            var r = new FRandom(1);

            var a = r.NextFixed();
            Assert.AreEqual(new Fixed(995757), a);

            var b = r.NextFixed(Fixed.FromFraction(-102324, 1000), Fixed.FromFraction(12034, 100));
            Assert.AreEqual(new Fixed(-16118036), b);
        }
    }
}
