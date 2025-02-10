using System.Numerics;
using System.Security.Principal;
using Main.Codewars._3;

namespace Main
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(">>> START");

			TheMillionthFibonacciKata.TestAll();
			
			//BigInteger res = TheMillionthFibonacciKata.fib(1000000);
			//Console.WriteLine(res);
			

			//int n = 2_000_000; // Change this value to compute Fibonacci of different numbers
			//var t1 = DateTime.Now;
			//Console.WriteLine($">>> {TheMillionthFibonacciKata.fib(n)}");
			//Console.WriteLine((DateTime.Now - t1).TotalSeconds);

			Console.WriteLine(">>> FINISH");
			Console.ReadLine();
		}
	}
}