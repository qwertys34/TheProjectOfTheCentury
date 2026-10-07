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
        public int health = 3;

        public int GetDamage() => enemyDamage;

        public void TakeDamage(int damage) 
        {
            Debug.Log("Enemy damaged");
            OnDamageTaken?.Invoke();
            var calculatedHealth = Mathf.Max(0, health - damage);
            health = calculatedHealth;
            Debug.Log("enemy health: " + health);
            if (health == 0)
            {
                SetDisable();
            }
        }

        private void SetDisable() => gameObject.SetActive(false);
    }
}