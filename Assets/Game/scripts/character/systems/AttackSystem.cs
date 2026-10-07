using Game.scripts.interfaces;
using UnityEngine;

namespace Game.scripts.character.systems
{
    public class AttackSystem : MonoBehaviour
    {
        public int damage = 1;

        public void ApplyDamage(IDamagable damagable)
        {
            damagable.TakeDamage(damage);
        }
    }
}