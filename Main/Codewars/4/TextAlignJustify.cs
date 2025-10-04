// https://www.codewars.com/kata/537e18b6147aa838f600001b

using System.Text;

namespace Main.Codewars._4
{
	public class TextAlignJustify
	{
		public static string Justify(string str, int len)
		{
			var words = new Queue<string>(str.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));
			var lines = new List<string>();

			var line = new List<string>();

			while (words.Count > 0)
			{
				var word = words.Peek();

				var length = line.Sum(s => s.Length) + line.Count + word.Length;

				if (length <= len)
				{
					line.Add(words.Dequeue());
				}
				else
				{
					lines.Add(SpacesToDistribute(line, len, words.Count == 0));
					line.Clear();
				}
			}

			if (line.Count > 0)
				lines.Add(SpacesToDistribute(line, len, true));

			var res = string.Join("\n", lines);
			return res;
		}

		public static string SpacesToDistribute(List<string> words, int len, bool isLastLine)
		{
			if (isLastLine)
			{
				return string.Join(" ", words);
			}

			var lineLength = words.Sum(s => s.Length);
			var spacesToDistribute = len - lineLength;
			var gaps = words.Count - 1;
			if (gaps == 0)
			{
				return words[0];
			}

			var spaces = new int[gaps];
			for (int i = 0; i < spacesToDistribute; i++)
			{
				spaces[i % gaps]++;
			}

			var sb = new StringBuilder();
			for (int i = 0; i < words.Count; i++)
			{
				sb.Append(words[i]);
				if (i < spaces.Length)
				{
					sb.Append(' ', spaces[i]);
				}
			}

			return sb.ToString();
		}

		public static void TestAll()
		{
//			Console.WriteLine(Justify("123 45 6", 7) == "123  45\n6");
			Console.WriteLine(Justify("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sagittis dolor mauris, at elementum ligula tempor eget. In quis rhoncus nunc, at aliquet orci. Fusce at dolor sit amet felis suscipit tristique. Nam a imperdiet tellus. Nulla eu vestibulum urna. Vivamus tincidunt suscipit enim, nec ultrices nisi volutpat ac. Maecenas sit amet lacinia arcu, non dictum justo. Donec sed quam vel risus faucibus euismod. Suspendisse rhoncus rhoncus felis at fermentum. Donec lorem magna, ultricies a nunc sit amet, blandit fringilla nunc. In vestibulum velit ac felis rhoncus pellentesque. Mauris at tellus enim. Aliquam eleifend tempus dapibus. Pellentesque commodo, nisi sit amet hendrerit fringilla, ante odio porta lacus, ut elementum justo nulla et dolor.", 15));
			//Console.WriteLine(Justify("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sagittis dolor mauris, at elementum ligula tempor eget. In quis rhoncus nunc, at aliquet orci. Fusce at dolor sit amet felis suscipit tristique. Nam a imperdiet tellus. Nulla eu vestibulum urna. Vivamus tincidunt suscipit enim, nec ultrices nisi volutpat ac. Maecenas sit amet lacinia arcu, non dictum justo. Donec sed quam vel risus faucibus euismod. Suspendisse rhoncus rhoncus felis at fermentum. Donec lorem magna, ultricies a nunc sit amet, blandit fringilla nunc. In vestibulum velit ac felis rhoncus pellentesque. Mauris at tellus enim. Aliquam eleifend tempus dapibus. Pellentesque commodo, nisi sit amet hendrerit fringilla, ante odio porta lacus, ut elementum justo nulla et dolor.", 30) == "Lorem  ipsum  dolor  sit amet,\r\nconsectetur  adipiscing  elit.\r\nVestibulum    sagittis   dolor\r\nmauris,  at  elementum  ligula\r\ntempor  eget.  In quis rhoncus\r\nnunc,  at  aliquet orci. Fusce\r\nat   dolor   sit   amet  felis\r\nsuscipit   tristique.   Nam  a\r\nimperdiet   tellus.  Nulla  eu\r\nvestibulum    urna.    Vivamus\r\ntincidunt  suscipit  enim, nec\r\nultrices   nisi  volutpat  ac.\r\nMaecenas   sit   amet  lacinia\r\narcu,  non dictum justo. Donec\r\nsed  quam  vel  risus faucibus\r\neuismod.  Suspendisse  rhoncus\r\nrhoncus  felis  at  fermentum.\r\nDonec lorem magna, ultricies a\r\nnunc    sit    amet,   blandit\r\nfringilla  nunc. In vestibulum\r\nvelit    ac    felis   rhoncus\r\npellentesque. Mauris at tellus\r\nenim.  Aliquam eleifend tempus\r\ndapibus. Pellentesque commodo,\r\nnisi    sit   amet   hendrerit\r\nfringilla,   ante  odio  porta\r\nlacus,   ut   elementum  justo\r\nnulla et dolor.");
			/*Console.WriteLine(Justify("This is an example of text justification.", 16) == "This    is    an\nexample  of text\njustification.  ");
			Console.WriteLine(Justify("What must be acknowledgment shall be", 16) == "What   must   be\nacknowledgment  \nshall be        ");
			Console.WriteLine(Justify("Science is what we understand well enough to explain to a computer. Art is everything else we do", 20) ==
				"Science  is  what we\nunderstand      well\nenough to explain to\na  computer.  Art is\neverything  else  we\ndo                  ");*/
		}
	}
}
