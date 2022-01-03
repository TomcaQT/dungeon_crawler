using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private Transform _roomParent;

    private Camera _camera;
    
    [SerializeField] private GameObject _wall;


    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _endPortal;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private List<GameObject> _items;


    private void Awake()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        //BuildRoom(GetTestingRoom());
    }

    private Room GetTestingRoom()
    {
        Room room = new Room();
        room.Number = 1;
        room.Size = new Vector2Int(20, 10);
        room.Shape = RoomShape.Square;

        room.EnemyCount = 1;
        room.BoostCount = 1;

        room.RoomFlags = 0x00;
        
        return room;
    }
    
    
    public GameObject BuildRoom(Room room, out List<Enemy> enemies)
    {
        enemies = new List<Enemy>();
        GameObject end = null;
        if(_roomParent != null)
            ClearChilds(_roomParent);
        //_roomParent = Instantiate(new GameObject("Wall Parent"), Vector3.zero, Quaternion.identity).transform;
        SpawnWalls(room);

        for (int x = 0; x < room.Grid.Width; x++)
            for (int y = 0; y < room.Grid.Height; y++)
            {
                GameObject toSpawn = null;
                if (room.Grid.Get(x, y) == RoomEntity.ENEMY)
                    toSpawn = GetRandomFromList(_enemies);
                else if (room.Grid.Get(x, y) == RoomEntity.BOOST)
                    toSpawn = GetRandomFromList(_items);
                else if (room.Grid.Get(x, y) == RoomEntity.START)
                    _player.transform.position = new Vector3(x, y, 0);
                else if (room.Grid.Get(x, y) == RoomEntity.END)
                    toSpawn = _endPortal;

                GameObject spawned = null;
                if (toSpawn != null)
                    spawned = Instantiate(toSpawn, new Vector3(x, y, 0), Quaternion.identity,_roomParent);
                if (room.Grid.Get(x, y) == RoomEntity.ENEMY && spawned != null)
                    enemies.Add(spawned.GetComponent<Enemy>());
                if (room.Grid.Get(x, y) == RoomEntity.END && spawned != null)
                    end = spawned;


            }
        
        //Temp -> camera will follow player
        _camera.transform.position = new Vector3(room.Size.x / 2, room.Size.y / 2, -10);
        return end;
    }

    private void SpawnWalls(Room room)
    {
        var leftWall = Instantiate(_wall, new Vector3(-1, room.Size.y/2f, 0), Quaternion.identity, _roomParent);
        leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, room.Size.y+3, leftWall.transform.localScale.z);
        var rightWall = Instantiate(_wall, new Vector3(room.Size.x + 1 , room.Size.y/2f, 0), Quaternion.identity, _roomParent);
        rightWall.transform.localScale = new Vector3(rightWall.transform.localScale.x, room.Size.y+3, rightWall.transform.localScale.z);
        var upWall = Instantiate(_wall, new Vector3(room.Size.x/2f, room.Size.y + 1, 0), Quaternion.identity, _roomParent);
        upWall.transform.localScale = new Vector3(room.Size.x+3,upWall.transform.localScale.y, upWall.transform.localScale.z);
        var downWall = Instantiate(_wall, new Vector3(room.Size.x/2f, -1, 0), Quaternion.identity, _roomParent);
        downWall.transform.localScale = new Vector3(room.Size.x+3,downWall.transform.localScale.y, downWall.transform.localScale.z);
    }

    private T GetRandomFromList<T>(List<T> data) => data != null ? data[Random.Range(0, data.Count)] : default(T);
    
    private void ClearChilds(Transform parent)
    {
        foreach(Transform child in parent)
        {
            if(child.gameObject.name != "Background")
                Destroy(child.gameObject);
        }
    }

}
