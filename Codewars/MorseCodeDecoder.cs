// https://www.codewars.com/kata/54b72c16cd7f5154e9000457

namespace Main.Codewars
{
	public class MorseCodeDecoder
	{
		public static string DecodeBits(string bits)
		{
			var cleanedBits = bits.Trim('0');
			var rate = GetRate();
			return cleanedBits
				.Replace(GetDelimiter(7, "0"), "   ")
				.Replace(GetDelimiter(3, "0"), " ")
				.Replace(GetDelimiter(3, "1"), "-")
				.Replace(GetDelimiter(1, "1"), ".")
				.Replace(GetDelimiter(1, "0"), "");

			string GetDelimiter(int len, string c) => Enumerable.Range(0, len * rate).Aggregate("", (acc, _) => acc + c);
			int GetRate() => GetLengths("0").Union(GetLengths("1")).Min();
			IEnumerable<int> GetLengths(string del) => cleanedBits.Split(del, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Length);
		}

		public static string DecodeMorse(string morseCode)
		{
			return morseCode
				.Split("   ")
				.Aggregate("", (res, word) => $"{res}{ConvertWord(word)} ")
				.Trim();

			string ConvertWord(string word) => word.Split(' ').Aggregate("", (wordRes, c) => wordRes + MorseCode.Get(c));
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

	public class MorseCode
	{
		private static readonly Dictionary<string, string> _codes = new Dictionary<string, string>();

		static MorseCode()
		{
			_codes.Add(".-", "A");
			_codes.Add("-...", "B");
			_codes.Add("-.-.", "C");
			_codes.Add("-..", "D");
			_codes.Add(".", "E");

			_codes.Add("..-.", "F");
			_codes.Add("--.", "G");
			_codes.Add("....", "H");
			_codes.Add("..", "I");
			_codes.Add(".---", "J");

			_codes.Add("-.-", "K");
			_codes.Add(".-..", "L");
			_codes.Add("--", "M");
			_codes.Add("-.", "N");
			_codes.Add("O", "---");

			_codes.Add(".--.", "P");
			_codes.Add("--.-", "Q");
			_codes.Add(".-.", "R");
			_codes.Add("...", "S");
			_codes.Add("-", "T");

			_codes.Add("..-", "U");
			_codes.Add("...-", "V");
			_codes.Add(".--", "W");
			_codes.Add("-..-", "X");
			_codes.Add("-.--", "Y");

			_codes.Add("--..", "Z");

			_codes.Add(".----", "1");
			_codes.Add("..---", "2");
			_codes.Add("...--", "3");
			_codes.Add("....-", "4");
			_codes.Add(".....", "5");

			_codes.Add("-....", "6");
			_codes.Add("--...", "7");
			_codes.Add("---..", "8");
			_codes.Add("----.", "9");
			_codes.Add("-----", "0");
		}

		public static string Get(string morseCode)
		{
			return _codes[morseCode];
		}
	}
}
