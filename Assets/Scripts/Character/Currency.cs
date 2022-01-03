using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Currency
{
    [SerializeField] private string _name;
    [SerializeField] private int _value;

    //public event EventHandler OnResourceChange; ????
    
    public Currency(int initialValue = 0, string name = "currencyName")
    {
        _name = name;
        _value = initialValue;
    }

    public void Add(int amount)
    {
        if (amount < 0)
            return;
        _value += amount;
    }

    public void Add(Currency other)
    {
        Add(other.Value);
    }
    
    public void Take(int amount, bool force = false)
    {
        if (amount < 0)
            amount = 0;
        _value -= amount;
    }
    public bool TryTake(int amount)
    {
        if (amount < 0)
            return false;
        if (amount > _value)
            return false;
        Take(amount);
        return true;
    }

    public string Name => _name;
    
    public int Value => _value;
    
    public override string ToString() => $"{_value}{_name}";
}
