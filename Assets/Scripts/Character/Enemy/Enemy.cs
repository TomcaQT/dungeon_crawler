using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected Resource _hp;

    protected EnemyController _enemyController;
    
    public event EventHandler OnEnemyDeath;

    [SerializeField] private EnemyData _enemyData;
    
    private void Awake()
    {
        //Load Enemy Data
        

        _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        //TODO Loading data
        _hp = new Resource(_enemyData.Hp, "health");
       _enemyController.LoadData(_enemyData);
    }
    
    public void TakeDamage(float damage)
    {
        if (_hp.Take(damage))
        {
            OnEnemyDeath?.Invoke(this,new EventArgs());
            DropItems();
            Destroy(gameObject);
        }
        
    }

    private void DropItems()
    {
        
    }
    
    
}
