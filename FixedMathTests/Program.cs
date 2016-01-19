using System;

namespace FixedMath.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunFixedTest();
            //RunFractionTest();
            RunFRandomTest();
            //SqrtAlgoComparison.Compare();
            //SinAlgoComparison.Compare();
            //AtanAlgoComparison.Compare();

            Console.WriteLine("Exiting...");
            Console.ReadKey();
        }

        static void RunFixedTest()
        {
            var t = new FixedTest();
            t.TestFactory();
            t.TestParseInt();
            t.TestParseFloat();
            t.TestAdd();
            t.TestSubtract();
            t.TestMultiply();
            t.TestDivide();
            t.TestSqrt();
            t.TestSin();
            t.TestCos();
            t.TestTan();
            t.TestAtan();
            t.TestAtan2();
            t.TestAsin();
            t.TestAcos();
        }

        static void RunFractionTest()
        {
            var t = new FractionTest();
            t.TestAdd();
        }

        static void RunFRandomTest()
        {
            var t = new FRandomTest();
            t.TestNext();
            t.TestNextFixed();
        }
    }
}
