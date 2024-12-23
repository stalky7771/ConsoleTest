using System.Security.Cryptography.X509Certificates;
using Main.Codewars;

namespace Main
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(">>> START");
			TicTacToe.TestAll();
			Console.WriteLine(">>> FINISH");
			Console.ReadLine();
		}
	}
}