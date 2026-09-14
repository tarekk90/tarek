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

        Console.Write("Введите силу: ");
        int strength = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите ловкость: ");
        int agility = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите интеллект: ");
        int intellect = Convert.ToInt32(Console.ReadLine());

        // Задание 5: Расчеты
        int sum = strength + agility + intellect;
        int health = 100 + strength * 10;
        int attack = strength * 2 + agility;
        int mana = 50 + intellect * 5;

        // Задание 7: Дополнительная характеристика
        int combatRating = strength * 2 + agility * 2 + intellect;

        // Задание 8: Самостоятельное расширение (2-3 новые характеристики)
        int speed = agility * 2;
        int armor = strength + 10;
        int endurance = strength + agility;

        // Вывод информации о персонаже
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("          СОЗДАННЫЙ ПЕРСОНАЖ            ");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Имя: " + name);
        Console.WriteLine("Раса: " + race);
        Console.WriteLine("Класс: " + charClass);
        Console.WriteLine("Возраст: " + age);
        Console.WriteLine("Сила: " + strength);
        Console.WriteLine("Ловкость: " + agility);
        Console.WriteLine("Интеллект: " + intellect);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Сумма характеристик: " + sum);
        Console.WriteLine("Здоровье: " + health);
        Console.WriteLine("Атака: " + attack);
        Console.WriteLine("Мана: " + mana);
        Console.WriteLine("Боевой рейтинг: " + combatRating);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Скорость: " + speed);
        Console.WriteLine("Броня: " + armor);
        Console.WriteLine("Выносливость: " + endurance);
    }
}