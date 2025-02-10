using System.Text;

namespace Main.Codewars._3
{
	public class RailFenceCipher
	{
		public static string Encode(string s, int n)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			var arr = new char[n][];
			for (var i = 0; i < n; i++)
			{
				arr[i] = new char[s.Length];
			}

			var delta = -1;
			var pointer = 0;

			for (var i = 0; i < s.Length; i++)
			{
				arr[pointer][i] = s[i];
				if (pointer == 0 || pointer == n - 1)
					delta *= -1;

				pointer += delta;
			}

			var sb = new StringBuilder();

			for (var i = 0; i < n; i++)
			{
				var tmp = arr[i].ToList();
				tmp.RemoveAll(x => x == '\0');
				sb.Append(new string(tmp.ToArray()));
			}

			return sb.ToString();
		}

		public static string Decode(string s, int n)
		{
			var arr = new char[n][];
			for (var i = 0; i < n; i++)
			{
				arr[i] = new char[s.Length];
			}

			var delta = -1;
			var pointer = 0;

			for (var i = 0; i < s.Length; i++)
			{
				arr[pointer][i] = 'x';
				if (pointer == 0 || pointer == n - 1)
					delta *= -1;

				pointer += delta;
			}

			var queue = new Queue<char>(s);
			for (int i = 0; i < n; i++)
			{
				for (var j = 0; j < s.Length; j++)
				{
					if (arr[i][j] == 'x')
					{
						arr[i][j] = queue.Dequeue();
					}
				}
			}

			delta = -1;
			pointer = 0;
			var sb = new StringBuilder();

			for (var i = 0; i < s.Length; i++)
			{
				sb.Append(arr[pointer][i]);
				if (pointer == 0 || pointer == n - 1)
					delta *= -1;

				pointer += delta;
			}

			return sb.ToString();
		}

		public static void EncodeSampleTests()
		{
			string[][] encodes =
			{
				new[] { "Hello, World!", "Hoo!el,Wrdl l" },    // 3 rails
				new[] { "WEAREDISCOVEREDFLEEATONCE", "WECRLTEERDSOEEFEAOCAIVDEN" },  // 3 rails
				new[] { "Hello, World!", "H !e,Wdloollr" },    // 4 rails
				new[] { "", "" }                               // 3 rails (even if...)
			};
			int[] rails = { 3, 3, 4, 3 };
			for (int i = 0; i < encodes.Length; i++)
			{
				var res = Encode(encodes[i][0], rails[i]) == encodes[i][1];
				if (res)
				{
					Console.WriteLine($"OK - {encodes[i][0]}");
				}
				else
				{
					Console.WriteLine($"ERROR - {encodes[i][0]}");
				}
			}
		}

		public static void DecodeSampleTests()
		{
			string[][] decodes =
			{
				new[] { "WECRLTEERDSOEEFEAOCAIVDEN", "WEAREDISCOVEREDFLEEATONCE" },    // 3 rails
				new[] { "H !e,Wdloollr", "Hello, World!" },    // 4 rails
				new[] { "", "" }                               // 3 rails (even if...)
			};
			int[] rails = { 3, 4, 3 };
			for (int i = 0; i < decodes.Length; i++)
			{
				var res = Decode(decodes[i][0], rails[i]) == decodes[i][1];
				if (res)
				{
					Console.WriteLine($"OK - {decodes[i][0]}");
				}
				else
				{
					Console.WriteLine($"ERROR - {decodes[i][0]}");
				}
			}
		}
	}
}
