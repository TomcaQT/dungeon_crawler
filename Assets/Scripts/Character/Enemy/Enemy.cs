using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Resource _hp;

    private EnemyController _enemyController;
    
    public event EventHandler OnEnemyDeath;

    [SerializeField] private EnemyData _enemyData;
    
    private void Awake()
    {
        //Load Enemy Data
        _hp = new Resource(20f, "health");

        _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        //TODO Loading data
        
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
