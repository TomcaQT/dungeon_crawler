using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int Number;
    
    public Vector2Int Size;
    public RoomShape RoomShape = RoomShape.Square;
    
    //TO SPAWN COUNT
    public int EnemyCount;
    public int BoostCount;
    
    //FLAGS 0 - BossRoom 1- Shop
    public int RoomFlags = 0x0;

    public bool IsBossRoom => (RoomFlags & 0x1) != 0;
    public bool IsShopRoom=> (RoomFlags & 0x01) != 0;

    
    
}
