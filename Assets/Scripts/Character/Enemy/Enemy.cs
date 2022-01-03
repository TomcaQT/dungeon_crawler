using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Resource _hp;

    private EnemyController _enemyController;
    
    public event EventHandler OnEnemyDeath;
    
    private void Awake()
    {
        //Load Enemy Data
        _hp = new Resource(20f, "health");

        _enemyController = GetComponent<EnemyController>();
    }

    public void LoadData(EnemyData enemyData)
    {
        //TODO Loading data
        
       
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
