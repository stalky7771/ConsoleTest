// https://www.codewars.com/kata/556deca17c58da83c00002db/csharp

namespace Main.Codewars
{
	public class TribonacciSequence
	{
		public double[] Tribonacci(double[] signature, int n)
		{
			var seq = new List<double>(signature);

			for (var i = 3; i < n; i++)
			{
				seq.Add(seq[i - 1] + seq[i - 2] + seq[i - 3]);
			}

			return seq.Take(n).ToArray();
		}
	}
}
