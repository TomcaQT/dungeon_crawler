using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemData",menuName = "Items/BaseItem")]
    public class ItemData : ScriptableObject
    {
        public string Name;

        public string Description; // ????

        public ItemQuality ItemQuality;

        public GameObject ItemPrefab;
        
        public void OnPickUp()
        {
            
        }
    }
}
