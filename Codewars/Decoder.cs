using System.Numerics;
using System.Text;

namespace Main.Codewars
{
	public class Decoder
	{
		private const string SIMPLE = "!@#$%^&*()_+-";
		private const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,? ";
		private static readonly int SIZE = ALPHABET.Length + 1;

		public static string Decode(string p_what)
		{
			var sb = new StringBuilder();

			var offset = 1;

			foreach (var s in p_what)
			{
				if (!SIMPLE.Contains(s))
				{
					var i0 = ALPHABET.IndexOf(s) - 1;
					var rate = (decimal)BigInteger.Pow(2, offset);
					decimal k = 0;
					decimal k0 = 0;
					if (rate - i0 > SIZE)
					{
						k0 = k = System.Math.Truncate(rate / SIZE) * 2;
					}

					while (true)
					{
						if ((i0 + SIZE * k + 2) % rate == 0)
							break;
						
						k++;
					}

					Console.WriteLine($">>> k0 = {k0}, k = {k}, {sb.ToString()}");

					var i = (i0 + SIZE * k + 2) / rate - 1;
					i %= SIZE;
					sb.Append(ALPHABET[(int)i]);

					offset++;
					offset %= (SIZE - 1);
				}
				else
				{
					offset = 1;
					sb.Append(s);
				}
			}

			return sb.ToString();
		}

		public static string Encode(string p_what)
		{
			var sb = new StringBuilder();

			var offset = 1;

			foreach (var s in p_what)
			{
				if (!SIMPLE.Contains(s))
				{
					var i0 = ALPHABET.IndexOf(s) + 1;
					var rate = (decimal)BigInteger.Pow(2, offset);
					var i = rate * i0 - 1;
					i %= SIZE + 1;
					sb.Append(ALPHABET[(int)i]);

					offset++;
					offset %= SIZE;
				}
				else
				{
					offset = 1;
					sb.Append(s);
				}
			}

			return sb.ToString();
		}

		public static void TestAll()
		{
			var list = new[]
			{
				//new { strIn  = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
					//  strOut = "bdhpF,82QsLirJejtNmzZKgnB3SwTyXG ?.6YIcflxVC5WE94UA1OoD70MkvRuPqHabdhpF,82QsLir" },

				new { strIn  = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
					  strOut = "dhpF,82QsLirJejtNmzZKgnB3SwTyXG ?.6YIcflxVC5WE94UA1OoD70MkvRuPqHabdhp" },

				//new { strIn  = "!@#$%^&*()_+-",
				//	  strOut = "!@#$%^&*()_+-" },

				//new { strIn  = "cccccccccccccccccc",
				//	  strOut = "flxVC5WE94UA1OoD70" },

				//new { strIn  = "ddddddddddddddddddd",
				//	  strOut = "hpF,82QsLirJejtNmzZ" },

				//new { strIn  = "Hello World!",
				//	  strOut = "atC5kcOuKAr!" },

			}.ToList();

			//list.ForEach(i => TestEncode(i.strIn, i.strOut));
			list.ForEach(i => TestDecode(i.strOut, i.strIn));
		}

		public static void TestDecode(string p_what, string expected)
		{
			var res = Decode(p_what) == expected ? "OK" : "ERROR";
			Console.WriteLine($"{res} - {p_what} (decode)");
		}

		public static void TestEncode(string p_what, string expected)
		{
			var res = Encode(p_what) == expected ? "OK" : "ERROR";
			Console.WriteLine($"{res} - {p_what} (encode)");

		}
	}
}
