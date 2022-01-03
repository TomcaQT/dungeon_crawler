using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject _tempBullet;


    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(Input.GetButtonDown("Fire1"))
            Shoot();
            
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookingDir =  mousePosition - transform.position;
        var bullet = Instantiate(_tempBullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(gameObject);
        bullet.GetComponent<Bullet>().Shoot(lookingDir);
    }
}
