﻿//https://www.codewars.com/kata/54b724efac3d5402db00065e

using System.Text;

namespace Main.Codewars
{
	public class MorseCodeDecoder1
	{
		private const string SPACE = " ";
		private const string SPACE_LONG = "   ";

		public static string Decode(string morseCode)
		{
			if (string.IsNullOrEmpty(morseCode))
				return string.Empty;

			var result = new StringBuilder();

			foreach (var word in morseCode.Split(SPACE_LONG, StringSplitOptions.RemoveEmptyEntries))
			{
				foreach (var symbol in word.Split(SPACE, StringSplitOptions.RemoveEmptyEntries))
				{
					result.Append(MorseCode.Get(symbol));
				}
				result.Append(SPACE);
			}

			return result.ToString().Trim(' ');
		}

		public static void TestAll()
		{
			Test(".... . -.--   .--- ..- -.. .", "HEY JUDE");
		}

		private static void Test(string morseCode, string expected)
		{
			var res = Decode(morseCode);
			if (res == expected)
				Console.WriteLine("Ok");
			else
				Console.WriteLine("Error");
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
			_codes.Add("---", "O");

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

			_codes.Add(".-.-.-", ",");
		}

		public static string Get(string morseCode)
		{
			return _codes[morseCode];
		}
	}
}
