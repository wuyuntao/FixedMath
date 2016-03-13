using System;

namespace FixedMath.Benchmarks
{
    public class FixedSubtractBenchmark : Benchmark
    {
        public override void Run()
        {
            var r = new Random();

            Run_Sub_Float_Float(r);
            Run_Sub_Float_Int(r);
            Run_Sub_Int_Float(r);
            Run_Sub_Int_Int(r);
        }

        #region Sub

        private void Run_Sub_Float_Float(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var b = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = Fixed.FromFloat(a);
            var fb = Fixed.FromFloat(b);
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (!Fixed.Approximately(fresult, Fixed.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Sub_Float_Int(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var b = r.Next(-10000, 10000);
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = Fixed.FromFloat(a);
            var fb = Fixed.FromInt(b);
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (!Fixed.Approximately(fresult, Fixed.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Sub_Int_Float(Random r)
        {
            var a = r.Next(-10000, 10000);
            var b = (float)(r.NextDouble() * r.Next(-10000, 10000));
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = Fixed.FromInt(a);
            var fb = Fixed.FromFloat(b);
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (!Fixed.Approximately(fresult, Fixed.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Sub_Int_Int(Random r)
        {
            var a = r.Next(-10000, 10000);
            var b = r.Next(-10000, 10000);
            var result = a - b;

            LoopBase(() => result = a - b);

            var fa = Fixed.FromInt(a);
            var fb = Fixed.FromInt(b);
            var fresult = fa - fb;

            LoopFixed(() => fresult = fa - fb);

            if (result != fresult)
                throw new InvalidOperationException();
        }

        #endregion
    }
}
