﻿using System;

namespace AlgsTimeComplexity.Models;

public static class TimeComplexity
{
    public static double Const(int size)
    {
        return 1;
    }
    
    public static double Linear(int size)
    {
        return size;
    }
    
    public static double LogN(int size)
    {
        return Math.Log(size);
    }
    
    public static double NLogN(int size)
    {
        return size * Math.Log(size);
    }
    
    public static double Square(int size)
    {
        return size * size;
    }

    public static double Pow3(int size)
    {
        return Math.Pow(size, 3);
    }

    public static double NLogNLogLogN(int size)
    {
        var val = size * LogN(size) * Math.Log(LogN(size));
        if (val is float.NaN)
            return 0;
        return val;
    }
    
    public static double NLogNLogN(int size)
    {
        var val = size * LogN(size) * LogN(size);
        if (val is float.NaN)
            return 0;
        return val;
    }
}