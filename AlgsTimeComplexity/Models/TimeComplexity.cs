using System;

namespace AlgsTimeComplexity.Models;

public static class TimeComplexity
{
    public static double Const(double singleTime, int size)
    {
        return singleTime;
    }
    
    public static double Linear(double singleTime, int size)
    {
        return singleTime * size;
    }
    
    public static double LogN(double singleTime, int size)
    {
        return singleTime * Math.Log(size);
    }
    
    public static double NLogN(double singleTime, int size)
    {
        return singleTime * size * Math.Log(size);
    }
    
    public static double Square(double singleTime, int size)
    {
        return singleTime * size * size;
    }
}