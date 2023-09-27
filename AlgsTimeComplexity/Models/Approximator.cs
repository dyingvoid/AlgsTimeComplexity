using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace AlgsTimeComplexity.Models;

public static class Approximator
{
    public static void Approximate(ObservableCollection<double> list, MethodInfo algorithm, int size, int iterations)
    {
        var average = new double[size];
        
        var parameters = Enumerable.Range(1, size);
        for (var i = 0; i < iterations; i++)
        {
            var results = Partitioner
                .Create(parameters, EnumerablePartitionerOptions.NoBuffering)
                .AsParallel()
                .AsOrdered()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(j =>
                {
                    var param = Generator.GenerateParameters(algorithm, j);
                    return ((TimeSpan)algorithm.Invoke(null, param)).TotalMilliseconds;
                })
                .ToArray();

            Add(average, results);
        }
        
        Divide(average, iterations);
        var av = Average(average);

        for (var i = 1; i <= average.Length; i++)
        {
            list.Add(av * i * i);
        }
    }

    private static double Average(double[] arr)
    {
        var s = arr.Sum();
        return s / arr.Length;
    }

    private static void Divide(double[] arr, int number)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] /= number;
        }
    }

    private static void Add(double[] l, double[] r)
    {
        for (var i = 0; i < l.Length; i++)
        {
            l[i] += r[i];
        }
    }
}