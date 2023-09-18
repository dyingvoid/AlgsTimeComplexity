using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlgsTimeComplexity.Models;

public static class TestingMethods
{
    public static TimeSpan Const(List<int> list, int size)
    {
        var watch = Stopwatch.StartNew();
        if (list.Count != 0)
        {
            var a = list[0];
        }
        watch.Stop();

        return watch.Elapsed;
    }
    
    public static TimeSpan Sum(List<int> list, int size)
    {
        long sum = 0;
        var watch = Stopwatch.StartNew();
        for(var i = 0; i < size; i++)
        {
            sum += list[i];
        }
        watch.Stop();

        return watch.Elapsed;
    }

    public static TimeSpan Product(List<int> list, int size)
    {
        long sum = 0;
        var watch = Stopwatch.StartNew();
        for(var i = 0; i < size; i++)
        {
            sum *= list[i];
        }
        watch.Stop();

        return watch.Elapsed;
    }

    public static TimeSpan BubbleSort(List<int> list, int size)
    {
        var cpList = new List<int>(list);

        var watch = Stopwatch.StartNew();
        
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size - 1; j++)
            {
                if (cpList[j] > cpList[j + 1])
                {
                    (cpList[j + 1], cpList[j]) = (cpList[j], cpList[j + 1]);
                }
            }
        }

        watch.Stop();
        return watch.Elapsed;
    }

    public static TimeSpan QuickSort(List<int> list, int size)
    {

        var watch = Stopwatch.StartNew();
        var result = QuickSortArray(list, 0, size - 1);
        watch.Stop();

        return watch.Elapsed;
    }

    private static List<int> QuickSortArray(List<int> list, int lIndex, int rIndex)
    {
        var cpList = new List<int>(list);
        var i = lIndex;
        var j = rIndex;
        var pivot = cpList[lIndex];

        while (i <= j)
        {
            while (cpList[i] < pivot)
                i++;
            while (cpList[j] > pivot)
                j--;

            if (i <= j)
            {
                (cpList[i], cpList[j]) = (cpList[j], cpList[i]);
                i++;
                j--;
            }
        }

        if (lIndex < j)
            QuickSortArray(cpList, lIndex, j);
        if (i < rIndex)
            QuickSortArray(cpList, i, rIndex);

        return cpList;
    }

    public static TimeSpan Horner(List<int> list, int size)
    {
        double result = list[0];
        var x = 1.5f;

        var watch = Stopwatch.StartNew();
        for (int i = 1; i < size; i++)
        {
            result = result * x + list[i];
        }
        watch.Stop();

        return watch.Elapsed;
    }
}