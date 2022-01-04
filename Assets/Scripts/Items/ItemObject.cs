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
        GetComponent<SpriteRenderer>().sprite = _item.ItemSprite;
    }

    public bool AutoCollect
    {
        get => false;
        set => throw new System.NotImplementedException();
    }
    
    public void OnPickUp(PlayerStats playerStats) => _item.OnPickup(playerStats);

}
