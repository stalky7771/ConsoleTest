using Main.Math;

namespace Main
{
	internal class Program
	{
		static void Main(string[] args)
		{
			char[] a = "ABCD".ToCharArray();
			MathResearch.PrintPermutations(a, 0, a.Length - 1);

			Console.WriteLine("Finished");
			Console.ReadKey();
		}
	}
}