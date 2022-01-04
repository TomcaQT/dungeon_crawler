using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    
    
    
    public abstract class ItemData : ScriptableObject
    {
        public string Name;

        public string Description; // ????

        public ItemQuality ItemQuality;

        public GameObject ItemPrefab;

        public abstract void OnPickup(PlayerStats player);

    }

    [CreateAssetMenu(fileName = "BoostingItemData",menuName = "Items/BoostingItem")]
    public class BoostingItemData : ItemData
    {

        public string ToBoost;
        public float Amount;
        public override void OnPickup(PlayerStats player)
        {
            player.IncreaseStatOrResource(ToBoost,Amount);
        }
    }
    
}
