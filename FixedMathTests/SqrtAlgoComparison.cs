using System;
using System.Diagnostics;

namespace FixedMath.Tests
{
    static class SqrtAlgoComparison
    {
        public static void Compare()
        {
            var seed = 1;

            Test(seed, Sqrt0);
            Test(seed, Sqrt1);
            Test(seed, Sqrt2);
        }

        public static void Test(int seed, Func<long, long> sqrt, int loop = 1000000)
        {
            var r = new Random(seed);
            var timer = new Stopwatch();
            var error = 0.0;

            for (int i = 0; i < loop; i++)
            {
                var value = (long)r.Next(0, int.MaxValue) << 32 | (uint)r.Next(0, int.MaxValue);
                var answer0 = Sqrt0(value);

                timer.Start();
                var answer1 = sqrt(value);
                timer.Stop();

                error += Math.Abs(answer0 - answer1);
            }

            Console.WriteLine("Loop: {0}, Time: {1:f6}ms, Error: {2:f6}", loop, timer.ElapsedMilliseconds, error);
        }

        static long Sqrt0(long value)
        {
            return (long)Math.Sqrt(value);
        }

        static long Sqrt1(long value)
        {
            var res = 0L;
            var bit = 1L << 62; // The second-to-top bit is set: 1 << 30 for 32 bits

            // "bit" starts at the highest power of four <= the argument.
            while (bit > value)
                bit >>= 2;

            while (bit != 0)
            {
                if (value >= res + bit)
                {
                    value -= res + bit;
                    res = (res >> 1) + bit;
                }
                else
                    res >>= 1;
                bit >>= 2;
            }

            return res;
        }

        static long Sqrt2(long value)
        {
            if (value == 0) return 0;
            var n = (value >> 1) + 1;
            var n1 = (n + (value / n)) >> 1;
            while (n1 < n)
            {
                n = n1;
                n1 = (n + (value / n)) >> 1;
            }
            return n;
        }
    }
}
