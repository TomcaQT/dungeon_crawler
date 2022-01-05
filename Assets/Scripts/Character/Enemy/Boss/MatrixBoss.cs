using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(BossController))]
public class MatrixBoss : MonoBehaviour
{
    private Enemy _enemy;
    private BossController _bossController;
    private BossData _bossData;
    
    
    [SerializeField] private TextMeshPro _matrixText;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _bossController = GetComponent<BossController>();
        _bossData = (BossData) _enemy.Data;
    }

    private float timeToChange = 1f;
    private float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeToChange/(_bossController.Phase+1))
        {
            _matrixText.text =
            $"{Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)}\n" +
            $"{Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)}\n" +
            $"{Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)} {Random.Range(0,10)}";
            time = 0f;
        }

    }
}
