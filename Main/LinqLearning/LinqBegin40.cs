namespace Main.LinqLearning;

using System;

public partial class LinqBegin
{
	private void LinqBegin21()
	{
		var strings = new List<string>
		{
			"DOG", "CAT", "APPLE", "ZEBRA", "ZEBRB", "ANT", "BAT", "ALPHA", "OMEGA", "AAA"
		};

		var sorted = strings
			.OrderBy(s => s.Length)		// Сначала по возрастанию длины
			.ThenByDescending(s => s)	// Потом по убыванию лексикографически
			.ToList();

		Console.WriteLine(string.Join("\n", sorted));
	}

	private void LinqBegin22()
	{
		int K = 4;
		var A = new List<string>
		{
			"A333", "A111", "A222", "1234", "WORLD", "C0D3", "Z9", "A9", "AB9", "TEST7", "9999"
		};

		var result = A
			.Where(s => s.Length == K && char.IsDigit(s[^1])) // ^1 — последний символ
			.OrderBy(s => s)
			.ToList();

		Console.WriteLine(string.Join("\n", result));
	}

	private void LinqBegin23()
	{
		int K = 3;
		int[] numbers = { 12, 25, 36, 45, 21, 14, 35, 15, 25, 15 };

		var result = numbers
			.Skip(K - 1)	// начиная с элемента с порядковым номером K
			.Where(x => Math.Abs(x) >= 10 && Math.Abs(x) <= 99 && x % 2 != 0)	// нечетные двузначные
			.OrderByDescending(x => x)	// по убыванию
			.ToList();
		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin24()
	{
		int K = 4;
		string[] strings = { "Apple", "banana", "Cat", "Dog", "elephant", "Fox", "Giraffe" };
		var result = strings
			.Take(K - 1)
			.Where(s => s.Length % 2 != 0 && char.IsUpper(s, 0) && char.IsLetter(s, 0))
			.OrderByDescending(s => s);

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin25()
	{
		int K1 = 3;
		int K2 = 7;
		int[] numbers = { 1, 2, 3, -4, 5, 6, 7, 8, 9, 10 };
		var result = numbers.Skip(K1 - 1).Take(K2 - K1 + 1).ToList().Where(n => n > 0).Sum();

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin26()
	{
		string[] strings = { "Apple", "Banana", "Cat", "Dog", "Elephant", "Fox", "Giraffe" };
		int K1 = 2;
		int K2 = 4;
		int N = strings.Length;

		int startIdx = K1 - 1;
		int endIdx = K2 - 1;

		var lengths = strings
			.Where((str, index) => index < startIdx || index > endIdx)	// исключаем от K1 до K2 включительно
			.Select(s => s.Length);	// длины строк

		double average = lengths.Average();

		Console.WriteLine($"Среднее арифметическое длин: {average:F2}");
	}

	private void LinqBegin27()
	{
		int D = 10;
		int[] numbers = { 3, 5, 9, 11, -7, 12, 15, 8, 7, 13 };

		// Найдём индекс первого элемента, большего D
		int index = Array.FindIndex(numbers, x => x > D);

		// Получим подпоследовательность начиная с этого элемента
		var result = numbers
			.Skip(index)
			.Where(x => x > 0 && x % 2 != 0)
			.Reverse()
			.ToList();

		// Вывод результата
		Console.WriteLine("Результат: " + string.Join(", ", result));
	}

	private void LinqBegin28()
	{
		int L = 3;

		var strings = new List<string> { "123A", "45Z", "789", "HELLO", "AB1", "Z9", "LONGSTRING", "A1", "B2" };

		// Найти индекс первого элемента, длина которого превышает L
		int index = strings.FindIndex(s => s.Length > L);

		// Выбор строк до найденного элемента, которые оканчиваются буквой
		var result = strings
			.Take(index)
			.Where(s => char.IsLetter(s[^1])) // Последний символ - буква
			.OrderByDescending(s => s.Length)
			.ThenBy(s => s)
			.ToList();

		// Вывод результата
		foreach (var str in result)
		{
			Console.WriteLine(str);
		}
	}

	private void LinqBegin29()
	{
		int D = 10;
		int K = 3;
		int[] A = { 2, 4, 6, 12, 8, 10, 14, 16, 6 };

		// Найти индекс первого элемента, большего D
		int indexGreaterThanD = Array.FindIndex(A, x => x > D);
		if (indexGreaterThanD == -1)
			indexGreaterThanD = A.Length; // если таких нет, берем все

		// Первый фрагмент — до первого элемента > D (не включая его)
		var firstPart = A.Take(indexGreaterThanD);

		// Второй фрагмент — начиная с элемента с номером K (включая его)
		var secondPart = A.Skip(K);

		// Объединение, удаление дубликатов и сортировка по убыванию
		var result = firstPart
			.Union(secondPart)
			.OrderByDescending(x => x);

		// Вывод результата
		Console.WriteLine("Результат: " + string.Join(", ", result));
	}

	private void LinqBegin30()
	{
		int K = 3;
		int[] A = { 1, 4, 6, 8, 10, 3, 4, 12, 14 };

		// Первый фрагмент: все чётные числа
		var evenElements = A.Where(x => x % 2 == 0);

		// Второй фрагмент: элементы с индексами > K
		var indexGreaterThanK = A
			.Select((value, index) => new { value, index })
			.Where(item => item.index > K)
			.Select(item => item.value);

		// Теоретико-множественная разность (без повторов)
		var result = evenElements
			.Except(indexGreaterThanK)
			.Distinct();

		// Вывод результата
		Console.WriteLine(string.Join(", ", result));

		int[] A1 = { 1, 2, 3, 4, 5 };
		int[] B1 = { 3, 4, 5, 6, 7 };

		var result1 = A1.Intersect(B1);

		Console.WriteLine(string.Join(", ", result1));
	}

	private void LinqBegin31() { /* not relevant */ }

	private void LinqBegin32()
	{
		var strings = new List<string> { "123A", "45Z", "789", "HELLO", "AB1", "Z9", "LONGSTRING", "A1", "B2" };
		var result = strings.Select(s => s[0]).Reverse();

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin33()
	{
		int[] numbers = { -2, 4, 6, 121, -871, 151, 143, 16, 6 };
		var result = numbers
			.Where(n => n > 0)
			.Select(n => n % 10)
			.Distinct();
		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin34()
	{
		int[] numbers = { 1, 41, 7, 52, 122, 872, 152 };
		var result = numbers
			.Where(n => n % 2 != 0)
			.Select(n => n.ToString())
			.OrderBy(s => s);
		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin35()
	{
		int[] numbers = { 1, 41, -7, 52, 122, 872, 152 };
		var result = numbers
			.Select((value, index) => value * (index + 1))
			.Where(n => Math.Abs(n) > 9 && Math.Abs(n) < 100)
			.Reverse();

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin36() { /* not relevant */ }

	private void LinqBegin37() { /* not relevant */ }
	
	private void LinqBegin38() { /* not relevant */ }

	private void LinqBegin39()
	{
		string[] strings = { "abc123", "d45e", "9z0", "hello" };

		var result = strings
			.SelectMany(str => str)        // превращаем каждую строку в символы
			.Where(ch => char.IsDigit(ch)) // оставляем только цифры
			.ToList();                     // можно ToList или ToArray

		Console.WriteLine(string.Join(", ", result));
	}

	private void LinqBegin40()
	{
		int K = 4;
		string[] strings = { "abc", "d45A", "9z0A", "ho" };

		var result = strings
			.Where(s => s.Length >= K)
			.SelectMany(str => str)
			.Reverse();

		Console.WriteLine(string.Join(", ", result));
	}
}