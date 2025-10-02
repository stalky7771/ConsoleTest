namespace Main.LinqLearning;

using System;

public partial class LinqBegin
{
	public void Test()
	{
		//LinqBegin51();
	}

	private void LinqBegin41()
	{
		int K = 3;
		string[] A = { "ABC.DEF", "GHI.JKL.MNOP", "XYZ.ABC" };

		var result = A
			.SelectMany(str => str.Split('.'))		// разбиваем по точкам
			.Where(word => word.Length == K)		// фильтруем по длине
			.OrderBy(word => word)					// сортируем лексикографически
			.ToList();

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin42()
	{
		string[] A = { "aBCDe", "FgHiJk", "LMnoP", "qRStUv" };

		var result = A
			.SelectMany((str, index) =>
				str.Where(ch =>
						(index % 2 == 0 && char.IsUpper(ch)) ||   // нечётные строки → заглавные
						(index % 2 == 1 && char.IsLower(ch))      // чётные строки → строчные
				)
			)
			.ToList();

		Console.WriteLine(string.Join("", result));
	}

	private void LinqBegin43() { /* not relevant */ }

	private void LinqBegin44()
	{
		int K1 = 10;
		int K2 = 20;

		int[] numbersA = { 5, 12, 25, 8, 15 };
		int[] numbersB = { 3, 18, 25, 7, 30 };

		var resA = numbersA.Where(n => n > K1);
		var resB = numbersB.Where(n => n < K2);

		var result = resA.Concat(resB).OrderBy(n => n);

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin45()
	{
		int L1 = 3;
		int L2 = 4;

		string[] stringsA = { "ABC", "A1", "XYZ", "1234", "HELLO" };
		string[] stringsB = { "QWER", "AB", "TEST", "ZZZZ", "X1" };

		var result = stringsA
			.Where(s => s.Length == L1)
			.Concat(stringsB.Where(s => s.Length == L2))
			.OrderByDescending(s => s);

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin46()
	{
		int[] A = { 12, 23, 49, 58 };
		int[] B = { 129, 33, 19, 78, 49 };

		var result = A.
			SelectMany(a => B.Where(b => a % 10 == b % 10)	// ищем совпадения по последней цифре
			.Select(b => $"{a}-{b}"));						// формируем строку

		Console.WriteLine("Результат:");
		foreach (var pair in result)
			Console.WriteLine(pair);
	}

	private void LinqBegin47()
	{
		int[] A = { 49, 23, 15 };
		int[] B = { 921, 345, 567, 912, 95 };

		var result = A.SelectMany(
			a => B
				.Where(b => (a % 10).ToString() == b.ToString()[0].ToString())	// последняя цифра A = первая цифра B
				.OrderBy(b => b.ToString())		// сортируем B как строки
				.Select(b => $"{a}:{b}")		// формируем "a:b"
		);

		foreach (var pair in result)
			Console.WriteLine(pair);
	}

	private void LinqBegin48() { /* not relevant */ }

	private void LinqBegin49() { /* not relevant */ }

	private void LinqBegin50()
	{
		int[] numbersA = { 1, 2, 3, 4 };
		int[] numbersB = { 10, 20, 30, 40 };

		//var result = numbersA.Zip(numbersB, (x, y) => x * y);

		string[] A = { "ABC", "XYZ", "HELLO" };
		string[] B = { "AXE", "APPLE", "X1", "HERO", "HI", "ZEBRA" };

		var result = A.GroupJoin(
			B,
			a => a[0],		// ключ для A — первая буква
			b => b[0],		// ключ для B — первая буква
			(a, groupB) => $"{a}:{groupB.Count()}"		// собираем строку "E:N"
		);

		foreach (var item in result)
			Console.WriteLine(item);
	}

	public class Student
	{
		public int id;
		public string name;
	}
}