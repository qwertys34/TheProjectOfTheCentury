using UnityEngine;

namespace Game.scripts
{
    public class CharacterStats : MonoBehaviour // динамические данные игрока
    {
        public static CharacterStats Stats { get; private set; }

        [SerializeField] private GameObject activeWeaponRef;
        [SerializeField] private WeaponDataSO weaponData;
        [SerializeField] private Animator animator;
        private int _startHealth = 10;  
        
        // ВСЕ ИЗМЕНЯЕМЫЕ ПУБЛИЧНЫЕ ПОЛЯ ДЛЯ ЧТЕНИЯ
        public int CurrentHealth;
        public float CurrentDamage;
        public Transform ActiveWeaponPosition => activeWeaponRef.transform;
        public WeaponDataSO WeaponData => weaponData;
        public Animator WeaponAnimator => animator;
        //ublic GameObject WeaponPrefab => weaponData.prefab;
        
        private void Awake()
        {
            if (Stats != null) return;

            Stats = this;
            DontDestroyOnLoad(this);
        }

        public void EnableWeapon(bool activate) => activeWeaponRef.SetActive(activate);
        
        public void InitializeStartStats()
        {
            CurrentHealth = _startHealth;
            CurrentDamage = weaponData.damage1;
        }
    }
}