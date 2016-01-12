using System;
using System.Diagnostics;

namespace FixedMath.Tests
{
    static class SinAlgoComparison
    {
        public static void Compare()
        {
            var seed = 1;

            Test(seed, Sin0);
            Test(seed, Sin1);
        }

        static void Test(int seed, Func<Fixed, Fixed> sin, int loop = 1000000)
        {
            var r = new Random(seed);
            var timer = new Stopwatch();
            var error = 0.0;

            for (int i = 0; i < loop; i++)
            {
                var angle = Fixed.FromFloat((float)(r.NextDouble() * r.Next(0, 1440) - 720.0));
                var answer0 = Sin0(angle);

                timer.Start();
                var answer1 = sin(angle);
                timer.Stop();

                error += Math.Abs(answer0.ToFloat() - answer1.ToFloat());
            }

            Console.WriteLine("Loop: {0}, Time: {1:f6}ms, Error: {2:f6}", loop, timer.ElapsedMilliseconds, error / loop);
        }


        static Fixed Sin0(Fixed angle)
        {
            return Fixed.FromFloat((float)Math.Sin(angle.ToFloat()));
        }

        static Fixed Sin1(Fixed angle)
        {
            // https://en.wikipedia.org/wiki/Trigonometric_functions#Series_definitions
            var pi = FMath.PI.RawValue;
            var pi2 = pi << 1;

            // Normalize rad to [-π, +π]
            var rad = angle.RawValue % pi2;
            if (rad > pi)
                rad -= pi2;
            else if (rad < -pi)
                rad += pi2;

            var squareOfRad = (rad * rad) >> Fixed.SHIFT_BITS;

            var r = rad;                // x
            rad = (rad * squareOfRad) >> Fixed.SHIFT_BITS;
            r -= rad / 6L;              // - x^3 / 3!
            rad = (rad * squareOfRad) >> Fixed.SHIFT_BITS;
            r += rad / 120L;            // + x^5 / 5!
            rad = (rad * squareOfRad) >> Fixed.SHIFT_BITS;
            r -= rad / 5040L;           // - x^7 / 7!
            rad = (rad * squareOfRad) >> Fixed.SHIFT_BITS;
            r += rad / 362880L;         // + x^9 / 9!
            rad = (rad * squareOfRad) >> Fixed.SHIFT_BITS;
            r -= rad / 39916800L;       // - x^11 / 11!
            rad = (rad * squareOfRad) >> Fixed.SHIFT_BITS;
            r += rad / 6227020800L;     // + x^13 / 13!

            return new Fixed(r);
        }
    }
}
