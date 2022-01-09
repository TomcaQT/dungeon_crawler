using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{

    private PlayerStats _player;

    [SerializeField] private List<ItemData> _buyableItems;

    [SerializeField] private GameObject _shopPrefab;
    private GameObject[] _spots;
    
    private const int PRICE_MULTIPLIER = 2;
    private int[] _prices = new int[] {100, 100, 100};
    private ItemData _itemToSell;

    private AudioSource _audioSource;

    private void Awake()
    {
        _spots = new GameObject[3];
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    
    public void Buy(int index)
    {
        if (!_player.Currency.TryTake(_prices[index]) || !_spots[index].activeSelf)
            return;
        OnBuy(index);
        _prices[index] *= PRICE_MULTIPLIER;
        _spots[index].SetActive(false);
        _audioSource.Play();
    }

    private void OnBuy(int index)
    {
        switch (index)
        {
            case 0:
                _player.HealPercent(0.6f);
                break;
            case 1:
                _itemToSell.OnPickup(_player);
                break;
            case 2:
                _player.UpgradeWeapon();
                break;
        }
    }

    public void SpawnShop(Vector3 position, Transform parent)
    {
        var shop = Instantiate(_shopPrefab, position, Quaternion.identity, parent);
        int i = 0;
        foreach (Transform child in shop.transform)
            _spots[i++] = child.gameObject;

        
        _itemToSell = _buyableItems[Random.Range(0, _buyableItems.Count)];
        _spots[1].GetComponent<SpriteRenderer>().sprite = _itemToSell.ItemSprite;

        i = 0;
        foreach (var spot in _spots)
        {
            UnityEvent<int> unityEvent = new UnityEvent<int>();
            unityEvent.AddListener(Buy);
            spot.GetComponent<ShopSpot>().Init(i,unityEvent);
            spot.transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{_prices[i++]}μ";
        }

    }
    
}
