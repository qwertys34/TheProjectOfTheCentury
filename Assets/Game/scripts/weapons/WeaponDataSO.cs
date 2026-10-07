using UnityEngine;

namespace Game.scripts
{
    [CreateAssetMenu]
    public class WeaponDataSO : ScriptableObject
    {
        public GameObject prefab1;
        public int damage1 = 1;
        public float attackRate1;
        
        public GameObject prefab2;
        public int damage2;
        public float attackRate2;
        
        public GameObject prefab3;
        public int damage3;
        public float attackRate3;
    }
}