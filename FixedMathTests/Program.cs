using System;

namespace FixedMath.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			RunSimpleTest();

			Console.WriteLine("Exiting...");
			Console.ReadKey();
		}

		static void RunSimpleTest()
		{
			var tests = new SimpleTest();
			tests.TestFactory();
			tests.TestAdd();
			tests.TestSubtract();
			tests.TestMultiply();
			tests.TestDivide();
		}
	}
}
