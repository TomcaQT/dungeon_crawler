using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{

    [SerializeField] private Resource _hp;
    [SerializeField] private Resource _energy; // ????

    private Stat _damage;
    private Stat _resistency;
    private Stat _attackSpeed;
    private Stat _bulletSpeed;

    private const float MAX_MONEY = 1000000;
    private Currency _currency;
    
    
    private void Awake()
    {
        _hp = new Resource(100f, "Hitpoints", 1f);
        _energy = new Resource(100f, "Energy", 20f);

        _damage = new Stat(10f, "Damage");
        _resistency = new Stat(5f, "Resistency");
        _attackSpeed = new Stat(.4f, "Attack Speed");
        _bulletSpeed = new Stat(10f, "Bullet Speed");

        _currency = new Currency(100, "µ");
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
