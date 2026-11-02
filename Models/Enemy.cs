namespace DungeonGame.Models
{
    public class Enemy
    {
        public string Name;
        public int Health;
        public int MaxHealth;
        public int Attack;
        public int Defense;
        public int ExperienceReward;
        public int GoldReward;
        public int PoisonTurns;
        public int PoisonDamage;

        public Enemy(string name, int health, int attack, int defense,
                     int experienceReward, int goldReward)
        {
            Name = name;
            MaxHealth = Math.Max(1, health);
            Health = MaxHealth;
            Attack = Math.Max(0, attack);
            Defense = Math.Max(0, defense);
            ExperienceReward = Math.Max(0, experienceReward);
            GoldReward = Math.Max(0, goldReward);
            PoisonTurns = 0;
            PoisonDamage = 0;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public int TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            actualDamage = Math.Min(actualDamage, Health);
            Health -= actualDamage;
            return actualDamage;
        }

        public int TakeHit(int damage)
        {
            int actualDamage = Math.Max(0, damage);
            actualDamage = Math.Min(actualDamage, Health);
            Health -= actualDamage;
            return actualDamage;
        }

        public void ApplyDifficulty(int difficulty)
        {
            // difficulty: 1 = Easy, 2 = Normal, 3 = Hard
            double multiplier = difficulty switch
            {
                1 => 0.85,
                3 => 1.25,
                _ => 1.0
            };

            MaxHealth = Math.Max(1, (int)Math.Round(MaxHealth * multiplier));
            Health = MaxHealth;
            Attack = Math.Max(1, (int)Math.Round(Attack * multiplier));
            Defense = Math.Max(0, (int)Math.Round(Defense * multiplier));
            ExperienceReward = Math.Max(1, (int)Math.Round(ExperienceReward * multiplier));
            GoldReward = Math.Max(1, (int)Math.Round(GoldReward * multiplier));
        }

        public void ApplyPoison(int turns, int damage)
        {
            PoisonTurns = Math.Max(PoisonTurns, Math.Max(0, turns));
            PoisonDamage = Math.Max(PoisonDamage, Math.Max(0, damage));
        }

        public int ProcessPoison()
        {
            if (PoisonTurns <= 0 || !IsAlive())
                return 0;

            int damage = TakeHit(PoisonDamage);
            PoisonTurns--;

            if (PoisonTurns == 0)
                PoisonDamage = 0;

            return damage;
        }
    }
}
