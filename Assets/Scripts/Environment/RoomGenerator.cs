using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Container which contains probability of choosing determined value
/// </summary>
/// <typeparam name="T">Type of value</typeparam>
[System.Serializable]
public struct ProbabilityValue<T>
{
    public ProbabilityValue(T value,float probability)
    {
        this.probability = probability;
        this.value = value;
    }
    
    public T value;
    public float probability;
    
}

public class RoomGenerator : MonoBehaviour
{
    
    [SerializeField] private Vector2Int _roomSize;
    [SerializeField] private Vector2Int _startingPlace;
    
    [SerializeField] private List<ProbabilityValue<int>> _enemyCountProbability;
    [SerializeField] private List<ProbabilityValue<int>> _boostCountProbability;


    private void Start()
    {

    }


    public void GenerateNewRoom()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"Enemy Count: {GetRandomCount(_enemyCountProbability)}");
            Debug.Log($"Boost Count: {GetRandomCount(_boostCountProbability)}");
        }
    }
    
    /// <summary>
    /// Get random int (from interval [0,probabilities.Count -1]) based on list of probabilities.
    /// </summary>
    /// <param name="probabilities">List of probabilities</param>
    /// <returns>Random int</returns>
    private int GetRandomCount(List<ProbabilityValue<int>> probabilities)
    {
        //Get total probabilty (should be 1);
        float totalProbabilty = probabilities.Select(x => x.probability).Sum();
        //Generate random number without zero
        float randomNumber = Random.Range(0.000001f, totalProbabilty);
        //Iterate through all probabilities and find correct interval
        foreach (var pv in probabilities)
        {
            if (randomNumber <= pv.probability)
                return pv.value;
            randomNumber -= pv.probability;
        }

        return -1;
    }
    
}
