using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AlgsTimeComplexity.Models;

public class Calculator
{
    public void Calculate(int size, double time, 
        MethodInfo testMethod, MethodInfo approximationMethod, PlotModel<double> plot, bool maxPerformance)
    {
        if (plot.List.Count != 0 || plot.Approximation.Count != 0)
        {
            plot.List.Clear();
        }
        
        if(maxPerformance)
            CalculateExperimentTimeParallel(size, testMethod, plot.List);
        else
            CalculateExperimentTime(size, testMethod, plot.List);
    }

    public void CalculateApproximation(int size, double time, 
        MethodInfo complexity, ObservableCollection<double> complexityPlot)
    {
        for (var i = 1; i < size; i++)
        {
            var timeApproximation = (double)complexity.Invoke(null, new object[] { time, i });
            complexityPlot.Add(timeApproximation);
        }
    }

    private void CalculateExperimentTimeParallel(int size, MethodInfo methodInfo, ObservableCollection<double> plot)
    {
        var parameters = Enumerable.Range(1, size);

        IEnumerable<double> results = Partitioner
            .Create(parameters, EnumerablePartitionerOptions.NoBuffering)
            .AsParallel()
            .AsOrdered()
            .WithMergeOptions(ParallelMergeOptions.NotBuffered)
            .Select(i =>
            {
                var param = GenerateParameters(methodInfo, i);
                return ((TimeSpan)methodInfo.Invoke(null, param)).TotalMilliseconds;
            })
            .AsEnumerable();
        
        foreach(var result in results)
        {
            plot.Add(result);
        }
    }
    
    private async void CalculateExperimentTime(int size, MethodInfo methodInfo, 
        ObservableCollection<double> plot)
    {
        for (var i = 0; i < size; i++)
        {
            TimeSpan timeSpan = TimeSpan.Zero;
            var parameters = GenerateParameters(methodInfo, i);
            
            await Task.Run(() =>
            {
                timeSpan = (TimeSpan)methodInfo.Invoke(null, parameters);
            });
            
            plot.Add(timeSpan.TotalMilliseconds);
        }
    }

    private object[] GenerateParameters(MethodInfo methodInfo, int size)
    {
        if (methodInfo.DeclaringType == typeof(TestingMethods))
        {
            
            return new object[] { GenerateArray(size + 1), size + 1 };
            
        }
        else
        {
            return new object[] { GenerateMatrix(size + 1), GenerateMatrix(size + 1), size + 1 };
        }
    }

    private int[] GenerateArray(int size)
    {
        var arr = new int[size];
        var random = new Random();

        for (var i = 0; i < size; i++)
            arr[i] = random.Next(0, 1000);

        return arr;
    }

    private int[][] GenerateMatrix(int size)
    {
        var matrix = new int[size][];

        for (var i = 0; i < size; i++)
            matrix[i] = GenerateArray(size);

        return matrix;
    }
}