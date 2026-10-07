using System.Collections.Generic;
using Game.scripts.interfaces;
using UnityEngine;

namespace Game.scripts.character
{
    public class InventorySystem : MonoBehaviour
    {
        public List<IWeaponStrategy> Items;
        private int _indexCurrentItem = 0;
        
        /*public IWeaponStrategy GetItem()
        {
            
        }*/
    }
}