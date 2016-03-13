using FixedMath.Benchmarks;
using System;

namespace FixedMath.BenchmarkRunner
{
    class Program
    {
        private static void Main(string[] args)
        {
            RunFixedBenchmarks();

            Console.ReadKey();
        }

        private static void RunFixedBenchmarks()
        {
            var add = new FixedAddBenchmark();
            add.Run();
            Console.WriteLine(add.ToString());

            var sub = new FixedSubtractBenchmark();
            sub.Run();
            Console.WriteLine(sub.ToString());

            var mul = new FixedMultiplyBenchmark();
            mul.Run();
            Console.WriteLine(mul.ToString());

            var div = new FixedDivideBenchmark();
            div.Run();
            Console.WriteLine(div.ToString());
        }
    }
}
