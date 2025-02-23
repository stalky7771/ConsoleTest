//https://www.codewars.com/kata/59b7571bbf10a48c75000070/train/csharp

namespace Main.Codewars._6
{
	public class StringTops
	{
		public static string Tops(string msg)
		{
			if (msg == string.Empty)
				return string.Empty;

			var count = 1;
			var index = 1;
			
			List<char> tmpList = new();
			while (index < msg.Length)
			{
				tmpList.Add(msg[index]);
				index += ++count + ++count;
			}

			tmpList.Reverse();
			return new string(tmpList.ToArray());
		}

		public static void TestAll()
		{
			Test(String.Empty, String.Empty);
			Test("12", "2");
			Test("abcdefghijklmnopqrstuvwxyz12345", "3pgb");
			Test("abcdefghijklmnopqrstuvwxyz1236789ABCDEFGHIJKLMN", "M3pgb");
		}

		public static void Test(string msg, string expected)
		{
			var res = Tops(msg);
			if (res == expected)
				Console.WriteLine("OK");
			else
			{
				Console.WriteLine($"Error {msg}, exp {expected}, res {res}");
			}
		}
	}
}
