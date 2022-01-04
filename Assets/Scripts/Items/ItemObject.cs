using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class ItemObject : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemData _item;

    public void Init(ItemData item)
    {
        _item = item;
        //TODO Change sprite
    }

    public bool AutoCollect { get; set; }
    
    public void OnPickUp(PlayerStats playerStats) => _item.OnPickup(playerStats);

}
