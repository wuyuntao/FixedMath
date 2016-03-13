using System;

namespace FixedMath.Benchmarks
{
    public class FractionDivideBenchmark : Benchmark
    {
        public override void Run()
        {
            var r = new Random();

            Run_Divide_Float_Float(r);
            Run_Divide_Float_Int(r);
            Run_Divide_Int_Float(r);
            Run_Divide_Int_Int(r);
        }

        #region Divide

        private void Run_Divide_Float_Float(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-1000, 1000));
            a = float.Parse(a.ToString("f5"));
            var b = (float)(r.NextDouble() * r.Next(-1000, 1000));
            b = float.Parse(b.ToString("f5"));
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = FractionHelper.FromFloat(a);
            var fb = FractionHelper.FromFloat(b);
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FractionHelper.Approximately(fresult, FractionHelper.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Divide_Float_Int(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-1000, 1000));
            a = float.Parse(a.ToString("f5"));
            var b = r.Next(-1000, 1000);
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = FractionHelper.FromFloat(a);
            var fb = new Fraction(b);
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FractionHelper.Approximately(fresult, FractionHelper.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Divide_Int_Float(Random r)
        {
            var a = r.Next(-1000, 1000);
            var b = (float)(r.NextDouble() * r.Next(-1000, 1000));
            b = float.Parse(b.ToString("f5"));
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = new Fraction(a);
            var fb = FractionHelper.FromFloat(b);
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FractionHelper.Approximately(fresult, FractionHelper.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Divide_Int_Int(Random r)
        {
            var a = r.Next(-1000, 1000);
            var b = r.Next(-1000, 1000);
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = new Fraction(a);
            var fb = new Fraction(b);
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (result != fresult)
                throw new InvalidOperationException();
        }

        #endregion
    }
}
