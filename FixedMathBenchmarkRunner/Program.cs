using FixedMath.Benchmarks;
using System;

namespace FixedMath.BenchmarkRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var add = new AddBenchmark();
            add.Run();
            Console.WriteLine(add.ToString());

            var sub = new SubtractBenchmark();
            sub.Run();
            Console.WriteLine(sub.ToString());

            var mul = new MultiplyBenchmark();
            mul.Run();
            Console.WriteLine(mul.ToString());

            var div = new DivideBenchmark();
            div.Run();
            Console.WriteLine(div.ToString());

            Console.ReadKey();
        }
    }
}
