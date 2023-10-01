using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AlgsTimeComplexity.Models;

public static class Calculator
{
    public static void Calculate(int size, MethodInfo testMethod, PlotModel<double> plot)
    {
        if (plot.List.Count != 0 || plot.Approximation.Count != 0)
        {
            plot.List.Clear();
            plot.Approximation.Clear();
        }
        
        CalculateExperimentTime(size, testMethod, plot.List);
    }

    public static double[] CalculateMethod(MethodInfo testMethod, int size)
    {
        var parameters = Enumerable.Range(1, size);
        return Partitioner
            .Create(parameters, EnumerablePartitionerOptions.NoBuffering)
            .AsParallel()
            .AsOrdered()
            .WithMergeOptions(ParallelMergeOptions.NotBuffered)
            .Select(j =>
            {
                var param = Generator.GenerateParameters(testMethod, j);
                return (double)testMethod.Invoke(null, param);
            })
            .ToArray();
    }

    private static void CalculateExperimentTime(int size, MethodInfo methodInfo, ObservableCollection<double> plot)
    {
        var parameters = Enumerable.Range(1, size);

        var results = CalculateMethod(methodInfo, size);
        
        foreach(var result in results)
        {
            plot.Add(result);
        }
    }
}