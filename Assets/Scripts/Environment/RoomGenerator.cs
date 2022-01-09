using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Helpers;


public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _roomSizeMin;
    [SerializeField] private Vector2Int _roomSizeMax;

    [SerializeField] private List<ProbabilityValue<int>> _enemyCountProbability;
    [SerializeField] private List<ProbabilityValue<int>> _boostCountProbability;


    private void Start()
    {
        
    }

    public Room GenerateNewRoom(int roomNumber)
    {
        Room room = new Room();
        //TEMP DEBUG
        /*
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"Enemy Count: {GetRandomCount(_enemyCountProbability)}");
            Debug.Log($"Boost Count: {GetRandomCount(_boostCountProbability)}");
        }8/*/
        room.Number = roomNumber;
        room.Size = GetRandomIntSize(_roomSizeMin, _roomSizeMax);
        room.Shape = RoomShape.Square;
        room.Grid = new Grid<RoomEntity>(room.Size.x, room.Size.y);
        room.Grid.SetWithBorder(room.Size.x / 2, room.Size.y / 2, RoomEntity.Start, RoomEntity.Space);
        room.Grid.Set(0, 0, RoomEntity.End);

        room.EnemyCount = GetRandomCount(_enemyCountProbability);
        room.BoostCount = GetRandomCount(_boostCountProbability);

        for (int i = 0; i < room.EnemyCount; i++)
        {
            var position = GetRandomFreePositionOnGrid(room.Grid, RoomEntity.None);
            if(position.x >= 0 && position.y >= 0)
                room.Grid.Set(position.x,position.y,RoomEntity.Enemy);
        }
        
        for (int i = 0; i < room.BoostCount; i++)
        {
            var position = GetRandomFreePositionOnGrid(room.Grid, RoomEntity.None);
            if(position.x >= 0 && position.y >= 0)
                room.Grid.Set(position.x,position.y,RoomEntity.Boost);
        }

        //if(room.Number % 2 == 0)
            //GetRandomBoss()
        //else if(rng < chanceOnTreasure())
        //GetRandomTreasure
        //every 5th and 9th room
        //RandomShop()
        //GetRandomRoomData()
        return room;
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

    private Vector2Int GetRandomFreePositionOnGrid<T>(Grid<T> grid, T free)
    {
        int MAX_FAILS = 1000;
        for (int i = 0; i < MAX_FAILS; i++)
        {
            var position = GetRandomPositionOnGrid(grid);
            if (grid.Get(position.x, position.y).Equals(free))
                return position;
        }
        return new Vector2Int(-1, -1);
    }
    
    private Vector2Int GetRandomPositionOnGrid<T>(Grid<T> grid)
    {
        return new Vector2Int(Random.Range(0, grid.Width), Random.Range(0, grid.Height));
    }
}
    