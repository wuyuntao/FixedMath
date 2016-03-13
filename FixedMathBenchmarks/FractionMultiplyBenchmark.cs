using System;

namespace FixedMath.Benchmarks
{
    public class FractionCompareBenchmark : Benchmark
    {
        public override void Run()
        {
            var r = new Random();

            Run_Compare_Float_Float(r);
        }

        #region Compare

        private void Run_Compare_Float_Float(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-1000, 1000));
            a = float.Parse(a.ToString("f5"));
            var b = (float)(r.NextDouble() * r.Next(-1000, 1000));
            b = float.Parse(b.ToString("f5"));
            var result = a.CompareTo(b);

            LoopBase(() => result = a.CompareTo(b));

            var fa = FloatHelper.ToFraction(a);
            var fb = b.ToFraction();
            var fresult = fa.CompareTo(fb);

            LoopFixed(() => fresult = fa.CompareTo(fb));

            if (result != fresult)
                throw new InvalidOperationException();
        }

        #endregion
    }
}
