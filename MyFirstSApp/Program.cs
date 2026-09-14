using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("          CHARACTER GENERATOR           ");
        Console.WriteLine("========================================");

        Console.Write("Введите имя: ");
        string name = Console.ReadLine();

        Console.Write("Введите расу: ");
        string race = Console.ReadLine();

        Console.Write("Введите класс: ");
        string charClass = Console.ReadLine();

        Console.Write("Введите возраст: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("          СОЗДАННЫЙ ПЕРСОНАЖ            ");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Имя: " + name);
        Console.WriteLine("Раса: " + race);
        Console.WriteLine("Класс: " + charClass);
        Console.WriteLine("Возраст: " + age);
    }
}