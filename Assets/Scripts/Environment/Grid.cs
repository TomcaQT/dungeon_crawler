using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    private int _width;
    private int _heigth;
    private T[,] _data;

    public Grid(int width, int heigth)
    {
        
        _data = new T[width, heigth];
        _width = width;
        _heigth = heigth;
    }
    
    public Grid(int width, int heigth, T initValue)
    {
        
        _data = new T[width, heigth];
        _width = width;
        _heigth = heigth;
    }

    public void Set(int x, int y, T value)
    {
        if (CheckBounds(x, y))
            _data[x, y] = value;
    }

    public void SetWithBorder(int x, int y, T value, T border, int borderSize = 1)
    {
        if (CheckBounds(x, y))
            _data[x, y] = value;
        for (int i = Mathf.Max(0,x-borderSize); i < Mathf.Min(_width,x+borderSize); i++)
            for (int j = Mathf.Max(0,y-borderSize); j < Mathf.Min(_heigth,y+borderSize); j++)
                if (i != x || j != y)
                    _data[x, y] = border;
        
    }
    
    
    public T Get(int x, int y)
    {
        return CheckBounds(x,y) ? _data[x, y] : default(T);
    }

    private bool CheckBounds(int x, int y)
    {
        return x >= 0 && x < _width && y >= 0 && y < _heigth;
    }

}
