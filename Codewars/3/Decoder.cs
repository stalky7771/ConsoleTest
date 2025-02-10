//https://www.codewars.com/kata/52cf02cd825aef67070008fa

using System.Numerics;
using System.Text;

namespace Main.Codewars
{
	public class Decoder
	{
		private const string SIMPLE = "!@#$%^&*()_+-";
		private const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,? ";
		private static readonly int SIZE = ALPHABET.Length;

		public static string Decode(string p_what)
		{
			var sb = new StringBuilder();
			var offset = 1;

			foreach (var s in p_what)
			{
				sb.Append(SIMPLE.Contains(s) ? s : ALPHABET.First(x => s == EncodeSymbol(x, offset)));
				offset++;
				offset %= SIZE;
			}

			return sb.ToString();
		}

		public static char EncodeSymbol(char c, int offset)
		{
			var i0 = ALPHABET.IndexOf(c) + 1;
			var rate = (decimal)BigInteger.Pow(2, offset);
			var i = rate * i0 - 1;
			i %= SIZE + 1;
			var result = ALPHABET[(int)i];
			return result;
		}

		public static string Encode(string p_what)
		{
			var sb = new StringBuilder();

			var offset = 1;

			foreach (var s in p_what)
			{
				if (!SIMPLE.Contains(s))
				{
					sb.Append(EncodeSymbol(s, offset));
				}
				else
				{
					
					sb.Append(s);
				}
				offset++;
				offset %= SIZE;
			}

			return sb.ToString();
		}

		public static void TestAll()
		{
			var list = new[]
			{
				new { strIn  = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
					  strOut = "bdhpF,82QsLirJejtNmzZKgnB3SwTyXG ?.6YIcflxVC5WE94UA1OoD70MkvRuPqHabdhpF,82QsLir" },

				new { strIn  = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
					  strOut = "dhpF,82QsLirJejtNmzZKgnB3SwTyXG ?.6YIcflxVC5WE94UA1OoD70MkvRuPqHabdhp" },

				new { strIn  = "!@#$%^&*()_+-",
					  strOut = "!@#$%^&*()_+-" },

				new { strIn  = "cccccccccccccccccc",
					  strOut = "flxVC5WE94UA1OoD70" },

				new { strIn  = "ddddddddddddddddddd",
					  strOut = "hpF,82QsLirJejtNmzZ" },

				new { strIn  = "Hello World!",
					  strOut = "atC5kcOuKAr!" },

				new { strIn  = "mid-century. The Venona intercepts contained",
					strOut = "zJF-CZXBFglEWVNXUR?CQWg0YUCVxhW6UCdQBu7fOaMW" },

			}.ToList();

			list.ForEach(i => TestEncode(i.strIn, i.strOut));
			list.ForEach(i => TestDecode(i.strOut, i.strIn));
		}

		public static void TestDecode(string p_what, string expected)
		{
			var decodeResult = Decode(p_what);
			var res = decodeResult == expected ? "OK" : "ERROR";
			Console.WriteLine($"{res} - {decodeResult} (decode)");
		}

		public static void TestEncode(string p_what, string expected)
		{
			var res = Encode(p_what) == expected ? "OK" : "ERROR";
			Console.WriteLine($"{res} - {p_what} (encode)");

		}
	}
}
