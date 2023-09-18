using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace AlgsTimeComplexity.Models;

public static class TestingMethods
{
    public static TimeSpan Const(int[] list, int size)
    {
        var watch = Stopwatch.StartNew();
        if (list.Length != 0)
        {
            var a = list[0];
        }
        watch.Stop();

        return watch.Elapsed;
    }
    
    public static TimeSpan Sum(int[] list, int size)
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

    public static TimeSpan Product(int[] list, int size)
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

    public static TimeSpan BubbleSort(int[] list, int size)
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

    public static TimeSpan QuickSort(int[] list, int size)
    {

        var watch = Stopwatch.StartNew();
        var result = QuickSortArray(list, 0, size - 1);
        watch.Stop();

        return watch.Elapsed;
    }

    private static int[] QuickSortArray(int[] list, int lIndex, int rIndex)
    {
        var i = lIndex;
        var j = rIndex;
        var pivot = list[lIndex];

        while (i <= j)
        {
            while (list[i] < pivot)
                i++;
            while (list[j] > pivot)
                j--;

            if (i <= j)
            {
                (list[i], list[j]) = (list[j], list[i]);
                i++;
                j--;
            }
        }

        if (lIndex < j)
            QuickSortArray(list, lIndex, j);
        if (i < rIndex)
            QuickSortArray(list, i, rIndex);

        return list;
    }

    public static TimeSpan Horner(int[] list, int size)
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

    public static TimeSpan SimplePow(int[] list, int size)
    {
        var element = list[size - 1];
        
        var watch = Stopwatch.StartNew();
        for (int i = 0; i < size; i++)
        {
            element *= element;
        }
        watch.Stop();

        return watch.Elapsed;
    }

    public static TimeSpan RecursivePow(int[] list, int size)
    {
        var element = list[size - 1];
        var watch = Stopwatch.StartNew();
        Recursive(element, size);
        watch.Stop();

        return watch.Elapsed;
    }

    private static int Recursive(int number, int power)
    {
        int m;
        
        if (power == 0)
            return 1;
        if (number % 2 == 0)
        {
           m = Recursive(number, power / 2);
            return m * m;
        }

        return number * Recursive(number, power - 1);
    }

    public static TimeSpan QuickPow(int[] list, int size)
    {
        var watch = Stopwatch.StartNew();
        Quick(list[size - 1], size);
        watch.Stop();

        return watch.Elapsed;
    }

    private static int Quick(int number, int power)
    {
        int f = power % 2 == 1 ? number : 1;

        while (power != 0)
        {
            power /= 2;
            number *= number;

            if (power % 2 == 1)
                f *= number;
        }

        return f;
    }

    public static TimeSpan QuickPow1(int[] list, int size)
    {
        var watch = Stopwatch.StartNew();
        Quick1(list[size - 1], size);
        watch.Stop();

        return watch.Elapsed;
    }

    private static int Quick1(int number, int power)
    {
        int c = number;
        int f = 1;
        int k = power;

        while (k != 0)
        {
            if (k % 2 == 0)
            {
                c *= c;
                k /= 2;
            }
            else
            {
                f *= c;
                k -= 1;
            }
        }

        return f;
    }
}