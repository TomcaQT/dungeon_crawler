using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoomGenerator _roomGenerator;
    private RoomBuilder _roomBuilder;

    private List<Enemy> _enemies;
    
    private void Awake()
    {
        _roomGenerator = GameObject.Find("Room Generator").GetComponent<RoomGenerator>();
        _roomBuilder = GameObject.Find("Room Builder").GetComponent<RoomBuilder>();

        _enemies = new List<Enemy>();
    }

    private void Start()
    {
        NextRoom();
    }

    public void NextRoom()
    {
        _enemies = _roomBuilder.BuildRoom(_roomGenerator.GenerateNewRoom());
        if(_enemies.Count <= 0)
            NextRoom();
        _enemies.ForEach(x => x.OnEnemyDeath += e_OnEnemyDeath);
    }

    private void e_OnEnemyDeath(object sender, EventArgs e)
    {
        _enemies.Remove((Enemy) sender);
        if(_enemies.Count <= 0)
            NextRoom();
    }
    
    
    
}
