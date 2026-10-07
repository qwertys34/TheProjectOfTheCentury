using UnityEngine;

namespace Game.scripts.interfaces
{
    public interface IWeaponStrategy
    {
        protected void AttackRight(Transform tr, WeaponDataSO data);
        void AttackLeft(Transform tr, int damage);
        void Equip();
        void Unequip();
    }
}