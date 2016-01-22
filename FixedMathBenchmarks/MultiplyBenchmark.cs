﻿using System;

namespace FixedMath.Benchmarks
{
    public class MultiplyBenchmark : Benchmark
    {
        public override void Run()
        {
            var r = new Random();

            Run_Mul_Float_Float(r);
            Run_Mul_Float_Int(r);
            Run_Mul_Int_Float(r);
            Run_Mul_Int_Int(r);
        }

        #region Mul

        private void Run_Mul_Float_Float(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-1000, 1000));
            var b = (float)(r.NextDouble() * r.Next(-1000, 1000));
            var result = a * b;

            LoopBase(() => result = a * b);

            var fa = Fixed.FromFloat(a);
            var fb = Fixed.FromFloat(b);
            var fresult = fa * fb;

            LoopFixed(() => fresult = fa * fb);

            if (!Fixed.Approximately(fresult, Fixed.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Mul_Float_Int(Random r)
        {
            var a = (float)(r.NextDouble() * r.Next(-1000, 1000));
            var b = r.Next(-1000, 1000);
            var result = a * b;

            LoopBase(() => result = a * b);

            var fa = Fixed.FromFloat(a);
            var fb = Fixed.FromInt(b);
            var fresult = fa * fb;

            LoopFixed(() => fresult = fa * fb);

            if (!Fixed.Approximately(fresult, Fixed.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Mul_Int_Float(Random r)
        {
            var a = r.Next(-1000, 1000);
            var b = (float)(r.NextDouble() * r.Next(-1000, 1000));
            var result = a * b;

            LoopBase(() => result = a * b);

            var fa = Fixed.FromInt(a);
            var fb = Fixed.FromFloat(b);
            var fresult = fa * fb;

            LoopFixed(() => fresult = fa * fb);

            if (!Fixed.Approximately(fresult, Fixed.FromFloat(result)))
                throw new InvalidOperationException();
        }

        private void Run_Mul_Int_Int(Random r)
        {
            var a = r.Next(-1000, 1000);
            var b = r.Next(-1000, 1000);
            var result = a * b;

            LoopBase(() => result = a * b);

            var fa = Fixed.FromInt(a);
            var fb = Fixed.FromInt(b);
            var fresult = fa * fb;

            LoopFixed(() => fresult = fa * fb);

            if (result != fresult)
                throw new InvalidOperationException();
        }

        #endregion
    }
}
