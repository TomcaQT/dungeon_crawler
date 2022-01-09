using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoomGenerator _roomGenerator;
    private RoomBuilder _roomBuilder;

    private List<Enemy> _enemies;
    
    [SerializeField] private GameObject _endPoint;

    public int RoomNumber;
    
    private void Awake()
    {
        _roomGenerator = GameObject.Find("Room Generator").GetComponent<RoomGenerator>();
        _roomBuilder = GameObject.Find("Room Builder").GetComponent<RoomBuilder>();

        _enemies = new List<Enemy>();
    }

    private void Start()
    {
        NextRoom();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.R))
            BossRoom();
        if(Input.GetKeyDown(KeyCode.T))
            ShopRoom();
        #endif
    }

    private void BossRoom()
    {
        _endPoint = _roomBuilder.BuildRoom(_roomBuilder.GetBossRoom(),out _enemies);
        _enemies.ForEach(x => x.OnEnemyDeath += e_OnEnemyDeath);
        _endPoint.GetComponent<End>().OnRoomEnd += e_OnRoomEnd;
    }
    
    private void ShopRoom()
    {
        _endPoint = _roomBuilder.BuildRoom(_roomBuilder.GetShop(),out _enemies);
        //_enemies.ForEach(x => x.OnEnemyDeath += e_OnEnemyDeath);
        _endPoint.GetComponent<End>().OnRoomEnd += e_OnRoomEnd;
    }
    
    public void NextRoom()
    {
        RoomNumber++;
        if (RoomNumber % 5 == 0)
        {
            BossRoom();
            return;
        }

        if (RoomNumber % 5 == 4)
        {
            ShopRoom();
            return;
        }
        _endPoint = _roomBuilder.BuildRoom(_roomGenerator.GenerateNewRoom(RoomNumber),out _enemies);
        _enemies.ForEach(x => x.OnEnemyDeath += e_OnEnemyDeath);
        _endPoint.GetComponent<End>().OnRoomEnd += e_OnRoomEnd;
    }

    private void e_OnRoomEnd(object sender, EventArgs e)
    {
        if (_enemies.Count <= 0)
          NextRoom();
    }
    
    private void e_OnEnemyDeath(object sender, EventArgs e)
    {
        _enemies.Remove((Enemy) sender);
        if (_enemies.Count <= 0)
            _endPoint.SetActive(true);
    }
    
    
    
}
