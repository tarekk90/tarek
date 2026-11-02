using DungeonGame.Gameplay;
using DungeonGame.Models;
using DungeonGame.UI;

namespace DungeonGame
{
    internal class Program
    {
        private static readonly Random Random = new Random();
        private static readonly GameStatistics Statistics = new GameStatistics();
        private static int Difficulty = 2;

        private static void Main(string[] args)
        {
            ConsoleUI.ShowTitle();
            Player player = CreateCharacter();
            ChooseDifficulty();

            Console.WriteLine();
            Console.WriteLine($"Welcome, {player.Name}! Your dungeon adventure begins.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            RunDungeon(player);
            ConsoleUI.ShowStatistics(Statistics);
            Console.WriteLine(player.IsAlive()
                ? "You completed the dungeon. Congratulations!"
                : "Game over. Your hero has fallen.");
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        private static Player CreateCharacter()
        {
            Console.Write("Enter your hero name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                name = "Hero";

            return new Player(name.Trim(), ConsoleUI.ChooseHeroClass());
        }

        private static void ChooseDifficulty()
        {
            Console.WriteLine();
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. Hard");
            Difficulty = ConsoleUI.ReadChoice("Your choice: ", 1, 3);
        }

        private static void RunDungeon(Player player)
        {
            const int totalRooms = 10;

            for (int roomNumber = 1; roomNumber <= totalRooms && player.IsAlive(); roomNumber++)
            {
                Console.Clear();
                ConsoleUI.ShowTitle();
                Console.WriteLine($"Room {roomNumber} of {totalRooms}");
                ConsoleUI.ShowSeparator();

                ProcessRoom(player, GenerateRoom(roomNumber));

                if (player.IsAlive() && roomNumber < totalRooms)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue to the next room...");
                    Console.ReadLine();
                }
            }
        }

        private static RoomType GenerateRoom(int roomNumber)
        {
            if (roomNumber == 10)
                return RoomType.StrongEnemy;

            int roll = Random.Next(100);
            if (roll < 35) return RoomType.Enemy;
            if (roll < 48) return RoomType.StrongEnemy;
            if (roll < 63) return RoomType.Trap;
            if (roll < 77) return RoomType.Treasure;
            if (roll < 89) return RoomType.Rest;
            return RoomType.Merchant;
        }

        private static void ProcessRoom(Player player, RoomType roomType)
        {
            Statistics.RegisterRoom();

            switch (roomType)
            {
                case RoomType.Enemy:
                    Console.WriteLine("A hostile creature appears!");
                    Enemy regularEnemy = EnemyGenerator.CreateRandom();
                    regularEnemy.ApplyDifficulty(Difficulty);
                    StartBattle(player, regularEnemy);
                    break;

                case RoomType.StrongEnemy:
                    Console.WriteLine("A powerful enemy blocks your path!");
                    Enemy strongEnemy = Statistics.RoomsPassed >= 10
                        ? EnemyGenerator.CreateBoss()
                        : EnemyGenerator.CreateStrong();
                    strongEnemy.ApplyDifficulty(Difficulty);
                    StartBattle(player, strongEnemy);
                    break;

                case RoomType.Trap:
                    ProcessTrap(player);
                    break;

                case RoomType.Treasure:
                    ProcessTreasure(player);
                    break;

                case RoomType.Rest:
                    ProcessRest(player);
                    break;

                case RoomType.Merchant:
                    ProcessMerchant(player);
                    break;
            }
        }

        private static void StartBattle(Player player, Enemy enemy)
        {
            player.ResetBattleState();
            Console.WriteLine($"Battle started: {player.Name} vs {enemy.Name}!");

            while (player.IsAlive() && enemy.IsAlive())
            {
                ConsoleUI.ShowBattleStatus(player, enemy);
                PlayerTurn(player, enemy);

                if (!enemy.IsAlive() || !player.IsAlive())
                    break;

                EnemyTurn(player, enemy);
            }

            if (player.IsAlive() && !enemy.IsAlive())
            {
                Statistics.RegisterKill();
                player.AddGold(enemy.GoldReward);
                player.AddExperience(enemy.ExperienceReward);
                ConsoleUI.ShowReward(enemy);
            }

            player.ResetBattleState();
        }

        private static void PlayerTurn(Player player, Enemy enemy)
        {
            while (true)
            {
                ConsoleUI.ShowBattleMenu(player);
                int choice = ConsoleUI.ReadChoice("Choose action: ", 1, 4);

                switch (choice)
                {
                    case 1:
                    {
                        int damage = enemy.TakeDamage(player.GetCurrentAttack());
                        Statistics.RegisterDamageDealt(damage);
                        Console.WriteLine($"You attack {enemy.Name} and deal {damage} damage.");
                        return;
                    }

                    case 2:
                        player.IsDefending = true;
                        Console.WriteLine("You take a defensive stance.");
                        return;

                    case 3:
                    {
                        int oldHealth = enemy.Health;
                        if (!player.UseAbility(enemy))
                        {
                            Console.WriteLine("You cannot use your ability now (cooldown or insufficient mana).");
                            break;
                        }

                        Statistics.RegisterDamageDealt(oldHealth - enemy.Health);
                        ConsoleUI.ShowAbilityUsed(player);
                        Console.WriteLine($"{enemy.Name} now has {enemy.Health} HP.");
                        return;
                    }

                    case 4:
                        if (player.UsePotion())
                        {
                            Statistics.RegisterPotion();
                            Console.WriteLine("You used a potion and restored health.");
                            return;
                        }

                        Console.WriteLine("You cannot use a potion now.");
                        break;
                }
            }
        }

        private static void EnemyTurn(Player player, Enemy enemy)
        {
            int poisonDamage = enemy.ProcessPoison();
            if (poisonDamage > 0)
                Console.WriteLine($"{enemy.Name} suffers {poisonDamage} poison damage.");

            if (enemy.IsAlive())
            {
                int damage = player.TakeDamage(enemy.Attack);
                Statistics.RegisterDamageTaken(damage);
                Console.WriteLine($"{enemy.Name} attacks you and deals {damage} damage.");
            }

            player.EndTurn();
        }

        private static void ProcessTrap(Player player)
        {
            int damage = Math.Max(1, Random.Next(12, 26) + (Difficulty - 2) * 5);
            int actualDamage = player.TakeHit(damage);
            Statistics.RegisterDamageTaken(actualDamage);
            Console.WriteLine($"A trap is triggered! You take {actualDamage} damage.");
        }

        private static void ProcessTreasure(Player player)
        {
            int gold = Random.Next(15, 41);
            player.AddGold(gold);
            Console.WriteLine($"You found a treasure chest containing {gold} gold!");
        }

        private static void ProcessRest(Player player)
        {
            int healed = player.Heal(30);
            player.Mana = Math.Min(player.MaxMana, player.Mana + 20);
            Console.WriteLine($"You rest and recover {healed} HP and some mana.");
        }

        private static void ProcessMerchant(Player player)
        {
            Console.WriteLine("A merchant offers supplies.");
            Console.WriteLine($"Your gold: {player.Gold}");
            Console.WriteLine("1. Buy a potion (15 gold)");
            Console.WriteLine("2. Restore up to 30 HP (10 gold)");
            Console.WriteLine("3. Leave");

            int choice = ConsoleUI.ReadChoice("Choose: ", 1, 3);
            switch (choice)
            {
                case 1:
                    if (player.TrySpendGold(15))
                    {
                        player.Potions++;
                        Console.WriteLine("You bought one potion.");
                    }
                    else
                        Console.WriteLine("Not enough gold.");
                    break;

                case 2:
                    if (player.Health >= player.MaxHealth)
                    {
                        Console.WriteLine("Your health is already full.");
                    }
                    else if (player.TrySpendGold(10))
                    {
                        int healed = player.Heal(30);
                        Console.WriteLine($"You restored {healed} HP.");
                    }
                    else
                        Console.WriteLine("Not enough gold.");
                    break;

                case 3:
                    Console.WriteLine("You leave the merchant.");
                    break;
            }
        }
    }
}
