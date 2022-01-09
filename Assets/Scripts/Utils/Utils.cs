using System.Collections;
using System.Collections.Generic;
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
}
