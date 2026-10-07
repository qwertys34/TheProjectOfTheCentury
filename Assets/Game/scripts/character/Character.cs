using Game.scripts.character.systems;
using Game.scripts.interfaces;
using UnityEngine;

namespace Game.scripts.character
{
    public class Character : MonoBehaviour
    {
        public static Character C { get; private set; }
        
        public MovementSystem Movement { get; private set; }
        public WeaponSystem Weapons { get; private set; }
        public HealthSystem Health { get; private set; }
        public InventorySystem Inventory { get; private set; }

        private void Awake()
        {
            if (C != null)
            {
                Destroy(gameObject);
                return;
            }

            C = this;
            DontDestroyOnLoad(C);
            
            //Stats = GetComponent<StatsComponent>();
            Movement = GetComponent<MovementSystem>();
            Health = GetComponent<HealthSystem>();
            Weapons = GetComponent<WeaponSystem>();
            Inventory = GetComponent<InventorySystem>();
            //Abilities = GetComponent<AbilitySystem>();
        }

        private void OnEnable()
        {
            Health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            Health.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            Movement.enabled = false;
            Weapons.enabled = false;
            //Abilities.enabled = false;
        }
        
        //public void ChangeWeapon(WeaponDataSO weapon, IWeaponStrategy strategy) => Weapons.Inputs_TakeFirstWeapon(weapon, strategy);
        public bool IsAlive() => Health.IsAlive;
        public bool IsMoving() => Movement.IsMoving;
        public Transform GetCharPos() => transform;
        public Transform GetWeaponPos() => transform;

        public bool HasActiveWeapon(out IWeaponStrategy weapon)
        {
            weapon = null;
            if (Inventory.Items.Count != 0)
            {
                weapon = Inventory.Items[0];
                return true;
            }

            return false;
        }
    }
}