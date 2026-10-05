using System;

class Program
{
    static void Main()
    {
        // Задание 1: Проверка состояния персонажа
        Console.Write("Введите текущее здоровье: ");
        int health = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите максимальное здоровье: ");
        int maxHealth = Convert.ToInt32(Console.ReadLine());

        double healthPercentage = ((double)health / maxHealth) * 100;

        if (healthPercentage > 70)
        {
            Console.WriteLine("Состояние персонажа: отличное");
        }
        else if (healthPercentage >= 30 && healthPercentage <= 70)
        {
            Console.WriteLine("Состояние персонажа: тяжёлое");
        }
        else
        {
            Console.WriteLine("Состояние персонажа: критическое");
        }

        // Задание 2: Проверка входных данных
        Console.Write("Введите класс (1 - Воин, 2 - Маг, 3 - Разбойник): ");
        int classChoice = Convert.ToInt32(Console.ReadLine());

        switch (classChoice)
        {
            case 1:
                Console.WriteLine("Воин");
                break;
            case 2:
                Console.WriteLine("Маг");
                break;
            case 3:
                Console.WriteLine("Разбойник");
                break;
            default:
                Console.WriteLine("Ошибка: неизвестный класс.");
                break;
        }

        // Задание 3: Выбор класса и характеристик
        Console.WriteLine("--- Характеристики персонажа ---");
        
        int attack = 0;
        int defense = 0;
        int potions = 0;

        switch (classChoice)
        {
            case 1:
                health = 120; maxHealth = 120; attack = 18; defense = 10; potions = 3;
                break;
            case 2:
                health = 80; maxHealth = 80; attack = 25; defense = 4; potions = 5;
                break;
            case 3:
                health = 100; maxHealth = 100; attack = 21; defense = 6; potions = 3;
                break;
            default:
                break;
        }

        Console.WriteLine($"Здоровье: {health}");
        Console.WriteLine($"Атака: {attack}");
        Console.WriteLine($"Защита: {defense}");
        Console.WriteLine($"Зелья: {potions}");

        // Задание 4: Использование зелья
        Console.WriteLine("--- Использование зелья ---");

        if (health >= maxHealth)
        {
            Console.WriteLine("Здоровье уже максимальное.");
        }
        else if (potions > 0)
        {
            health = health + 30;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            potions = potions - 1;

            Console.WriteLine("Вы использовали зелье.");
            Console.WriteLine("+30 здоровья");
            Console.WriteLine($"Здоровье: {health} / {maxHealth}");
            Console.WriteLine($"Зелий осталось: {potions}");
        }
        else
        {
            Console.WriteLine("У вас нет зелий.");
        }
    }
}