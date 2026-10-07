using Game.scripts.weapons;
using UnityEngine;

namespace Game.scripts
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField] private GameObject weaponGo;
        [SerializeField] private WeaponStrategyBase _currentStrategy;
        private WeaponDataSO _weaponData;

        private void Start()
        {
            //Inputs.InputReader.OnAttack += Attack;
            _currentStrategy = GetComponentInChildren<SwordAttack>();
            _currentStrategy.Equip();
            
            Inputs.InputReader.OnTakeWeponInHand += Inputs_OnTakeWeaponInHand;
        }

        private void Inputs_OnTakeWeaponInHand(WeaponNumber wn)
        {
            if (Inputs.InputReader.ConsumeAction(InputActionType.TakeWeapon))
            {
                switch (wn)
                {
                    case WeaponNumber.First:
                        //_weaponData = CharacterStats.Stats.WeaponData.attackRate1
                        _currentStrategy?.Unequip();
                        _currentStrategy = GetComponent<SwordAttack>();
                        _currentStrategy?.Equip();
                        break;
                    /*case WeaponNumber.Second:
                        _currentStrategy = */
                }
            }
  
        }
        
        /*public void Inputs_OnTakeWeaponInHand(WeaponDataSO weaponData, IWeaponStrategy weaponStrategy)
        {
            
            if (_weaponData == weaponData) return;
            _weaponData = weaponData;
            
            if (weaponStrategy != null && _currentStrategy == weaponStrategy) return;
            
            _currentStrategy.Unequip();
            _currentStrategy = weaponStrategy;
            _currentStrategy?.Equip();
            
        }*/
        
        /*public void Attack()
        {
            if (_currentStrategy == null) return;
            
            _currentStrategy.AttackBase(weaponGo);
        }*/

        private void OnDisable()
        {
            //Inputs.InputReader.OnAttack -= Attack;
            Inputs.InputReader.OnTakeWeponInHand -= Inputs_OnTakeWeaponInHand;
        }
    }
}