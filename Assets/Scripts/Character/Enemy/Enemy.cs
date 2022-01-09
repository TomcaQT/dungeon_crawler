using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected Resource _hp;
    protected LootTable _lootTable;
    
    protected EnemyController _enemyController;
    protected SpriteRenderer _spriteRenderer;
    protected PrefabManager _prefabManager;
    public event EventHandler OnEnemyDeath;

    [SerializeField] private EnemyData _enemyData;
    
    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _prefabManager = GameObject.Find("Prefab Manager").GetComponent<PrefabManager>();
    }

    public void LoadData(EnemyData data)
    {
        Awake();
        _enemyData = data;
        _hp = new Resource(data.Hp, "Health");
        _hp.OnResourceChange += _enemyController.OnHpChanged;
        _lootTable = data.LootTable;
        if(_spriteRenderer != null) _spriteRenderer.sprite = data.Sprite;
        _enemyController.LoadData(data);
    }
    
    public void TakeDamage(float damage)
    {
        if (_hp.Value > 0f && _hp.Take(damage))
        {
            OnEnemyDeath?.Invoke(this,new EventArgs());
            DropItems();
            Destroy(gameObject);
        }
        
    }
    
    private void DropItems()
    {
        //Drop money
        if (_lootTable.MoneyAmount > 0f)
        {
            var moneyPrefab = _prefabManager.MoneyPrefab;
            moneyPrefab.GetComponent<MoneyObject>().Init(_lootTable.MoneyAmount);
            DropItem(moneyPrefab);
        }
        //Drop always drops
        if (_lootTable.AlwaysDrop.Count > 0)
        {
            foreach (var item in _lootTable.AlwaysDrop)
            {
                var itemPrefab = _prefabManager.ItemPrefab;
                itemPrefab.GetComponent<ItemObject>().Init(item);
                DropItem(itemPrefab);
            }
        }
        //Drop random count of random items with random rarity
        if (_lootTable.DropChances.Count > 0 && _lootTable.DropCount.Count > 0)
        {
            int count = Utils.GetRandom(_lootTable.DropCount);
            for (int i = 0; i < count; i++)
            {
                var item = Utils.GetRandom(_lootTable.DropChances);
                var itemPrefab = _prefabManager.ItemPrefab;
                itemPrefab.GetComponent<ItemObject>().Init(item);
                DropItem(itemPrefab);
            }
        }
    }

    private Vector3 spawnOffset = new Vector3(1f, 0f,0f);
    private void DropItem(GameObject item)
    {
        Instantiate(item, transform.position + spawnOffset, Quaternion.identity, _prefabManager.RoomParent);
        spawnOffset = Quaternion.AngleAxis(-90, Vector3.up) * spawnOffset;
    }


    public EnemyData Data => _enemyData;

}
