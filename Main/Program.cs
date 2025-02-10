using Main.Codewars._3;

namespace Main
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(">>> START");

			RailFenceCipher.EncodeSampleTests();
			RailFenceCipher.DecodeSampleTests();

			Console.WriteLine(">>> FINISH");
			Console.ReadLine();
		}
	}
}