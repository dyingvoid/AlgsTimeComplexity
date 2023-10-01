using System;
using System.Linq;

namespace AlgsTimeComplexity.Models;

public static class ArrayExtensions
{
    public static double Mean(this double[] arr)
    {
        var s = arr.Sum();
        return s / arr.Length;
    }

    public static void Divide(this double[] arr, int number)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] /= number;
        }
    }

    public static void Divide(this double[] arr, double[] other)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            var division = arr[i] / other[i];
            if (double.IsPositiveInfinity(division) ||
                double.IsNegativeInfinity(division) ||
                double.IsNaN(division))
                arr[i] = 0;
            else
                arr[i] /= other[i];
        }
    }

    public static void Add(this double[] l, double[] r)
    {
        for (var i = 0; i < l.Length; i++)
        {
            l[i] += r[i];
        }
    }

    public static T[][] Split<T>(this T[] arr, int maxSizeBatch)
    {
        var numberOfParts = arr.Length % maxSizeBatch == 0 ? 
            arr.Length / maxSizeBatch : 
            arr.Length / maxSizeBatch + 1;
        
        var split = new T[numberOfParts][]; 
        
        for (var i = 0; i < numberOfParts; i++)
        {
            var numberOfElements = Math.Min(maxSizeBatch, arr.Length - i * maxSizeBatch);
            split[i] = arr.Skip(i * maxSizeBatch).Take(numberOfElements).ToArray();
        }

        return split;
    }
}