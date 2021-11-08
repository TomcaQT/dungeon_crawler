using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    
    [SerializeField] private GameObject _wall;

    [SerializeField]
    private void Start()
    {
        BuildRoom(GetTestingRoom());
    }

    public Room GetTestingRoom()
    {
        Room room = new Room();
        room.Number = 1;
        room.Size = new Vector2Int(20, 10);
        room.RoomShape = RoomShape.Square;

        room.EnemyCount = 1;
        room.BoostCount = 1;

        room.RoomFlags = 0x00;
        
        return room;
    }
    
    
    public void BuildRoom(Room room)
    {
        var leftWall = Instantiate(_wall, _origin.position + new Vector3(-room.Size.x / 2, 0, 0), Quaternion.identity);
        leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x,room.Size.y,leftWall.transform.localScale.z);
        var rightWall = Instantiate(_wall, _origin.position + new Vector3(room.Size.x / 2, 0, 0), Quaternion.identity);
        rightWall.transform.localScale = new Vector3(rightWall.transform.localScale.x,room.Size.y,rightWall.transform.localScale.z);
        var upWall = Instantiate(_wall, _origin.position + new Vector3(0, room.Size.y / 2, 0), Quaternion.identity);
        upWall.transform.localScale = new Vector3(room.Size.x,upWall.transform.localScale.y,upWall.transform.localScale.z);
        var downWall = Instantiate(_wall, _origin.position + new Vector3(0, -room.Size.y / 2, 0), Quaternion.identity);
        downWall.transform.localScale = new Vector3(room.Size.x,downWall.transform.localScale.y,downWall.transform.localScale.z);
    }

}
