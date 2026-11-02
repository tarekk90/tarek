namespace DungeonGame.Models
{
    public class Player
    {
        public string Name;
        public HeroClass Class;
        public int Health;
        public int MaxHealth;
        public int Attack;
        public int Defense;
        public int Mana;
        public int MaxMana;
        public int Potions;
        public int Gold;
        public int Level;
        public int Experience;
        public bool IsDefending;
        public int AbilityCooldown;
        public int RageTurns;

        public Player(string name, HeroClass heroClass)
        {
            Name = name;
            Class = heroClass;
            Level = 1;
            Experience = 0;
            Potions = 2;
            Gold = 20;
            IsDefending = false;
            AbilityCooldown = 0;
            RageTurns = 0;

            switch (heroClass)
            {
                case HeroClass.Warrior:
                    MaxHealth = 140; Attack = 18; Defense = 8; MaxMana = 30;
                    break;
                case HeroClass.Mage:
                    MaxHealth = 90; Attack = 24; Defense = 3; MaxMana = 100;
                    break;
                case HeroClass.Rogue:
                    MaxHealth = 110; Attack = 21; Defense = 5; MaxMana = 60;
                    break;
                default:
                    MaxHealth = 100; Attack = 15; Defense = 5; MaxMana = 50;
                    break;
            }

            Health = MaxHealth;
            Mana = MaxMana;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public int TakeDamage(int damage)
        {
            int incoming = Math.Max(1, damage - Defense);
            if (IsDefending)
                incoming = Math.Max(1, incoming / 2);

            incoming = Math.Min(incoming, Health);
            Health -= incoming;
            return incoming;
        }

        public int TakeHit(int damage)
        {
            int incoming = Math.Max(0, damage);
            if (IsDefending)
                incoming = (incoming + 1) / 2;

            incoming = Math.Min(incoming, Health);
            Health -= incoming;
            return incoming;
        }

        public int Heal(int amount)
        {
            int before = Health;
            Health = Math.Min(MaxHealth, Health + Math.Max(0, amount));
            return Health - before;
        }

        public bool UsePotion()
        {
            if (Potions <= 0 || Health >= MaxHealth)
                return false;

            Potions--;
            Heal(40);
            return true;
        }

        public void AddGold(int amount)
        {
            Gold = Math.Max(0, Gold + amount);
        }

        public bool TrySpendGold(int amount)
        {
            if (amount < 0 || Gold < amount)
                return false;

            Gold -= amount;
            return true;
        }

        public int GetCurrentAttack()
        {
            return Attack + (RageTurns > 0 ? 8 : 0);
        }

        public bool CanUseAbility()
        {
            if (AbilityCooldown > 0)
                return false;

            return Class switch
            {
                HeroClass.Warrior => Mana >= 15,
                HeroClass.Mage => Mana >= 25,
                HeroClass.Rogue => Mana >= 20,
                _ => false
            };
        }

        public bool UseAbility(Enemy enemy)
        {
            if (enemy == null || !enemy.IsAlive() || !CanUseAbility())
                return false;

            switch (Class)
            {
                case HeroClass.Warrior:
                    Mana -= 15;
                    RageTurns = 3;
                    enemy.TakeHit(GetCurrentAttack() + 8);
                    break;

                case HeroClass.Mage:
                    Mana -= 25;
                    enemy.TakeHit(Attack * 2);
                    break;

                case HeroClass.Rogue:
                    Mana -= 20;
                    enemy.TakeHit(Attack + 12);
                    enemy.ApplyPoison(3, 5);
                    break;
            }

            AbilityCooldown = 2;
            return true;
        }

        public void EndTurn()
        {
            if (AbilityCooldown > 0)
                AbilityCooldown--;

            if (RageTurns > 0)
                RageTurns--;

            Mana = Math.Min(MaxMana, Mana + 5);
            IsDefending = false;
        }

        public void ResetBattleState()
        {
            IsDefending = false;
            AbilityCooldown = 0;
            RageTurns = 0;
        }

        public void AddExperience(int amount)
        {
            Experience += Math.Max(0, amount);
            while (Experience >= Level * 100)
            {
                Experience -= Level * 100;
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Level++;
            MaxHealth += 15;
            Attack += 3;
            Defense += 1;
            MaxMana += 10;
            Health = MaxHealth;
            Mana = MaxMana;
        }
    }
}
