using System;

namespace FixedMath.Benchmarks
{
    static class FractionHelper
    {
        public static Fraction FromFloat(float value)
        {
            return Fraction.Parse(value.ToString("f"));
        }

        public static bool Approximately(Fraction left, Fraction right)
        {
            var lf = (float)left.Numerator / left.Denominator;
            var rf = (float)right.Numerator / right.Denominator;

            return (Math.Abs(lf - rf)) < Math.Max(1e-3f * Math.Max(Math.Abs(lf), Math.Abs(rf)), 3e-30f * 16);
        }
    }
}
