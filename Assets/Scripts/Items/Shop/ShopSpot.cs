using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopSpot : MonoBehaviour
{
    [SerializeField] private UnityEvent _onBuy;


    public void Buy()
    {
        _onBuy?.Invoke();
    }
    
    
}
