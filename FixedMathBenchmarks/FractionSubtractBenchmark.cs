using System;

namespace FixedMath.Benchmarks
{
    public class FractionSubtractBenchmark : Benchmark
    {
        public override void Run()
        {
            var r = new Random();

            Run_Subtract_Float_Float(r);
            Run_Subtract_Float_Int(r);
            Run_Subtract_Int_Float(r);
            Run_Subtract_Int_Int(r);
        }

        #region Subtract

        private void Run_Subtract_Float_Float(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var b = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = FloatHelper.ToFraction(a);
            var fb = b.ToFraction();
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                throw new InvalidOperationException();
        }

        private void Run_Subtract_Float_Int(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var b = r.Next(-10000, 10000);
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = FloatHelper.ToFraction(a);
            var fb = new Fraction(b);
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                throw new InvalidOperationException();
        }

        private void Run_Subtract_Int_Float(Random r)
        {
            var a = r.Next(-10000, 10000);
            var b = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = new Fraction(a);
            var fb = b.ToFraction();
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                throw new InvalidOperationException();
        }

        private void Run_Subtract_Int_Int(Random r)
        {
            var a = r.Next(-10000, 10000);
            var b = r.Next(-10000, 10000);
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = new Fraction(a);
            var fb = new Fraction(b);
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (result != fresult)
                throw new InvalidOperationException();
        }

        #endregion
    }
}