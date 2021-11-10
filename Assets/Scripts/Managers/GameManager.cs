using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoomGenerator _roomGenerator;
    private RoomBuilder _roomBuilder;

    private void Awake()
    {
        _roomGenerator = GameObject.Find("Room Generator").GetComponent<RoomGenerator>();
        _roomBuilder = GameObject.Find("Room Builder").GetComponent<RoomBuilder>();
    }

    public void NextRoom()
    {
        _roomBuilder.BuildRoom(_roomGenerator.GenerateNewRoom());
    }
    
    
    
}
