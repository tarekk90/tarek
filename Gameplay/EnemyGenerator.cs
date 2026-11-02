using DungeonGame.Models;

namespace DungeonGame.Gameplay
{
    public static class EnemyGenerator
    {
        private static readonly Random Random = new Random();

        public static Enemy CreateRandom()
        {
            string[] names = { "Goblin", "Skeleton", "Cave Spider", "Bandit" };
            int index = Random.Next(names.Length);

            Enemy enemy = new Enemy(
                names[index],
                Random.Next(35, 56),
                Random.Next(8, 15),
                Random.Next(1, 5),
                Random.Next(25, 41),
                Random.Next(8, 16));

            return enemy;
        }

        public static Enemy CreateStrong()
        {
            string[] names = { "Orc Warrior", "Dark Knight", "Troll" };
            string name = names[Random.Next(names.Length)];

            return new Enemy(
                name,
                Random.Next(75, 101),
                Random.Next(15, 23),
                Random.Next(4, 9),
                Random.Next(55, 81),
                Random.Next(18, 31));
        }

        public static Enemy CreateBoss()
        {
            return new Enemy("Dungeon Boss", 220, 27, 10, 200, 100);
        }
    }
}
