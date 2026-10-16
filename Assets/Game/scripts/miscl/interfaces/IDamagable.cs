
using System;

namespace Game.scripts.interfaces
{
    public interface IDamagable
    {
        public event Action OnDamageTaken;

        public int GetMaxHealth();

        public int GetCurrentHealth();
        
        public void TakeDamage(int damage);
        
    }
}