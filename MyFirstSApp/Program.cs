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

int charHealth = 0;
int charAttack = 0;
int charDefense = 0;
int charPotions = 0;

switch (classChoice)
{
    case 1:
        charHealth = 120; charAttack = 18; charDefense = 10; charPotions = 3;
        Console.WriteLine("Воин");
        break;
    case 2:
        charHealth = 80; charAttack = 25; charDefense = 4; charPotions = 5;
        Console.WriteLine("Маг");
        break;
    case 3:
        charHealth = 100; charAttack = 21; charDefense = 6; charPotions = 3;
        Console.WriteLine("Разбойник");
        break;
    default:
        Console.WriteLine("Ошибка: неизвестный класс.");
        break;
}

Console.WriteLine($"Здоровье: {charHealth}");
Console.WriteLine($"Атака: {charAttack}");
Console.WriteLine($"Защита: {charDefense}");
Console.WriteLine($"Зелья: {charPotions}");

if (charHealth > 0 && charPotions > 0)
{
    Console.WriteLine("Можно использовать зелье.");
}
else
{
    Console.WriteLine("Нельзя использовать зелье.");
}