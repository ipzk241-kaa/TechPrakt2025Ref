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
public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; set; }
    public U Value1 { get; set; }
    public V Value2 { get; set; }

    public ExtendedDictionaryElement(T key, U value1, V value2)
    {
        Key = key;
        Value1 = value1;
        Value2 = value2;
    }
}

public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
{
    private List<ExtendedDictionaryElement<T, U, V>> elements = new();

    // Додавання елемента
    public void Add(T key, U value1, V value2)
    {
        if (elements.Any(e => e.Key.Equals(key)))
            throw new ArgumentException("Key already exists.");
        elements.Add(new ExtendedDictionaryElement<T, U, V>(key, value1, value2));
    }

    // Видалення елемента
    public bool Remove(T key)
    {
        return elements.RemoveAll(e => e.Key.Equals(key)) > 0;
    }

    // Перевірка наявності ключа
    public bool ContainsKey(T key)
    {
        return elements.Any(e => e.Key.Equals(key));
    }

    // Перевірка наявності значення
    public bool ContainsValue(U value1, V value2)
    {
        return elements.Any(e => e.Value1.Equals(value1) && e.Value2.Equals(value2));
    }

    // Повернення елемента за ключем
    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get => elements.FirstOrDefault(e => e.Key.Equals(key))
               ?? throw new KeyNotFoundException("Ключ не знайдено.");
    }

    // Кількість елементів
    public int Count => elements.Count;

    // Реалізація foreach для IEnumerable<T>
    public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
    {
        return elements.GetEnumerator();
    }

    // Реалізація foreach для IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
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
