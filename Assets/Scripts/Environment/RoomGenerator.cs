using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _roomSizeMin;
    [SerializeField] private Vector2Int _roomSizeMax;
    [SerializeField] private Vector2Int _startingPlace;

    [SerializeField] private List<ProbabilityValue<int>> _enemyCountProbability;
    [SerializeField] private List<ProbabilityValue<int>> _boostCountProbability;


    private void Start()
    {
    }


    public void GenerateNewRoom()
    {
        //TEMP DEBUG
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"Enemy Count: {GetRandomCount(_enemyCountProbability)}");
            Debug.Log($"Boost Count: {GetRandomCount(_boostCountProbability)}");
        }
        //if(room.Number % 10 == 0)
        //GetRandomBoss()
        //else if(rng < chanceOnTreasure())
        //GetRandomTreasure
        //every 5th and 9th room
        //RandomShop()
        //GetRandomRoomData()
        //GetRandomCount(_enemyCountProbability)
        //GetRandomCount(_boostCountProbability)
        //SpawnRoom()
        //CreateWalls()
        //SpawnEnemies()
        //SpawnBoosts()
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

    private Vector2Int GetRandomIntSize(Vector2Int min, Vector2Int max)
    {
        return new Vector2Int(Random.Range(min.x, max.x + 1), Random.Range(min.y, max.y + 1));
    }
}