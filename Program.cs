using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Guard
{
    public static void ThrowIfNull<T>(T obj, string paramName)
    {
        if (obj == null) throw new ArgumentNullException(paramName);
    }
}
public static class CollectionExtensions
{
    public static int CountOccurrences<T>(this IEnumerable<T> collection, T value) where T : IEquatable<T>
    {
        Guard.ThrowIfNull(collection, nameof(collection));
        return collection.Count(item => item.Equals(value));
    }
}

public static class StringExtensions
{
    public static string ReverseString(this string input)
    {
        Guard.ThrowIfNull(input, nameof(input));
        return new string(input.Reverse().ToArray());
    }
}

public static class ArrayExtensions
{

    public static T[] ToUniqueArray<T>(this T[] array)
    {
        Guard.ThrowIfNull(array, nameof(array));
        return array.Distinct().ToArray();
    }
}

public class ExtendedDictionary<T, U, V> : IEnumerable<Tuple<T, U, V>>
{
    private readonly Dictionary<T, Tuple<T, U, V>> elements = new();
    
    public void Add(T key, U value1, V value2)
    {
        if (elements.ContainsKey(key))
            throw new ArgumentException($"Key {key} already exists.", nameof(key));

        elements[key] = Tuple.Create(key, value1, value2);
    }
    public bool Remove(T key) => elements.Remove(key);

    public bool ContainsKey(T key) => elements.ContainsKey(key);
    
    public Tuple<T, U, V> this[T key] => elements.TryGetValue(key, out var element)
        ? element
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

        TestStringExtensions();
        TestArrayExtensions();
        TestExtendedDictionary();
    }

    /// <summary>
    /// Тестує розширення для роботи з рядками.
    /// </summary>
    static void TestStringExtensions()
    {
        string text = "hello world";
        Console.WriteLine($"Інвертування: {text.ReverseString()}");
        Console.WriteLine($"Кількість входжень символу 'l': {text.CountOccurrences('l')}");
    }

    /// <summary>
    /// Тестує розширення для роботи з масивами.
    /// </summary>
    static void TestArrayExtensions()
    {
        int[] numbers = { 1, 2, 2, 3, 4, 4, 4, 5 };
        Console.WriteLine($"Кількість входжень числа 4: {numbers.CountOccurrences(4)}");
        Console.WriteLine($"Масив унікальних елементів: {string.Join(", ", numbers.ToUniqueArray())}");
    }

    /// <summary>
    /// Тестує роботу розширеного словника.
    /// </summary>
    static void TestExtendedDictionary()
    {
        var dictionary = new ExtendedDictionary<int, string, string>();
        dictionary.Add(1, "Аліса", "Менеджер");
        dictionary.Add(2, "Василь", "Розробник");
        dictionary.Add(3, "Роман", "Аналітик");

        Console.WriteLine($"Чи існує ключ 2: {dictionary.ContainsKey(2)}");
        Console.WriteLine($"Значення для ключа 1: {dictionary[1]}");

        dictionary.Remove(2);
        Console.WriteLine($"Кількість елементів після видалення ключа 2: {dictionary.Count}");
    }
}
