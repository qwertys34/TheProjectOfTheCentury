using System;
using Game.scripts.enemes;
using UnityEngine;

namespace Game.scripts.character.systems
{
    public class HealthSystem : MonoBehaviour
    {
        public int MaxHealth { get; private set; } = 4;
        public int CurrentHealth { get; private set; }
        public event Action OnDeath;
        public bool IsAlive { get; private set; }

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Enemy e))
            {
                TakeDamage(e.GetDamage());
            }
        }

        private void TakeDamage(int damage)
        {
            var health = CurrentHealth - damage;
            CurrentHealth = Mathf.Max(0, health);
            
            if (CurrentHealth == 0)
                OnDeath?.Invoke();
        }
    }
}