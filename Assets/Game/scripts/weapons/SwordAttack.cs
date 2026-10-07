using System;
using UnityEngine;

namespace Game.scripts.weapons
{
    public class SwordAttack : WeaponStrategyBase
    {
        [SerializeField] private Transform attackPosition;
        public event Action OnAttackRight;
        public event Action OnAttackLeft;
        private int _damageRightAttack = 1;
        private int _damageLeftAttack = 1;
        
        protected override void AttackRight(Transform tr, WeaponDataSO data)
        {
            var successAttack = TrySphere(tr, out var target,0.7f, _damageRightAttack);
            if (successAttack)
            {
                target.TakeDamage(_damageRightAttack);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            
            Gizmos.DrawSphere(attackPosition.position, 0.7f);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(attackPosition.position, 0.7f);
        }

        protected override void AttackLeft(Transform tr, int damaga)
        {
            var successAttack = TrySphere(tr, out var target,0.7f, _damageLeftAttack);
            if (successAttack)
            {
                StartCoroutine(Utils.ActionWithPause(PauseTime.Short, () =>
                {
                    if (target is UnityEngine.Object uo && uo == null) return;
                    target.TakeDamage(_damageLeftAttack);
                }));
            }
        }
        
        public override void Equip()
        {
            Inputs.InputReader.OnRightAttack += Inputs_OnAttackRight;
            Inputs.InputReader.OnLeftAttack += Inputs_OnAttackLeft;
        }
        
        public override void Unequip()
        {
            Inputs.InputReader.OnRightAttack -= Inputs_OnAttackRight;
            Inputs.InputReader.OnLeftAttack -= Inputs_OnAttackLeft;
        }

        private void Inputs_OnAttackRight()
        {
            AttackRight(attackPosition, CharacterStats.Stats.WeaponData);
            OnAttackRight?.Invoke();
        }

        private void Inputs_OnAttackLeft()
        {
            AttackLeft(attackPosition, _damageLeftAttack);
            OnAttackLeft?.Invoke();
        }

        
    }
}
