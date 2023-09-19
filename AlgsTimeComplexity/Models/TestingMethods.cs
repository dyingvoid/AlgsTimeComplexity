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

    //не доделал
    public static TimeSpan TimSort(List<int> list, int size)
    {

        var watch = Stopwatch.StartNew();
        var result = TimSortArray(list);
        watch.Stop();

        return watch.Elapsed;
    }
    //не доделал
    private static int CalcMinRun(int length)
    {
        int r = 0;
        while (length >= 32)
        {
            r |= length & 1;
            length >>= 1;
        }
        return length + r;
    }

    private static List<int> InsertionSortArray(List<int> list, int left, int right)
    {
        var cpList = new List<int>(list);
        int temp;
        for (int i = left + 1; i < right + 1; i++)
        {
            int j = i;
            while (j > left && cpList[j] < cpList[j - 1])
            {
                temp = cpList[j];
                cpList[j] = cpList[j - 1];
                cpList[j - 1] = temp;
                j--;
            }
        }
        return cpList;
    }
    //не доделал
    private static void Merge(List<int> list, int left, int middle, int right)
    {
        int len1 = middle - left + 1, len2 = right - middle, i;
        int[] leftArr = new int[len1];
        int[] rightArr = new int[len2];
        for (i = 0; i < len1; i++)
        {
            leftArr[i] = list[left + i];
        }
        for (i = 0; i < len2; i++)
        {
            rightArr[i] = list[middle + 1 + i];
        }

        int j = 0, k = left;
        i = 0;
        while (i < len1 && j < len2)
        {
            if (leftArr[i] <= rightArr[j])
            {
                list[k] = leftArr[i];
                i++;
            }
            else
            {
                list[k] = rightArr[j];
                j++;
            }

            k++;
        }
        while (i < len1)
        {
            list[k] = leftArr[i];
            k++;
            i++;
        }
        while (j < len2)
        {
            list[k] = rightArr[j];
            k++;
            j++;
        }
    }
    //не доделал
    private static List<int> TimSortArray(List<int> list)
    {
        var cpList = new List<int>(list);
        int minRun = CalcMinRun(cpList.Count);

        for (int start = 0; start < cpList.Count; start += minRun)
        {
            int end = Math.Min(start + minRun - 1, cpList.Count - 1);
            InsertionSortArray(cpList, start, end);
        }

        int size = minRun;

        while (size < cpList.Count)
        {
            for (int left = 0; left < cpList.Count; left += 2 * size)
            {
                int middle = Math.Min(cpList.Count - 1, left + size - 1);
                int right = Math.Min(left + 2 * size - 1, cpList.Count - 1);
                if (middle < right)
                {
                    Merge(cpList, left, middle, right);
                }
            }

            size *= 2;
        }

        return cpList;
    }

    public static TimeSpan InsertionSort(List<int> list, int size)
    {
        var watch = Stopwatch.StartNew();
        var result = InsertionSortArray(list, 0, size - 1);
        watch.Stop();

        return watch.Elapsed;
    }

    public static TimeSpan LinearSearch(List<int> list, int size)
    {
        var cpList = new List<int>(list);
        Random random = new();
        int searchValue = random.Next(0, 100);
        int index = default;

        var watch = Stopwatch.StartNew();
        for (int i = 0; i < cpList.Count; i++)
        {
            if (cpList[i] == searchValue)
                index = i;
        }
        return watch.Elapsed;
    }
}