using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Stat
{
    private string _name;
    private int _value;
    //private int _initialValue;
    
    public Stat(int value, string name = "defaultName")
    {
        _value = value;
        
        _name = name;
    }

    public void Increase(int amount = 1)
    {
        if (amount < 0)
            return;
        _value += amount;
    }

    public void Decrease(int amount = 1)
    {
        if (amount < 0 && _value - amount < 0)
            return;
        _value -= amount;
    }

    public void ChangeStatTemporary(int newValue, float time)
    {
        if(newValue < 0 || time < 0f)
            return;
#pragma warning disable 4014
        ChangeStatOnTime(newValue, time);
#pragma warning restore 4014
    }

    private async Task ChangeStatOnTime(int newValue, float time)
    {
        int oldValue = _value;
        _value = newValue;
        await Task.Delay((int)time * 1000);
        _value = oldValue;
    }

    public int Value => _value;
    public string Name => _name;

}