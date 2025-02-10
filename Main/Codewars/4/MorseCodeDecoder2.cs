// https://www.codewars.com/kata/54b72c16cd7f5154e9000457

using System.Text;

namespace Main.Codewars
{
	public class MorseCodeDecoder2
	{
		public static string DecodeBits(string bits)
		{
			bits = bits.Trim('0');
			if (string.IsNullOrEmpty(bits) || bits.All('0'.Equals))
				return string.Empty;

			if (bits.All('1'.Equals))
			{
				return ".";
			}

			var timeUnitRate = GetTimeUnitRate(bits);
			if (timeUnitRate == 0)
				return string.Empty;

			var temp = bits.Split("1").Where(x => x != "").ToList();
			if (temp.Count == 0)
				return string.Empty;

			var wordSeparator = new string('0', 7 * timeUnitRate);
			var letterSeparator = new string('0', 3 * timeUnitRate);
			var charSeparator = new string('0', timeUnitRate);
			var dotSymbol = new string('1', timeUnitRate);

			var words = bits.Split(wordSeparator);

			var result = new StringBuilder();

			for (int i = 0; i < words.Length; i++)
			{
				var letters = words[i].Split(letterSeparator);
				foreach (var s in letters)
				{
					var characters = s.Split(charSeparator);
					foreach (var c in characters)
					{
						result.Append(c.Equals(dotSymbol) ? "." : "-");
					}
					result.Append(" ");
				}
				result.Append("  ");
			}

			return result.ToString().Trim(' ');
		}

		public static int GetTimeUnitRate(string bits)
		{
			var units = bits.Split("0").Where(x => x != "").ToList();
			if (units.Count == 0)
				return 0;

			var rate = units.OrderBy(x => x.Length).First().Length;

			units = bits.Split("1").Where(x => x != "").ToList();
			if (units.Count == 0)
				return 0;

			return System.Math.Min(units.OrderBy(x => x.Length).First().Length, rate);
		}

		public static string DecodeMorse(string morseCode)
		{
			if (string.IsNullOrEmpty(morseCode))
				return string.Empty;

			var result = new StringBuilder();

			var words = morseCode.Split("   ");
			for (int i = 0; i < words.Length; i++)
			{
				var symbols = words[i].Split(" ");
				foreach (var symbol in symbols)
				{
					result.Append(MorseCode.Get(symbol));
				}

				result.Append(" ");
			}

			return result.ToString().Trim(' ');
		}

		public static void Test()
		{
			var message = "1100110011001100000011000000111111001100111111001111110000000000000011001111110011111100111111000000110011001111110000001111110011001100000011";
			//var message = "1";
			//var message = "1110111";

			var result = DecodeBits(message);
			Console.WriteLine($"Expected: [.... . -.--   .--- ..- -.. .]");
			Console.WriteLine($"Result:   [{result}]");
			Console.WriteLine($"English:  [{DecodeMorse(result)}]");
		}
	}
}
