using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

public static class Utils
{

    public static Color ColorFromInt(int r, int g, int b, float a = 1) 
        => new Color(r / 255f, g / 255f, b / 255f, a);
    

    public static Color ItemQualityToColor(ItemQuality itemQuality)
    {
        switch (itemQuality)
        {
            case ItemQuality.Normal:
                return Color.green;
            case ItemQuality.Rare:
                return Color.blue;
            case ItemQuality.Epic:
                return ColorFromInt(157, 71, 237);
            case ItemQuality.Legendary:
                return ColorFromInt(237, 160, 71);
            default:
                return Color.white;
        }
    }

    public static int LayerToOpposite(string name) =>
        LayerMask.NameToLayer(name) == LayerMask.NameToLayer("Player")
            ? LayerMask.NameToLayer("Enemy")
            : LayerMask.NameToLayer("Player");

    public static T GetNextOver<T>(List<T> data, ref int curr)
    {
        curr++;
        if (curr >= data.Count)
            curr = 0;
        return data[curr];
    }

    public static float WeaponQualityMultiplier(ItemQuality quality)
    {
        switch (quality)
        {
            case ItemQuality.Normal:
                return 1f;
            case ItemQuality.Rare:
                return 1.1f;
            case ItemQuality.Epic:
                return 1.4f;
            case ItemQuality.Legendary:
                return 1.8f;
        }

        return 1f;
    }
    
    public static T GetRandom<T>(List<ProbabilityValue<T>> probabilities)
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

        return default(T);
    }

    public static ItemQuality GetRandomQuality()
    {
        float num = Random.Range(0f, 1f);
        if (num >= .96f)
            return ItemQuality.Legendary;
        if (num >= .89f)
            return ItemQuality.Epic;
        if (num >= .79f)
            return ItemQuality.Rare;
        return ItemQuality.Normal;
    }
    
    public static Vector2 GetRandomVector(Vector2 min, Vector2 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }
    
    
    
    
}
