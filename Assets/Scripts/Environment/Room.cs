using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomEntity { NONE=0, SPACE, ENEMY, START, END, BOOST}

public class Room
{
    public int Number;
    
    public Vector2Int Size;
    public RoomShape Shape = RoomShape.Square;
    
    //TO SPAWN COUNT
    public int EnemyCount;
    public int BoostCount;
    //OR
    public Grid<RoomEntity> Grid;
    
    //FLAGS 0 - BossRoom 1- Shop
    public int RoomFlags = 0x0;

    public bool IsBossRoom => (RoomFlags & 0x1) != 0;
    public bool IsShopRoom=> (RoomFlags & 0x01) != 0;

    
    
}
