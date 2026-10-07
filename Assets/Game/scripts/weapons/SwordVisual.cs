using Game.scripts.character;
using Game.scripts.weapons;
using UnityEngine;

namespace Game.scripts
{
    [RequireComponent(typeof(Animator))]
    public class SwordVisual : MonoBehaviour
    {
        private static readonly int IsAttackRight = Animator.StringToHash("isAttackRight");
        private static readonly int IsAttackLeft = Animator.StringToHash("isAttackLeft");
        private static readonly int IsMove = Animator.StringToHash("isMove");
        private Transform _cameraTransform;
        [SerializeField] private SwordAttack swordAttack;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider collision;
        
        private void Start()
        {
            swordAttack.OnAttackRight += AnimateWeaponRight;
            swordAttack.OnAttackLeft += AnimateWeaponLeft;
        }

        private void Update()
        {
            animator.SetBool(IsMove, Character.C.IsMoving());
        }

        private void AnimateWeaponRight()
        {
            //if (Inputs.InputReader.ConsumeAction(InputActionType.Attack))
            Debug.Log("Attack right");
            animator.SetTrigger(IsAttackRight);
        }
        
        private void AnimateWeaponLeft()
        {
            //if (Inputs.InputReader.ConsumeAction(InputActionType.Attack))
            Debug.Log("Attack left");
            animator.SetTrigger(IsAttackLeft);
        }

        public void WeaponColliderState() => collision.enabled = !collision.enabled;
        
        private void OnDisable()
        {
            swordAttack.OnAttackRight -= AnimateWeaponRight;
            swordAttack.OnAttackLeft -= AnimateWeaponLeft;
        }
    }
}