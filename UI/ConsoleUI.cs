using DungeonGame.Models;

namespace DungeonGame.UI
{
    public static class ConsoleUI
    {
        public static void ShowTitle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("             DUNGEON GAME");
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine("Explore rooms, defeat enemies and collect treasure!");
        }

        public static void ShowSeparator()
        {
            Console.WriteLine("----------------------------------------");
        }

        public static int ReadChoice(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                    return choice;

                Console.WriteLine($"Please enter a number from {min} to {max}.");
            }
        }

        public static HeroClass ChooseHeroClass()
        {
            Console.WriteLine("Choose your hero class:");
            Console.WriteLine("1. Warrior - high health and defense");
            Console.WriteLine("2. Mage    - powerful magic attacks");
            Console.WriteLine("3. Rogue   - fast attacks and poison");
            int choice = ReadChoice("Your choice: ", 1, 3);

            return choice switch
            {
                1 => HeroClass.Warrior,
                2 => HeroClass.Mage,
                _ => HeroClass.Rogue
            };
        }

        public static void ShowPlayer(Player player)
        {
            Console.WriteLine($"{player.Name} ({player.Class}) - Level {player.Level}");
            Console.WriteLine($"HP: {player.Health}/{player.MaxHealth} | Mana: {player.Mana}/{player.MaxMana}");
            Console.WriteLine($"Attack: {player.Attack} | Defense: {player.Defense}");
            Console.WriteLine($"Potions: {player.Potions} | Gold: {player.Gold} | EXP: {player.Experience}");
        }

        public static void ShowEnemy(Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name} - HP: {enemy.Health}/{enemy.MaxHealth}");
            Console.WriteLine($"Attack: {enemy.Attack} | Defense: {enemy.Defense}");
        }

        public static void ShowBattleStatus(Player player, Enemy enemy)
        {
            ShowSeparator();
            ShowPlayer(player);
            ShowEnemy(enemy);
            ShowSeparator();
        }

        public static void ShowBattleMenu(Player player)
        {
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            Console.WriteLine("3. Use ability");
            Console.WriteLine("4. Use potion");
            Console.WriteLine($"Ability available: {(player.CanUseAbility() ? "Yes" : "No")}");
        }

        public static void ShowAbilityUsed(Player player)
        {
            string ability = player.Class switch
            {
                HeroClass.Warrior => "Berserker Strike",
                HeroClass.Mage => "Fireball",
                HeroClass.Rogue => "Poison Strike",
                _ => "Special ability"
            };

            Console.WriteLine($"{player.Name} used {ability}!");
        }

        public static void ShowReward(Enemy enemy)
        {
            Console.WriteLine($"Victory! You received {enemy.ExperienceReward} EXP and {enemy.GoldReward} gold.");
        }

        public static void ShowStatistics(GameStatistics statistics)
        {
            ShowSeparator();
            Console.WriteLine("              GAME STATISTICS");
            ShowSeparator();
            Console.WriteLine($"Rooms passed: {statistics.RoomsPassed}");
            Console.WriteLine($"Enemies defeated: {statistics.EnemiesDefeated}");
            Console.WriteLine($"Potions used: {statistics.PotionsUsed}");
            Console.WriteLine($"Damage dealt: {statistics.DamageDealt}");
            Console.WriteLine($"Damage taken: {statistics.DamageTaken}");
            ShowSeparator();
        }
    }
}
