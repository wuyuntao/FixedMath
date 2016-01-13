using System;

namespace FixedMath.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSimpleTest();
            //SqrtAlgoComparison.Compare();
            //SinAlgoComparison.Compare();
            //AtanAlgoComparison.Compare();

            Console.WriteLine("Exiting...");
            Console.ReadKey();
        }

        static void RunSimpleTest()
        {
            var tests = new SimpleTest();
            tests.TestFactory();
            tests.TestParseInt();
            tests.TestParseFloat();
            tests.TestAdd();
            tests.TestSubtract();
            tests.TestMultiply();
            tests.TestDivide();
            tests.TestSqrt();
            tests.TestSin();
            tests.TestCos();
            tests.TestTan();
            tests.TestAtan();
            tests.TestAtan2();
            tests.TestAsin();
            tests.TestAcos();
        }
    }
}
