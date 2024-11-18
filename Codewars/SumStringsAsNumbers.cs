// https://www.codewars.com/kata/5324945e2ece5e1f32000370

using System.Numerics;

namespace Main.Codewars
{
	public class SumStringsAsNumbers
	{
		public static string sumStrings(string a, string b)
		{
			BigInteger aInt;
			BigInteger bInt;

			BigInteger.TryParse(a, out aInt);
			BigInteger.TryParse(b, out bInt);

			return (aInt + bInt).ToString();
		}
	}
}
