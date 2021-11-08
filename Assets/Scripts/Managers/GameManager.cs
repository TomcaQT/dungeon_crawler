using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoomGenerator _roomGenerator;

    private void Awake()
    {
        _roomGenerator = GameObject.Find("Room Generator").GetComponent<RoomGenerator>();
    }
    
    
    
    
    
}
