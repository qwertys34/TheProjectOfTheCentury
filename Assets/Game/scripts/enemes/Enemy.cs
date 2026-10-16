using System;
using Game.scripts.interfaces;
using UnityEngine;

namespace Game.scripts.enemes
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        public event Action OnDamageTaken;
        //public event Action OnDie;
        
        public int enemyDamage = 1;
        private int _maxHealth = 3;
        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public int GetDamage() => enemyDamage;

        public int GetMaxHealth() => _maxHealth;
        public int GetCurrentHealth() => _currentHealth;

        public void TakeDamage(int damage) 
        {
            Debug.Log("Enemy damaged");
            var calculatedHealth = Mathf.Max(0, _currentHealth - damage);
            _currentHealth = calculatedHealth;
            OnDamageTaken?.Invoke();
            Debug.Log("enemy health: " + _currentHealth);
            if (_currentHealth == 0)
            {
                SetDisable();
            }
        }

        private void SetDisable() => gameObject.SetActive(false);
    }
}