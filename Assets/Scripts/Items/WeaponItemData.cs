using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData",menuName = "Weapons/Weapon")]
public class WeaponItemData : ItemData
{

    public GameObject Bullet;
    
    
    
    public override void OnPickup(PlayerStats player)
    {
        player.Weapon = this;
    }
}