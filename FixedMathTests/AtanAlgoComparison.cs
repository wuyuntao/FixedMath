using System;
using System.Diagnostics;

namespace FixedMath.Tests
{
    static class AtanAlgoComparison
    {
        public static void Compare()
        {
            var seed = 1;

            Test(seed, Atan0);
            Test(seed, Atan1);
        }

        static void Test(int seed, Func<Fixed, Fixed> atan, int loop = 1000000)
        {
            var r = new Random(seed);
            var timer = new Stopwatch();
            var error = 0.0;

            for (int i = 0; i < loop; i++)
            {
                var value = Fixed.FromFloat((float)(r.NextDouble() * 4 - 2));
                var answer0 = Atan0(value);

                timer.Start();
                var answer1 = atan(value);
                timer.Stop();

                error += Math.Abs(answer0.ToFloat() - answer1.ToFloat());
            }

            Console.WriteLine("Loop: {0}, Time: {1:f6}ms, Error: {2:f6}", loop, timer.ElapsedMilliseconds, error / loop);
        }

        static Fixed Atan0(Fixed d)
        {
            return Fixed.FromFloat((float)Math.Atan(d.ToFloat()));
        }

        static Fixed Atan1(Fixed d)
        {
            // https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
            // Make sure |d| <= 1
            bool inversed;
            if (FMath.Abs(d) > Fixed.One)
            {
                d = Fixed.One / d;
                inversed = true;
            }
            else
                inversed = false;

            var v = d.RawValue;
            var square = (v * v) >> Fixed.SHIFT_BITS;

            var r = v;
            var n = 3L;

            for (int i = 0; i < 15; i++)
            {
                v = (v * square) >> Fixed.SHIFT_BITS;
                r -= v / n;
                n += 2;

                v = (v * square) >> Fixed.SHIFT_BITS;
                r += v / n;
                n += 2;
            }

            if (inversed)
            {
                if (r > 0)
                    r = FMath.PI.RawValue / 2 - r;
                else
                    r = -FMath.PI.RawValue / 2 - r;
            }

            return new Fixed(r);
        }
    }
}
