using System.ComponentModel;
using UnityEngine;

namespace Items
{
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