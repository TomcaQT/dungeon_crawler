using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{

    [SerializeField] private Resource _hp;
    [SerializeField] private Resource _energy; // ????

    public Stat _damage;
    public Stat _resistency;
    public Stat _attackSpeed;
    public Stat _bulletSpeed;
    
    
    
    
    private void Awake()
    {
        _hp = new Resource(100f, "Hitpoints", 1f);
        _energy = new Resource(100f, "Energy", 20f);

        _damage = new Stat(10f, "Damage");
        _resistency = new Stat(5f, "Resistency");
        _attackSpeed = new Stat(.4f, "Attack Speed");
        _bulletSpeed = new Stat(10f, "Bullet Speed");
    }


    public void TakeDamage(float amount)
    {
        if (_hp.Take(amount))
            Debug.Log("Player Die");
    }
    
    #region Resources and Stats accessors
    public bool TryTakeEnergy(float amount) => _energy.TryTake(amount);

    public float Damage => _damage.Value;
    public float Resistency => _resistency.Value;
    public float AttackSpeed => _attackSpeed.Value;
    public float BulletSpeed => _bulletSpeed.Value;

    #endregion
}
