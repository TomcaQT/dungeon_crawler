using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    

    private PlayerStats _playerStats;
    private float timeToAttack;

    private const float DASH_COST = 50f;
    private Rigidbody2D _rigidbody;
    

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        // Instant shoot
        timeToAttack = _playerStats.AttackSpeed;
    }


    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        timeToAttack += Time.deltaTime;
        if (Input.GetButton("Fire1") 
            && timeToAttack >= _playerStats.AttackSpeed + ((1f-Utils.WeaponQualityMultiplier(_playerStats.Weapon.ItemQuality))*.1f) 
            && _playerStats.Energy.TryTake(_playerStats.Weapon.EnergyCost))
        {
            Shoot();
            timeToAttack = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _playerStats.Energy.TryTake(DASH_COST))
        {
            Dash();
        }
            
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookingDir =  mousePosition - transform.position;
        var bullet = Instantiate(_playerStats.Weapon.Bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(_playerStats.Weapon.Bullet,_playerStats.Damage * Utils.WeaponQualityMultiplier(_playerStats.Weapon.ItemQuality) ,_playerStats.BulletSpeed);
        bullet.GetComponent<Bullet>().Shoot(lookingDir);
    }

    private void Dash()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookingDir =  mousePosition - transform.position;
        _rigidbody.AddForce(lookingDir.normalized * 10000f, ForceMode2D.Force);
    }


}
