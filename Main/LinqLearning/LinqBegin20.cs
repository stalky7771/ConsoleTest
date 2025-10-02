namespace Main.LinqLearning;

public partial class LinqBegin
{
	private void LinqBegin1()
	{
		var numbers = new List<int>() { -12, -10, -2, 0, 1, 3, 5, 8, 11, 14 };

		int? firstPositive = numbers.FirstOrDefault(n => n > 0);

		int? lastNegative = numbers.LastOrDefault(n => n < 0);

		Console.WriteLine("Первый положительный элемент: " +
			(firstPositive.HasValue ? firstPositive.ToString() : "не найден"));

		Console.WriteLine("Последний отрицательный элемент: " +
			(lastNegative.HasValue ? lastNegative.ToString() : "не найден"));
	}

	private void LinqBegin2()
	{
		int D = 7;
		int[] numbers = { -17, 23, 37, -47, 15, 27, -77 };

		int result = numbers
			.Where(n => n > 0 && n % 10 == D)
			.FirstOrDefault();

		Console.WriteLine(result);
	}

	private void LinqBegin3()
	{
		int L = 16;
		string[] strings = { "12345", "abcde", "9abcd", "1xy", "45def", "abc1d", "777778" };
		
		string result = strings
			.Where(s => s.Length == L && s.Length > 0 && char.IsDigit(s[0]))
			.LastOrDefault()
			?? "Not found";

		Console.WriteLine(result);
	}

	private void LinqBegin4()
	{
		char C = 'x';
		string[] data = { "hello", "world", "box", "linux1", "tax1" };

		try
		{
			string result = data.Where(s => s.EndsWith(C.ToString())).Single();

			Console.WriteLine(result);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(data.Count(s => s.EndsWith(C.ToString())) == 0 ? "" : "Error");
		}
	}

	private void LinqBegin5()
	{
		char C = 'x';
		string[] A = { "x12345x", "xabcde", "9abcdx", "x1xyx", "45def", "abc1d", "x777778x" };
		var result = A.Count(s => s.Length > 1 && s.StartsWith(C) && s.EndsWith(C));
		Console.WriteLine(result);
	}

	private void LinqBegin6()
	{
		string[] strings = { "hello", "world", "C#", "LINQ", "" };

		int totalLength = strings.Sum(s => s.Length);

		Console.WriteLine("Сумма длин всех строк: " + totalLength);
	}

	private void LinqBegin7()
	{
		int[] numbers = new[] { -7, -4, -1, 1, 2, 3 };
		var negatives = numbers.Where(n => n < 0);
		var amount = negatives.Count();
		if (amount > 0)
			Console.WriteLine(negatives.Sum());
		else
			Console.WriteLine("00");
	}

	private void LinqBegin8()
	{
		int[] numbers = new[] { 1, 11, 22, 34, 107 };
		var twoDigits = numbers.Where(n => n > 9 && n < 100);
		if (twoDigits.Count() == 0)
		{
			Console.WriteLine("0");
			Console.WriteLine((float)0);
			return;
		}

		var count = twoDigits.Count();
		Console.WriteLine(count);
		Console.WriteLine((float)twoDigits.Sum() / count);
	}

	private void LinqBegin9()
	{
		var numbers = new[] { -7, -5, -1, 0, 11, 22, 34, 107 };
		var positives = numbers.Where(n => n > 0);
		int result = positives.Any() ? positives.Min() : 0;

		// Вывод результата
		Console.WriteLine(result);
	}

	private void LinqBegin10()
	{
		int L = 3;
		string[] strings = { "XTT", "CAB", "AAB", "ABBC", "AACX", "BAA", "ABA" };
		string result = strings
			.Where(s => s.Length == L)
			.DefaultIfEmpty("") // если нет строк длины L, возвращает пустую строку
			.Max(); // находит наибольшую строку

		// Вывод результата
		Console.WriteLine(result);
	}

	private void LinqBegin11()
	{
		string[] strings = { "Alpha", "Betta", "Gamma" };
		string result = strings.Aggregate("", (acc, s) => acc + s[0]);
		Console.WriteLine(result);
	}

	private void LinqBegin12()
	{
		int[] numbers = { 123, -47, 56, 19 };

		double result = numbers
			.Select(n => System.Math.Abs(n % 10))
			.Aggregate(1.0, (acc, n) => acc * n);

		Console.WriteLine(result);
	}

	private void LinqBegin13()
	{
		int i = 3;
		var result = Enumerable.Range(1, i).Sum(n => 1.0 / n);
		Console.WriteLine(result);
	}

	private void LinqBegin14()
	{
		int a = 3;
		int b = 5;
		var result = Enumerable.Range(a, b - a + 1).Average(n => n * n);
		Console.WriteLine(result);
	}

	private void LinqBegin15()
	{
		int i = 5;
		var result = Enumerable.Range(1, i).Aggregate(1.0, (acc, n) => acc * n);
		Console.WriteLine(result);
	}

	private void LinqBegin16()
	{
		var numbers = new[] { -7, -5, -1, 0, 11, 22, 34, 107 };
		var result = numbers.Where(n => n > 0).ToList();
		Console.WriteLine(result);
	}

	private void LinqBegin17()
	{
		int[] numbers = { 1, 3, 5, 3, 2, 5, 7, 1, 9, 7 };

		var result = numbers
			.Where(x => x % 2 != 0)		// только нечётные
			.Distinct()					// удаляет повторы, сохраняя порядок
			.ToList();					// преобразуем к списку

		Console.WriteLine("Уникальные нечётные числа в порядке появления:");
		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin18()
	{
		int[] numbers = { -1, -2, 3, 5, 3, -4, 5, 7, 1, 9, 7 };

		var result = numbers
			.Where(x => x < 0 && x % 2 == 0) // только нечётные
			.Reverse()
			.ToList();

		Console.WriteLine("Уникальные нечётные числа в порядке появления:");
		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin19()
	{
		int d = 5;
		int[] numbers = { 12, 25, 36, 45, 21, 14, 35, 15, -5, 25, 15 };

		var result1 = numbers.Where(x => x > 0 && x % 10 == d).ToArray();	// Положительные, оканчивающиеся на D
		var result2 = result1.Reverse().ToArray();							// Переворачиваем, чтобы сохранить последние вхождения
		var result3 = result2.Distinct().ToArray();							// Убираем дубликаты (сохраняются последние)
		var result4	= result3.Reverse().ToArray();							// Возвращаем исходный порядок

		Console.WriteLine("Результат:");
		Console.WriteLine(string.Join(", ", result4));
	}

	private void LinqBegin20()
	{
		int[] numbers = { 12, 25, 36, 45, 21, 14, 35, 15, -5, 25, 15 };

		var result = numbers.Where(n => n > 9 && n < 100).OrderBy(n => n);
		Console.WriteLine(string.Join(", ", result));
	}
}