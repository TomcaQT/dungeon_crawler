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

    public void LoadData(EnemyData data = null)
    {
        //TODO Loading data
        _hp = new Resource(_enemyData.Hp, "health");
        _hp.OnResourceChange += _enemyController.OnHpChanged;
        
        if(data == null)
            _enemyController.LoadData(_enemyData);
        else
            _enemyController.LoadData(data);
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


    public EnemyData Data => _enemyData;

}
