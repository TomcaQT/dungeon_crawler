using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemData _item;

    [SerializeField] private GameObject _itemDescription;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemQualityText;

    [SerializeField] private ParticleSystem _qualityParticles;
    
    public void Init(ItemData item)
    {
        _item = item;
        //TODO Change sprite
        GetComponent<SpriteRenderer>().sprite = _item.ItemSprite;

        _itemNameText.text = item.Name;
        _itemQualityText.text = item.ItemQuality.ToString();
        _itemDescription.GetComponent<Image>().color = Utils.ItemQualityToColor(item.ItemQuality); 
        _itemDescription.SetActive(false);

        var main = _qualityParticles.main;
        main.startColor = Utils.ItemQualityToColor(item.ItemQuality); 
    }

    public bool AutoCollect
    {
        get => false;
        set => throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _itemDescription.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _itemDescription.SetActive(false);
    }

    public void OnPickUp(PlayerStats playerStats) => _item.OnPickup(playerStats);

}
