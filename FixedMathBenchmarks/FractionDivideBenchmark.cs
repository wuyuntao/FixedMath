﻿using System;

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
            if (b == 0)
                b = 1;
            b = float.Parse(b.ToString("f5"));
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = FloatHelper.ToFraction(a);
            var fb = b.ToFraction();
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                throw new InvalidOperationException();
        }

        private void Run_Divide_Float_Int(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-1000, 1000));
            a = float.Parse(a.ToString("f5"));
            var b = r.Next(-1000, 1000);
            if (b == 0)
                b = 1;
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = FloatHelper.ToFraction(a);
            var fb = new Fraction(b);
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                throw new InvalidOperationException();
        }

        private void Run_Divide_Int_Float(Random r)
        {
            var a = r.Next(-1000, 1000);
            var b = (float)(r.NextDouble() * r.Next(-1000, 1000));
            if (b == 0)
                b = 1;
            b = float.Parse(b.ToString("f5"));
            var result = a / b;

            LoopBase(() => result = a / b);

            var fa = new Fraction(a);
            var fb = b.ToFraction();
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                throw new InvalidOperationException();
        }

        private void Run_Divide_Int_Int(Random r)
        {
            var a = r.Next(-1000, 1000);
            var b = r.Next(-1000, 1000);
            if (b == 0)
                b = 1;
            var result = (float)a / b;

            LoopBase(() => result = (float)a / b);

            var fa = new Fraction(a);
            var fb = new Fraction(b);
            var fresult = fa / fb;

            LoopFixed(() => fresult = fa / fb);

            if (!FloatHelper.Approximately(fresult.ToFloat(), result))
                    throw new InvalidOperationException();
        }

        #endregion
    }
}
