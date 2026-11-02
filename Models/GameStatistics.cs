namespace DungeonGame.Models
{
    public class GameStatistics
    {
        public int RoomsPassed;
        public int EnemiesDefeated;
        public int PotionsUsed;
        public int DamageDealt;
        public int DamageTaken;

        public void RegisterRoom()
        {
            RoomsPassed++;
        }

        public void RegisterDamageDealt(int damage)
        {
            DamageDealt += Math.Max(0, damage);
        }

        public void RegisterKill()
        {
            EnemiesDefeated++;
        }

        public void RegisterPotion()
        {
            PotionsUsed++;
        }

        public void RegisterDamageTaken(int damage)
        {
            DamageTaken += Math.Max(0, damage);
        }
    }
}
