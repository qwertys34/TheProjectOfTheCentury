using System;
using Game.scripts.enemes;
using UnityEngine;

namespace Game.scripts.character.systems
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats;
        public event Action OnDeath;
        public bool IsAlive { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Enemy e))
            {
                TakeDamage(e.GetDamage());
            }
        }

        private void TakeDamage(int damage)
        {
            var health = characterStats.CurrentHealth - damage;
            characterStats.CurrentHealth = Mathf.Max(0, health);
            
            if (characterStats.CurrentHealth == 0)
                OnDeath?.Invoke();
        }
    }
}