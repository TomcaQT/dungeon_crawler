using UnityEngine;

public enum RoomEntity { None=0, Space, Enemy, Start, End, Boost, Boss, EnemyWaypoint}


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
    public int RoomFlags = 0x00;

    public bool IsBossRoom => (RoomFlags & 0x1) != 0;
    public bool IsShopRoom=> (RoomFlags & 0x10) != 0;

    
    
}
