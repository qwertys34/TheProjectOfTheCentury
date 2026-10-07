using Game.scripts.interfaces;
using UnityEngine;

namespace Game.scripts.weapons
{
    public abstract class WeaponStrategyBase : MonoBehaviour
    {
        private readonly Collider[] _hits = new Collider[16];

        protected bool TrySphere(Transform origin, out IDamagable obj, float radius, float distance)
        {
            obj = null;

            Vector3 center = origin.position + origin.forward * distance * 0.5f;
            int count = Physics.OverlapSphereNonAlloc(
                center, radius, _hits, ~0, QueryTriggerInteraction.Collide);

            for (int i = 0; i < count; i++)
            {
                if (_hits[i].transform.TryGetComponent(out IDamagable damagable))
                {
                    obj = damagable;
                    return true;
                }
            }
            return false;
        }

        protected bool TryRayHit(Transform tr, out IDamagable obj, int damage)
        {
            obj = null;
            var ray = new Ray(tr.position, tr.forward);
            if (Physics.Raycast(ray, out var hit, 80) &&
                hit.transform.TryGetComponent(out IDamagable damagable))
            {
                obj = damagable;
                return true;
            }
            return false;
        }

        protected abstract void AttackRight(Transform tr, WeaponDataSO data);
        protected abstract void AttackLeft(Transform tr, int damage);
        public abstract void Equip();
        public abstract void Unequip();
    }
}