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