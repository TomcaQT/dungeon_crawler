using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopSpot : MonoBehaviour
{
    private UnityEvent<int> _onBuy;
    private int _index;
    public void Init(int index,UnityEvent<int> initEvent)
    {
        _onBuy = initEvent;
        _index = index;
    }
    
    public void Buy()
    {
        _onBuy?.Invoke(_index);
    }
    
    
}
