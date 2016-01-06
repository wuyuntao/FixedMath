using System;

namespace FixedMath.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new FixedTests();
            //tests.TestFactory();
            tests.TestAdd();

            Console.WriteLine("Exiting...");
            Console.ReadKey();
        }
    }
}
