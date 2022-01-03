using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{

    private float testHP = 25f;

    public void TakeDamage(float amount)
    {
        testHP -= amount;
        if (testHP <= 0)
            Debug.Log("Player Die");
    }
}
