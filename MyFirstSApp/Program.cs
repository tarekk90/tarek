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