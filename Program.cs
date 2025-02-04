using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//ЗАВДАННЯ 1
public static class StringExtensions
{
    // Інвертування рядка
    public static string ReverseString(this string input)
    {
        if (input == null) return null;
        return new string(input.Reverse().ToArray());
    }

    // Підрахунок кількості входжень символа у рядок
    public static int CountOccurrences(this string input, char symbol)
    {
        if (input == null) return 0;
        return input.Count(c => c == symbol);
    }
}

public static class ArrayExtensions
{
    // Підрахунок кількості входжень значення у масиві
    public static int CountOccurrences<T>(this T[] array, T value) where T : IEquatable<T>
    {
        if (array == null) return 0;
        return array.Count(item => item.Equals(value));
    }

    // Отримання масиву унікальних елементів
    public static T[] ToUniqueArray<T>(this T[] array)
    {
        if (array == null) return Array.Empty<T>();
        return array.Distinct().ToArray();
    }
}

//ЗАВДАННЯ 2

public class ExtendedDictionary<T, U, V> : IEnumerable<Tuple<T, U, V>>
{
    private Dictionary<T, Tuple<T, U, V>> elements = new();

    public void Add(T key, U value1, V value2)
    {
        if (elements.ContainsKey(key))
            throw new ArgumentException($"Key {key} already exists.", nameof(key));

        elements[key] = Tuple.Create(key, value1, value2);
    }

    public bool Remove(T key) => elements.Remove(key);

    public bool ContainsKey(T key) => elements.ContainsKey(key);

    public Tuple<T, U, V> this[T key] => elements.TryGetValue(key, out var element) ? element
        : throw new KeyNotFoundException($"Key {key} not found.");

    public int Count => elements.Count;

    public IEnumerator<Tuple<T, U, V>> GetEnumerator() => elements.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}





class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Перевірка роботи методів розширення класу String:");
        string text = "hello world";
        Console.WriteLine($"Інвертування: {text.ReverseString()}");
        Console.WriteLine($"Кількість входжень символу 'l': {text.CountOccurrences('l')}");

        Console.WriteLine("Перевірка роботи методів розширення одновимірних масивів:");
        int[] numbers = { 1, 2, 2, 3, 4, 4, 4, 5 };
        Console.WriteLine($"Кількість входжень символу 4: {numbers.CountOccurrences(4)}");
        Console.WriteLine($"Масив унікальних символів: {string.Join(", ", numbers.ToUniqueArray())}");
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Приклад роботи з словником:");
        var dictionary = new ExtendedDictionary<int, string, string>();
        dictionary.Add(1, "Аліса", "Менеджер");
        dictionary.Add(2, "Василь", "Розробник");
        dictionary.Add(3, "Роман", "Аналітик");

        Console.WriteLine($"Чи існує елемент який має ключ 2: {dictionary.ContainsKey(2)}");
        Console.WriteLine($"Чи існує елемент який має значення (Аліса, Менеджер): {dictionary.ContainsValue("Аліса", "Менеджер")}");
        Console.WriteLine($"Передати елемент з ключем 1: {dictionary[1].Value1}, {dictionary[1].Value2}");

        dictionary.Remove(2);
        Console.WriteLine($"Кількість елементів після видалення ключа 2: {dictionary.Count}");

        Console.WriteLine("Перечислення цих елементів: ");
        foreach (var element in dictionary)
        {
            Console.WriteLine($"Key: {element.Key}, Value1: {element.Value1}, Value2: {element.Value2}");
        }

    }
}
