using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    private int _width;
    private int _height;
    private T[,] _data;

    public Grid(int width, int height)
    {
        
        _data = new T[width, height];
        _width = width;
        _height = height;
    }
    
    public Grid(int width, int height, T initValue)
    {
        
        _data = new T[width, height];
        _width = width;
        _height = height;
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
        for (int i = Mathf.Max(0,x-borderSize); i <= Mathf.Min(_width-1,x+borderSize); i++)
            for (int j = Mathf.Max(0,y-borderSize); j <= Mathf.Min(_height-1,y+borderSize); j++)
                if (i != x || j != y)
                    _data[i, j] = border;
        
    }
    
    
    public T Get(int x, int y)
    {
        return CheckBounds(x,y) ? _data[x, y] : default(T);
    }

    private bool CheckBounds(int x, int y)
    {
        return x >= 0 && x < _width && y >= 0 && y < _height;
    }


    public int Width => _width;
    public int Height => _height;

}
