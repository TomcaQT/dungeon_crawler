using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class Shop : MonoBehaviour
{

    private PlayerStats _player;

    [SerializeField] private List<ItemData> _buyableItems;

    [SerializeField] private GameObject _shopPrefab;
    
    private const int PRICE_MULTIPLIER = 2;
    private int[] _prices = new int[] {100, 100, 100};


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public void Test()
    {
        
    }

    public void Buy(int index)
    {
        if (!_player.Currency.TryTake(_prices[index]))
            return;
        
        _prices[index] *= PRICE_MULTIPLIER;
    }

    private void OnBuy(int index)
    {
        switch (index)
        {
            case 0:
                _player.HealPercent(0.6f);
                break;
            case 1:
                //TODO Item
                break;
            case 2:
                _player.UpgradeWeapon();
                break;
        }
    }

    public void SpawnShop(Vector3 position, Transform parent)
    {
        var shop = Instantiate(_shopPrefab, position, Quaternion.identity, parent);
    }
    
}
