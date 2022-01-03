using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyObject : MonoBehaviour, ICollectible
{
    [SerializeField] private int _value;
    
    private void Awake()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().text = _value.ToString();
    }

    public void Init(int value)
    {
        _value = value;
    }

    public void OnPickUp(PlayerStats playerStats)
    {
        playerStats.Currency.Add(_value);
    }

    public bool AutoCollect
    {
        get => true;
        set => throw new System.NotImplementedException();
    }
}
