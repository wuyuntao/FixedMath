using System;

namespace FixedMath.Benchmarks
{
    static class FloatHelper
    {
        public static Fraction ToFraction(this float value)
        {
            return Fraction.Parse(value.ToString("f"));
        }

        public static bool Approximately(float left, float right)
        {
            return (Math.Abs(left - right)) < Math.Max(1e-3f * Math.Max(Math.Abs(left), Math.Abs(right)), 3e-30f * 16);
        }
    }
}
