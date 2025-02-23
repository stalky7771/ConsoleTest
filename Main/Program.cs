using Main.Codewars._3;
using Main.Codewars._4;
using Main.Codewars._5;
using Main.Math;
using System.IO;

namespace Main
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(">>> START");
			Tournament.BuildMatchesTable(6);
			Console.WriteLine(">>> FINISH");
			Console.ReadLine();
		}
	}
}